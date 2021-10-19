using Simulator;

namespace Simulator
{
	// An instruction fetcher
	public class Fetcher
	{
		Register PC1;	// The program counter
		Register PC2;	// The program counter
		Memory memory;  // Memory

		// Fecther constructor
		public Fetcher(Register PC1, Register PC2, Memory memory)
		{
			this.PC1 = PC1;
			this.PC2 = PC2;
			this.memory = memory;
		}

		// Fetch an instruction from memory and increment the program counter
		public ushort Fetch()
		{
			int index = ((PC1.Value & 0x000F) << 16) | PC2.Value;

			// Retrieve the instruction from memory
			ushort instruction = memory[index];
			// Increment the program counter
			index += 2;

			PC1.Value = (ushort)((uint)(index & 0x000F0000) >> 16); 
			PC2.Value = (ushort)(index & 0x0000FFFF);

			return instruction;
		}

	}

}

