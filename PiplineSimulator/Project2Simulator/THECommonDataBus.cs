/* Author: Austin Kincer */

using Project2Simulator.ReorderBuffers;
using Project2Simulator.Registers;

namespace Project2Simulator
{
	public class THECommonDataBus
	{
		public ReorderBufferID ReorderID;
		public RegisterValue Value;
		public RegisterID ValueRegister;
		public RegisterValue Value2;
		public RegisterID Value2Register;
		public bool ValidValue2;

		public bool Valid;

		public void Write(ReorderBufferID id, RegisterValue value, RegisterID valueID, RegisterValue value2, RegisterID value2ID, bool validValue2)
		{
			ReorderID = new ReorderBufferID(id);
			Value = new RegisterValue(value);
			ValueRegister = new RegisterID(valueID);
			Value2 = new RegisterValue(value2);
			Value2Register = new RegisterID(value2ID);
			ValidValue2 = validValue2;

			Valid = true;
		}
		public void Flush()
        {
			Valid = false;
        }

	}

}

