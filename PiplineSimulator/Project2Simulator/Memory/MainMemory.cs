/* Author: Austin Kincer */

using Project2Simulator.Registers;

namespace Project2Simulator.Memory
{
	// The memory of the simulator
	public class MainMemory
	{
		private readonly  byte[] TheWholeNineYards;
		private readonly bool bigEndian;

		// Create a new memory
		public MainMemory(int size, bool bigEndian)
        {
			// Create the byte array
			TheWholeNineYards = new byte[size];

			this.bigEndian = bigEndian;
        }

		// Index into the whole nine yards
		public RegisterValue this[int index]
		{
			get
			{
				// Return a short
				if (bigEndian)
					return new RegisterValue((uint)(
						(TheWholeNineYards[index] << 24) | TheWholeNineYards[index + 1] << 16 | (TheWholeNineYards[index + 2] << 8) | (TheWholeNineYards[index + 3]))
						);
				else
					return new RegisterValue((uint)(
						(TheWholeNineYards[index + 3] << 24) | (TheWholeNineYards[index + 2] << 16) |(TheWholeNineYards[index + 1] << 8) | TheWholeNineYards[index]
						));

			}
			set
			{
				// Set the individual bytes
				if (bigEndian)
				{
					TheWholeNineYards[index + 0] = (byte)((value.Value & 0xFF000000) >> 24);
					TheWholeNineYards[index + 1] = (byte)((value.Value & 0x00FF0000) >> 16);
					TheWholeNineYards[index + 2] = (byte)((value.Value & 0x0000FF00) >> 8);
					TheWholeNineYards[index + 3] = (byte)((value.Value & 0x000000FF) >> 0);
				}
				else
                {
					TheWholeNineYards[index + 3] = (byte)((value.Value & 0xFF000000) >> 24);
					TheWholeNineYards[index + 2] = (byte)((value.Value & 0x00FF0000) >> 16);
					TheWholeNineYards[index + 1] = (byte)((value.Value & 0x0000FF00) >> 8);
					TheWholeNineYards[index + 0] = (byte)((value.Value & 0x000000FF) >> 0);
                }
            }
		}

	}

}

