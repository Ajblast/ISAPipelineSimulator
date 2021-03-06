/* Author: Seth Bowden */
using Project2Simulator.ReservationStations;
using Project2Simulator.ReorderBuffers;
using Project2Simulator.Instructions;
using Project2Simulator.Registers;
using System.Collections.Generic;
using System;

namespace Project2Simulator.FetchDecode
{
	public class InstructionQueue
	{
		public Queue<Instruction> Instructions;

		private Stations reservationStations;

		private ReorderBuffer reorderBuffer;

		private int Capacity;

		private int numBranchInstructions;

		private int numHaltInstructions;

		public InstructionQueue(Stations stations, ReorderBuffer buffer, int capacity)
		{
			Instructions = new Queue<Instruction>();
			reservationStations = stations;
			reorderBuffer = buffer;
			Capacity = capacity;
			numBranchInstructions = 0;
			numHaltInstructions = 0;
		}

		//Precondition: Instructions.Count >= Capacity
		public void QueueInstruction(Instruction instruction)
		{
			if (instruction.FunctionalUnitType == FunctionalUnits.FunctionalUnitType.BRANCH_UNIT)
				numBranchInstructions++;
			if (instruction.FunctionalUnitType == FunctionalUnits.FunctionalUnitType.HALT)
				numHaltInstructions++;
			Instructions.Enqueue(instruction);
		}

		public void IssueInstruction()
		{
			if (Instructions.Count < 1 || reorderBuffer.IsUncommittedBranchInstruction())
				return;

			Instruction newInstruction = Instructions.Peek();

            if (newInstruction.FunctionalUnitType == FunctionalUnits.FunctionalUnitType.HALT)
            {
				return;
            }

			if (newInstruction.FunctionalUnitType == FunctionalUnits.FunctionalUnitType.NULL)
			{
				Instructions.Dequeue();
				return;
			}

			if (reservationStations.HasFreeStation(newInstruction.FunctionalUnitType) == false || reorderBuffer.HasFreeSlot() == false)
				return;

			ReservationStation newStation = reservationStations.GetFreeStation(newInstruction.FunctionalUnitType);
			ReorderBufferSlot newSlot = reorderBuffer.FreeSlot();

			Instructions.Dequeue();
			if (newInstruction.FunctionalUnitType == FunctionalUnits.FunctionalUnitType.BRANCH_UNIT)
				numBranchInstructions--;

			newSlot.Ocupodo = true;
			newSlot.DestRegId = newInstruction.Destination;
			newSlot.DestRegId2 = newInstruction.Destination2;
			newSlot.ValidValue = false;
			newSlot.ValidValue2 = false;
			newSlot.Instruction = newStation;

			newStation.Issue(newInstruction, newSlot.ReorderBufferID);

		}

        internal bool HasHaltInstruction()
        {
			return numHaltInstructions > 0;
        }

        public bool IsFull()
        {
			return Instructions.Count >= Capacity;
        }

		public bool HasBranchInstruction()
        {
			return numBranchInstructions > 0;
        }

	}

}

