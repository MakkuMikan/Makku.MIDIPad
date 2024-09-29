using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDI.APCMiniMk2.Constants
{
    public static class Behaviour
    {
        public static readonly FourBitNumber TenPercent = new(0);
        public static readonly FourBitNumber TwentyFivePercent = new(1);
        public static readonly FourBitNumber FiftyPercent = new(2);
        public static readonly FourBitNumber SixtyFivePercent = new(3);
        public static readonly FourBitNumber SeventyFivePercent = new(4);
        public static readonly FourBitNumber NinetyPercent = new(5);
        public static readonly FourBitNumber OneHundredPercent = new(6);
        public static readonly FourBitNumber PulsingSixteenths = new(7);
        public static readonly FourBitNumber PulsingEighths = new(8);
        public static readonly FourBitNumber PulsingQuarters = new(9);
        public static readonly FourBitNumber PulsingHalfs = new(10);
        public static readonly FourBitNumber BlinkingTwentyFourths = new(11);
        public static readonly FourBitNumber BlinkingSixteenths = new(12);
        public static readonly FourBitNumber BlinkingEighths = new(13);
        public static readonly FourBitNumber BlinkingQuarters = new(14);
        public static readonly FourBitNumber BlinkingHalfs = new(15);
    }
}
