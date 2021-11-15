/* Author: Austin Kincer */

namespace Project2Simulator.Registers
{
	public class RegisterValue
	{
        public ushort Value { get; set; }

        public RegisterValue()
        {
            Value = 0;
        }
        public RegisterValue(ushort value)
        {
            Value = value;
        }
        public RegisterValue(RegisterValue other)
        {
            Value = other.Value;
        }
    }

}

