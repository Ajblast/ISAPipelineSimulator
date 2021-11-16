/* Author: Austin Kincer */

namespace Project2Simulator.Registers
{
	public class RegisterID
	{
		public int ID;

        public RegisterID(int ID)
        {
            this.ID = ID;
        }
        public RegisterID(RegisterID other)
        {
            ID = other.ID;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(RegisterID))
                return false;

            return ID == ((RegisterID)obj).ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

    }

}

