using Simulator;

namespace Simulator
{
	// All of the registers
	public class Registers
	{
		// The registers
		private Register[] regs = new Register[16];

		public Registers()
        {
            for (int i = 0; i < 16; i++)
            {
				regs[i] = new Register();
            }
        }

		// Get a register
		public Register this[int index]
		{
			get
			{
				return regs[index];
			}
		}

		// Get rE register
		public Register RE
		{
			get
			{
				return this[4];
			}

		}
		// Get rF register
		public Register RF
		{
			get
			{
				return this[5];
			}
		}
		// Get the program counter
		public Register PC1
        {
			get {
				return this[11];
			}
        }
		// Get the program counter
		public Register PC2
        {
			get {
				return this[12];
			}
        }
		// Get the upper order stack pointer bits
		public Register SP1
		{
			get
			{
				return this[13];
			}
		}
		// Get the lower order stack pointer bits
		public Register SP2
		{
			get
			{
				return this[14];
			}
		}
		// Get the flag register
		public Register FLAG
        {
			get
			{
				return this[15];
			}
        }


	}

}

