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

		private int CommitIndex;

		public ReorderBuffer(RegisterFile regs, THECommonDataBus bus, int bufferSize)
		{
			registerFile = regs;
			THECommonDataBus = bus;
			CommitIndex = 0;
			bufferSlots = new ReorderBufferSlot[bufferSize];
            for (int i = 0; i < bufferSize; i++)
            {
				bufferSlots[i] = new ReorderBufferSlot(i);
            }
		}

		public void CommitHead()
		{
            if (bufferSlots[CommitIndex].ValidValue == true)
			{
				registerFile[bufferSlots[CommitIndex].DestRegId.ID].Value = bufferSlots[CommitIndex].Value;
				registerFile[bufferSlots[CommitIndex].DestRegId.ID].Busy = false;
				registerFile[bufferSlots[CommitIndex].DestRegId.ID].ReorderId = null;

				bufferSlots[CommitIndex].Ocupodo = false;
				bufferSlots[CommitIndex].ValidValue = false;
			}

		}

		public ReorderBufferSlot FreeSlot()
		{
			int curCommitIndex = CommitIndex;
			do
			{
				if (bufferSlots[CommitIndex].Ocupodo == false)
				{
					return bufferSlots[CommitIndex];
				}
				IncrementIndex();
			}
			while (CommitIndex != curCommitIndex);

			return null;
		}

		public void Flush()
		{
			throw new System.NotImplementedException("No speculative execution capabilities yet");
		}

		private void IncrementIndex()
        {
			if (CommitIndex == (bufferSlots.Length - 1))
			{
				CommitIndex = 0;
			}
            else
            {
				CommitIndex++;
            }


        }

	}

}

