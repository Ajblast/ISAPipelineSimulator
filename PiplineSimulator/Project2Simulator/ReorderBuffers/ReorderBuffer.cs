/* Author: Seth Bowden */
using Project2Simulator;
using Project2Simulator.Registers;
using Project2Simulator.ReorderBuffers;

namespace Project2Simulator.ReorderBuffers
{
	public class ReorderBuffer
	{
		private RegisterFile registerFile;

		private THECommonDataBus THECommonDataBus;

		private ReorderBufferSlot[] bufferSlots;

		private int Head;
		private int Tail;
		private readonly int bufferSize;

		public ReorderBuffer(RegisterFile regs, THECommonDataBus bus, int bufferSize)
		{
			registerFile = regs;
			THECommonDataBus = bus;
			bufferSlots = new ReorderBufferSlot[bufferSize];
            for (int i = 0; i < bufferSize; i++)
				bufferSlots[i] = new ReorderBufferSlot(i);

			Head = 0;
			Tail = 0;

			this.bufferSize = bufferSize;
		}

		public void CheckDataBus()
		{
			// Can't steal data off the buss if it isn't valid
			if (THECommonDataBus.Valid == false)
				return;

			bufferSlots[THECommonDataBus.ReorderID.BufferID].Value = THECommonDataBus.Value;
			bufferSlots[THECommonDataBus.ReorderID.BufferID].ValidValue = true;
			
			if (THECommonDataBus.ValidValue2)
			{
				bufferSlots[THECommonDataBus.ReorderID.BufferID].Value2 = THECommonDataBus.Value2;
				bufferSlots[THECommonDataBus.ReorderID.BufferID].ValidValue2 = true;
			}

			bufferSlots[THECommonDataBus.ReorderID.BufferID].Ocupodo = false;
		}


		public void CommitHead()
		{
			// TODO: Change from valid value to the speculative execution tracker
            if (bufferSlots[Head].Ocupodo == false && bufferSlots[Head].ValidValue == true)
			{
                if (bufferSlots[Head].DestRegId != null && bufferSlots[Head].ValidValue)
                {
					registerFile[bufferSlots[Head].DestRegId.ID].Value = bufferSlots[Head].Value;
					registerFile[bufferSlots[Head].DestRegId.ID].Busy = false;
					registerFile[bufferSlots[Head].DestRegId.ID].ReorderId = null;
				}

                if (bufferSlots[Head].DestRegId2 != null && bufferSlots[Head].ValidValue2)
                {
					registerFile[bufferSlots[Head].DestRegId2.ID].Value = bufferSlots[Head].Value2;
					registerFile[bufferSlots[Head].DestRegId2.ID].Busy = false;
					registerFile[bufferSlots[Head].DestRegId2.ID].ReorderId = null;
				}

				bufferSlots[Head].ValidValue = false;
				bufferSlots[Head].ValidValue2 = false;

				// Increment the head
				Head = (Head + 1) % bufferSize;
			}

		}

		public ReorderBufferSlot FreeSlot()
		{
			int nextSlot = (Tail + 1) % bufferSize;

			if (bufferSlots[nextSlot].Ocupodo == true)
            {
				// Tail caught up to head. Structural Hazard
				return null;
            }

			// Set new tail
			Tail = nextSlot;

			return bufferSlots[Tail];
		}

		public void Flush()
		{
			throw new System.NotImplementedException("No speculative execution capabilities yet");
		}
    }

}

