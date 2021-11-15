/* Author: Austin Kincer */

using Project2Simulator.FunctionalUnits;
using Project2Simulator.Registers;
using Project2Simulator.Instructions;
using Project2Simulator.ReorderBuffers;

namespace Project2Simulator.ReservationStations
{
	public class ReservationStation
	{
		public FunctionalUnit FunctionalUnit;
		public StationID StationID;
		public StationValues Values;

		public bool Busy;

		private bool AskedForCommit;

		private RegisterFile registerFile;
		private DataBusControlUnit dataBusControlUnit;
		private THECommonDataBus bus;

		public ReservationStation(RegisterFile regs, THECommonDataBus bus, DataBusControlUnit control, FunctionalUnitType type, CoreID core, StationID id)
		{
			registerFile = regs;
			dataBusControlUnit = control;
			this.bus = bus;

			FunctionalUnit = FunctionalFactory.CreateUnit(type, core);
			StationID = id;
			Values = new StationValues();

			Busy = false;

			AskedForCommit = false;
		}

		public void Issue(Instruction instruction, ReorderBufferID id)
        {
			Values.Opcode = instruction.Opcode;

			// Check if op1 uses the register file
			if (instruction.Op1Reg != null)
			{
				// Check the register file busy flag
				ReorderBufferID op1ID = registerFile[instruction.Op1Reg.ID].ReorderId;
				if (op1ID == null)
                {
					Values.Op1 = new RegisterValue(registerFile[instruction.Op1Reg.ID].Value);
					Values.Op1Src = null;
                }
				else
                {
					Values.Op1 = null;
					Values.Op1Src = new ReorderBufferID(op1ID);
                }
			}
			else
            {
				Values.Op1 = instruction.Op1;
				Values.Op1Src = null;
            }

			// Check if op2 uses the register file
			if (instruction.Op2Reg != null)
			{
				// Check the register file busy flag
				ReorderBufferID op2ID = registerFile[instruction.Op2Reg.ID].ReorderId;
				if (op2ID == null)
                {
					Values.Op2 = new RegisterValue(registerFile[instruction.Op2Reg.ID].Value);
					Values.Op2Src = null;
                }
				else
                {
					Values.Op2 = null;
					Values.Op2Src = new ReorderBufferID(op2ID);
                }
			}
			else
            {
				Values.Op2 = instruction.Op2;
				Values.Op2Src = null;
            }

			// Get the address from the instruction
			Values.Addr = instruction.Address;

			// If the destination is valid, set it as used
			if (instruction.Destination != null)
			{
				Values.Dest = new ReorderBufferID(id);
				registerFile[instruction.Destination.ID].ReorderId = new ReorderBufferID(id);
			}

			// If the instruction touches the flags register, set it as used
			if (OpcodeHelper.TouchesFlags(instruction.Opcode))
				registerFile.FLAG.ReorderId = new ReorderBufferID(id);

			Busy = true;
        }

		public void CheckDataBus()
        {
			if (Busy == false)
				return;

			// Can't steal data off the buss if it isn't valid
			if (bus.Valid == false)
				return;

			if (Values.Op1 == null && bus.ReorderID.Equals(Values.Op1Src))
			{
				Values.Op1 = bus.Value;
				Values.Op1Src = null;
			}
			if (Values.Op2 == null && bus.ReorderID.Equals(Values.Op2Src))
			{
				Values.Op2 = bus.Value;
				Values.Op2Src = null;
			}
		}
		public void Cycle()
		{
			if (Busy == false)
				return;

			// Check if this station is currently waiting to commit or not
			if (AskedForCommit && dataBusControlUnit.RequestAccess(StationID))
				Commit();
			else
			{
				// Do nothing if the values are not present in the reservation station
				if (Values.Op1 == null || Values.Op2 == null)
					return;

				// Cycle the functional unit
				if (FunctionalUnit.Cycle())
				{
					if (dataBusControlUnit.RequestAccess(StationID))
						Commit();
					else
						AskedForCommit = true;
				}
			}
		}
		public void Commit()
		{
			AskedForCommit = false;
			dataBusControlUnit.Flush();

			FunctionalUnit.Commit(Values.Dest);

			Busy = false;
		}
		public void Flush()
		{
			FunctionalUnit.Flush();

			Values = new StationValues();

			Busy = false;
			AskedForCommit = false;
		}

	}

}

