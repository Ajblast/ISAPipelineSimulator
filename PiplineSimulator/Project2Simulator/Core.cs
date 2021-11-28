/* Author: Austin Kincer */

using Project2Simulator.FetchDecode;
using Project2Simulator.ReorderBuffers;
using Project2Simulator.Registers;
using Project2Simulator.ReservationStations;
using Project2Simulator.Memory;

namespace Project2Simulator
{
	public class Core
	{
		private InstructionUnit instructionUnit;
		public InstructionQueue instructionQueue;

		public ReorderBuffer reorderBuffer;
		public Stations reservationStations;

		public RegisterFile registerFile;
		private THECommonDataBus bus;
		private DataBusControlUnit dataBusControlUnit;

		public CoreID coreID;

		private const int reorderBufferSize = 8;
		public ReservationStationCounts ResCounts;
		public Core(CoreID id, MainMemory memory, ReservationStationCounts counts)
        {
			coreID = id;
			ResCounts = counts;

			bus = new THECommonDataBus();
			dataBusControlUnit = new DataBusControlUnit();
			registerFile = new RegisterFile();

			reorderBuffer = new ReorderBuffer(registerFile, bus, reorderBufferSize);

			//counts.MemoryUnit = 1;
			//counts.BranchUnit = 1;
			//counts.IntegerAdder = 1;
			//counts.MovementUnit = 1;
			reservationStations = new Stations(registerFile, counts, bus, dataBusControlUnit, coreID);

			instructionQueue = new InstructionQueue(reservationStations, reorderBuffer, 8);
			instructionUnit = new InstructionUnit(instructionQueue, registerFile, memory, reorderBuffer);
        }

		public void Cycle()
		{
			dataBusControlUnit.Cycle();
			bus.Flush();

			reorderBuffer.CommitHead();
			reservationStations.CheckDataBus();	// Workaround reservation stations reading from the reorder buffer
			bus.Flush();						// Workaround reservation stations reading from the reorder buffer

			reservationStations.Cycle();
			reservationStations.CheckDataBus();
			reorderBuffer.CheckDataBus();

			instructionQueue.IssueInstruction();
			instructionUnit.FetchDecode();
		}

	}

}

