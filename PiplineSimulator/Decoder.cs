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
		public static Instruction Decode(uint EncodedInstruction)
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
					CreatedInstruction = CreateLOADInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.STOR:
					CreatedInstruction = CreateSTORInstruction(EncodedInstruction);
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
					CreatedInstruction = CreateJZInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.JNZ:
					CreatedInstruction = CreateJNZInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.JG:
					CreatedInstruction = CreateJGInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.JGE:
					CreatedInstruction = CreateJGEInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.JL:
					CreatedInstruction = CreateJLInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.JLE:
					CreatedInstruction = CreateJLEInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.JA:
					CreatedInstruction = CreateJAInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.JAE:
					CreatedInstruction = CreateJAEInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.JB:
					CreatedInstruction = CreateJBInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.JBE:
					CreatedInstruction = CreateJBEInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.LDA:
					CreatedInstruction = CreateLDAInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.NEG:
					CreatedInstruction = CreateNEGInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.XOR:
					CreatedInstruction = CreateXORInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.SUB:
					CreatedInstruction = CreateSUBInstruction(EncodedInstruction);
					break;
				case (ushort)Opcode.HALT:
					CreatedInstruction = new halt(halt);
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
					null,
					null,
					new Address((int)(((uint)UpperBits & 0xF) << 16) | LowerBits),
					OpcodeHelper.GetFunctionalUnitType(opcode)
					);
			else
				return new Instruction(
					opcode,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					new RegisterID(UpperBits & Arith1RegOP),
					new RegisterID(LowerBits & RegOP2),
					null,
					null,
					null,
					OpcodeHelper.GetFunctionalUnitType(opcode)
					);
		}


        private static Instruction CreateMOVInstruction(Opcode opcode, uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
			{
				return new Instruction(
					opcode,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					null,
					null,
					new RegisterValue(0),
					new RegisterValue(LowerBits),
					null,
					OpcodeHelper.GetFunctionalUnitType(opcode)
					);
			}
			else
				return new Instruction(
					opcode,
					new RegisterID((int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)),
					new RegisterID((UpperBits & Arith1RegOP)),
					null,
					new RegisterValue(0),
					new RegisterValue(0),
					null,
					OpcodeHelper.GetFunctionalUnitType(opcode)
					);
		}

        private static Instruction CreatePUSHInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
			{							
				return new Instruction(				
					LowerBits,			
					registers.SP1,		
					registers.SP2);		
			}							
			else						
				return new pushRegister(
					memory,				
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers.SP1,
					registers.SP2);
		}

        private Instruction CreatePOPInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			return new pop(
				memory,
				registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
				registers.SP1,
				registers.SP2);
		}

        private Instruction CreateCMPInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			return new cmp(
				registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
				registers[(int)((uint)(UpperBits & Arith1RegOP))],
				alu);
        }

        private Instruction CreateJZInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			return new jz(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
        }

        private Instruction CreateJNZInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			return new jnz(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
		}

        private Instruction CreateJGInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			return new jg(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
		}

        private Instruction CreateJGEInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			return new jge(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
		}

        private Instruction CreateJLInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			return new jl(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
		}

        private Instruction CreateJLEInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			return new jle(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
		}

        private Instruction CreateJAInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			return new ja(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
		}

        private Instruction CreateJAEInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			return new jae(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
		}

        private Instruction CreateJBInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			return new jb(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
		}

        private Instruction CreateJBEInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			return new jbe(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
		}

        private Instruction CreateLDAInstruction(uint encodedInstruction)
        {

			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			return new lda(
				registers.RE,
				registers.RF,
				(((uint)UpperBits & 0xF) << 16) | LowerBits);
		}

        private Instruction CreateNEGInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new negImmediate(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new negRegister(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateXORInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new xorImmediate(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new xorRegister(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateSUBInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new subImmediate(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new subRegister(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
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

