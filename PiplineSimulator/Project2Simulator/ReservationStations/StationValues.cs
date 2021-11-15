/* Author: Austin Kincer */

using Project2Simulator.Instructions;
using Project2Simulator.Registers;
using Project2Simulator.ReorderBuffers;

namespace Project2Simulator.ReservationStations
{
	public struct StationValues
	{
		public Opcode Opcode;

		public ReorderBufferID Dest;

		public RegisterValue Op1;
		public RegisterValue Op2;

		public ReorderBufferID Op1Src;
		public ReorderBufferID Op2Src;

		public Address Addr;
	}

}

