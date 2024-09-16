using Melanchall.DryWetMidi.Core;

namespace Makku.MIDI
{
    public class NoteOnEventArgs : EventArgs
    {
        public NoteOnEventArgs(NoteOnEvent noteOnEvent)
        {
            NoteOnEvent = noteOnEvent;
        }

        public NoteOnEvent NoteOnEvent { get; }
    }
}