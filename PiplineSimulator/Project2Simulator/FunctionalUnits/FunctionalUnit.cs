using Project2Simulator.FunctionalUnits;
using Project2Simulator;

namespace Project2Simulator.FunctionalUnits
{
	public abstract class FunctionalUnit
	{
		public FunctionalUnitType Type;

		private THECommonDataBus tHECommonDataBus;

		private FunctionalUnitType functionalUnitType;

		public FunctionalUnit(CoreID core)
		{

		}

		public bool Cycle()
		{
			return false;
		}

		public void Flush()
		{

		}

		public void Commit()
		{

		}

	}

}

