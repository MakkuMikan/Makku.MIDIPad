using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDIPad.Core
{
    public struct PadState
    {
        public SevenBitNumber Colour { get; set; }
        public FourBitNumber Behaviour { get; set; }
    }
}
