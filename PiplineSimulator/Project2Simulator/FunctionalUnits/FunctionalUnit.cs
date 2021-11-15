/* Author: Austin Kincer */

using Project2Simulator.ReorderBuffers;
using System;

namespace Project2Simulator.FunctionalUnits
{
	public abstract class FunctionalUnit
	{
		public FunctionalUnitType Type;

		private THECommonDataBus bus;
		private CoreID core;


		public FunctionalUnit(FunctionalUnitType type, THECommonDataBus bus, CoreID core)
		{
			Type = type;
			this.bus = bus;
			this.core = core;
		}

		public bool Cycle()
		{
			throw new NotImplementedException();
		}

		public void Flush()
		{
			throw new NotImplementedException();
		}

		public void Commit(ReorderBufferID id)
		{
			throw new NotImplementedException();
		}

	}

}

