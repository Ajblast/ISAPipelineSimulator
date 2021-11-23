/* Author: Austin Kincer */

using Project2Simulator.Memory;
using Project2Simulator.FunctionalUnits;
using Project2Simulator.ReservationStations;
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

		public CPU(ReservationStationCounts counts, MemoryCycleTimes memoryCycleTimes)
        {
			THEMMU = new MMU();
			memory = new MainMemory(0x100000, true);
			magicPerfectStupidCache = new MagicPerfectStupidCache(memory);

			cores = new Core[coreCount];
			for (int i = 0; i < coreCount; i++)
            {
				cores[i] = new Core(new CoreID(i), memory, counts);
            }

			FunctionalFactory.Initialize(THEMMU, magicPerfectStupidCache, memoryCycleTimes);
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

