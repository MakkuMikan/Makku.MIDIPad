using Melanchall.DryWetMidi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDI
{
    public interface IMIDIService
    {
        void SendEvent(MidiEvent midiEvent);
    }
}
