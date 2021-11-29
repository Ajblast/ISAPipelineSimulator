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
		private const ushort RegOP3 = 0x00F0; //Used for atomics Compare and Swap


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
					CreatedInstruction = new Instruction(Opcode.NOP, null, null, null, null, null, new RegisterValue(0), new RegisterValue(0), new RegisterValue(0), null, FunctionalUnitType.NULL);
					break;
				case (ushort)Opcode.ADD:
				case (ushort)Opcode.SUB:
				case (ushort)Opcode.AND:
				case (ushort)Opcode.OR:
				case (ushort)Opcode.NOR:
				case (ushort)Opcode.XOR:
				case (ushort)Opcode.SHL:
				case (ushort)Opcode.SHR:
				case (ushort)Opcode.SHAR:
				case (ushort)Opcode.ROR:
				case (ushort)Opcode.ROL:
					CreatedInstruction = CreateArithmeticInstruction((Opcode)opCode, EncodedInstruction);
					break;
				case (ushort)Opcode.ADDC:
				case (ushort)Opcode.SUBB:
				case (ushort)Opcode.RORC:
				case (ushort)Opcode.ROLC:
					CreatedInstruction = CreateArithmeticInstructionFlags((Opcode)opCode, EncodedInstruction);
					break;
				case (ushort)Opcode.LOAD:
					CreatedInstruction = CreateLoadInstruction(Opcode.LOAD, EncodedInstruction);
					break;
				case (ushort)Opcode.STOR:
					CreatedInstruction = CreateStorInstruction(Opcode.STOR, EncodedInstruction);
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
				case (ushort)Opcode.HALT:
					CreatedInstruction = new Instruction(Opcode.HALT, null, null, null, null, null, new RegisterValue(0), new RegisterValue(0), new RegisterValue(0), null, FunctionalUnitType.NULL);
					break;
				case (ushort)Opcode.FETCH:
					CreatedInstruction = CreateFetchInstruction((Opcode)opCode, EncodedInstruction);
					break;
				case (ushort)Opcode.ADDA:
				case (ushort)Opcode.SUBA:
				case (ushort)Opcode.ANDA:
				case (ushort)Opcode.ORA:
				case (ushort)Opcode.XORA:
					CreatedInstruction = CreateArithmeticInstructionAtomic((Opcode)opCode, EncodedInstruction);
					break;
				case (ushort)Opcode.CMPSW:
					CreatedInstruction = CreateCompareAndSwapAtomic(Opcode.CMPSW, EncodedInstruction);
					break;
				case (ushort)Opcode.SWAP:
					CreatedInstruction = CreateSwapAtomic(Opcode.SWAP, EncodedInstruction);
					break;
				default:
					throw new Exception("Invalid Instruction OP code Dedcoded");
			}

			return CreatedInstruction;
		}

        private Instruction CreateArithmeticInstruction(Opcode opcode, uint encodedInstruction)
        {
			ushort UpperBits = GetUpperBits(encodedInstruction);
			ushort LowerBits = GetLowerBits(encodedInstruction);
 			if (ImmediateBitSet(UpperBits))
				return new Instruction(
					opcode,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					new RegisterID(RegFile.FLAG.ID),
					new RegisterID(UpperBits & Arith1RegOP),
					null,
					null,
					new RegisterValue(0),
					new RegisterValue(LowerBits),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(opcode)
					);
			else
				return new Instruction(
					opcode,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					new RegisterID(RegFile.FLAG.ID),
					new RegisterID(UpperBits & Arith1RegOP),
					new RegisterID(LowerBits & RegOP2),
					null,				
					new RegisterValue(0),
					new RegisterValue(0),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(opcode)
					);
		}

		private Instruction CreateArithmeticInstructionFlags(Opcode opcode, uint encodedInstruction)
		{
			ushort UpperBits = GetUpperBits(encodedInstruction);
			ushort LowerBits = GetLowerBits(encodedInstruction);
			if (ImmediateBitSet(UpperBits))
				return new Instruction(
					opcode,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					new RegisterID(RegFile.FLAG.ID),
					new RegisterID(UpperBits & Arith1RegOP),
					null,
					new RegisterID(RegFile.FLAG.ID),
					new RegisterValue(0),
					new RegisterValue(LowerBits),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(opcode)
					);
			else
				return new Instruction(
					opcode,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					new RegisterID(RegFile.FLAG.ID),
					new RegisterID(UpperBits & Arith1RegOP),
					new RegisterID(LowerBits & RegOP2),
					new RegisterID(RegFile.FLAG.ID),
					new RegisterValue(0),
					new RegisterValue(0),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(opcode)
					);
		}


		private Instruction CreateLoadInstruction(Opcode opcode, uint encodedInstruction)
        {
			ushort UpperBits = GetUpperBits(encodedInstruction);
			ushort LowerBits = GetLowerBits(encodedInstruction);
			if (ImmediateBitSet(UpperBits))
				return new Instruction(
					opcode,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					null,
					null,
					null,
					null,
					new RegisterValue((uint)(((uint)UpperBits & 0xF) << 16) | LowerBits),
					new RegisterValue(0),
					new RegisterValue(0),
					new Address((int)(((uint)UpperBits & 0xF) << 16) | LowerBits),
					OpcodeHelper.GetFunctionalUnitType(opcode)
					);
			else
				return new Instruction(
					opcode,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					null,
					new RegisterID(UpperBits & Arith1RegOP),
					null,
					null,
					new RegisterValue(0),
					new RegisterValue(0),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(opcode)
					);
		}

		private Instruction CreateStorInstruction(Opcode opcode, uint encodedInstruction)
		{
			ushort UpperBits = GetUpperBits(encodedInstruction);
			ushort LowerBits = GetLowerBits(encodedInstruction);
			if (ImmediateBitSet(UpperBits))
				return new Instruction(
					opcode,
					null,
					null,
					null,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					null,
					new RegisterValue(0),
					new RegisterValue(0),
					new RegisterValue(0),
					new Address((int)(((uint)UpperBits & 0xF) << 16) | LowerBits),
					OpcodeHelper.GetFunctionalUnitType(opcode)
					);
			else
				return new Instruction(
					opcode,
					null,
					null,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					new RegisterID(UpperBits & Arith1RegOP),
					null,
					new RegisterValue(0),
					new RegisterValue(0),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(opcode)
					);
		}

		private Instruction CreateMOVInstruction(uint encodedInstruction)
        {
			ushort UpperBits = GetUpperBits(encodedInstruction);
			ushort LowerBits = GetLowerBits(encodedInstruction);
			if (ImmediateBitSet(UpperBits))
			{
				return new Instruction(
					Opcode.MOV,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					null,
					null,
					null,
					null,
					new RegisterValue(LowerBits),
					new RegisterValue(0),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(Opcode.MOV)
					);
			}
			else
				return new Instruction(
					Opcode.MOV,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					null,
					new RegisterID((UpperBits & Arith1RegOP)),
					null,
					null,
					new RegisterValue(0),
					new RegisterValue(0),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(Opcode.MOV)
					);
		}

        private Instruction CreatePUSHInstruction(uint encodedInstruction)
        {
			ushort UpperBits = GetUpperBits(encodedInstruction);
			ushort LowerBits = GetLowerBits(encodedInstruction);
			if (ImmediateBitSet(UpperBits))
			{
				return new Instruction(
					Opcode.PUSH,
					new RegisterID(RegFile.SP.ID),
					null,
					new RegisterID(RegFile.SP.ID),
					null,
					null,
					new RegisterValue(0),
					new RegisterValue(LowerBits),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(Opcode.PUSH)
					);
			}
			else
				return new Instruction(
					Opcode.PUSH,
					new RegisterID(RegFile.SP.ID),
					null,
					new RegisterID(RegFile.SP.ID),
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					null,
					new RegisterValue(0),
					new RegisterValue(0),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(Opcode.PUSH)
					);
		}

        private Instruction CreatePOPInstruction(uint encodedInstruction)
        {
			ushort UpperBits = GetUpperBits(encodedInstruction);
			ushort LowerBits = GetLowerBits(encodedInstruction);
			return new Instruction(
				Opcode.POP,
				new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
				new RegisterID(RegFile.SP.ID),
				new RegisterID(RegFile.SP.ID),
				null,
				null,
				new RegisterValue(0),
				new RegisterValue(0),
				new RegisterValue(0),
				null,
				OpcodeHelper.GetFunctionalUnitType(Opcode.POP)
				);
		}

        private Instruction CreateCMPInstruction(uint encodedInstruction)
        {
			ushort UpperBits = GetUpperBits(encodedInstruction);
			ushort LowerBits = GetLowerBits(encodedInstruction);
			return new Instruction(
				Opcode.CMP,
				new RegisterID(RegFile.RK.ID),
				new RegisterID(RegFile.FLAG.ID),
				new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
				new RegisterID((int)((uint)(UpperBits & Arith1RegOP))),
				null,
				new RegisterValue(0),
				new RegisterValue(0),
				new RegisterValue(0),
				null,
				OpcodeHelper.GetFunctionalUnitType(Opcode.CMP)
				);
        }

        private Instruction CreateBranchInstruction(Opcode opcode, uint encodedInstruction)
        {
			ushort UpperBits = GetUpperBits(encodedInstruction);
			ushort LowerBits = GetLowerBits(encodedInstruction);
			uint immediate =  (uint)(((UpperBits & 0b11111) << 16) | LowerBits);
			if ((UpperBits & 0b10000) == 0b10000)
				immediate |= 0xFFE00000;

			int newPC = (int) RegFile.PC.Value.Value + (int) immediate;

			return new Instruction(
				opcode,
				new RegisterID(RegFile.PC.ID),
				null,
				new RegisterID(RegFile.FLAG.ID),
				null,
				null,
				new RegisterValue(0),
				new RegisterValue(0),
				new RegisterValue(0),
				new Address(newPC),
				OpcodeHelper.GetFunctionalUnitType(opcode)
				);
        }

        private Instruction CreateLDAInstruction(uint encodedInstruction)
        {

			ushort UpperBits = GetUpperBits(encodedInstruction);
			ushort LowerBits = GetLowerBits(encodedInstruction);
			return new Instruction(
				Opcode.LDA,
				new RegisterID(RegFile.RE.ID),
				null,
				null,
				null,
				null,
				new RegisterValue(0),
				new RegisterValue(0),
				new RegisterValue(0),
				new Address((int)(((uint)UpperBits & 0xF) << 16) | LowerBits),
				OpcodeHelper.GetFunctionalUnitType(Opcode.LDA)
				);
		}

        private Instruction CreateNEGInstruction(uint encodedInstruction)
        {
			ushort UpperBits = GetUpperBits(encodedInstruction);
			ushort LowerBits = GetLowerBits(encodedInstruction);
			if (ImmediateBitSet(UpperBits))
				return new Instruction(
					Opcode.NEG,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					new RegisterID(RegFile.FLAG.ID),
					null,
					null,
					null,
					new RegisterValue(0),
					new RegisterValue(LowerBits),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(Opcode.NEG)
					);
			else
				return new Instruction(
					Opcode.NEG,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					new RegisterID(RegFile.FLAG.ID),
					new RegisterID((UpperBits & Arith1RegOP)),
					null,
					null,
					new RegisterValue(0),
					new RegisterValue(0),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(Opcode.NEG)
					);
		}


		private Instruction CreateFetchInstruction(Opcode opcode, uint encodedInstruction)
		{
			ushort UpperBits = GetUpperBits(encodedInstruction);
			ushort LowerBits = GetLowerBits(encodedInstruction);
			return new Instruction(
				opcode,
				new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
				null,
				new RegisterID(UpperBits & Arith1RegOP),
				null,
				null,
				new RegisterValue(0),
				new RegisterValue(0),
				new RegisterValue(0),
				null,
				OpcodeHelper.GetFunctionalUnitType(opcode)
				);
		}

		private Instruction CreateArithmeticInstructionAtomic(Opcode opcode, uint encodedInstruction)
		{
			ushort UpperBits = GetUpperBits(encodedInstruction);
			ushort LowerBits = GetLowerBits(encodedInstruction);
			if (ImmediateBitSet(UpperBits))
				return new Instruction(
					opcode,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					null,
					new RegisterID(UpperBits & Arith1RegOP),
					null,
					null,
					new RegisterValue(0),
					new RegisterValue(LowerBits),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(opcode)
					);
			else
				return new Instruction(
				opcode,
				new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
				null,
				new RegisterID(UpperBits & Arith1RegOP),
				new RegisterID(LowerBits & RegOP2),
				null,
				new RegisterValue(0),
				new RegisterValue(0),
				new RegisterValue(0),
				null,
				OpcodeHelper.GetFunctionalUnitType(opcode)
				);
		}

		
		private Instruction CreateCompareAndSwapAtomic(Opcode opcode, uint encodedInstruction)
		{
			ushort UpperBits = GetUpperBits(encodedInstruction);
			ushort LowerBits = GetLowerBits(encodedInstruction);
			return new Instruction(
				opcode,
				new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
				null,
				new RegisterID(UpperBits & Arith1RegOP),
				new RegisterID(LowerBits & RegOP2),
				new RegisterID(LowerBits & RegOP3),
				new RegisterValue(0),
				new RegisterValue(0),
				new RegisterValue(0),
				null,
				OpcodeHelper.GetFunctionalUnitType(opcode)
				);
		}

		private Instruction CreateSwapAtomic(Opcode opcode, uint encodedInstruction)
		{
			ushort UpperBits = GetUpperBits(encodedInstruction);
			ushort LowerBits = GetLowerBits(encodedInstruction);
			return new Instruction(
				opcode,
				new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
				null,
				new RegisterID(UpperBits & Arith1RegOP),
				new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
				null,
				new RegisterValue(0),
				new RegisterValue(0),
				new RegisterValue(0),
				null,
				OpcodeHelper.GetFunctionalUnitType(opcode)
				);
		}

		private static ushort ExtractOpCode(uint EncodedInstruction)
        {
			//I am using ASL due to c#, bitwise & produces int. Cast applies after all operations, so ASL should act as LSL
			return (ushort)(((uint)(EncodedInstruction >> 16) & OpCodeMask) >> 9);

        }

		private static bool ImmediateBitSet(ushort EncodedInstruction)
        {
			return Convert.ToBoolean(EncodedInstruction & ImmediateIdentifierMask);
        }

		private static ushort GetLowerBits(uint encodedInstruction)
		{
			return (ushort) (encodedInstruction & 0x0000FFFF);
		}

		private static ushort GetUpperBits(uint encodedInstruction)
        {
			return (ushort)(((uint)(encodedInstruction & 0xFFFF0000)) >> 16);
		}
		

	}

}

