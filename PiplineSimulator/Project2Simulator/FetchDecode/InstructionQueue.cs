/* Author: Seth Bowden */
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

		private int Capacity;

		public InstructionQueue(Stations stations, ReorderBuffer buffer, int Capacity)
		{

		}

		public void QueueInstruction(Instruction instruction)
		{

		}

		public void IssueInstruction()
		{
			/*
			 * Issue instruction asks for reorder buffer slot
			 * gives instruction to selected reservatation station and also reorder buffer slot ID
			 */
		}

		public bool IsFull()
        {
			return Instructions.Count >= Capacity;
        }

	}

}

