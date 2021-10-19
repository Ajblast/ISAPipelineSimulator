using Simulator;
using Simulator.Instructions;

namespace Simulator
{
	// The CPU
	public class CPU
	{
		private Fetcher fetcher;		// The instruction fetcher
		private Decoder decoder;		// The instruction decoder
		private Executor executor;		// The instruction executor
		public Registers registers;    // The registers
		public ALU alu;				// The AlU

		public Register halt= new Register();	// Private register that holds a value to know whether the computer has halted.

		// A CPU is created knowing what the memory is
		public CPU(Memory memory)
		{
			// Create the registers
			registers = new Registers();
			// Create the fetcher
			fetcher = new Fetcher(registers.PC1, registers.PC2, memory);

			alu = new ALU(registers.FLAG);

			// Create the decoder
			decoder = new Decoder(fetcher, registers, memory, alu, halt);

			// Create the executor
			executor = new Executor();

			halt.Value = 1;
		}

		// Run a clock cycle
		public void RunClockCycle()
		{
            if (halt.Value != 0)
				return;

			// Fetch from memory
			ushort encodedInstruction = fetcher.Fetch();

			// Decode the instruction
			Instruction instruction = decoder.Decode(encodedInstruction);

			// Execute the instruction
			executor.ExecuteInstruction(instruction);
		}

	}

}

