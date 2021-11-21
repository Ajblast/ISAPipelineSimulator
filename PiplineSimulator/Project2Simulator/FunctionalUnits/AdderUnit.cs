using Project2Simulator.Instructions;
using Project2Simulator.Registers;
using Project2Simulator.ReorderBuffers;

namespace Project2Simulator.FunctionalUnits
{
    public abstract class AdderUnit : FunctionalUnit
    {
        protected AdderUnit(FunctionalUnitType type, THECommonDataBus bus, CoreID core) : base(type, bus, core)
        {
        }
    }

}

