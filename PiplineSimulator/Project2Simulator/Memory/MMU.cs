/* Author: Austin Kincer */

using System.Collections.Generic;

namespace Project2Simulator.Memory
{
	public class MMU
	{
		private Dictionary<Address, CoreID> AtomicAddresses;

		public bool RequestMemory(CoreID id, Address address, bool atomic)
		{
			// Give memory access if the access isn't atomic. Don't care
			if (atomic == false)
				return true;

			if (AtomicAddresses.ContainsKey(address) == false)
            {
				// New atomic access at address
				AtomicAddresses.Add(address, new CoreID(id));
				return true;
            }
			else if (AtomicAddresses[address].Equals(id))
				return true;

			// CoreID doesn't match
			return false;
		}

		public void RemoveAtomic(CoreID id)
		{
            foreach (var item in AtomicAddresses)
            {
				if (item.Value.Equals(id))
                {
					AtomicAddresses.Remove(item.Key);
					break;
                }
            }
		}

	}

}

