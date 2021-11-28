/* Author: Austin Kincer */

using Project2Simulator.ReorderBuffers;
using Project2Simulator.Registers;

namespace Project2Simulator
{
	public class THECommonDataBus
	{
		public ReorderBufferID ReorderID;
		public RegisterValue Value;
		public RegisterValue Value2;
		public bool ValidValue2;

		public bool Valid;

		public void Write(ReorderBufferID id, RegisterValue value, RegisterValue value2, bool validValue2)
		{
			ReorderID = new ReorderBufferID(id);
			Value = new RegisterValue(value);
			Value2 = new RegisterValue(value2);
			ValidValue2 = validValue2;

			Valid = true;
		}
		public void Flush()
        {
			Valid = false;
        }

	}

}

