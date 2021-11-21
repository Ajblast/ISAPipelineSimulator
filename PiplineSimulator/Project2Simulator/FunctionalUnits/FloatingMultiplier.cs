using Project2Simulator.FunctionalUnits;

namespace Project2Simulator.FunctionalUnits
{
    public class FloatingMultiplier : MultiplierUnit
    {
        public FloatingMultiplier(THECommonDataBus bus, CoreID core) : base(FunctionalUnitType.FLOATING_MULTIPLIER, bus, core)
        {
        }

        public override bool Cycle()
        {
            throw new System.NotImplementedException();
        }
    }

}

