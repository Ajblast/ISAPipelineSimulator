using Project2Simulator.Instructions;
using Project2Simulator.Registers;
using Project2Simulator.ReorderBuffers;
using Project2Simulator;

namespace Project2Simulator.ReservationStations
{
	public struct StationValues
	{
		public Opcode Opcode;

		public RegisterValue Op1;
		public RegisterValue Op2;

		public ReorderBufferID Op1Src;
		public ReorderBufferID Op2Src;

		public ReorderBufferID Dest;

		public Address Addr;


	}

}

