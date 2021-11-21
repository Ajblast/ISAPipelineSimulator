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

			uint index = registerFile.PC.Value.Value;

			uint instruction = memory[(int)index].Value;

			Instruction instruciton = decoder.Decode(instruction);

			instructionQueue.QueueInstruction(instruciton);

			registerFile.PC.Value.Value += 4;
			//TODO: Austin needs to ruin this with branch prediction :)
		}

	}

}

