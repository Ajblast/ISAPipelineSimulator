using System.ComponentModel;
namespace Project2Simulator.FunctionalUnits
{
	public enum FunctionalUnitType
	{
		NULL,
		[Description("MEM")]
		MEMORY_UNIT,
		[Description("BRANCH")]
		BRANCH_UNIT,
		[Description("FADD")]
		FLOATING_ADDER,
		[Description("FMULT")]
		FLOATING_MULTIPLIER,
		[Description("IADD")]
		INTEGER_ADDER,
		[Description("IMULT")]
		INTEGER_MULTIPLIER,
		[Description("MOV")]
		MOVEMENT_UNIT,
		ILLEGAL
	}

}

