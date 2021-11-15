/* Author: Austin Kincer */

using Project2Simulator.Registers;
using Project2Simulator.FunctionalUnits;

namespace Project2Simulator.ReservationStations
{
	public class Stations
	{
		private ReservationStation[] reservationStations;

		public Stations(RegisterFile regs, ReservationStationCounts counts, THECommonDataBus bus, DataBusControlUnit control, CoreID core)
		{
			FunctionalFactory.Initialize(bus);

			reservationStations = new ReservationStation[counts.Total()];

			// Create all reservation stations
			int index = 0;
            for (int i = 0; i < counts.MemoryUnit; i++, index++)
				reservationStations[index] = new ReservationStation(regs, control, FunctionalUnitType.MEMORY_UNIT, core, new StationID(index));
            for (int i = 0; i < counts.FloatingAdder; i++, index++)
				reservationStations[index] = new ReservationStation(regs, control, FunctionalUnitType.FLOATING_ADDER, core, new StationID(index));
            for (int i = 0; i < counts.FloatingMultiplier; i++, index++)
				reservationStations[index] = new ReservationStation(regs, control, FunctionalUnitType.FLOATING_MULTIPLIER, core, new StationID(index));
            for (int i = 0; i < counts.IntegerAdder; i++, index++)
				reservationStations[index] = new ReservationStation(regs, control, FunctionalUnitType.INTEGER_ADDER, core, new StationID(index));
            for (int i = 0; i < counts.IntegerMultiplier; i++, index++)
				reservationStations[index] = new ReservationStation(regs, control, FunctionalUnitType.INTEGER_MULTIPLIER, core, new StationID(index));
            for (int i = 0; i < counts.MovementUnit; i++, index++)
				reservationStations[index] = new ReservationStation(regs, control, FunctionalUnitType.MOVEMENT_UNIT, core, new StationID(index));
		}

		public void Cycle()
		{
            foreach (var station in reservationStations)
				station.Cycle();
            foreach (var station in reservationStations)
				station.CheckDataBus();
		}

	}

}

