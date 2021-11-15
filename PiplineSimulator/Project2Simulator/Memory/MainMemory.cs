using Project2Simulator.Registers;

namespace Project2Simulator.Memory
{
	// The memory of the simulator
	public class MainMemory
	{
		private byte[] TheWholeNineYards;
		bool bigEndian;

		// Create a new memory
		public MainMemory(int size, bool bigEndian)
        {
			// Create the byte array
			TheWholeNineYards = new byte[size];

			this.bigEndian = bigEndian;
        }

		// Index into the whole nine yards
		public ushort this[int index]
		{
			get
			{
				// Return a short
				if (bigEndian)
					return (ushort)((TheWholeNineYards[index] << 8) | TheWholeNineYards[index + 1]);
				else
					return (ushort)((TheWholeNineYards[index + 1] << 8) | TheWholeNineYards[index]);

			}
			set
			{
				// Set the individual bytes
				if (bigEndian)
				{
					TheWholeNineYards[index] = (byte)((value & 0xFF00) >> 8);
					TheWholeNineYards[index + 1] = (byte)(value & 0x00FF);
				}
				else
                {
					TheWholeNineYards[index + 1] = (byte)((value & 0xFF00) >> 8);
					TheWholeNineYards[index] = (byte)(value & 0x00FF);
                }
            }
		}

	}

}
