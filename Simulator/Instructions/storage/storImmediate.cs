using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.storage
{
	public class storImmediate : Instruction
	{
        private Memory memory;
        private uint dest;
        private Register src;
        
        public storImmediate(Memory memory, uint dest, Register src)
		{
            this.memory = memory;
            this.dest = dest;
            this.src = src;
        }

        public override void Execute()
        {
            memory[(int) dest] = src.Value;
        }

    }

}

