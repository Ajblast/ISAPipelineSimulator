/* Author: Seth Bowden */
using Project2Simulator.FetchDecode;
using Project2Simulator.Memory;
using Project2Simulator.Registers;

namespace Project2Simulator.FetchDecode
{
	public class InstructionUnit
	{
		private InstructionQueue instructionQueue;

		private MainMemory memory;

		private RegisterFile registerFile;

		public InstructionUnit(InstructionQueue queue, RegisterFile regFile, MainMemory mem)
		{
			instructionQueue = queue;
			registerFile = regFile;
			memory = mem;
		}

		public void FetchDecode()
		{
			int index = ((registerFile.PC1.Value.Value & 0x000F) << 16) | registerFile.PC2.Value.Value;

			ushort instruction = memory[index];

			
		}

	}

}

