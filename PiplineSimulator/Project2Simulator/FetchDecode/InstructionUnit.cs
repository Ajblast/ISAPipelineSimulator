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
			// We are fetching the fetch instruction after jnz for some reason. I don't know why, but we are. After that, adda was properly loaded as the program counter
			// was committed. I think what it is is that the branch instruction because not occupied, so it can commit. As such, the IsUncommittedBranchInstruction() will
			// return false because it is only checking if the branch instruction slot is unused.
			// We need to figure out a better way of keeping tracking if something in the reorder buffer is not in use. Getting tired of the reorder buffer "stuff" and
			// Just want to get things done. Thinking of just adding a boolean flag that says if it is in actual use to make things simplier. Maybe, Don't know anything
			// at this point.
			//throw new System.Exception(); 


			if (reorderBuffer.IsUncommittedBranchInstruction() || instructionQueue.HasBranchInstruction() || instructionQueue.IsFull() || instructionQueue.HasHaltInstruction())
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

