namespace Project2Simulator.ReservationStations
{
	public struct ReservationStationCounts
	{
		public int MemoryUnit;
		public int BranchUnit;
		public int IntegerAdder;
		public int IntegerMultiplier;
		public int FloatingAdder;
		public int FloatingMultiplier;
		public int MovementUnit;

		public ReservationStationCounts(int memory, int branch, int intAdder, int intMultiplier, int floatAdder, int floatMultiplier, int movement)
        {
			MemoryUnit = memory;
			BranchUnit = branch;
			IntegerAdder = intAdder;
			IntegerMultiplier = intMultiplier;
			FloatingAdder = floatAdder;
			FloatingMultiplier = floatMultiplier;
			MovementUnit = movement;
        }
		public int Total()
        {
			return MemoryUnit + BranchUnit + IntegerAdder + IntegerMultiplier + FloatingAdder + FloatingMultiplier + MovementUnit;
        }

	}

}

