using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.logical
{
	public class cmp : Instruction
	{
        private Register op1;
        private Register op2;
        private ALU alu;

		public cmp(Register op1, Register op2, ALU alu)
		{
            this.op1 = op1;
            this.op2 = op2;
            this.alu = alu;
		}

        public override void Execute()
        {
            alu.op1.Value = op1.Value;
            alu.op2.Value = op2.Value;

            // Internal subtraction to compare the two values
            alu.Sub();
        }
    }

}

