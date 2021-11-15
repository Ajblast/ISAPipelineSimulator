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

        public Instruction(Opcode opcode, RegisterID dest, RegisterID op1Reg, RegisterID op2Reg, FunctionalUnitType type)
        {
			Opcode = opcode;
			Destination = dest;
			Op1Reg = op1Reg;
			Op2Reg = op2Reg;
			FunctionalUnitType = type;
			Op1 = null;
			Op2 = null;
			RegisterValue = null;
			Address = null;
        }


		public Instruction(Opcode opcode, RegisterID dest, RegisterID op1Reg, RegisterValue immediate, FunctionalUnitType type)
		{
			Opcode = opcode;
			Destination = dest;
			Op1Reg = op1Reg;
			Op2Reg = null;
			FunctionalUnitType = type;
			Op1 = null;
			Op2 = immediate;
			RegisterValue = null;
			Address = null;
		}
	}

}

