using Makku.MIDI.LED;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using System.Drawing;

namespace Makku.MIDI.ExtendedLED
{
    public interface IExtendedLEDService : ILEDService
    {
        FourBitNumber DefaultLEDBehaviour { get; }

        void SetLED(ExtendedLEDState state);

        public class ExtendedLEDState(SevenBitNumber led, SevenBitNumber color, FourBitNumber behaviour) : LEDState(led, color)
        {
            public FourBitNumber Behaviour { get; set; } = behaviour;
        }
    }
}
