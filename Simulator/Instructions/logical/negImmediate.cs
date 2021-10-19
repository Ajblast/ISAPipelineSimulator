using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.logical
{
    public class negImmediate : Instruction
    {
        private Register dest;
        private ushort op1;
        private Register flag;
        private ALU alu;
        
        public negImmediate(Register dest, ushort imm, Register flag, ALU alu)
        {
            this.dest = dest;
            this.op1 = imm;
            this.flag = flag;
            this.alu = alu;
        }

        public override void Execute()
        {
            // Set the op values;
            alu.op1.Value = op1;

            // Add the values
            alu.Neg();

            // Get the result from the alu
            dest.Value = alu.dest.Value;
        }
    }

}

