using Simulator;
using Simulator.Instructions;
using Simulator.Instructions.arithmetic;
using Simulator.Instructions.control;
using Simulator.Instructions.logical;
using Simulator.Instructions.storage;
using System;

namespace Simulator
{
	// An instruction decoder
	public class Decoder
	{
		
		private Registers registers;
		private Memory memory;
		private ALU alu;
		private Register halt;
		private const ushort OpCodeMask = 0xFE00;
		private const ushort DestRegMask = 0x00F0;
		private const ushort ImmediateIdentifierMask = 0x0100;

		//Arithmetic Masks
		private const ushort ArithDestRegMask = 0x00F0;
		private const ushort Arith1RegOP = 0x000F;
		private const ushort RegOP2 = 0x000F; //used for any instruction format where 2nd op is 4 LSB of 32-bit LBSs

		// Create the decoder with the fetcher, registers, and memory
		public Decoder(Registers registers, Memory memory, ALU alu, Register halt)
		{
			this.registers = registers;
			this.memory = memory;
			this.alu = alu;
			this.halt = halt;
		}

		// Decode an instruction
		public Instruction Decode(uint EncodedInstruction)
		{
			ushort OpCode = ExtractOpCode(EncodedInstruction);
			Instruction CreatedInstruction;
			switch (OpCode)
			{
				case (ushort)Operation.NOP:
					CreatedInstruction = new Instructions.control.nop();
					break;
				case (ushort)Operation.ADD:
					CreatedInstruction = CreateADDInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.ADDC:
					CreatedInstruction = CreateADDCInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.SUBB:
					CreatedInstruction = CreateSUBBInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.AND:
					CreatedInstruction = CreateANDInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.OR:
					CreatedInstruction = CreateORInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.NOR:
					CreatedInstruction = CreateNORInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.SHL:
					CreatedInstruction = CreateSHLInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.SHR:
					CreatedInstruction = CreateSHRInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.SHAR:
					CreatedInstruction = CreateSHARInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.ROR:
					CreatedInstruction = CreateRORInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.ROL:
					CreatedInstruction = CreateROLInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.RORC:
					CreatedInstruction = CreateRORCInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.ROLC:
					CreatedInstruction = CreateROLCInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.LOAD:
					CreatedInstruction = CreateLOADInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.STOR:
					CreatedInstruction = CreateSTORInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.MOV:
					CreatedInstruction = CreateMOVInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.PUSH:
					CreatedInstruction = CreatePUSHInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.POP:
					CreatedInstruction = CreatePOPInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.CMP:
					CreatedInstruction = CreateCMPInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.JZ:
					CreatedInstruction = CreateJZInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.JNZ:
					CreatedInstruction = CreateJNZInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.JG:
					CreatedInstruction = CreateJGInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.JGE:
					CreatedInstruction = CreateJGEInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.JL:
					CreatedInstruction = CreateJLInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.JLE:
					CreatedInstruction = CreateJLEInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.JA:
					CreatedInstruction = CreateJAInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.JAE:
					CreatedInstruction = CreateJAEInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.JB:
					CreatedInstruction = CreateJBInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.JBE:
					CreatedInstruction = CreateJBEInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.LDA:
					CreatedInstruction = CreateLDAInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.NEG:
					CreatedInstruction = CreateNEGInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.XOR:
					CreatedInstruction = CreateXORInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.SUB:
					CreatedInstruction = CreateSUBInstruction(EncodedInstruction);
					break;
				case (ushort)Operation.HALT:
					CreatedInstruction = new halt(halt);
					break;
				default:
					throw new Exception("Invalid Instruction OP code Dedcoded");
			}

			return CreatedInstruction;
		}

