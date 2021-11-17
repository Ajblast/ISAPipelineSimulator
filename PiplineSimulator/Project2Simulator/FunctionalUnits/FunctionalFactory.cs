/* Author: Austin Kincer */

using Project2Simulator.Memory;
using System;

namespace Project2Simulator.FunctionalUnits
{
	public static class FunctionalFactory
    {
        private static MMU mmu;
        private static MagicPerfectStupidCache cache;

        public static void Initialize(MMU mmu, MagicPerfectStupidCache cache)
        {
            FunctionalFactory.mmu = mmu;
            FunctionalFactory.cache = cache;
        }

		public static FunctionalUnit CreateUnit(THECommonDataBus bus, FunctionalUnitType type, CoreID id)
        {
            throw new NotImplementedException();
        }
	}

}

