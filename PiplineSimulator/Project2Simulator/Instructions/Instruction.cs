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

		public FunctionalUnitType FunctionalUnitType;

		public RegisterValue RegisterValue;

		public Address Address;

        public Instruction(Opcode opcode, RegisterID dest, RegisterID op1Reg, RegisterID op2Reg, RegisterValue op1Val, RegisterValue op2Val, FunctionalUnitType type)
        {
			Opcode = opcode;
			Destination = dest;
			Op1Reg = op1Reg;
			Op2Reg = op2Reg;
			FunctionalUnitType = type;
			Op1 = op1Val;
			Op2 = op2Val;
			RegisterValue = null;
			Address = null;
        }

	}

}

