/* Author: Austin Kincer */

namespace Project2Simulator
{
	public class Address
	{
		public int Value;

        public Address()
        {
            Value = 0;            
        }
        public Address(Address other)
        {
            this.Value = other.Value;
        }
        public Address(int Value)
        {
            this.Value = Value;
        }
        public Address(uint Value)
        {
            this.Value = (int)Value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Address))
                return false;

            return Value == ((Address)obj).Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

    }

}

