using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.storage
{
	public class pushImmediate : Instruction
	{
        private Memory memory;
        private ushort imm;
        private Register sp1;
        private Register sp2;

		public pushImmediate(Memory memory, ushort imm, Register sp1, Register sp2)
		{
            this.memory = memory;
            this.imm = imm;
            this.sp1 = sp1;
            this.sp2 = sp2;
		}

        public override void Execute()
        {
            int index = (sp1.Value & 0xF) << 16 | sp2.Value;

            memory[index] = imm;

            index -= 2;

            sp1.Value = (ushort)((uint)(index & 0x000F0000) >> 16);
            sp2.Value = (ushort)(index & 0x0000FFFF);
        }
    }

}

