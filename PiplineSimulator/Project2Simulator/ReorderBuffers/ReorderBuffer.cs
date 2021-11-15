using Project2Simulator;
using Project2Simulator.Registers;
using Project2Simulator.ReorderBuffers;

namespace Project2Simulator.ReorderBuffers
{
	public class ReorderBuffer
	{
		private THECommonDataBus tHECommonDataBus;

		private RegisterFile registerFile;

		private THECommonDataBus THECommonDataBus;


		public ReorderBuffer(RegisterFile regs, THECommonDataBus bus)
		{

		}

		public void CommitHead()
		{

		}

		public ReorderBufferSlot FreeSlot()
		{
			return null;
		}

		public void Flush()
		{

		}

	}

}

