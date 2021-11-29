/* Author: Austin Kincer */

using Project2Simulator.Instructions;
using Project2Simulator.Memory;
using Project2Simulator.Registers;
using Project2Simulator.ReorderBuffers;

namespace Project2Simulator.FunctionalUnits
{
    public struct MemoryCycleTimes
    {
        public int LoadMemory;
        public int StorMemory;

        public int AtomicOperation;
        public int AtomicLoadMemory;
        public int AtomicStorMemory;
    }

    public class MemoryUnit : MemoryAccessUnit
	{
        enum AtomicStage
        {
            NOP,
            LOAD,
            EXECUTE,
            STORE
        }

        private AtomicStage atomicStage;
        private MemoryCycleTimes cycleTimings;

        private RegisterValue tempValue;
        private Address tempAddress;
        private bool shouldWrite = false;

        public MemoryUnit(MMU mmu, MagicPerfectStupidCache cache, THECommonDataBus bus, CoreID core, MemoryCycleTimes cycleTimes) : base(mmu, cache, bus, core)
        {
            atomicStage = AtomicStage.NOP;
            cycleTimings = cycleTimes;
        }

        public override void StartExecution(Opcode opcode, RegisterValue op1, RegisterValue op2, RegisterValue op3, Address address)
        {
            base.StartExecution(opcode, op1, op2, op3, address);

            atomicStage = AtomicStage.LOAD;
        }

