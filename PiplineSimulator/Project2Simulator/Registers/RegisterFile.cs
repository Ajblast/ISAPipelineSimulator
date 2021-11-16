/* Author: Austin Kincer */

namespace Project2Simulator.Registers
{
	public class RegisterFile
	{
		// The registers
		private Register[] regs = new Register[16];

		public RegisterFile()
		{
			for (int i = 0; i < 16; i++)
			{
				regs[i] = new Register();
				regs[i].ID = new RegisterID(i);
				regs[i].Value = new RegisterValue();
				regs[i].ReorderId = null;
				regs[i].Busy = false;
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

		// Get the program counter
		public Register PC
		{
			get
			{
				return this[13];
			}
		}

		// Get the upper order stack pointer bits
		public Register SP
		{
			get
			{
				return this[14];
			}
		}

		public Register RK
        {
            get
            {
				return this[10];
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

