/* Author: Austin Kincer */

using Project2Simulator.ReorderBuffers;
using Project2Simulator.Instructions;
using Project2Simulator.Registers;

namespace Project2Simulator.FunctionalUnits
{
	public abstract class FunctionalUnit
	{
		public FunctionalUnitType Type;

		public bool Executing;

		protected THECommonDataBus bus;
		protected CoreID core;

		protected Opcode opcode;
		protected RegisterValue op1;
		protected RegisterValue op2;
		protected RegisterValue op3;

		protected RegisterValue dest1;
		protected RegisterValue dest2;
		protected bool dest2Valid;

		public FunctionalUnit(FunctionalUnitType type, THECommonDataBus bus, CoreID core)
		{
			Type = type;
			this.bus = bus;
			this.core = core;

			op1 = new RegisterValue();
			op2 = new RegisterValue();
			op3 = new RegisterValue();

			dest1 = new RegisterValue();
			dest2 = new RegisterValue();
			dest2Valid = false;

		}

		public virtual void StartExecution(Opcode opcode, RegisterValue op1, RegisterValue op2, RegisterValue op3)
        {
			this.opcode = opcode;
			this.op1 = op1;
			this.op2 = op2;
			this.op3 = op3;

			dest1 = new RegisterValue();
			dest2 = new RegisterValue();
			dest2Valid = false;

			Executing = true;
        }

		public abstract bool Cycle();

		public virtual void Flush()
        {
			opcode = Opcode.NOP;
			op1.Value = 0;
			op2.Value = 0;
			op3.Value = 0;

			Executing = false;
        }

		public virtual void Commit(ReorderBufferID id)
        {
			bus.Write(id, dest1, dest2, dest2Valid);

			Executing = false;
        }

	}

}

