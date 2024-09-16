using Makku.APCMini.MK2.Constants;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.APCMini.MK2.Helpers;

public class MatrixPadState
{
    public FourBitNumber Behaviour { get; set; }
    public SevenBitNumber Value { get; set; }
    public SevenBitNumber Colour { get; set; }

    public MatrixPadState(FourBitNumber behaviour, SevenBitNumber value, SevenBitNumber colour)
    {
        Behaviour = behaviour;
        Value = value;
        Colour = colour;
    }

    public static PartialMatrixPadState FromBehaviour(FourBitNumber behaviour)
    {
        return PartialMatrixPadState.FromBehaviour(behaviour);
    }

    public static PartialMatrixPadState FromValue(SevenBitNumber value)
    {
        return PartialMatrixPadState.FromValue(value);
    }

    public static PartialMatrixPadState FromColour(SevenBitNumber colour)
    {
        return PartialMatrixPadState.FromColour(colour);
    }

    public static MatrixPadState SolidOn(SevenBitNumber value, SevenBitNumber colour)
    {
        return new MatrixPadState(PadState.OneHundredPercent, value, colour);
    }

    public MatrixPadState WithBehaviour(FourBitNumber behaviour)
    {
        return new MatrixPadState(behaviour, Value, Colour);
    }

    public NoteOnEvent ToNoteOnEvent()
    {
        return new NoteOnEvent(Value, Colour)
        {
            Channel = Behaviour
        };
    }

    public static implicit operator NoteOnEvent(MatrixPadState matrixPadState)
    {
        return matrixPadState.ToNoteOnEvent();
    }
}

public class PartialMatrixPadState
{
    public FourBitNumber? Behaviour { get; set; }
    public SevenBitNumber? Value { get; set; }
    public SevenBitNumber? Colour { get; set; }

    public PartialMatrixPadState(FourBitNumber? behaviour, SevenBitNumber? value, SevenBitNumber? colour)
    {
        Behaviour = behaviour;
        Value = value;
        Colour = colour;
    }

    public PartialMatrixPadState() { }

    public static PartialMatrixPadState FromBehaviour(FourBitNumber behaviour)
    {
        return new PartialMatrixPadState(behaviour, null, null);
    }

    public static PartialMatrixPadState FromValue(SevenBitNumber value)
    {
        return new PartialMatrixPadState(null, value, null);
    }

    public static PartialMatrixPadState FromColour(SevenBitNumber colour)
    {
        return new PartialMatrixPadState(null, null, colour);
    }

    public PartialMatrixPadState WithBehaviour(FourBitNumber behaviour)
    {
        return new PartialMatrixPadState(behaviour, Value, Colour);
    }

    public PartialMatrixPadState WithValue(SevenBitNumber value)
    {
        return new PartialMatrixPadState(Behaviour, value, Colour);
    }

    public PartialMatrixPadState WithColour(SevenBitNumber colour)
    {
        return new PartialMatrixPadState(Behaviour, Value, colour);
    }

    public MatrixPadState Complete()
    {
        if (Behaviour == null || Value == null || Colour == null)
        {
            throw new InvalidOperationException("Cannot complete partial matrix pad state.");
        }

        return new MatrixPadState(Behaviour.Value, Value.Value, Colour.Value);
    }

    public static implicit operator MatrixPadState(PartialMatrixPadState partialMatrixPadState)
    {
        return partialMatrixPadState.Complete();
    }

    public static implicit operator PartialMatrixPadState(MatrixPadState matrixPadState)
    {
        return new PartialMatrixPadState(matrixPadState.Behaviour, matrixPadState.Value, matrixPadState.Colour);
    }

    public static PartialMatrixPadState operator +(PartialMatrixPadState state1, PartialMatrixPadState state2)
    {
        return new PartialMatrixPadState(
            state1.Behaviour ?? state2.Behaviour,
            state1.Value ?? state2.Value,
            state1.Colour ?? state2.Colour);
    }

    public static PartialMatrixPadState operator +(PartialMatrixPadState state1, MatrixPadState state2)
    {
        return new PartialMatrixPadState(
            state1.Behaviour ?? state2.Behaviour,
            state1.Value ?? state2.Value,
            state1.Colour ?? state2.Colour);
    }
}
