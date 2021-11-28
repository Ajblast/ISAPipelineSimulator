/* Author: Austin Kincer */

namespace Project2Simulator
{
	public class CoreID
	{
        public int ID;

        public CoreID(int ID)
        {
            this.ID = ID;
        }
        public CoreID(CoreID other)
        {
            ID = other.ID; ;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(CoreID))
                return false;

            return ID == ((CoreID)obj).ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public override string ToString()
        {
            return ID.ToString();
        }

    }

}

