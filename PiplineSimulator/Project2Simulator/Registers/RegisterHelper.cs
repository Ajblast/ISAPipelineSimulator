using System;

namespace Project2Simulator.Registers
{
    public enum RegisterName : byte
    {
        RA = 0,
        RB,
        RC,
        RD,
        RE,
        RF,
        RG,
        RH,
        RI,
        RJ,
        RK,
        RL,
        RM,
        PC,
        SP,
        FLAG
    }
    public static class RegisterHelper
    {
        public static RegisterName StringToName(string name)
        {
            return (RegisterName)Enum.Parse(typeof(RegisterName), name, true);
        }

    }
}
