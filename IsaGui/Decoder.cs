using System;
using System.IO;
using System.Text;

namespace Decoder
{
	// An instruction decoder
	public class Decoder
	{
		private const ushort OpCodeMask = 0xFE00;
		private const ushort ImmediateIdentifierMask = 0x0100;

		private BinaryReader br;
		private string decodedFile;
		private string decodedStats;

		private int totalInstructions = 0;
		private int addInstructions = 0;
		private int addcInstructions = 0;
		private int subbInstructions = 0;
		private int andInstructions = 0;
		private int orInstructions = 0;
		private int norInstructions = 0;
		private int shlInstructions = 0;
		private int shrInstructions = 0;
		private int sharInstructions = 0;
		private int rorInstructions = 0;
		private int rolInstructions = 0;
		private int rorcInstructions = 0;
		private int rolcInstructions = 0;
		private int loadInstructions = 0;
		private int storInstructions = 0;
		private int movInstructions = 0;
		private int pushInstructions = 0;
		private int popInstructions = 0;
		private int cmpInstructions = 0;
		private int jumpInstructions = 0;
		private int ldaInstructions = 0;
		private int negInstructions = 0;
		private int haltInstructions = 0;
		private int subInstructions = 0;
		private int xorInstructions = 0;
		private int immediateInstructions = 0;
		private int registerInstructions = 0;
		private int nopInstructions = 0;
		private int controlInstructions = 0;

		// Create the decoder with the fetcher, registers, and memory
		public Decoder(string filePath)
		{
			br = new BinaryReader(File.Open(filePath, FileMode.Open));

			decodedFile = Decode();
			decodedStats = summaryStats();

			br.Close();
		}

		public string DecodedFile()
        {
			return decodedFile;
		}
		public string DecodedStats()
        {
			return decodedStats;
        }

		private string Decode()
        {
			StringBuilder sb = new StringBuilder();
			while (br.PeekChar() != -1)
			{
				ushort nextInstruction = (ushort)(br.ReadByte() << 8 | br.ReadByte());
				sb.AppendLine(Decode(nextInstruction));
			}

			return sb.ToString();
		}
		// Decode an instruction
		private string Decode(ushort EncodedInstruction)
		{
			ushort OpCode = ExtractInstruction(EncodedInstruction);
			string CreatedInstruction;
			switch (OpCode)
			{
				case (ushort)Operation.NOP:
					CreatedInstruction = CreateNOPInstruction(EncodedInstruction);
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
					CreatedInstruction = CreateHALTInstruction(EncodedInstruction);
					break;
				default:
					throw new Exception("Invalid Instruction OP code Dedcoded");
			}

			return CreatedInstruction;
		}

