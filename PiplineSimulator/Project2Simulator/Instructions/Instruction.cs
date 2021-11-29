/* Author: Seth Bowden */
using Project2Simulator.Instructions;
using Project2Simulator.Registers;
using Project2Simulator.FunctionalUnits;
using PiplineSimulator;
using System;
using System.Reflection;
using System.ComponentModel;

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
			return string.Format(StringFormatService.GetInstructionFormat(),
				Opcode.ToString(),
                (Destination == null) ? "X": RegisterHelper.IDtoName(Destination),
				(Destination2 == null) ? "X": RegisterHelper.IDtoName(Destination2),
				(Op1Reg == null) ? "X": RegisterHelper.IDtoName(Op1Reg),
				Op1.Value,
				(Op2Reg == null) ? "X": RegisterHelper.IDtoName(Op2Reg),
				Op2.Value,
				(Op3Reg == null) ? "X": RegisterHelper.IDtoName(Op3Reg),
				Op3.Value,
				(Address == null) ? "X":Address.Value.ToString(),
				GetDescription(this.FunctionalUnitType)
				);
		}

		//Sourced from https://stackoverflow.com/questions/1415140/can-my-enums-have-friendly-names
		public static string GetDescription(Enum value)
		{
			Type type = value.GetType();
			string name = Enum.GetName(type, value);
			if (name != null)
			{
				FieldInfo field = type.GetField(name);
				if (field != null)
				{
					DescriptionAttribute attr =
						   Attribute.GetCustomAttribute(field,
							 typeof(DescriptionAttribute)) as DescriptionAttribute;
					if (attr != null)
					{
						return attr.Description;
					}
				}
			}
			return null;
		}

	}

}

