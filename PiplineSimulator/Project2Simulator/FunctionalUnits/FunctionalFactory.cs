/* Author: Austin Kincer */

using Project2Simulator.FunctionalUnits;
using System;

namespace Project2Simulator.FunctionalUnits
{
	public static class FunctionalFactory
    {
        private static THECommonDataBus bus;

        public static void Initialize(THECommonDataBus bus)
        {
            FunctionalFactory.bus = bus;
        }

		public static FunctionalUnit CreateUnit(FunctionalUnitType type, CoreID id)
        {
            throw new NotImplementedException();
        }
	}

}

