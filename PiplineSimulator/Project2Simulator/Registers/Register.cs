/* Author: Austin Kincer */

using Project2Simulator.Registers;
using Project2Simulator.ReorderBuffers;

namespace Project2Simulator.Registers
{
	public class Register
	{
		public RegisterID ID;
		public RegisterValue Value;
		public ReorderBufferID ReorderId;
		public bool Busy;
	}

}

