/* Author: Austin Kincer */

using Project2Simulator.Registers;
using System.Collections.Generic;
using System;

namespace Project2Simulator.Memory
{
	public class MagicPerfectStupidCache
	{
		private List<Tuple<Address, RegisterValue>> StoreQueue;
		private MainMemory memory;


		public MagicPerfectStupidCache(MainMemory memory)
        {
			StoreQueue = new List<Tuple<Address, RegisterValue>>();
			this.memory = memory;
        }

		public RegisterValue Load(Address addr)
		{
			// Get the last version of the address
			Tuple<Address, RegisterValue> value = StoreQueue.FindLast(
				(Tuple<Address, RegisterValue> tuple) => { return tuple.Item1.Equals(addr); }
				);

			if (value != null)
				return new RegisterValue(value.Item2);
			else
				return new RegisterValue(memory[addr.Value]);
		}

		public void Store(Address addr, RegisterValue value)
		{
			StoreQueue.Add(new Tuple<Address, RegisterValue>(new Address(addr), new RegisterValue(value)));
		}

		public void Cycle()
		{
            foreach (var pair in StoreQueue)
				memory[pair.Item1.Value] = pair.Item2;

			StoreQueue.Clear();
		}

	}

}

