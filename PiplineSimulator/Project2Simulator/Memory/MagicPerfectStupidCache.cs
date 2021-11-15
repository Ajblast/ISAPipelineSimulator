using Project2Simulator.Memory;
using Project2Simulator.Registers;
using Project2Simulator;
using System.Collections.Generic;
using System;

namespace Project2Simulator.Memory
{
	public class MagicPerfectStupidCache
	{
		private List<Tuple<Address, RegisterValue>> StoreQueue;

		private MainMemory memory;

		private MMU mMU;

		private RegisterValue registerValue;

		private Address address;

		public RegisterValue Load(Address addr)
		{
			return null;
		}

		public void Store(Address addr, RegisterValue value)
		{

		}

		public void Cycle()
		{

		}

	}

}

