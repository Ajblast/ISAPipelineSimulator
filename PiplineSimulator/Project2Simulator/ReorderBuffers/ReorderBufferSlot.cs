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

		public bool Head;
		public bool Tail;

        public ReorderBufferSlot(int reorderBufferID)
        {
			ReorderBufferID = new ReorderBufferID(reorderBufferID);
			Value = new RegisterValue();
			Value2 = new RegisterValue();
			ValidValue = false;
			ValidValue2 = false;

			Head = false;
			Tail = false;
        }

        public override string ToString()
        {
            return string.Format(StringFormatService.GetReorderBufferSlotFormat(),
				Head == true ? "H" : Tail == true ? "T" : "X",
				ReorderBufferID.BufferID,
				(Instruction == null) ? "X" : Instruction.StationID.ToString(),
				Ocupodo.ToString(),
				//(DestRegId == null) ? "X" : DestRegId.ToString(),
				(DestRegId == null) ? "X" : RegisterHelper.IDtoName(DestRegId),
				Value.Value,
				ValidValue,
				//(DestRegId2 == null) ? "X" : DestRegId2.ToString(),
				(DestRegId2 == null) ? "X" : RegisterHelper.IDtoName(DestRegId2),
				Value2.Value,
				ValidValue2
				);
		}

    }

}

