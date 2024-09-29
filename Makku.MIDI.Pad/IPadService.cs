namespace Makku.MIDI.Pad
{
    /// <summary>
    /// A service for controlling pads.
    /// <typeparamref name="TIdent"/> The type of the pad identifier.
    /// </summary>
    public interface IPadService<TIdent> : INoteHandleService
    {
        public static event EventHandler<PadPressedEventArgs<TIdent>>? PadPressedEvent;
        public static event EventHandler<PadReleasedEventArgs<TIdent>>? PadReleasedEvent;

        protected virtual void OnPadPressed(PadPressedEventArgs<TIdent> e)
        {
            PadPressedEvent?.Invoke(this, e);
        }

        protected virtual void OnPadReleased(PadReleasedEventArgs<TIdent> e)
        {
            PadReleasedEvent?.Invoke(this, e);
        }

        PadPressedEventArgs<TIdent>? GetPadFromNoteOn(NoteOnEventArgs e);

        protected virtual void NoteOnReceived(object? sender, NoteOnEventArgs e)
        {
            PadPressedEventArgs<TIdent>? pad = GetPadFromNoteOn(e);
            if (pad != null)
            {
                PadPressedEvent?.Invoke(this, pad);
            }
        }

        protected abstract PadReleasedEventArgs<TIdent>? GetPadFromNoteOff(NoteOffEventArgs e);

        protected virtual void NoteOffReceived(object? sender, NoteOffEventArgs e)
        {
            PadReleasedEventArgs<TIdent>? pad = GetPadFromNoteOff(e);
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
