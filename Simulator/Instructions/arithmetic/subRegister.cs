using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.arithmetic
{
    public class subRegister : Instruction
    {
        private Register dest;
        private Register op1;
        private Register op2;
        private Register flag;
        private ALU alu;

        public subRegister(Register dest, Register op1, Register op2, Register flag, ALU alu)
        {
            this.dest = dest;
            this.op1 = op1;
            this.op2 = op2;
            this.flag = flag;
            this.alu = alu;
        }

        public override void Execute()
        {
            // The carry in is always 0
            flag.Value = (ushort)(flag.Value & ~(1 << 0) | 0);

            // Set the op values;
            alu.op1.Value = op1.Value;
            alu.op2.Value = op2.Value;

            // Sub the values
            alu.Sub();

            // Get the result from the alu
            dest.Value = alu.dest.Value;
        }
    }

}