        public override bool Cycle()
        {
            // Increase the current cycle count
            CurrentCycle++;

            // Determine the lookup address
            Address lookupAddress;
            switch (opcode)
            {
                case Opcode.LOAD:
                case Opcode.STOR:
                case Opcode.FETCH:
                    if (address == null)
                        lookupAddress = new Address(op1.Value);
                    else
                        lookupAddress = address;
                    break;
                case Opcode.PUSH:
                case Opcode.POP:
                    lookupAddress = new Address(op1.Value);
                    break;
                case Opcode.ADDA:
                case Opcode.SUBA:
                case Opcode.ANDA:
                case Opcode.ORA:
                case Opcode.XORA:
                case Opcode.CMPSW:
                case Opcode.SWAP:
                    lookupAddress = new Address(op1.Value);
                    break;
                default:
                    lookupAddress = null;
                    break;
            }

            // Determine memory access
            bool hasMemoryAccess = false;
            switch (opcode)
            {
                case Opcode.LOAD:
                case Opcode.STOR:
                case Opcode.PUSH:
                case Opcode.POP:
                    hasMemoryAccess = MMU.RequestMemory(core, lookupAddress, false);
                    break;

                case Opcode.FETCH:
                case Opcode.ADDA:
                case Opcode.SUBA:
                case Opcode.ANDA:
                case Opcode.ORA:
                case Opcode.XORA:
                case Opcode.CMPSW:
                case Opcode.SWAP:
                    hasMemoryAccess = MMU.RequestMemory(core, lookupAddress, true);
                    break;

                default:
                    break;
            }

            if (hasMemoryAccess == false)
                return false;

            // Do the stuff
            bool retValue = false;
            switch (opcode)
            {
                case Opcode.LOAD:
                    if (CurrentCycle == cycleTimings.LoadMemory)
                    {
                        dest1.Value = magicPerfectStupidCache.Load(lookupAddress).Value;

                        retValue = true;
                    }
                    break;
                case Opcode.STOR:
                    if (CurrentCycle == cycleTimings.StorMemory)
                    {
                        tempAddress = new Address(lookupAddress);
                        tempValue.Value = op2.Value;

                        shouldWrite = true;
                        retValue = true;
                    }
                    break;
                case Opcode.PUSH:
                    if (CurrentCycle == cycleTimings.StorMemory)
                    {
                        dest1.Value = (uint) lookupAddress.Value - RegisterValue.ByteSize;
                        tempAddress = new Address(lookupAddress);
                        tempValue.Value = op2.Value;
                    
                        shouldWrite = true;
                        retValue = true;
                    }
                    break;
                case Opcode.POP:
                    if (CurrentCycle == cycleTimings.LoadMemory)
                    {
                        dest1.Value = magicPerfectStupidCache.Load(new Address(lookupAddress)).Value;
                        dest2.Value = (uint) lookupAddress.Value + RegisterValue.ByteSize;

                        retValue = true;
                    }
                    break;

                case Opcode.FETCH:  // Same Load
                    if (CurrentCycle == cycleTimings.AtomicLoadMemory)
                    {
                        dest1.Value = magicPerfectStupidCache.Load(new Address(lookupAddress)).Value;   // Register

                        retValue = true;
                    }
                    break;
                case Opcode.ADDA:   // Same as Add
                    if (atomicStage == AtomicStage.LOAD && CurrentCycle == cycleTimings.AtomicLoadMemory)
                    {
                        tempAddress = new Address(lookupAddress);
                        tempValue.Value = magicPerfectStupidCache.Load(tempAddress).Value;
                        dest1.Value = tempValue.Value;
                        CurrentCycle = 0;

                        atomicStage = AtomicStage.EXECUTE;
                    }
                    else if (atomicStage == AtomicStage.EXECUTE && CurrentCycle == cycleTimings.AtomicOperation)
                    {
                        tempValue.Value += op2.Value;
                        CurrentCycle = 0;

                        atomicStage = AtomicStage.STORE;
                    }
                    else if (atomicStage == AtomicStage.STORE && CurrentCycle == cycleTimings.AtomicStorMemory)
                    {
                        atomicStage = AtomicStage.NOP;
                        CurrentCycle = 0;

                        shouldWrite = true;
                        retValue = true;
                    }
                    break;
                case Opcode.SUBA:
                    if (atomicStage == AtomicStage.LOAD && CurrentCycle == cycleTimings.AtomicLoadMemory)
                    {
                        tempAddress = new Address(lookupAddress);
                        tempValue.Value = magicPerfectStupidCache.Load(tempAddress).Value;
                        dest1.Value = tempValue.Value;
                        CurrentCycle = 0;

                        atomicStage = AtomicStage.EXECUTE;
                    }
                    else if (atomicStage == AtomicStage.EXECUTE && CurrentCycle == cycleTimings.AtomicOperation)
                    {
                        tempValue.Value -= op2.Value;
                        CurrentCycle = 0;

                        atomicStage = AtomicStage.STORE;
                    }
                    else if (atomicStage == AtomicStage.STORE && CurrentCycle == cycleTimings.AtomicStorMemory)
                    {
                        atomicStage = AtomicStage.NOP;
                        CurrentCycle = 0;
                        
                        shouldWrite = true;
                        retValue = true;
                    }
                    break;
                case Opcode.ANDA:
                    if (atomicStage == AtomicStage.LOAD && CurrentCycle == cycleTimings.AtomicLoadMemory)
                    {
                        tempAddress = new Address(lookupAddress);
                        tempValue.Value = magicPerfectStupidCache.Load(tempAddress).Value;
                        dest1.Value = tempValue.Value;
                        CurrentCycle = 0;

                        atomicStage = AtomicStage.EXECUTE;
                    }
                    else if (atomicStage == AtomicStage.EXECUTE && CurrentCycle == cycleTimings.AtomicOperation)
                    {
                        tempValue.Value &= op2.Value;
                        CurrentCycle = 0;

                        atomicStage = AtomicStage.STORE;
                    }
                    else if (atomicStage == AtomicStage.STORE && CurrentCycle == cycleTimings.AtomicStorMemory)
                    {
                        atomicStage = AtomicStage.NOP;
                        CurrentCycle = 0;
                        shouldWrite = true;
                        retValue = true;
                    }
                    break;
                case Opcode.ORA:
                    if (atomicStage == AtomicStage.LOAD && CurrentCycle == cycleTimings.AtomicLoadMemory)
                    {
                        tempAddress = new Address(lookupAddress);
                        tempValue.Value = magicPerfectStupidCache.Load(tempAddress).Value;
                        dest1.Value = tempValue.Value;
                        CurrentCycle = 0;

                        atomicStage = AtomicStage.EXECUTE;
                    }
                    else if (atomicStage == AtomicStage.EXECUTE && CurrentCycle == cycleTimings.AtomicOperation)
                    {
                        tempValue.Value |= op2.Value;
                        CurrentCycle = 0;

                        atomicStage = AtomicStage.STORE;
                    }
                    else if (atomicStage == AtomicStage.STORE && CurrentCycle == cycleTimings.AtomicStorMemory)
                    {
                        atomicStage = AtomicStage.NOP;
                        CurrentCycle = 0;
                        shouldWrite = true;
                        retValue = true;
                    }
                    break;
                case Opcode.XORA:
                    if (atomicStage == AtomicStage.LOAD && CurrentCycle == cycleTimings.AtomicLoadMemory)
                    {
                        tempAddress = new Address(lookupAddress);
                        tempValue.Value = magicPerfectStupidCache.Load(tempAddress).Value;
                        dest1.Value = tempValue.Value;
                        CurrentCycle = 0;

                        atomicStage = AtomicStage.EXECUTE;
                    }
                    else if (atomicStage == AtomicStage.EXECUTE && CurrentCycle == cycleTimings.AtomicOperation)
                    {
                        tempValue.Value ^= op2.Value;
                        CurrentCycle = 0;

                        atomicStage = AtomicStage.STORE;
                    }
                    else if (atomicStage == AtomicStage.STORE && CurrentCycle == cycleTimings.AtomicStorMemory)
                    {
                        atomicStage = AtomicStage.NOP;
                        CurrentCycle = 0;
                        shouldWrite = true;
                        retValue = true;
                    }
                    break;

                case Opcode.CMPSW:
                    if (atomicStage == AtomicStage.LOAD && CurrentCycle == cycleTimings.AtomicLoadMemory)
                    {
                        tempAddress = new Address(lookupAddress);
                        tempValue.Value = magicPerfectStupidCache.Load(tempAddress).Value;
                        CurrentCycle = 0;

                        atomicStage = AtomicStage.EXECUTE;
                    }
                    else if (atomicStage == AtomicStage.EXECUTE && CurrentCycle == cycleTimings.AtomicOperation)
                    {
                        if (tempValue.Value == op2.Value)
                        {
                            dest1.Value = op3.Value;
                            tempValue.Value = op3.Value;
                            shouldWrite = true;
                        }
                        else
                            dest1.Value = tempValue.Value;
                        CurrentCycle = 0;

                        atomicStage = AtomicStage.STORE;
                    }
                    else if (atomicStage == AtomicStage.STORE && CurrentCycle == cycleTimings.AtomicStorMemory)
                    {
                        atomicStage = AtomicStage.NOP;
                        CurrentCycle = 0;

                        retValue = true;
                    }
                    break;
                case Opcode.SWAP:
                    if (atomicStage == AtomicStage.LOAD && CurrentCycle == cycleTimings.AtomicLoadMemory)
                    {
                        tempAddress = new Address(lookupAddress);
                        tempValue.Value = magicPerfectStupidCache.Load(tempAddress).Value;
                        CurrentCycle = 0;

                        atomicStage = AtomicStage.EXECUTE;
                    }
                    else if (atomicStage == AtomicStage.EXECUTE && CurrentCycle == cycleTimings.AtomicOperation)
                    {
                        dest1.Value = tempValue.Value;
                        tempValue.Value = op2.Value;
                        CurrentCycle = 0;

                        atomicStage = AtomicStage.STORE;
                    }
                    else if (atomicStage == AtomicStage.STORE && CurrentCycle == cycleTimings.AtomicStorMemory)
                    {
                        atomicStage = AtomicStage.NOP;
                        CurrentCycle = 0;
                       
                        shouldWrite = true;
                        retValue = true;
                    }
                    break;

                default:
                    break;
            }

            return retValue;
        }

        public override void Flush()
        {
            tempValue = new RegisterValue();
            tempAddress = new Address();
            shouldWrite = false;

            MMU.RemoveAtomic(core);
            base.Flush();
        }

        public override void Commit(ReorderBufferID id)
        {
            base.Commit(id);

            if (shouldWrite)
                magicPerfectStupidCache.Store(tempAddress, tempValue);
            
            tempValue = new RegisterValue();
            tempAddress = new Address();
            shouldWrite = false;
        }
    }

}

