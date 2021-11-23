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
		public RegisterID Destination2;

		public RegisterValue Op1;
		public RegisterValue Op2;
		public RegisterValue Op3;

		public RegisterID Op1Reg;
		public RegisterID Op2Reg;
		public RegisterID Op3Reg;

		public FunctionalUnitType FunctionalUnitType;

		public RegisterValue RegisterValue;

		public Address Address;

        public Instruction(Opcode opcode, RegisterID dest, RegisterID dest2, RegisterID op1Reg, RegisterID op2Reg, RegisterID op3Reg, RegisterValue op1Val, RegisterValue op2Val, RegisterValue op3Val, Address addr, FunctionalUnitType type)
        {
			Opcode = opcode;
			Destination = dest;
			Destination2 = dest2;

			Op1Reg = op1Reg;
			Op2Reg = op2Reg;
			Op3Reg = op3Reg;

			FunctionalUnitType = type;

			Op1 = op1Val;
			Op2 = op2Val;
			Op3 = op3Val;
			RegisterValue = null;

			Address = addr;
        }

        public override string ToString()
        {
			return string.Format("{0,-10}{1,-10}{2,-10}{3,-10}{4,-10}{5,-10}{6,-10}{7,-10}{8}",
				Opcode.ToString(),
                (Destination == null) ? "X":Destination.ID.ToString(),
				(Destination2 == null) ? "X":Destination2.ID.ToString(),
				(Op1Reg == null) ? "X":Op1Reg.ID.ToString(),
				Op1.Value,
				(Op2Reg == null) ? "X":Op2Reg.ID.ToString(),
				Op2.Value,
				(Op2Reg == null) ? "X":Op3Reg.ID.ToString(),
				Op3.Value,
				(Address == null) ? "X":Address.Value.ToString(),
				FunctionalUnitType.ToString()
				);
		}

    }

}

