using Project2Simulator.FunctionalUnits;
using Project2Simulator.ReservationStations;
using Project2Simulator.ReorderBuffers;
using Project2Simulator.Registers;
using Project2Simulator;

namespace Project2Simulator.ReservationStations
{
	public class ReservationStation
	{
		public FunctionalUnit FunctionalUnit;

		public StationID StationID;

		public bool Busy;

		public StationValues Values;

		private bool AskedForCommit;

		private ReorderBuffer reorderBuffer;

		private StationValues stationValues;

		private StationID stationID;

		private DataBusControlUnit dataBusControlUnit;

		private FunctionalFactory functionalFactory;

		private RegisterFile registerFile;

		public ReservationStation(RegisterFile regs, THECommonDataBus bus, DataBusControlUnit control, FunctionalUnitType type, CoreID core)
		{

		}

		public void Cycle()
		{

		}

		public void Commit()
		{

		}

		public void Flush()
		{

		}

	}

}

