using Project2Simulator.ReservationStations;
using System.Collections.Generic;

namespace Project2Simulator.ReservationStations
{
	public class DataBusControlUnit
	{
		private List<StationID> stationsAsking;

		public StationID CommitingStation;

		private Stations reservationStations;

		private StationID stationID;

		public void Flush()
		{

		}

	}

}

