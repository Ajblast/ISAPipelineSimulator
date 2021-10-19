using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.logical
{
	public class lda : Instruction
	{
        private Register rE;
        private Register rF;
        private uint imm;

		public lda(Register rE, Register rF, uint imm)
		{
            this.rE = rE;
            this.rF = rF;
            this.imm = imm;
		}

        public override void Execute()
        {
            rE.Value = (ushort)((imm & 0x000F0000) >> 16);
            rF.Value = (ushort)(imm & 0x0000FFFF);
        }
    }

}

