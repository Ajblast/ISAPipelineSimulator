/* Author: Seth Bowden */
using Project2Simulator.FunctionalUnits;
using Project2Simulator.Instructions;
using Project2Simulator.Registers;
using System;

namespace Project2Simulator
{
	// An instruction decoder
	public class Decoder
	{
		RegisterFile RegFile;

		private const ushort OpCodeMask = 0xFE00;
		private const ushort DestRegMask = 0x00F0;
		private const ushort ImmediateIdentifierMask = 0x0100;

		//Arithmetic Masks
		private const ushort ArithDestRegMask = 0x00F0;
		private const ushort Arith1RegOP = 0x000F;
		private const ushort RegOP2 = 0x000F; //used for any instruction format where 2nd op is 4 LSB of 32-bit LBSs


        public Decoder(RegisterFile regFile)
        {
			RegFile = regFile;
        }

		// Create the decoder with the fetcher, registers, and memory


		// Decode an instruction
		public Instruction Decode(uint EncodedInstruction)
		{
			ushort opCode = ExtractOpCode(EncodedInstruction);
			Instruction CreatedInstruction;
			switch (opCode)
			{
				case (ushort)Opcode.NOP:
					CreatedInstruction = new Instruction(Opcode.NOP, null, null, null, null, null, null, FunctionalUnitType.NULL);
					break;
				case (ushort)Opcode.ADD:
					CreatedInstruction = CreateArithmeticInstruction(Opcode.ADD, EncodedInstruction);
					break;
				case (ushort)Opcode.ADDC:
					CreatedInstruction = CreateArithmeticInstruction(Opcode.ADDC, EncodedInstruction);
					break;
				case (ushort)Opcode.SUBB:
					CreatedInstruction = CreateArithmeticInstruction(Opcode.SUBB, EncodedInstruction);
					break;
				case (ushort)Opcode.AND:
					CreatedInstruction = CreateArithmeticInstruction(Opcode.AND, EncodedInstruction);
					break;
				case (ushort)Opcode.OR:
					CreatedInstruction = CreateArithmeticInstruction(Opcode.OR, EncodedInstruction);
					break;
				case (ushort)Opcode.NOR:
					CreatedInstruction = CreateArithmeticInstruction(Opcode.NOR, EncodedInstruction);
					break;
				case (ushort)Opcode.SHL:
					CreatedInstruction = CreateArithmeticInstruction(Opcode.SHL, EncodedInstruction);
					break;
				case (ushort)Opcode.SHR:
					CreatedInstruction = CreateArithmeticInstruction(Opcode.SHR, EncodedInstruction);
					break;
				case (ushort)Opcode.SHAR:
					CreatedInstruction = CreateArithmeticInstruction(Opcode.SHAR, EncodedInstruction);
					break;
				case (ushort)Opcode.ROR:
					CreatedInstruction = CreateArithmeticInstruction(Opcode.ROR, EncodedInstruction);
					break;
				case (ushort)Opcode.ROL:
					CreatedInstruction = CreateArithmeticInstruction(Opcode.ROL, EncodedInstruction);
					break;
				case (ushort)Opcode.RORC:
					CreatedInstruction = CreateArithmeticInstruction(Opcode.RORC, EncodedInstruction);
					break;
				case (ushort)Opcode.ROLC:
					CreatedInstruction = CreateArithmeticInstruction(Opcode.ROLC, EncodedInstruction);
					break;
				case (ushort)Opcode.LOAD:
					CreatedInstruction = CreateMemInstruction(Opcode.LOAD, EncodedInstruction);
					break;
				case (ushort)Opcode.STOR:
					CreatedInstruction = CreateMemInstruction(Opcode.STOR, EncodedInstruction);
					break;
				case (ushort)Opcode.MOV:
					CreatedInstruction = CreateMOVInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.PUSH:
					CreatedInstruction = CreatePUSHInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.POP:
					CreatedInstruction = CreatePOPInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.CMP:
					CreatedInstruction = CreateCMPInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.JZ:
				case (ushort)Opcode.JNZ:
				case (ushort)Opcode.JG:
				case (ushort)Opcode.JGE:
				case (ushort)Opcode.JL:
				case (ushort)Opcode.JLE:
				case (ushort)Opcode.JA:
				case (ushort)Opcode.JAE:
				case (ushort)Opcode.JB:
				case (ushort)Opcode.JBE:
					CreatedInstruction = CreateBranchInstruction((Opcode)opCode, EncodedInstruction);
					break;
				case (ushort)Opcode.LDA:
					CreatedInstruction = CreateLDAInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.NEG:
					CreatedInstruction = CreateNEGInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.XOR:
					CreatedInstruction = CreateArithmeticInstruction(Opcode.XOR, EncodedInstruction);
					break;
				case (ushort)Opcode.SUB:
					CreatedInstruction = CreateArithmeticInstruction(Opcode.SUB, EncodedInstruction);
					break;
				case (ushort)Opcode.HALT:
					CreatedInstruction = new Instruction(Opcode.HALT, null, null, null, new RegisterValue(0), new RegisterValue(0), null, FunctionalUnitType.NULL);
					break;
				default:
					throw new Exception("Invalid Instruction OP code Dedcoded");
			}

			return CreatedInstruction;
		}

