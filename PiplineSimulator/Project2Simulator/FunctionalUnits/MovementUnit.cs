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
            dest1.Value = op1.Value;

            return true;
        }
    }

}

