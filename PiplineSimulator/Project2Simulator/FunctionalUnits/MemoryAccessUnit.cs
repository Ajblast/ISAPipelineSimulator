using Project2Simulator.FunctionalUnits;
using Project2Simulator.Memory;

namespace Project2Simulator.FunctionalUnits
{
	public abstract class MemoryAccessUnit : FunctionalUnit
	{
		private MMU MMU;

		private MainMemory memory;

		private MagicPerfectStupidCache magicPerfectStupidCache;

        protected MemoryAccessUnit(CoreID core) : base(core)
        {
        }
    }

}

