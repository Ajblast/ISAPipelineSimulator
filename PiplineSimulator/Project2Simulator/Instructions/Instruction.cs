/* Author: Seth Bowden */
using Project2Simulator.Instructions;
using Project2Simulator.Registers;
using Project2Simulator.FunctionalUnits;

namespace Project2Simulator.Instructions
{
	public struct Instruction
	{
		public Opcode Opcode;

		public RegisterID Destination;

		public RegisterValue Op1;

		public RegisterValue Op2;

		public RegisterID Op1Reg;

		public RegisterID Op2Reg;

		private FunctionalUnitType functionalUnitType;

		private RegisterValue registerValue;

		private Address address;

        public Instruction(Opcode opcode, RegisterID dest, RegisterID op1Reg, RegisterID op2Reg, FunctionalUnitType type)
        {
			Opcode = opcode;
			Destination = dest;
			Op1Reg = op1Reg;
			Op2Reg = op2Reg;
			functionalUnitType = type;
			Op1 = null;
			Op2 = null;
			registerValue = null;
			address = null;
        }
	}

}

