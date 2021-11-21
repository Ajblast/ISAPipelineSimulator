using Project2Simulator.FunctionalUnits;

namespace Project2Simulator.FunctionalUnits
{
    public class IntegerMultiplier : MultiplierUnit
    {
        public IntegerMultiplier(THECommonDataBus bus, CoreID core) : base(FunctionalUnitType.INTEGER_MULTIPLIER, bus, core)
        {
        }

        public override bool Cycle()
        {
            throw new System.NotImplementedException();
        }
    }

}

