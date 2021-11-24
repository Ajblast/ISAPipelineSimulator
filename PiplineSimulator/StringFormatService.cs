using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiplineSimulator
{
    public static class StringFormatService
    {
        public static string GetInstructionFormat() { return "{0,-5:X}{1,-4:X2}{2,-4:X2}{3,-6:X2}{4,-9:X8}{5,-6:X2}{6,-9:X8}{7,-6:x2}{8,-9:X8}{9,-6:x5}{10}"; }

        public static string GetReorderBufferSlotFormat() { return "{0,-4:X}{1,-5:X}{2,-6}{3,-3:X2}{4,-9:X8}{5,-6}{6,-3:X2}{7,-9:X8}{8}"; }

        public static string GetReservationStationFormat() { return "{0,-6:X}{1,-6:X2}{2,-5:X1}{3,-9:X8}{4,-9:X8}{5,-9:X8}{6,-5:X1}{7,-5:X1}{8,-5:X1}{9}"; }
    }
}
