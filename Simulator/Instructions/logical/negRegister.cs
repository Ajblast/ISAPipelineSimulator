using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.logical
{
    public class negRegister : Instruction
    {
        private Register dest;
        private Register op1;
        private Register flag;
        private ALU alu;
        
        public negRegister(Register dest, Register op1, Register flag, ALU alu)
        {
            this.dest = dest;
            this.op1 = op1;
            this.flag = flag;
            this.alu = alu;
        }

        public override void Execute()
        {
            // Set the op values;
            alu.op1.Value = op1.Value;

            // Add the values
            alu.Neg();

            // Get the result from the alu
            dest.Value = alu.dest.Value;
        }
    }

}

