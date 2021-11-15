using Project2Simulator.ReservationStations;
using Project2Simulator.ReorderBuffers;
using Project2Simulator.Instructions;
using System.Collections.Generic;

namespace Project2Simulator.FetchDecode
{
	public class InstructionQueue
	{
		public Queue<Instruction> Instructions;

		private Stations reservationStations;

		private ReorderBuffer reorderBuffer;

		public InstructionQueue(Stations stations, ReorderBuffer buffer)
		{

		}

		public void QueueInstruction(Instruction instruction)
		{

		}

		public void IssueInstruction()
		{

		}

	}

}

