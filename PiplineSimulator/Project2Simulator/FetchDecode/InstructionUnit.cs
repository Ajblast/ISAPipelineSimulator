/* Author: Seth Bowden */
using Project2Simulator.FetchDecode;
using Project2Simulator.Instructions;
using Project2Simulator.Memory;
using Project2Simulator.Registers;
using Project2Simulator.ReorderBuffers;

namespace Project2Simulator.FetchDecode
{
	public class InstructionUnit
	{
		private InstructionQueue instructionQueue;

		private MainMemory memory;

		private RegisterFile registerFile;

		private Decoder decoder;

		private ReorderBuffer reorderBuffer;

		public InstructionUnit(InstructionQueue queue, RegisterFile regFile, MainMemory mem, ReorderBuffer reorderBuff)
		{
			instructionQueue = queue;
			registerFile = regFile;
			memory = mem;
			decoder = new Decoder(registerFile);
			reorderBuffer = reorderBuff;
		}

		public void FetchDecode()
		{
			if (reorderBuffer.IsUncommittedBranchInstruction() || instructionQueue.HasBranchInstruction() || instructionQueue.IsFull())
				return;

			uint PCValue = registerFile.PC.Value.Value;

			uint encodedInstruction = memory[(int)PCValue].Value;	// WARNING: This breaks if the PC is greater than 2^20 because memory

			Instruction instruction = decoder.Decode(encodedInstruction);
			instructionQueue.QueueInstruction(instruction);


			// PREDICTION
			// If instruction address < PC, take the branch
			// Else, PC += 4

			registerFile.PC.Value.Value += 4;
			//TODO: Austin needs to ruin this with branch prediction :)
		}

	}

}

