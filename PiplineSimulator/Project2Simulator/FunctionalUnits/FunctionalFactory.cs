/* Author: Austin Kincer */

using Project2Simulator.Memory;
using System;

namespace Project2Simulator.FunctionalUnits
{
	public static class FunctionalFactory
    {
        private static MMU mmu;
        private static MagicPerfectStupidCache cache;
        private static MemoryCycleTimes memoryCycleTimes;

        public static void Initialize(MMU mmu, MagicPerfectStupidCache cache, MemoryCycleTimes memoryCycleTimes)
        {
            FunctionalFactory.mmu = mmu;
            FunctionalFactory.cache = cache;
            FunctionalFactory.memoryCycleTimes = memoryCycleTimes;
        }

		public static FunctionalUnit CreateUnit(THECommonDataBus bus, FunctionalUnitType type, CoreID id)
        {
            switch (type)
            {
                case FunctionalUnitType.NULL:
                    return null;
                case FunctionalUnitType.MEMORY_UNIT:
                    return new MemoryUnit(mmu, cache, bus, id, memoryCycleTimes);
                case FunctionalUnitType.BRANCH_UNIT:
                    return new BranchUnit(bus, id);
                case FunctionalUnitType.FLOATING_ADDER:
                    return new FloatingAdder(bus, id);
                case FunctionalUnitType.FLOATING_MULTIPLIER:
                    return new FloatingMultiplier(bus, id);
                case FunctionalUnitType.INTEGER_ADDER:
                    return new IntegerAdder(bus, id);
                case FunctionalUnitType.INTEGER_MULTIPLIER:
                    return new IntegerMultiplier(bus, id);
                case FunctionalUnitType.MOVEMENT_UNIT:
                    return new MovementUnit(bus, id);
                default:
                case FunctionalUnitType.ILLEGAL:
                    throw new Exception("Asking to build an illegal functional unit type");
            }
        }
	}

}

