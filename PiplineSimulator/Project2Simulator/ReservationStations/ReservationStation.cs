/* Author: Austin Kincer */

using Project2Simulator.FunctionalUnits;
using Project2Simulator.Registers;
using Project2Simulator.Instructions;
using Project2Simulator.ReorderBuffers;
using PiplineSimulator;

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

			FunctionalUnit = FunctionalFactory.CreateUnit(bus, type, core);
			StationID = id;
			Values = new StationValues();
			Values.Dest = null;
			Values.Op1 = new RegisterValue();
			Values.Op2 = new RegisterValue();
			Values.Op3 = new RegisterValue();
			Values.Op1Present = false;
			Values.Op2Present = false;
			Values.Op3Present = false;

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
					Values.Op1Present = true;
					Values.Op1Src = null;
                }
				else
                {
					Values.Op1Present = false;
					Values.Op1Src = new ReorderBufferID(op1ID);
                }
			}
			else
            {
				Values.Op1 = instruction.Op1;
				Values.Op1Present = true;
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
					Values.Op2Present = true;
					Values.Op2Src = null;
				}
				else
				{
					Values.Op2Present = false;
					Values.Op2Src = new ReorderBufferID(op2ID);
				}
			}
			else
			{
				Values.Op2 = instruction.Op2;
				Values.Op2Present = true;
				Values.Op2Src = null;
			}

			// Check if op3 uses the register file
			if (instruction.Op3Reg != null)
			{
				// Check the register file busy flag
				ReorderBufferID op3ID = registerFile[instruction.Op3Reg.ID].ReorderId;
				if (op3ID == null)
				{
					Values.Op3 = new RegisterValue(registerFile[instruction.Op3Reg.ID].Value);
					Values.Op3Present = true;
					Values.Op3Src = null;
				}
				else
				{
					Values.Op3Present = false;
					Values.Op3Src = new ReorderBufferID(op3ID);
				}
			}
			else
			{
				Values.Op3 = instruction.Op3;
				Values.Op3Present = true;
				Values.Op3Src = null;
			}

			// Get the address from the instruction
			Values.Addr = instruction.Address;

			// If the destination is valid, set it as used
			if (instruction.Destination != null)
			{
				Values.Dest = new ReorderBufferID(id);
				registerFile[instruction.Destination.ID].ReorderId = new ReorderBufferID(id);
			}
			
			// If the destination 2 is valid, set it as used
			if (instruction.Destination2 != null)
			{
				registerFile[instruction.Destination2.ID].ReorderId = new ReorderBufferID(id);
			}

			Busy = true;
        }

		public void CheckDataBus()
        {
			if (Busy == false)
				return;

			// Can't steal data off the buss if it isn't valid
			if (bus.Valid == false)
				return;

			if (Values.Op1Present == false && bus.ReorderID.Equals(Values.Op1Src))
			{
				Values.Op1 = bus.Value;
				Values.Op1Src = null;
			}
			if (Values.Op2Present == false && bus.ReorderID.Equals(Values.Op2Src))
			{
				Values.Op2 = bus.Value;
				Values.Op2Src = null;
			}
			if (Values.Op3Present == false && bus.ReorderID.Equals(Values.Op3Src))
			{
				Values.Op3 = bus.Value;
				Values.Op3Src = null;
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
				if (Values.Op1Present == false || Values.Op2Present == false || Values.Op3Present == false)
					return;

				if (FunctionalUnit.Executing == false)
					FunctionalUnit.StartExecution(Values.Opcode, Values.Op1, Values.Op2, Values.Op3, Values.Addr);

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
			//dataBusControlUnit.Flush();

			FunctionalUnit.Commit(Values.Dest);
			Values = new StationValues();
			Values.Op1 = new RegisterValue();
			Values.Op2 = new RegisterValue();
			Values.Op3 = new RegisterValue();
			Values.Op1Present = false;
			Values.Op2Present = false;
			Values.Op3Present = false;

			Busy = false;
		}
		public void Flush()
		{
			FunctionalUnit.Flush();

			Values = new StationValues();
			Values.Op1 = new RegisterValue();
			Values.Op2 = new RegisterValue();
			Values.Op3 = new RegisterValue();
			Values.Op1Present = false;
			Values.Op2Present = false;
			Values.Op3Present = false;

			Busy = false;
			AskedForCommit = false;
		}

        public override string ToString()
        {
			return string.Format(StringFormatService.GetReservationStationFormat(), 
				Busy.ToString(), 
				Values.Opcode.ToString(),
				(Values.Dest == null) ? "X":Values.Dest.BufferID.ToString(),
				Values.Op1.Value,
				Values.Op2.Value,
				Values.Op3.Value,
				(Values.Op1Src == null) ? "X":Values.Op1Src.BufferID.ToString(),
				(Values.Op2Src == null) ? "X":Values.Op2Src.BufferID.ToString(),
				(Values.Op3Src == null) ? "X":Values.Op3Src.BufferID.ToString(),
				(Values.Addr == null) ? "X":Values.Addr.Value.ToString()
				);
        }
    }

}

