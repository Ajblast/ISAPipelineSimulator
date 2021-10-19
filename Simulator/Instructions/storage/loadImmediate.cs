using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.storage
{
	public class loadImmediate : Instruction
	{
        private Memory memory;
        private Register dest;
        private uint imm;

		public loadImmediate(Memory memory, Register dest, uint imm)
		{
            this.memory = memory;
            this.dest = dest;
            this.imm = imm;
		}

        public override void Execute()
        {
            dest.Value = memory[(int) imm];
        }
    }

}

