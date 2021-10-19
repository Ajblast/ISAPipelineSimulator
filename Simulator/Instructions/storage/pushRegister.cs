using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.storage
{
	public class pushRegister : Instruction
	{
        private Memory memory;
        private Register src;
        private Register sp1;
        private Register sp2;

		public pushRegister(Memory memory, Register src, Register sp1, Register sp2)
		{
            this.memory = memory;
            this.src = src;
            this.sp1 = sp1;
            this.sp2 = sp2;
		}

        public override void Execute()
        {
            int index = (sp1.Value & 0xF) << 16 | sp2.Value;

            memory[index] = src.Value;

            index -= 2;

            sp1.Value = (ushort)((uint)(index & 0x000F0000) >> 16);
            sp2.Value = (ushort)(index & 0x0000FFFF);
        }
    }

}

