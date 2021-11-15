using Project2Simulator.ReorderBuffers;
using Project2Simulator.Registers;

namespace Project2Simulator
{
	public class THECommonDataBus
	{
		public ReorderBufferID ReorderID;

		public RegisterValue Value;

		public RegisterValue StatusRegValue;

		public bool StatusRegUpdated;

		private RegisterValue registerValue;

		public void Write(ReorderBufferID id, RegisterValue value)
		{

		}

	}

}

