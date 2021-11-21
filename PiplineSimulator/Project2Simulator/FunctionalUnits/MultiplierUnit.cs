using Project2Simulator.FunctionalUnits;

namespace Project2Simulator.FunctionalUnits
{
    public abstract class MultiplierUnit : FunctionalUnit
    {
        protected MultiplierUnit(FunctionalUnitType type, THECommonDataBus bus, CoreID core) : base(type, bus, core)
        {
        }
    }

}

