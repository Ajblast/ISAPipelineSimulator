using Project2Simulator.Registers;
using Project2Simulator.ReorderBuffers;

namespace Project2Simulator.Registers
{
	public class Register
	{
		public RegisterValue Value;

		public ReorderBufferID ReorderId;

		public bool Busy;

		public RegisterID ID;

		private ReorderBufferID reorderBufferID;

		public RegisterID registerID;

		private RegisterValue registerValue;

	}

}

