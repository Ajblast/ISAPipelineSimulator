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
        FG
    }
    public static class RegisterHelper
    {
        public static RegisterName StringToName(string name)
        {
            return (RegisterName)Enum.Parse(typeof(RegisterName), name, true);
        }

        public static string IDtoName(RegisterID ID)
        {
            return ((RegisterName)ID.ID).ToString();
        }

    }
}
