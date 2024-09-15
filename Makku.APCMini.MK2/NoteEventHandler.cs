using Melanchall.DryWetMidi.Core;

namespace Makku.APCMini.MK2
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