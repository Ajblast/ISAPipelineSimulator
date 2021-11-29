/* Author: Austin Kincer */
using System;
using Project2Simulator.Instructions;

namespace Project2Simulator.FunctionalUnits
{
    public class BranchUnit : FunctionalUnit
    {
        public BranchUnit(THECommonDataBus bus, CoreID core) : base(FunctionalUnitType.BRANCH_UNIT, bus, core)
        {
        }

        public override bool Cycle()
        {
            bool retValue = false;

            // If the correct flag (op1) is set, put into destination the value in op2 
            switch (opcode)
            {
                case Opcode.JZ:
                    if ((op1.Value & 2) == 2)
                    {
                        dest1.Value = (uint) address.Value;
                    }
                        retValue = true;
                    break;
                case Opcode.JNZ:
                    if ((op1.Value & 2) == 0)
                    {
                        dest1.Value = (uint) address.Value;
                    }
                        retValue = true;
                    break;
                case Opcode.JG:
                    // ZF = 0 and SF = OF
                    if ((op1.Value & 2) == 0 && ((op1.Value & 16) >> 4 == (op1.Value & 8) >> 3))
                    {
                        dest1.Value = (uint) address.Value;
                    }
                        retValue = true;
                    break;
                case Opcode.JGE:
                    // SF = OF
                    if (((op1.Value & 16) >> 4 == (op1.Value & 8) >> 3))
                    {
                        dest1.Value = (uint) address.Value;
                    }
                        retValue = true;
                    break;
                case Opcode.JL:
                    // SF != OF
                    // ZF = 0 and SF = OF
                    if ((op1.Value & 16) >> 4 != (op1.Value & 8) >> 3)
                    {
                        dest1.Value = (uint) address.Value;
                    }
                        retValue = true;
                    break;
                case Opcode.JLE:
                    // ZF = 1 or SF != OF
                    if ((op1.Value & 2) == 2 || ((op1.Value & 16) >> 4 != (op1.Value & 8) >> 3))
                    {
                        dest1.Value = (uint) address.Value;
                    }
                        retValue = true;
                    break;
                case Opcode.JA:
                    // ZF == 0 and CF = 0;
                    if ((op1.Value & 2) == 0 && (op1.Value & 1) == 0)
                    {
                        dest1.Value = (uint) address.Value;
                    }
                        retValue = true;
                    break;
                case Opcode.JAE:
                    // CF = 0
                    if ((op1.Value & 1) == 0)
                    {
                        dest1.Value = (uint) address.Value;
                    }
                        retValue = true;
                    break;
                case Opcode.JB:
                    // CF = 1
                    if ((op1.Value & 1) == 1)
                    {
                        dest1.Value = (uint) address.Value;
                    }
                        retValue = true;
                    break;
                case Opcode.JBE:
                    // ZF = 1 or CF = 1
                    if ((op1.Value & 2) == 2 || (op1.Value & 1) == 1)
                    {
                        dest1.Value = (uint) address.Value;
                    }
                        retValue = true;
                    break;

                default:
                    break;
            }

            return retValue;
        }
    }

}

