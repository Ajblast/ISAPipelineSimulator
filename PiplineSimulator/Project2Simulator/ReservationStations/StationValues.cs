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

		public RegisterID Dest1Reg;
		public RegisterID Dest2Reg;
		
		public RegisterValue Op1;
		public RegisterValue Op2;
		public RegisterValue Op3;

		public RegisterID Op1Reg;
		public RegisterID Op2Reg;
		public RegisterID Op3Reg;

		public bool Op1Present;
		public bool Op2Present;
		public bool Op3Present;

		public ReorderBufferID Op1Src;
		public ReorderBufferID Op2Src;
		public ReorderBufferID Op3Src;

		public Address Addr;

	}

}

