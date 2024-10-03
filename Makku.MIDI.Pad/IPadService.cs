using Melanchall.DryWetMidi.Common;

namespace Makku.MIDI.Pad
{
    /// <summary>
    /// A service for controlling pads.
    /// <typeparamref name="SevenBitNumber"/> The type of the pad identifier.
    /// </summary>
    public interface IPadService : INoteHandleService
    {
        public static event EventHandler<PadPressedEventArgs<SevenBitNumber>>? PadPressedEvent;
        public static event EventHandler<PadReleasedEventArgs<SevenBitNumber>>? PadReleasedEvent;

        protected virtual void OnPadPressed(PadPressedEventArgs<SevenBitNumber> e)
        {
            PadPressedEvent?.Invoke(this, e);
        }

        protected virtual void OnPadReleased(PadReleasedEventArgs<SevenBitNumber> e)
        {
            PadReleasedEvent?.Invoke(this, e);
        }

        PadPressedEventArgs<SevenBitNumber>? GetPadFromNoteOn(NoteOnEventArgs e);

        protected virtual void NoteOnReceived(object? sender, NoteOnEventArgs e)
        {
            PadPressedEventArgs<SevenBitNumber>? pad = GetPadFromNoteOn(e);
            if (pad != null)
            {
                PadPressedEvent?.Invoke(this, pad);
            }
        }

        protected abstract PadReleasedEventArgs<SevenBitNumber>? GetPadFromNoteOff(NoteOffEventArgs e);

        protected virtual void NoteOffReceived(object? sender, NoteOffEventArgs e)
        {
            PadReleasedEventArgs<SevenBitNumber>? pad = GetPadFromNoteOff(e);
            if (pad != null)
            {
                OnPadReleased(pad);
            }
        }

        void Unsubscribe()
        {
            NoteOnEvent -= NoteOnReceived;
            NoteOffEvent -= NoteOffReceived;
        }
    }
}
