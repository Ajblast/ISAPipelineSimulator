/* Author: Austin Kincer */

using Project2Simulator.Instructions;
using Project2Simulator.Memory;
using Project2Simulator.Registers;
using Project2Simulator.ReorderBuffers;

namespace Project2Simulator.FunctionalUnits
{
	public class MemoryUnit : MemoryAccessUnit
	{
        enum AtomicStage
        {
            LOAD,
            EXECUTE,
            STORE
        }

        private AtomicStage atomicStage;

        public MemoryUnit(MMU mmu, MagicPerfectStupidCache cache, THECommonDataBus bus, CoreID core) : base(mmu, cache, bus, core)
        {
        }

        public override void StartExecution(Opcode opcode, RegisterValue op1, RegisterValue op2, RegisterValue op3)
        {
            base.StartExecution(opcode, op1, op2, op3);

            atomicStage = AtomicStage.LOAD;
        }

        public override bool Cycle()
        {
            switch (opcode)
            {
                case Opcode.LOAD:
                    break;
                case Opcode.STOR:
                    break;
                case Opcode.PUSH:
                    break;
                case Opcode.POP:
                    break;

                // TODO: Memory unit instructions
                case Opcode.FETCH:
                case Opcode.ADDA:
                case Opcode.SUBA:
                case Opcode.ANDA:
                case Opcode.ORA:
                case Opcode.XORA:
                case Opcode.CMPSW:
                case Opcode.SWAP:

                default:
                    break;
            }

            throw new System.NotImplementedException();
        }

        public override void Flush()
        {
            MMU.RemoveAtomic(core);
            base.Flush();
        }

        public override void Commit(ReorderBufferID id)
        {
            base.Commit(id);

            // Commit to memory
        }
    }

}

