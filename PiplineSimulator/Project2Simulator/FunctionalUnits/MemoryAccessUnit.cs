/* Author: Austin Kincer */

using Project2Simulator.Memory;

namespace Project2Simulator.FunctionalUnits
{
	public abstract class MemoryAccessUnit : FunctionalUnit
	{
		protected MMU MMU;
		protected MagicPerfectStupidCache magicPerfectStupidCache;

        protected MemoryAccessUnit(MMU mmu, MagicPerfectStupidCache cache, THECommonDataBus bus, CoreID core) : base(FunctionalUnitType.MEMORY_UNIT, bus, core)
        {
            MMU = mmu;
        }
    }

}

