using Makku.MIDI.APCMiniMk2.Constants;
using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDIPad.Core
{
    public struct PadScheme
    {
        public PadState OnState { get; set; }
        public PadState OffState { get; set; }

        public static PadScheme Default => new()
        {
            OnState = new PadState
            {
                Colour = Colour.White,
                Behaviour = Behaviour.OneHundredPercent
            },
            OffState = new PadState
            {
                Colour = Colour.Black,
                Behaviour = Behaviour.OneHundredPercent
            }
        };
    }
}
