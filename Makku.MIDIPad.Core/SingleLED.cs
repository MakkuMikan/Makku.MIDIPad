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
        public SingleLEDScheme Scheme { get; set; }

        public virtual void WhenOn() { }
        public virtual void WhenOff() { }
        public virtual bool Load() => State;

        public SingleLED(SevenBitNumber button, SingleLEDScheme scheme)
        {
            Button = button;
            Scheme = scheme;
        }

        public SingleLED(SevenBitNumber button)
        {
            Button = button;
            Scheme = SingleLEDScheme.Default;
        }

        public bool Toggle()
        {
            State = !State;
            return State;
        }
    }
}
