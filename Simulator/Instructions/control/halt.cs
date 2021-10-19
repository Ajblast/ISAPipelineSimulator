using Simulator.Instructions;

namespace Simulator.Instructions.control
{
	public class halt : Instruction
	{
        private Register haltRegister;
		public halt(Register halt)
		{
            this.haltRegister = halt;
		}

        public override void Execute()
        {
            haltRegister.Value = 1;
        }
    }

}

