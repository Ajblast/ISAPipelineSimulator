namespace Project2Simulator.ReservationStations
{
	public struct ReservationStationCounts
	{
		public int MemoryUnit;
		public int FloatingAdder;
		public int IntegerAdder;
		public int IntegerMultiplier;
		public int FloatingMultiplier;
		public int MovementUnit;

		public int Total()
        {
			return MemoryUnit + IntegerAdder + IntegerMultiplier + FloatingAdder + FloatingMultiplier + MovementUnit;
        }

	}

}

