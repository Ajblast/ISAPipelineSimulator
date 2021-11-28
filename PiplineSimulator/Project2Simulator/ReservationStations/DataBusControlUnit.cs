/* Author: Austin Kincer */

using System.Collections.Generic;

namespace Project2Simulator.ReservationStations
{
	public class DataBusControlUnit
	{
		private List<StationID> stationsAsking;
		private StationID CommitingStation;

		public DataBusControlUnit()
        {
			stationsAsking = new List<StationID>();
			CommitingStation = null;
        }
		public void Cycle()
		{
			if (stationsAsking.Count != 0)
			{
				CommitingStation = stationsAsking[0];
				stationsAsking.RemoveAt(0);
			}
			else
				CommitingStation = null;
		}

		public void Flush()
		{
			CommitingStation = null;
			stationsAsking.Clear();
		}
		public bool RequestAccess(StationID id)
        {
			if (CommitingStation == null)
				CommitingStation = new StationID(id);

			if (id.Equals(CommitingStation))
				return true;
			else
            {
				stationsAsking.Add(new StationID(id));
				return false;
            }
        }

	}

}

