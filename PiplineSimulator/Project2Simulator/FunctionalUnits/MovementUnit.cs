using Project2Simulator.FunctionalUnits;

namespace Project2Simulator.FunctionalUnits
{
    public class MovementUnit : FunctionalUnit
    {
        public MovementUnit(THECommonDataBus bus, CoreID core) : base(FunctionalUnitType.MOVEMENT_UNIT, bus, core)
        {
        }

        public override bool Cycle()
        {
            bool retValue = false;
            switch (opcode)
            {
                case Instructions.Opcode.MOV:
                    dest1.Value = op1.Value;
                    retValue = true;
                    break;
                default:

                    break;
            }

            return retValue;
        }
    }

}

