using Melanchall.DryWetMidi.Common;

namespace Makku.MIDIPad.Core;

public class MidiDevice
{
    public string Id { get; set; }
    
    public string[] Names { get; set; }
    
    public Pad[] Pads { get; set; }
    
    public MidiDevice(string id, string[] names, Pad[] pads)
    {
        Id = id;
        Names = names;
        Pads = pads;
    }
}

public record PadType
{
    public string Name { get; set; }
    
    public NoteOnEventFormat NoteOnFormat { get; set; }
    
    public Colour[] Colours { get; set; }
    
    public Behaviour[] Behaviours { get; set; }
}

public record Pad
{
    public string Id { get; set; }
    public PadType Type { get; set; }
}

public record NoteOnEventFormat
{
    // The *Value property is only used if the ValueFormat is Constant
    
    public ValueFormat NoteNumber { get; set; }
    public SevenBitNumber? NoteNumberValue { get; set; }

    public ValueFormat Velocity { get; set; }
    public SevenBitNumber? VelocityValue { get; set; }

    public ValueFormat Channel { get; set; }
    public FourBitNumber? ChannelValue { get; set; }
}

public enum ValueFormat
{
    Pad,
    Colour,
    Behaviour,
    Constant
}

public record Colour
{
    public string Name { get; set; }
    public short Value { get; set; }
}

public record Behaviour
{
    public string Name { get; set; }
    public short Value { get; set; }
}