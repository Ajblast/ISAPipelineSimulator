using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.control
{
	public class jb : Instruction
	{
        Register flag;
        private Register PC1;
        private Register PC2;
        private Register rE;
        private Register rF;        

        public jb(Register flag, Register pc1, Register pc2, Register rE, Register rF)
		{
            this.flag = flag;
            this.PC1 = pc1;
            this.PC2 = pc2;
            this.rE = rE;
            this.rF = rF;
        }

        public override void Execute()
        {
            // CF = 1
            if ((flag.Value & 1) == 1)
            {
                PC1.Value = rE.Value;
                PC2.Value = rF.Value;
            }
        }

    }

}

