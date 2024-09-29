using Makku.MIDI.LED;
using Melanchall.DryWetMidi.Core;
using System.Drawing;

namespace Makku.MIDI.ExtendedLED
{
    public interface IExtendedLEDService<TIdent, TColor, TBehaviour> : ILEDService<TIdent, TColor>
    {
        TBehaviour DefaultLEDBehaviour { get; }

        void SetLED(ExtendedLEDState state);

        public class ExtendedLEDState(TIdent led, TColor color, TBehaviour behaviour) : LEDState(led, color)
        {
            public TBehaviour Behaviour { get; set; } = behaviour;
        }
    }
}
