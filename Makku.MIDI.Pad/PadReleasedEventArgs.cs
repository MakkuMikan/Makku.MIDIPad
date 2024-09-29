namespace Makku.MIDI.Pad
{
    public class PadReleasedEventArgs<TIdent>(TIdent identifier) : EventArgs
    {
        public TIdent Identifier { get; } = identifier;
    }
}