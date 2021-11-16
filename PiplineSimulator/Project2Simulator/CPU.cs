/* Author: Austin Kincer */

using Project2Simulator.Memory;
using Project2Simulator.FunctionalUnits;

namespace Project2Simulator
{
	public class CPU
	{
		private Core[] cores;

		private MMU THEMMU;
		private MagicPerfectStupidCache magicPerfectStupidCache;
		private MainMemory memory;

		private const int coreCount = 2;

		public CPU()
        {
			cores = new Core[coreCount];

			THEMMU = new MMU(); 
			memory= new MainMemory(0x100000, true);
			magicPerfectStupidCache = new MagicPerfectStupidCache(memory);

			FunctionalFactory.Initialize(THEMMU);
        }
		public void Cycle()
		{
			magicPerfectStupidCache.Cycle();

            foreach (var core in cores)
				core.Cycle();
		}

	}

}

