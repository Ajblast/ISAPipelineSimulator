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

		private ReorderBufferID reorderBufferID;

		private RegisterValue registerValue;

		private ReservationStation reservationStation;

		private RegisterID registerID;

        public ReorderBufferSlot(int reorderBufferID)
        {
			this.reorderBufferID = new ReorderBufferID(reorderBufferID);
        }

	}

}

