/* Author: Austin Kincer */

namespace Project2Simulator.ReservationStations
{
	public class StationID
	{
        public int ID;

        public StationID(int ID)
        {
            this.ID = ID;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(StationID))
                return false;

            return ID == ((StationID)obj).ID;
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

