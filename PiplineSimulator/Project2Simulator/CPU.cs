/* Author: Austin Kincer */

using Project2Simulator.Memory;
using Project2Simulator.FunctionalUnits;
using System.Collections.Generic;

namespace Project2Simulator
{
	public class CPU
	{
		private Core[] cores;

		public MMU THEMMU;
		private MagicPerfectStupidCache magicPerfectStupidCache;
		public MainMemory memory;

		private const int coreCount = 2;

		public CPU()
        {
			cores = new Core[coreCount];

			THEMMU = new MMU(); 
			memory= new MainMemory(0x100000, true);
			magicPerfectStupidCache = new MagicPerfectStupidCache(memory);

			FunctionalFactory.Initialize(THEMMU, magicPerfectStupidCache);
        }
		public void Cycle()
		{
			magicPerfectStupidCache.Cycle();

            foreach (var core in cores)
				core.Cycle();
		}

		public int GetCoreCount()
        {
			return coreCount;
        }

		public Core[] GetCores()
        {
			return cores;
        }

	}

}

