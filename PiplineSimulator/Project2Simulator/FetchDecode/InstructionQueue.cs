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

		public InstructionQueue(Stations stations, ReorderBuffer buffer, int capacity)
		{
			Instructions = new Queue<Instruction>();
			reservationStations = stations;
			reorderBuffer = buffer;
			Capacity = capacity;
		}

		//Precondition: Instructions.Count >= Capacity
		public void QueueInstruction(Instruction instruction)
		{
			Instructions.Enqueue(instruction);
		}

		public void IssueInstruction()
		{
			/*
			 * Issue instruction asks for reorder buffer slot
			 * gives instruction to selected reservatation station and also reorder buffer slot ID
			 */
			if (Instructions.Count < 1)
				return;
			Instruction newInstruction = Instructions.Peek();

			ReorderBufferSlot newSlot = reorderBuffer.FreeSlot();
			ReservationStation newStation = reservationStations.GetFreeStation(newInstruction.FunctionalUnitType);
			if (newSlot == null || newStation == null)
				return;

			Instructions.Dequeue();

			newSlot.Ocupodo = true;
			newSlot.DestRegId = newInstruction.Destination;
			newSlot.DestRegId2 = newInstruction.Destination2;
			newSlot.ValidValue = false;
			newSlot.ValidValue2 = false;

			newStation.Issue(newInstruction, newSlot.ReorderBufferID);

		}

		public bool IsFull()
        {
			return Instructions.Count >= Capacity;
        }

	}

}