        private static Instruction CreateArithmeticInstruction(Opcode opcode, uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
 			if (immediateBitSet(UpperBits))
				return new Instruction(
					opcode,
					new Registers.RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					new Registers.RegisterID(UpperBits & Arith1RegOP),
					null,
					new RegisterValue(0),
					new Registers.RegisterValue(LowerBits),
					null,
					OpcodeHelper.GetFunctionalUnitType(opcode)
					);
			else
				return new Instruction(
					opcode,
					new Registers.RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					new Registers.RegisterID(UpperBits & Arith1RegOP),
					new Registers.RegisterID((LowerBits & RegOP2)),
					new RegisterValue(0),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(opcode)
					);
		}

        private static Instruction CreateMemInstruction(Opcode opcode, uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new Instruction(
					opcode,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					null,
					null,
					new RegisterValue(0),
					new RegisterValue(0),
					new Address((int)(((uint)UpperBits & 0xF) << 16) | LowerBits),
					OpcodeHelper.GetFunctionalUnitType(opcode)
					);
			else
				return new Instruction(
					opcode,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					new RegisterID(UpperBits & Arith1RegOP),
					null,
					new RegisterValue(0),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(opcode)
					);
		}

        private static Instruction CreateMOVInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
			{
				return new Instruction(
					Opcode.MOV,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					null,
					null,
					new RegisterValue(0),
					new RegisterValue(LowerBits),
					null,
					OpcodeHelper.GetFunctionalUnitType(Opcode.MOV)
					);
			}
			else
				return new Instruction(
					Opcode.MOV,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					new RegisterID((UpperBits & Arith1RegOP)),
					null,
					new RegisterValue(0),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(Opcode.MOV)
					);
		}

        private Instruction CreatePUSHInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
			{
				return new Instruction(
					Opcode.PUSH,
					new RegisterID(RegFile.SP.ID.ID),
					new RegisterID(RegFile.SP.ID.ID),
					null,
					new RegisterValue(0),
					new RegisterValue(LowerBits),
					null,
					OpcodeHelper.GetFunctionalUnitType(Opcode.PUSH)
					);
			}
			else
				return new Instruction(
					Opcode.PUSH,
					new RegisterID(RegFile.SP.ID.ID),
					new RegisterID(RegFile.SP.ID.ID),
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					new RegisterValue(0),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(Opcode.PUSH)
					);
		}

        private Instruction CreatePOPInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			return new Instruction(
				Opcode.POP,
				new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
				new RegisterID(RegFile.SP.ID.ID),
				null,
				new RegisterValue(0),
				new RegisterValue(0),
				null,
				OpcodeHelper.GetFunctionalUnitType(Opcode.POP)
				);
		}

        private Instruction CreateCMPInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			return new Instruction(
				Opcode.CMP,
				new RegisterID(RegFile.RK.ID.ID),
				new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
				new RegisterID((int)((uint)(UpperBits & Arith1RegOP))),
				new RegisterValue(0),
				new RegisterValue(0),
				null,
				OpcodeHelper.GetFunctionalUnitType(Opcode.CMP)
				);
        }

        private Instruction CreateBranchInstruction(Opcode opcode, uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			return new Instruction(
				opcode,
				null,
				new RegisterID(RegFile.FLAG.ID.ID),
				null,
				new RegisterValue(0),
				new RegisterValue(0),
				null,
				OpcodeHelper.GetFunctionalUnitType(opcode)
				);
        }

        private Instruction CreateLDAInstruction(uint encodedInstruction)
        {

			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			return new Instruction(
				Opcode.LDA,
				new RegisterID(RegFile.RE.ID.ID),
				null,
				null,
				new RegisterValue(0),
				new RegisterValue(0),
				new Address((int)(((uint)UpperBits & 0xF) << 16) | LowerBits),
				OpcodeHelper.GetFunctionalUnitType(Opcode.LDA)
				);
		}

        private Instruction CreateNEGInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new Instruction(
					Opcode.NEG,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					null,
					null,
					new RegisterValue(0),
					new RegisterValue(LowerBits),
					null,
					OpcodeHelper.GetFunctionalUnitType(Opcode.NEG)
					);
			else
				return new Instruction(
					Opcode.NEG,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					new RegisterID((UpperBits & Arith1RegOP)),
					null,
					new RegisterValue(0),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(Opcode.NEG)
					);
		}



        private static ushort ExtractOpCode(uint EncodedInstruction)
        {
			//I am using ASL due to c#, bitwise & produces int. Cast applies after all operations, so ASL should act as LSL
			return (ushort)((uint)(EncodedInstruction & OpCodeMask) >> 25);

        }

		private static bool immediateBitSet(ushort EncodedInstruction)
        {
			return Convert.ToBoolean(EncodedInstruction & ImmediateIdentifierMask);
        }

		private static ushort getLowerBits(uint encodedInstruction)
		{
			return (ushort) (encodedInstruction & 0x0000FFFF);
		}

		private static ushort getUpperBits(uint encodedInstruction)
        {
			return (ushort)(((uint)(encodedInstruction & 0xFFFF0000)) >> 16);
		}
		

	}

}