        private Instruction CreateADDInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
 			if (immediateBitSet(UpperBits))
				return new addImmediate(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new addRegister(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateADDCInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new addcImmediate(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					LowerBits,
					registers.FLAG, 
					alu);
            else
				return new addcRegister(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateSUBBInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new subbImmediate(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new subbRegister(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateANDInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new andImmediate(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new andRegister(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateORInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new orImmediate(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new orRegister(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateNORInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new norImmediate(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new norRegister(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateSHLInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new shlImmediate(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new shlRegister(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateSHRInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new shrImmediate(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new shrRegister(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

		private Instruction CreateSHARInstruction(uint encodedInstruction)
		{
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new sharImmediate(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new sharRegister(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

		private Instruction CreateRORInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new rorImmediate(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new rorRegister(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateROLInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new rolImmediate(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new rolRegister(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateRORCInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new rorcImmediate(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new rorcRegister(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateROLCInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new rolcImmediate(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new rolcRegister(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateLOADInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new loadImmediate(
					memory,
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					(((uint)UpperBits & 0xF) << 16) | LowerBits);
			else
				return new loadRegister(
					memory,
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					registers[(LowerBits & RegOP2)]
					);
		}

        private Instruction CreateSTORInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
				return new storImmediate(
					memory,
					(((uint)UpperBits & 0xF) << 16) | LowerBits,
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)]);
			else
				return new storRegister(
					memory,
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)],
					registers[(LowerBits & RegOP2)]
					);
		}

        private Instruction CreateMOVInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
            {
				return new moveImmediate(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					LowerBits);
			}				
			else
				return new moveRegister(
					registers[(int)(((uint)(UpperBits & ArithDestRegMask)) >> 4)],
					registers[(UpperBits & Arith1RegOP)]);
		}

        private Instruction CreatePUSHInstruction(uint encodedInstruction)
        {
			ushort UpperBits = getUpperBits(encodedInstruction);
			ushort LowerBits = getLowerBits(encodedInstruction);
			if (immediateBitSet(UpperBits))
			{
				return new pushImmediate(
					memory,
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


        private ushort ExtractOpCode(uint EncodedInstruction)
        {
			//I am using ASL due to c#, bitwise & produces int. Cast applies after all operations, so ASL should act as LSL
			return (ushort)((uint)(EncodedInstruction & OpCodeMask) >> 25);

        }

		private bool immediateBitSet(ushort EncodedInstruction)
        {
			return Convert.ToBoolean(EncodedInstruction & ImmediateIdentifierMask);
        }

		private ushort getLowerBits(uint encodedInstruction)
		{
			return (ushort) (encodedInstruction & 0x0000FFFF);
		}

		private ushort getUpperBits(uint encodedInstruction)
        {
			return (ushort)(((uint)(encodedInstruction & 0xFFFF0000)) >> 16);
		}
	}

	/// <summary>
	/// Improves readability and maintainability in case Op codes change
	/// </summary>
	enum Operation :ushort
    {
		NOP = 0b0000000,
		ADD = 0b0000001,
		ADDC = 0b0000010,
		SUBB = 0b0000011,
		AND = 0b0000100,
		OR = 0b0000101,
		NOR = 0b0000110,
		SHL = 0b0000111,
		SHR = 0b0001000,
		SHAR = 0b0001001,
		ROR = 0b0001010,
		ROL = 0b0001011,
		RORC = 0b0001100,
		ROLC = 0b0001101,
		LOAD = 0b0001110,
		STOR = 0b0001111,
		MOV = 0b0010000,
		PUSH = 0b0010001,
		POP = 0b0010010,
		CMP = 0b0010011,
		JZ = 0b0010100,
		JNZ = 0b0010101,
		JG = 0b0010110,
		JGE = 0b0010111,
		JL = 0b0011000,
		JLE = 0b0011001,
		JA = 0b0011010,
		JAE = 0b0011011,
		JB = 0b0011100,
		JBE = 0b0011101,
		LDA = 0b0011110,
		NEG = 0b0011111,
		XOR = 0b0100000,
		SUB = 0b0100001,
		HALT = 0b1111111
	}


}

