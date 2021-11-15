using Project2Simulator.FunctionalUnits;
using Project2Simulator;
using System.Collections.Generic;

namespace Project2Simulator.Memory
{
	public class MMU
	{
		private Dictionary<Address, CoreID> AtomicAddresses;
		private MemoryAccessUnit memoryAccessUnit;

		private CoreID coreID;
		private Address address;

		public bool RequestMemory(CoreID id, Address address, bool atomic)
		{
			return false;
		}

		public void RemoveAtomic(CoreID id)
		{

		}

	}

}

