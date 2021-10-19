using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.arithmetic
{
	public class rolcImmediate : Instruction
	{
        private Register dest;
        private Register op1;
        private ushort op2;
        private Register flag;
        private ALU alu;
        
        public rolcImmediate(Register dest, Register op1, ushort imm, Register flag, ALU alu)
		{
            this.dest = dest;
            this.op1 = op1;
            this.op2 = imm;
            this.flag = flag;
            this.alu = alu;
        }

        public override void Execute()
        {
            // Set the op values;
            alu.op1.Value = op1.Value;
            alu.op2.Value = op2;

            // Add the values
            alu.Rolc();

            // Get the result from the alu
            dest.Value = alu.dest.Value;
        }
    }

}

