using Melanchall.DryWetMidi.Common;

namespace Makku.MIDI.Pad
{
    public class PadPressedEventArgs<TIdent>(TIdent identifier, SevenBitNumber? velocity) : EventArgs
    {
        public TIdent Identifier { get; } = identifier;

        public SevenBitNumber? Velocity { get; } = velocity;
    }
}