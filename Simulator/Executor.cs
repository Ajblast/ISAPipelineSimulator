using Simulator.Instructions;

namespace Simulator
{
	// An instruction executor
	public class Executor
	{
		// Default constructor
		public Executor() { }

		// Execute an instruction
		public void ExecuteInstruction(Instruction instruction)
		{
			// Execute the instruction
			instruction.Execute();
		}

	}

}

