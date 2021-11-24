using Project2Simulator.Registers;
using Project2Simulator.ReservationStations;
using Project2Simulator.ReorderBuffers;
using PiplineSimulator;

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
            return string.Format(StringFormatService.GetReorderBufferSlotFormat(),
				ReorderBufferID.BufferID,
				(Instruction == null) ? "X":Instruction.StationID.ID.ToString(),
				Ocupodo.ToString(),
				(DestRegId == null) ? "X":DestRegId.ID.ToString(),
				Value.Value,
				ValidValue,
				(DestRegId2 == null) ? "X":DestRegId2.ID.ToString(),
				Value2.Value,
				ValidValue2
				);
		}

    }

}

