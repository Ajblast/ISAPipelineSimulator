using Project2Simulator.Registers;
using Project2Simulator.ReservationStations;
using Project2Simulator.ReorderBuffers;

namespace Project2Simulator.ReorderBuffers
{
	public class ReorderBufferSlot
	{
		public ReorderBufferID ReorderBufferID;
		public ReservationStation Instruction;
		public bool Ocupodo;

		public RegisterID DestRegId;
		public RegisterValue Value;
		public bool ValidValue;

		public RegisterID DestRegId2;
		public RegisterValue Value2;
		public bool ValidValue2;

        public ReorderBufferSlot(int reorderBufferID)
        {
			ReorderBufferID = new ReorderBufferID(reorderBufferID);
        }

	}

}

