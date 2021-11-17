/* Author: Austin Kincer */

using Project2Simulator.Memory;
using Project2Simulator.ReorderBuffers;

namespace Project2Simulator.FunctionalUnits
{
	public class MemoryUnit : MemoryAccessUnit
	{
        public MemoryUnit(MMU mmu, MagicPerfectStupidCache cache, THECommonDataBus bus, CoreID core) : base(mmu, cache, bus, core)
        {
        }

        public override bool Cycle()
        {
            throw new System.NotImplementedException();
        }

        public override void Flush()
        {
            MMU.RemoveAtomic(core);
            base.Flush();
        }
    }

}

