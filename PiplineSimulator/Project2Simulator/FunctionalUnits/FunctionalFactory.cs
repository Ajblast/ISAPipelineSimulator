/* Author: Austin Kincer */

using Project2Simulator.Memory;
using System;

namespace Project2Simulator.FunctionalUnits
{
	public static class FunctionalFactory
    {
        private static MMU mmu;

        public static void Initialize(MMU mmu)
        {
            FunctionalFactory.mmu = mmu;
        }

		public static FunctionalUnit CreateUnit(THECommonDataBus bus, FunctionalUnitType type, CoreID id)
        {
            throw new NotImplementedException();
        }
	}

}

