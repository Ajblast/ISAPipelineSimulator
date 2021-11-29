/* Author: Seth Bowden */
using Project2Simulator;
using Project2Simulator.Registers;
using Project2Simulator.ReorderBuffers;

namespace Project2Simulator.ReorderBuffers
{
	public class ReorderBuffer
	{
		private RegisterFile registerFile;

		private THECommonDataBus THECommonDataBus;

		public ReorderBufferSlot[] bufferSlots;

		private int Head;
		private int Tail;
		private readonly int bufferSize;

		public ReorderBuffer(RegisterFile regs, THECommonDataBus bus, int bufferSize)
		{
			registerFile = regs;
			THECommonDataBus = bus;
			bufferSlots = new ReorderBufferSlot[bufferSize];
            for (int i = 0; i < bufferSize; i++)
				bufferSlots[i] = new ReorderBufferSlot(i);

			Head = 0;
			Tail = 0;

			bufferSlots[Head].Head = true;
			bufferSlots[Tail].Tail = true;

			this.bufferSize = bufferSize;
		}

		public void CheckDataBus()
		{
			// Can't steal data off the buss if it isn't valid
			if (THECommonDataBus.Valid == false)
				return;

			bufferSlots[THECommonDataBus.ReorderID.BufferID].Value = new RegisterValue(THECommonDataBus.Value);
			bufferSlots[THECommonDataBus.ReorderID.BufferID].ValidValue = true;
			
			if (THECommonDataBus.ValidValue2)
			{
				bufferSlots[THECommonDataBus.ReorderID.BufferID].Value2 = new RegisterValue(THECommonDataBus.Value2);
				bufferSlots[THECommonDataBus.ReorderID.BufferID].ValidValue2 = true;
			}

			bufferSlots[THECommonDataBus.ReorderID.BufferID].Ocupodo = false;
		}


		public void CommitHead()
		{
			// TODO: Change from valid value to the speculative execution tracker
            if (bufferSlots[Head].Ocupodo == false && bufferSlots[Head].ValidValue == true)
			{
                if (bufferSlots[Head].DestRegId != null && bufferSlots[Head].ValidValue)
                {
					registerFile[bufferSlots[Head].DestRegId.ID].Value = new RegisterValue(bufferSlots[Head].Value);
					registerFile[bufferSlots[Head].DestRegId.ID].Busy = false;
					registerFile[bufferSlots[Head].DestRegId.ID].ReorderId = null;
				}

                if (bufferSlots[Head].DestRegId2 != null && bufferSlots[Head].ValidValue2)
                {
					registerFile[bufferSlots[Head].DestRegId2.ID].Value = new RegisterValue(bufferSlots[Head].Value2);
					registerFile[bufferSlots[Head].DestRegId2.ID].Busy = false;
					registerFile[bufferSlots[Head].DestRegId2.ID].ReorderId = null;
				}

				// Do a work around that reservation stations need to be able to read from reorder buffers to pull value.
				// Just write to the common databus and assume it will be properly cleared.
				if (bufferSlots[Head].ValidValue)
					THECommonDataBus.Write(
						bufferSlots[Head].ReorderBufferID,
						bufferSlots[Head].Value,
						bufferSlots[Head].Value2,
						bufferSlots[Head].ValidValue2
						);


				bufferSlots[Head].ValidValue = false;
				bufferSlots[Head].ValidValue2 = false;

				if (bufferSlots[Head].Instruction != null)
					bufferSlots[Head].Instruction.Flush();


				// Increment the head
				bufferSlots[Head].Head = false;
				Head = (Head + 1) % bufferSize;
				bufferSlots[Head].Head = true;
			}

		}

		public ReorderBufferSlot FreeSlot()
		{
			if (HasFreeSlot() == false)
				return null;

			ReorderBufferSlot retValue = bufferSlots[Tail];

			bufferSlots[(Tail + bufferSize - 1) % bufferSize].Tail = false;
			Tail = (Tail + 1) % bufferSize;
			bufferSlots[(Tail + bufferSize - 1) % bufferSize].Tail = true;

			return retValue;
		}

		public bool HasFreeSlot()
        {
			// Tail caught up to head. Structural Hazard
			if (bufferSlots[Tail].Ocupodo == true || (bufferSlots[Tail].Ocupodo == false && bufferSlots[Tail].ValidValue == true))
				return false;

			return true;

		}

		public void Flush()
		{
			throw new System.NotImplementedException("No speculative execution capabilities yet");
		}

		public bool IsUncommittedBranchInstruction()
        {
			int slot = (Tail + bufferSize - 1) % bufferSize;
			return 
				(bufferSlots[slot].Ocupodo == true && bufferSlots[slot].Instruction.FunctionalUnit.Type == FunctionalUnits.FunctionalUnitType.BRANCH_UNIT) ||
				(bufferSlots[slot].Ocupodo == false && bufferSlots[slot].ValidValue == true && bufferSlots[slot].Instruction.FunctionalUnit.Type == FunctionalUnits.FunctionalUnitType.BRANCH_UNIT);
		}
    }

}

