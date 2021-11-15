using Project2Simulator.Instructions;
using Project2Simulator.Registers;
using Project2Simulator.FunctionalUnits;

namespace Project2Simulator.Instructions
{
	public struct Instruction
	{
		public Opcode Opcode;

		public bool Immediate;

		public RegisterID Destination;

		public RegisterValue Op1;

		public RegisterValue Op2;

		public RegisterID Op1Reg;

		public RegisterID Op2Reg;

		public FunctionalUnitType FunctionUnitType;

		private FunctionalUnitType functionalUnitType;

		private Opcode opcode;

		private RegisterValue registerValue;

	}

}