		private string CreateADDInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "add " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & 0x000F));
				registerInstructions++;
			}
			addInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateADDCInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "addc " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & 0x000F));
				registerInstructions++;
			}
			addcInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateSUBBInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "subb " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & 0x000F));
				registerInstructions++;
			}
			subbInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateANDInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "and " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & 0x000F));
				registerInstructions++;
			}
			andInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateORInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "or " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & 0x000F));
				registerInstructions++;
			}
			orInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateNORInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "nor " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & 0x000F));
				registerInstructions++;
			}
			norInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateSHLInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "shl " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & 0x000F));
				registerInstructions++;
			}
			shlInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateSHRInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "shr " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & 0x000F));
				registerInstructions++;
			}
			shrInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateSHARInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "shar " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & 0x000F));
				registerInstructions++;
			}
			sharInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateRORInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "ror " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & 0x000F));
				registerInstructions++;
			}
			rorInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateROLInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "rol " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & 0x000F));
				registerInstructions++;
			}
			rolInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateRORCInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "rorc " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & 0x000F));
				registerInstructions++;
			}
			rorcInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateROLCInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "rolc " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & 0x000F));
				registerInstructions++;
			}
			rolcInstructions++;
			totalInstructions++;
			return retValue;
		}

		private string CreateLOADInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "load " + returnRegister(destination);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + ((short)(op1 << 16 | temp));
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister(op1) + returnRegister((byte)(temp & 0x000F));
				registerInstructions++;
			}
			loadInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateSTORInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "stor " + returnRegister(destination);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + ((short)(op1 << 16 | temp));
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister(op1) + returnRegister((byte)(temp & 0x000F));
				registerInstructions++;
			}
			storInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateMOVInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "mov " + returnRegister(destination);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister(op1);
				registerInstructions++;
			}
			movInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreatePUSHInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "push ";
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister(op1);
				registerInstructions++;
			}
			pushInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreatePOPInstruction(ushort encodedInstruction)
		{
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);

			string retValue = "pop " + returnRegister(destination);
			popInstructions++;
			totalInstructions++;
			controlInstructions++;
			return retValue;
		}

		private string CreateCMPInstruction(ushort encodedInstruction)
		{
			byte op1 = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op2 = (byte)(encodedInstruction & 0x000F);

			string retValue = "cmp " + returnRegister(op1) + returnRegister(op2);
			cmpInstructions++;
			totalInstructions++;
			controlInstructions++;
			return retValue;
		}
		private string CreateJZInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			totalInstructions++;
			controlInstructions++;
			string retValue = "jz ";
			return retValue;
		}
		private string CreateJNZInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			totalInstructions++;
			controlInstructions++;
			string retValue = "jnz ";
			return retValue;
		}
		private string CreateJGInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			totalInstructions++;
			controlInstructions++;
			string retValue = "jg ";
			return retValue;
		}
		private string CreateJGEInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			controlInstructions++;
			totalInstructions++;
			string retValue = "jge ";
			return retValue;
		}
		private string CreateJLInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			totalInstructions++;
			controlInstructions++;
			string retValue = "jl ";
			return retValue;
		}
		private string CreateJLEInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			totalInstructions++;
			controlInstructions++;
			string retValue = "jle ";
			return retValue;
		}
		private string CreateJAInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			totalInstructions++;
			controlInstructions++;
			string retValue = "ja ";
			return retValue;
		}
		private string CreateJAEInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			totalInstructions++;
			controlInstructions++;
			string retValue = "jae ";
			return retValue;
		}
		private string CreateJBInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			totalInstructions++;
			controlInstructions++;
			string retValue = "jb ";
			return retValue;
		}
		private string CreateJBEInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			totalInstructions++;
			controlInstructions++;
			string retValue = "jbe ";
			return retValue;
		}

		private string CreateLDAInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte immediate20 = (byte)(encodedInstruction & 0x000F);

			string retValue = "lda ";
			retValue += "#" + ((int)(immediate20 << 16 | temp));
			ldaInstructions++;
			totalInstructions++;
			immediateInstructions++;
			return retValue;
		}
		private string CreateNEGInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "neg " + returnRegister(destination);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister(op1);
				registerInstructions++;
			}
			negInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateXORInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "xor " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & 0x000F));
				registerInstructions++;
			}
			xorInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateSUBInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[0] << 8 | bytes[1]);
			byte destination = (byte)((uint)(encodedInstruction & 0x00F0) >> 4);
			byte op1 = (byte)(encodedInstruction & 0x000F);

			string retValue = "sub " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & 0x000F));
				registerInstructions++;
			}
			subInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateNOPInstruction(ushort encodedInstruction)
		{
			string retValue = "nop ";
			nopInstructions++;
			totalInstructions++;
			controlInstructions++;
			return retValue;
		}
		private string CreateHALTInstruction(ushort encodedInstruction)
		{
			string retValue = "halt ";
			haltInstructions++;
			totalInstructions++;
			controlInstructions++;
			return retValue;
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
		private string returnRegister(byte number)
		{
			switch (number)
			{
				case 0:
					return "rA";
				case 1:
					return "rB";
				case 2:
					return "rC";
				case 3:
					return "rD";
				case 4:
					return "rE";
				case 5:
					return "rF";
				case 6:
					return "rG";
				case 7:
					return "rH";
				case 8:
					return "rI";
				case 9:
					return "rJ";
				case 10:
					return "rK";
				case 11:
					return "PC1";
				case 12:
					return "PC2";
				case 13:
					return "SP1";
				case 14:
					return "SP2";
				case 15:
					return "FLAG";
				default:
					throw new Exception("Register out of bounds.");
			}
		}

		private string summaryStats()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("\n\nSummary Statistics\n");
			sb.AppendLine("------------------\n");
			sb.AppendLine("Total instructions:\t\t" + totalInstructions + "\n");
			sb.AppendLine("Add instructions:\t\t" + addInstructions + "\n");
			sb.AppendLine("Addc Instructions:\t\t" + addcInstructions + "\n");
			sb.AppendLine("Subb Instructions:\t\t" + subbInstructions + "\n");
			sb.AppendLine("And Instructions:\t\t" + andInstructions + "\n");
			sb.AppendLine("Or Instructions:\t\t" + orInstructions + "\n");
			sb.AppendLine("Nor Instructions:\t\t" + norInstructions + "\n");
			sb.AppendLine("Shl Instructions:\t\t" + shlInstructions + "\n");
			sb.AppendLine("Shr Instructions:\t\t" + shrInstructions + "\n");
			sb.AppendLine("Shar Instructions:\t\t" + sharInstructions + "\n");
			sb.AppendLine("Ror Instructions:\t\t" + rorInstructions + "\n");
			sb.AppendLine("Rol Instructions:\t\t" + rolInstructions + "\n");
			sb.AppendLine("Rorc Instructions:\t\t" + rorcInstructions + "\n");
			sb.AppendLine("Rolc Instructions:\t\t" + rolcInstructions + "\n");
			sb.AppendLine("Load Instructions:\t\t" + loadInstructions + "\n");
			sb.AppendLine("Stor Instructions:\t\t" + storInstructions + "\n");
			sb.AppendLine("Mov Instructions:\t\t" + movInstructions + "\n");
			sb.AppendLine("Push Instructions:\t\t" + pushInstructions + "\n");
			sb.AppendLine("Pop Instructions:\t\t" + popInstructions + "\n");
			sb.AppendLine("Cmp Instructions:\t\t" + cmpInstructions + "\n");
			sb.AppendLine("Jump Instructions:\t\t" + jumpInstructions + "\n");
			sb.AppendLine("Lda Instructions:\t\t" + ldaInstructions + "\n");
			sb.AppendLine("Neg Instructions:\t\t" + negInstructions + "\n");
			sb.AppendLine("Halt Instructions:\t\t" + haltInstructions + "\n");
			sb.AppendLine("Sub Instructions:\t\t" + subInstructions + "\n");
			sb.AppendLine("Xor Instructions:\t\t" + xorInstructions + "\n\n");
			sb.AppendLine("Addressing Mode Uses\n");
			sb.AppendLine("Immediate Instructions:\t" + immediateInstructions + "\n");
			sb.AppendLine("Register Instructions:\t" + registerInstructions + "\n");
			sb.AppendLine("Nop Instructions:\t\t" + nopInstructions + "\n");
			sb.AppendLine("Control Instructions:\t" + controlInstructions + "\n");

			return sb.ToString();
		}

	}

	/// <summary>
	/// Improves readability and maintainability in case Op codes change
	/// </summary>
	enum Operation : ushort
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