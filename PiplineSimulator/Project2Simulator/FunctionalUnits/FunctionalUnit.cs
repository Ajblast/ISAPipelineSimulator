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
		protected RegisterValue op2;	// Immediate values are stored here
		protected RegisterValue op3;

		private RegisterID dest1Reg;
		private RegisterID dest2Reg;

		protected RegisterValue dest1;
		protected RegisterValue dest2;
		protected bool dest2Valid;

		protected Address address;

		public int CurrentCycle;

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

			address = new Address(0);

			CurrentCycle = 0;
		}

		public virtual void StartExecution(Opcode opcode, RegisterValue op1, RegisterValue op2, RegisterValue op3, RegisterID dest1Reg, RegisterID dest2Reg, Address addr)
        {
			this.opcode = opcode;
			this.op1 = new RegisterValue(op1);
			this.op2 = new RegisterValue(op2);
			this.op3 = new RegisterValue(op3);

			this.dest1Reg = new RegisterID(dest1Reg);
			this.dest2Reg = new RegisterID(dest2Reg);

			dest1 = new RegisterValue();
			dest2 = new RegisterValue();
			dest2Valid = false;

			address = addr;

			CurrentCycle = 0;

			Executing = true;
        }

		public abstract bool Cycle();

		public virtual void Reset()
        {
			opcode = Opcode.NOP;
			op1.Value = 0;
			op2.Value = 0;
			op3.Value = 0;

			address = new Address();

			CurrentCycle = 0;

			Executing = true;
		}
		public virtual void Flush()
        {
			Reset();
        }

		public virtual void Commit(ReorderBufferID id)
        {
			bus.Write(id, dest1, dest1Reg, dest2, dest2Reg, dest2Valid);
        }

	}

}

