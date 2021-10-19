using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.storage
{
	public class loadRegister : Instruction
	{
        private Memory memory;
        private Register dest;
        private Register op1;
        private Register op2;
        
        public loadRegister(Memory memory, Register dest, Register op1, Register op2)
		{
            this.memory = memory;
            this.dest = dest;
            this.op1 = op1;
            this.op2 = op2;
        }

        public override void Execute()
        {
            int index = (op1.Value & 0xF) << 16 | op2.Value;

            dest.Value = memory[index];
        }
    }

}

