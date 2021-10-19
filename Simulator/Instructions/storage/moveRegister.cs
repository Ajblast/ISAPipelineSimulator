using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.storage
{
	public class moveRegister : Instruction
	{
        private Register dest;
        private Register op1;

		public moveRegister(Register dest, Register op1)
		{
            this.dest = dest;
            this.op1 = op1;
		}

        public override void Execute()
        {
            dest.Value = op1.Value;
        }
    }

}

