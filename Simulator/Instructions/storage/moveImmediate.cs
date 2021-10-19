using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.storage
{
	public class moveImmediate : Instruction
	{
        private Register dest;
        private ushort imm;

		public moveImmediate(Register dest, ushort imm)
		{
            this.dest = dest;
            this.imm = imm;
		}

        public override void Execute()
        {
            dest.Value = imm;
        }
    }

}

