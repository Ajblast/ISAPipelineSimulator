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
		
		private Fetcher fetcher;
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
		public Decoder(Fetcher fetcher, Registers registers, Memory memory, ALU alu, Register halt)
		{
			this.fetcher = fetcher;
			this.registers = registers;
			this.memory = memory;
			this.alu = alu;
			this.halt = halt;
		}

		// Decode an instruction
		public Instruction Decode(ushort EncodedInstruction)
		{
			ushort OpCode = ExtractInstruction(EncodedInstruction);
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

        private Instruction CreateADDInstruction(ushort encodedInstruction)
        {
			ushort LowerBits = fetcher.Fetch();
			if (immediateBitSet(encodedInstruction))
				return new addImmediate(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new addRegister(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateADDCInstruction(ushort encodedInstruction)
        {
			ushort LowerBits = fetcher.Fetch();
			if (immediateBitSet(encodedInstruction))
				return new addcImmediate(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					LowerBits,
					registers.FLAG, 
					alu);
            else
				return new addcRegister(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateSUBBInstruction(ushort encodedInstruction)
        {
			ushort LowerBits = fetcher.Fetch();
			if (immediateBitSet(encodedInstruction))
				return new subbImmediate(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new subbRegister(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateANDInstruction(ushort encodedInstruction)
        {
			ushort LowerBits = fetcher.Fetch();
			if (immediateBitSet(encodedInstruction))
				return new andImmediate(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new andRegister(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateORInstruction(ushort encodedInstruction)
        {
			ushort LowerBits = fetcher.Fetch();
			if (immediateBitSet(encodedInstruction))
				return new orImmediate(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new orRegister(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateNORInstruction(ushort encodedInstruction)
        {
			ushort LowerBits = fetcher.Fetch();
			if (immediateBitSet(encodedInstruction))
				return new norImmediate(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new norRegister(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateSHLInstruction(ushort encodedInstruction)
        {
			ushort LowerBits = fetcher.Fetch();
			if (immediateBitSet(encodedInstruction))
				return new shlImmediate(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new shlRegister(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateSHRInstruction(ushort encodedInstruction)
        {
			ushort LowerBits = fetcher.Fetch();
			if (immediateBitSet(encodedInstruction))
				return new shrImmediate(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new shrRegister(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

		private Instruction CreateSHARInstruction(ushort encodedInstruction)
		{
			ushort LowerBits = fetcher.Fetch();
			if (immediateBitSet(encodedInstruction))
				return new sharImmediate(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new sharRegister(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

		private Instruction CreateRORInstruction(ushort encodedInstruction)
        {
			ushort LowerBits = fetcher.Fetch();
			if (immediateBitSet(encodedInstruction))
				return new rorImmediate(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new rorRegister(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateROLInstruction(ushort encodedInstruction)
        {
			ushort LowerBits = fetcher.Fetch();
			if (immediateBitSet(encodedInstruction))
				return new rolImmediate(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new rolRegister(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateRORCInstruction(ushort encodedInstruction)
        {
			ushort LowerBits = fetcher.Fetch();
			if (immediateBitSet(encodedInstruction))
				return new rorcImmediate(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new rorcRegister(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateROLCInstruction(ushort encodedInstruction)
        {
			ushort LowerBits = fetcher.Fetch();
			if (immediateBitSet(encodedInstruction))
				return new rolcImmediate(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new rolcRegister(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateLOADInstruction(ushort encodedInstruction)
        {
			ushort LowerBits = fetcher.Fetch();
			
			if (immediateBitSet(encodedInstruction))
				return new loadImmediate(
					memory,
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					(((uint)encodedInstruction & 0xF) << 16) | LowerBits);
			else
				return new loadRegister(
					memory,
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					registers[(LowerBits & RegOP2)]
					);
		}

        private Instruction CreateSTORInstruction(ushort encodedInstruction)
        {
			ushort LowerBits = fetcher.Fetch();

			if (immediateBitSet(encodedInstruction))
				return new storImmediate(
					memory,
					(((uint)encodedInstruction & 0xF) << 16) | LowerBits,
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)]);
			else
				return new storRegister(
					memory,
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					registers[(LowerBits & RegOP2)]
					);
		}

        private Instruction CreateMOVInstruction(ushort encodedInstruction)
        {
			if (immediateBitSet(encodedInstruction))
            {
				ushort LowerBits = fetcher.Fetch();
				return new moveImmediate(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					LowerBits);
			}				
			else
				return new moveRegister(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)]);
		}

        private Instruction CreatePUSHInstruction(ushort encodedInstruction)
        {
			if (immediateBitSet(encodedInstruction))
			{
				ushort LowerBits = fetcher.Fetch();
				return new pushImmediate(
					memory,
					LowerBits,
					registers.SP1,
					registers.SP2);
			}
			else
				return new pushRegister(
					memory,
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers.SP1,
					registers.SP2);
		}

        private Instruction CreatePOPInstruction(ushort encodedInstruction)
        {
			return new pop(
				memory,
				registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
				registers.SP1,
				registers.SP2);
		}

        private Instruction CreateCMPInstruction(ushort encodedInstruction)
        {
			return new cmp(
				registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
				registers[(int)((uint)(encodedInstruction & Arith1RegOP))],
				alu);
        }

        private Instruction CreateJZInstruction(ushort encodedInstruction)
        {
			return new jz(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
        }

        private Instruction CreateJNZInstruction(ushort encodedInstruction)
        {
			return new jnz(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
		}

        private Instruction CreateJGInstruction(ushort encodedInstruction)
        {
			return new jg(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
		}

        private Instruction CreateJGEInstruction(ushort encodedInstruction)
        {
			return new jge(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
		}

        private Instruction CreateJLInstruction(ushort encodedInstruction)
        {
			return new jl(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
		}

        private Instruction CreateJLEInstruction(ushort encodedInstruction)
        {
			return new jle(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
		}

        private Instruction CreateJAInstruction(ushort encodedInstruction)
        {
			return new ja(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
		}

        private Instruction CreateJAEInstruction(ushort encodedInstruction)
        {
			return new jae(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
		}

        private Instruction CreateJBInstruction(ushort encodedInstruction)
        {
			return new jb(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
		}

        private Instruction CreateJBEInstruction(ushort encodedInstruction)
        {
			return new jbe(
				registers.FLAG,
				registers.PC1,
				registers.PC2,
				registers.RE,
				registers.RF);
		}

        private Instruction CreateLDAInstruction(ushort encodedInstruction)
        {
			ushort LowerBits = fetcher.Fetch();
			return new lda(
				registers.RE,
				registers.RF,
				(((uint)encodedInstruction & 0xF) << 16) | LowerBits);
		}

        private Instruction CreateNEGInstruction(ushort encodedInstruction)
        {
			ushort LowerBits = fetcher.Fetch();
			if (immediateBitSet(encodedInstruction))
				return new negImmediate(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new negRegister(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateXORInstruction(ushort encodedInstruction)
        {
			ushort LowerBits = fetcher.Fetch();
			if (immediateBitSet(encodedInstruction))
				return new xorImmediate(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new xorRegister(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}

        private Instruction CreateSUBInstruction(ushort encodedInstruction)
        {
			ushort LowerBits = fetcher.Fetch();
			if (immediateBitSet(encodedInstruction))
				return new subImmediate(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					LowerBits,
					registers.FLAG,
					alu);
			else
				return new subRegister(
					registers[(int)(((uint)(encodedInstruction & ArithDestRegMask)) >> 4)],
					registers[(encodedInstruction & Arith1RegOP)],
					registers[(LowerBits & RegOP2)],
					registers.FLAG,
					alu);
		}


        private ushort ExtractInstruction(ushort EncodedInstruction)
        {
			//I am using ASL due to c#, bitwise & produces int. Cast applies after all operations, so ASL should act as LSL
			return (ushort)((uint)(EncodedInstruction & OpCodeMask) >> 9);

        }

		private bool immediateBitSet(ushort EncodedInstruction)
        {
			return Convert.ToBoolean(EncodedInstruction & ImmediateIdentifierMask);
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

