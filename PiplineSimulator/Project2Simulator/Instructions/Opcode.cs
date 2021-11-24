/* Author: Seth Bowden */
using Project2Simulator.FunctionalUnits;
using System;

namespace Project2Simulator.Instructions
{
	public enum Opcode : ushort
	{
		NOP = 0b0000000,
		ADD = 0b0000001,
		ADDC = 0b0000010,
		SUB = 0b0000011,
		SUBB = 0b0000100,
		AND = 0b0000101,
		OR = 0b0000110,
		NOR = 0b0000111,
		XOR = 0b0001000,
		SHL = 0b0001001,
		SHR = 0b0001010,
		SHAR = 0b0001011,
		ROR = 0b0001100,
		ROL = 0b0001101,
		RORC = 0b0001110,
		ROLC = 0b0001111,
		LOAD = 0b0010000,
		STOR = 0b0010001,
		MOV = 0b0010010,
		PUSH = 0b0010011,
		POP = 0b0010100,
		CMP = 0b0010101,
		NEG = 0b0010110,
		JZ = 0b0010111,
		JNZ = 0b0011000,
		JG = 0b0011001,
		JGE = 0b0011010,
		JL = 0b0011011,
		JLE = 0b0011100,
		JA = 0b0011101,
		JAE = 0b0011110,
		JB = 0b0011111,
		JBE = 0b0100000,
		LDA = 0b0100001,

		/* Atomic */
		FETCH	= 0b0100010,	// Fetch a value atomically
		ADDA	= 0b0100011,
		SUBA	= 0b0100100,
		ANDA	= 0b0100101,
		ORA		= 0b0100110,
		XORA	= 0b0100111,
		CMPSW	= 0b0101000,	// Compare and Swap	// MEM Reg, CMP Reg, Value Reg
		SWAP	= 0b0101001,	// Swap				// MEM Reg, Value Reg


		HALT = 0b1111111
	}

	/* Author: Austin Kincer */
	public static class OpcodeHelper
    {
		public static bool TouchesFlags(Opcode opcode)
        {
			bool retValue = false;

            switch (opcode)
            {
				case Opcode.ADD:
				case Opcode.ADDC:
				case Opcode.SUB:
				case Opcode.SUBB:
				case Opcode.AND:
				case Opcode.OR:
				case Opcode.NOR:
				case Opcode.NEG:
				case Opcode.XOR:
				case Opcode.SHL:
				case Opcode.SHR:
				case Opcode.SHAR:
				case Opcode.ROR:
				case Opcode.ROL:
				case Opcode.RORC:
				case Opcode.ROLC:
				case Opcode.CMP:
					retValue = true;
					break;
				default:
					break;
            }

			return retValue;
        }

		public static bool IsArithmetic(Opcode opcode)
        {
			bool retValue = false;

			switch (opcode)
			{
				case Opcode.ADD:
				case Opcode.ADDC:
				case Opcode.SUB:
				case Opcode.SUBB:
				case Opcode.AND:
				case Opcode.OR:
				case Opcode.NOR:
				case Opcode.NEG:
				case Opcode.XOR:
				case Opcode.SHL:
				case Opcode.SHR:
				case Opcode.SHAR:
				case Opcode.ROR:
				case Opcode.ROL:
				case Opcode.RORC:
				case Opcode.ROLC:
					retValue = true;
					break;
				default:
					break;
			}

			return retValue;

		}

		public static bool IsJumpInstruction(Opcode opcode)
        {
			bool retValue = false;
            switch (opcode)
            {
				case Opcode.JZ:
				case Opcode.JNZ:
				case Opcode.JG:
				case Opcode.JGE:
				case Opcode.JL:
				case Opcode.JLE:
				case Opcode.JA:
				case Opcode.JAE:
				case Opcode.JB:
				case Opcode.JBE:
					retValue = true;
					break;
            }

			return retValue;
        }

		public static Opcode StringToOpcode(string opcode)
        {
			return (Opcode) Enum.Parse(typeof(Opcode), opcode, true);
        }

		public static FunctionalUnitType GetFunctionalUnitType(Opcode opcode)
		{
			FunctionalUnitType functionalUnitType;
			switch (opcode)
			{
				case Opcode.NOP:
					functionalUnitType = FunctionalUnitType.NULL;
					break;
				case Opcode.ADD:
				case Opcode.ADDC:
				case Opcode.SUB:
				case Opcode.SUBB:
				case Opcode.AND:
				case Opcode.OR:
				case Opcode.NOR:
				case Opcode.NEG:
				case Opcode.XOR:
				case Opcode.SHL:
				case Opcode.SHR:
				case Opcode.SHAR:
				case Opcode.ROR:
				case Opcode.ROL:
				case Opcode.RORC:
				case Opcode.ROLC:
				case Opcode.CMP:
					functionalUnitType = FunctionalUnitType.INTEGER_ADDER;
					break;
				case Opcode.PUSH:
				case Opcode.POP:
				case Opcode.LOAD:
				case Opcode.STOR:
				/* Atomic */
				case Opcode.FETCH:
				case Opcode.ADDA:
				case Opcode.SUBA:
				case Opcode.ANDA:
				case Opcode.ORA:
				case Opcode.XORA:
				case Opcode.CMPSW:
				case Opcode.SWAP:
					functionalUnitType = FunctionalUnitType.MEMORY_UNIT;
					break;
				case Opcode.MOV:
					functionalUnitType = FunctionalUnitType.MOVEMENT_UNIT;
					break;
				case Opcode.JZ:
				case Opcode.JNZ:
				case Opcode.JG:
				case Opcode.JGE:
				case Opcode.JL:
				case Opcode.JLE:
				case Opcode.JA:
				case Opcode.JAE:
				case Opcode.JB:
				case Opcode.JBE:
				case Opcode.LDA:
					functionalUnitType = FunctionalUnitType.BRANCH_UNIT;
					break;
				case Opcode.HALT:
					functionalUnitType = FunctionalUnitType.NULL;
					break;
				default:
					functionalUnitType = FunctionalUnitType.ILLEGAL;
					break;
			}

			return functionalUnitType;
		}
	}
}

