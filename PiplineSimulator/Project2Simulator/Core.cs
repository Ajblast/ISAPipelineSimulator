using Project2Simulator.FetchDecode;
using Project2Simulator.ReorderBuffers;
using Project2Simulator;
using Project2Simulator.Registers;
using Project2Simulator.ReservationStations;

namespace Project2Simulator
{
	public class Core
	{
		private InstructionUnit instructionUnit;

		private InstructionQueue instructionQueue;

		private ReorderBuffer reorderBuffer;

		private THECommonDataBus bus;

		private RegisterFile registerFile;

		private Stations reservationStations;

		private DataBusControlUnit dataBusControlUnit;

		private CoreID coreID;

		public void Cycle()
		{

		}

	}

}

