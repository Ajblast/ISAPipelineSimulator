using Project2Simulator.FunctionalUnits;

namespace Project2Simulator.FunctionalUnits
{
    public class FloatingAdder : AdderUnit
    {
        public FloatingAdder(THECommonDataBus bus, CoreID core) : base(FunctionalUnitType.FLOATING_ADDER, bus, core)
        {
        }

        public override bool Cycle()
        {
            throw new System.NotImplementedException();
        }
    }

}

