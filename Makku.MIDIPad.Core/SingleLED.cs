using Makku.MIDI.APCMiniMk2.Constants;
using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDIPad.Core
{
    public class SingleLED
    {
        public SevenBitNumber Button { get; set; }
        public bool State { get; set; }
        public SevenBitNumber Behaviour { get; set; }

        public SingleLED(SevenBitNumber button, SevenBitNumber behaviour)
        {
            Button = button;
            Behaviour = behaviour;
        }

        public SingleLED(SevenBitNumber button)
        {
            Button = button;
            Behaviour = SingleLEDButtonState.Off;
        }

        public bool Toggle()
        {
            State = !State;
            return State;
        }
    }
}
