using Makku.APCMini.MK2.Constants;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.APCMini.MK2.Helpers;

public class SingleLEDState
{
    public SevenBitNumber Value { get; set; }
    public SevenBitNumber Behaviour { get; set; }

    public SingleLEDState(SevenBitNumber value, SevenBitNumber behaviour)
    {
        Value = value;
        Behaviour = behaviour;
    }

    public static PartialSingleLEDState FromValue(SevenBitNumber value)
    {
        return PartialSingleLEDState.FromValue(value);
    }

    public static PartialSingleLEDState FromBehaviour(SevenBitNumber behaviour)
    {
        return PartialSingleLEDState.FromBehaviour(behaviour);
    }

    public static SingleLEDState SolidOn(SevenBitNumber value)
    {
        return new SingleLEDState(value, SingleLEDButtonState.On);
    }

    public static SingleLEDState SolidOff(SevenBitNumber value)
    {
        return new SingleLEDState(value, SingleLEDButtonState.Off);
    }

    public NoteOnEvent ToNoteOnEvent()
    {
        return new NoteOnEvent(Value, Behaviour)
        {
            Channel = new(0)
        };
    }

    public NoteOffEvent ToNoteOffEvent()
    {
        return new NoteOffEvent(Value, SevenBitNumber.MinValue)
        {
            Channel = new(0)
        };
    }

    public static implicit operator NoteOnEvent(SingleLEDState SingleLEDState)
    {
        return SingleLEDState.ToNoteOnEvent();
    }

    public static implicit operator NoteOffEvent(SingleLEDState SingleLEDState)
    {
        return SingleLEDState.ToNoteOffEvent();
    }

    public static NoteOffEvent CreateNoteOffEvent(SevenBitNumber note)
    {
        return new NoteOffEvent(note, SevenBitNumber.MinValue);
    }
}

public class PartialSingleLEDState
{
    public SevenBitNumber? Value { get; set; }
    public SevenBitNumber? Behaviour { get; set; }

    public PartialSingleLEDState(SevenBitNumber? value, SevenBitNumber? behaviour)
    {
        Value = value;
        Behaviour = behaviour;
    }

    public PartialSingleLEDState() { }

    public static PartialSingleLEDState FromValue(SevenBitNumber value)
    {
        return new PartialSingleLEDState(value, null);
    }

    public static PartialSingleLEDState FromBehaviour(SevenBitNumber colour)
    {
        return new PartialSingleLEDState(null, colour);
    }

    public PartialSingleLEDState WithValue(SevenBitNumber value)
    {
        return new PartialSingleLEDState(value, Behaviour);
    }

    public PartialSingleLEDState WithBehaviour(SevenBitNumber behaviour)
    {
        return new PartialSingleLEDState(Value, behaviour);
    }

    public SingleLEDState Complete()
    {
        if (Behaviour == null || Value == null || Behaviour == null)
        {
            throw new InvalidOperationException("Cannot complete partial matrix pad state.");
        }

        return new SingleLEDState(Value.Value, Behaviour.Value);
    }

    public static implicit operator SingleLEDState(PartialSingleLEDState partialSingleLEDState)
    {
        return partialSingleLEDState.Complete();
    }

    public static implicit operator PartialSingleLEDState(SingleLEDState SingleLEDState)
    {
        return new PartialSingleLEDState(SingleLEDState.Value, SingleLEDState.Behaviour);
    }

    public static PartialSingleLEDState operator +(PartialSingleLEDState state1, PartialSingleLEDState state2)
    {
        return new PartialSingleLEDState(
            state1.Value ?? state2.Value,
            state1.Behaviour ?? state2.Behaviour);
    }

    public static PartialSingleLEDState operator +(PartialSingleLEDState state1, SingleLEDState state2)
    {
        return new PartialSingleLEDState(
            state1.Value ?? state2.Value,
            state1.Behaviour ?? state2.Behaviour);
    }
}
