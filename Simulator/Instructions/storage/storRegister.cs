using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.storage
{
	public class storRegister : Instruction
	{
        private Memory memory;
        private Register dest1;
        private Register dest2;
        private Register src;
        
        public storRegister(Memory memory, Register dest1, Register dest2, Register src)
		{
            this.memory = memory;
            this.dest1 = dest1;
            this.dest2 = dest2;
            this.src = src;
        }

        public override void Execute()
        {
            int index = (dest1.Value & 0xF) << 16 | dest2.Value;

            memory[index] = src.Value;
        }
    }

}

