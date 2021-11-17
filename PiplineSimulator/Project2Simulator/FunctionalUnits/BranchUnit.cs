/* Author: Austin Kincer */
using System;

namespace Project2Simulator.FunctionalUnits
{
    public class BranchUnit : FunctionalUnit
    {
        public BranchUnit(THECommonDataBus bus, CoreID core) : base(FunctionalUnitType.BRANCH_UNIT, bus, core)
        {
        }

        public override bool Cycle()
        {
            throw new NotImplementedException();            
        }
    }

}

