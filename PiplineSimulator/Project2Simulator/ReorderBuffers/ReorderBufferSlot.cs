using Project2Simulator.Registers;
using Project2Simulator.ReservationStations;
using Project2Simulator.ReorderBuffers;

namespace Project2Simulator.ReorderBuffers
{
	public class ReorderBufferSlot
	{
		public RegisterID DestRegId;

		public bool Ocupodo;

		public RegisterValue Value;

		public bool ValidValue;

		public ReservationStation Instruction;

		public RegisterValue StatusRegValue;

		public bool UpdatesStatusReg;

		public ReorderBufferID ReorderBufferID;

        public ReorderBufferSlot(int reorderBufferID)
        {
			ReorderBufferID = new ReorderBufferID(reorderBufferID);
        }

	}

}

