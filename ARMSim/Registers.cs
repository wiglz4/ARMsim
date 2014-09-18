using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace ARMSim
{
    class Registers : Memory
    {
        public Registers() : base(64) { ; }

        new public uint ReadWord(uint addr)
        {
            return BitConverter.ToUInt32(ram, (int)addr*4);
        }
    }
}
