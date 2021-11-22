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
			Value = new RegisterValue();
			Value2 = new RegisterValue();
			ValidValue = false;
			ValidValue2 = false;

        }

        public override string ToString()
        {
            return string.Format("{0,-10}{1,-10}{2,-10}{3,-10}{4,-10}{5,-10}{6,-10}{7,-10}{8}",
				ReorderBufferID,
				Instruction.StationID,
				Ocupodo,
				DestRegId.ID,
				Value,
				ValidValue,
				DestRegId2.ID,
				Value2,
				ValidValue2
				);
		}

    }

}

