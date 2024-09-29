using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDI.APCMiniMk2.Constants
{
    public static class SingleLEDButtonState
    {
        public static readonly SevenBitNumber Off = new(0);
        public static readonly SevenBitNumber On = new(1);
        public static readonly SevenBitNumber Blinking = new(2);
    }
}
