using Project2Simulator.FunctionalUnits;
using Project2Simulator.ReorderBuffers;

namespace Project2Simulator.FunctionalUnits
{
	public class MemoryUnit : MemoryAccessUnit
	{
		private ReorderBuffer reorderBuffer;

        public MemoryUnit(CoreID core) : base(core)
        {
        }
    }

}

