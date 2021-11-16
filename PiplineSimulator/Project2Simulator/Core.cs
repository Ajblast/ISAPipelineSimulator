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
		private InstructionQueue instructionQueue;

		private ReorderBuffer reorderBuffer;
		private Stations reservationStations;

		private RegisterFile registerFile;
		private THECommonDataBus bus;
		private DataBusControlUnit dataBusControlUnit;

		private CoreID coreID;

		private const int reorderBufferSize = 8;

		public Core(CoreID id, MainMemory memory, ReservationStationCounts counts)
        {
			coreID = id;

			bus = new THECommonDataBus();
			dataBusControlUnit = new DataBusControlUnit();
			registerFile = new RegisterFile();

			reorderBuffer = new ReorderBuffer(registerFile, bus, reorderBufferSize);

			//counts.MemoryUnit = 1;
			//counts.BranchUnit = 1;
			//counts.IntegerAdder = 1;
			//counts.MovementUnit = 1;
			reservationStations = new Stations(registerFile, counts, bus, dataBusControlUnit, coreID);

			instructionQueue = new InstructionQueue(reservationStations, reorderBuffer);
			instructionUnit = new InstructionUnit(instructionQueue, registerFile, memory);
        }

		public void Cycle()
		{
			bus.Flush();

			reorderBuffer.CommitHead();

			reservationStations.Cycle();
			reservationStations.CheckDataBus();
			reorderBuffer.CheckDataBus();

			instructionQueue.IssueInstruction();
			instructionUnit.FetchDecode();
		}

	}

}

