using Melanchall.DryWetMidi.Core;

namespace Makku.MIDI
{
    public class NoteOffEventArgs : EventArgs
    {
        public NoteOffEventArgs(NoteOffEvent noteOffEvent)
        {
            NoteOffEvent = noteOffEvent;
        }

        public NoteOffEvent NoteOffEvent { get; }
    }
}