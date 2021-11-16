/* Author: Austin Kincer */

namespace Project2Simulator.Registers
{
	public class RegisterValue
	{
        public uint Value { get; set; }
        public const byte DataSize = 32;

        public RegisterValue()
        {
            Value = 0;
        }
        public RegisterValue(uint value)
        {
            Value = value;
        }
        public RegisterValue(RegisterValue other)
        {
            Value = other.Value;
        }
    }

}

