/* Author: Austin Kincer */

using Project2Simulator.ReorderBuffers;
using Project2Simulator.Registers;

namespace Project2Simulator
{
	public class THECommonDataBus
	{
		public ReorderBufferID ReorderID;
		public RegisterValue Value;

		public RegisterValue StatusRegValue;
		public bool StatusRegUpdated;

		public bool Valid;

		public void Write(ReorderBufferID id, RegisterValue value, RegisterValue status, bool statusUpdated)
		{
			ReorderID = id;
			Value = value;
			StatusRegValue = status;
			StatusRegUpdated = statusUpdated;

			Valid = true;
		}
		public void Flush()
        {
			Valid = false;
        }

	}

}

