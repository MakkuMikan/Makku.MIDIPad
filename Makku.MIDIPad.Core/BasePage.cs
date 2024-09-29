using Makku.APCMini.MK2;
using Makku.APCMini.MK2.Constants;
using Makku.MIDI;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Makku.MIDIPad.Core;

public abstract class BasePage : IDisposable
{
    private readonly APCMiniService APCMini;
    protected readonly Action<BasePage> ChangePage;
    protected bool Disposed = false;

    public BasePage(APCMiniService apcMini, Action<BasePage> changePage)
    {
        APCMini = apcMini;
        ChangePage = changePage;
    }

    public virtual void OnLoad()
    {
        APCMini.NoteOnEvent += OnNoteOnEvent;
        APCMini.NoteOffEvent += OnNoteOffEvent;
    }

    protected virtual void OnUnload()
    {
        APCMini.NoteOnEvent -= OnNoteOnEvent;
        APCMini.NoteOffEvent -= OnNoteOffEvent;
    }

    public void Dispose()
    {
        OnUnload();
        APCMini.ResetAllPads();
        APCMini.ResetAllSLEDs();
        GC.SuppressFinalize(this);
    }

    public virtual void Update()
    {
        // Override this method to update the values
    }

    protected virtual void PadOn(SevenBitNumber value, SevenBitNumber colour)
    {
        APCMini.SetLED(PadState.OneHundredPercent, value, colour);
    }

    protected virtual void PadOff(SevenBitNumber value)
    {
        APCMini.ResetLED(value);
    }

    public virtual void SLEDOn(SevenBitNumber value)
    {
        APCMini.SetSLED(value, SingleLEDButtonState.On);
    }

    public virtual void SLEDOff(SevenBitNumber value)
    {
        APCMini.SetSLED(value, SingleLEDButtonState.Off);
    }

    public virtual void SLEDBlink(SevenBitNumber value)
    {
        APCMini.SetSLED(value, SingleLEDButtonState.Blinking);
    }

    #region Down
    protected virtual void OnA1Down() { }
    protected virtual void OnB1Down() { }
    protected virtual void OnC1Down() { }
    protected virtual void OnD1Down() { }
    protected virtual void OnE1Down() { }
    protected virtual void OnF1Down() { }
    protected virtual void OnG1Down() { }
    protected virtual void OnH1Down() { }

    protected virtual void OnA2Down() { }
    protected virtual void OnB2Down() { }
    protected virtual void OnC2Down() { }
    protected virtual void OnD2Down() { }
    protected virtual void OnE2Down() { }
    protected virtual void OnF2Down() { }
    protected virtual void OnG2Down() { }
    protected virtual void OnH2Down() { }

    protected virtual void OnA3Down() { }
    protected virtual void OnB3Down() { }
    protected virtual void OnC3Down() { }
    protected virtual void OnD3Down() { }
    protected virtual void OnE3Down() { }
    protected virtual void OnF3Down() { }
    protected virtual void OnG3Down() { }
    protected virtual void OnH3Down() { }

    protected virtual void OnA4Down() { }
    protected virtual void OnB4Down() { }
    protected virtual void OnC4Down() { }
    protected virtual void OnD4Down() { }
    protected virtual void OnE4Down() { }
    protected virtual void OnF4Down() { }
    protected virtual void OnG4Down() { }
    protected virtual void OnH4Down() { }

    protected virtual void OnA5Down() { }
    protected virtual void OnB5Down() { }
    protected virtual void OnC5Down() { }
    protected virtual void OnD5Down() { }
    protected virtual void OnE5Down() { }
    protected virtual void OnF5Down() { }
    protected virtual void OnG5Down() { }
    protected virtual void OnH5Down() { }

    protected virtual void OnA6Down() { }
    protected virtual void OnB6Down() { }
    protected virtual void OnC6Down() { }
    protected virtual void OnD6Down() { }
    protected virtual void OnE6Down() { }
    protected virtual void OnF6Down() { }
    protected virtual void OnG6Down() { }
    protected virtual void OnH6Down() { }

    protected virtual void OnA7Down() { }
    protected virtual void OnB7Down() { }
    protected virtual void OnC7Down() { }
    protected virtual void OnD7Down() { }
    protected virtual void OnE7Down() { }
    protected virtual void OnF7Down() { }
    protected virtual void OnG7Down() { }
    protected virtual void OnH7Down() { }

    protected virtual void OnA8Down() { }
    protected virtual void OnB8Down() { }
    protected virtual void OnC8Down() { }
    protected virtual void OnD8Down() { }
    protected virtual void OnE8Down() { }
    protected virtual void OnF8Down() { }
    protected virtual void OnG8Down() { }
    protected virtual void OnH8Down() { }

    protected virtual void OnVolumeDown() { }
    protected virtual void OnPanDown() { }
    protected virtual void OnSendDown() { }
    protected virtual void OnDeviceDown() { }
    protected virtual void OnUpDown() { }
    protected virtual void OnDownDown() { }
    protected virtual void OnLeftDown() { }
    protected virtual void OnRightDown() { }

    protected virtual void OnClipStopDown() { }
    protected virtual void OnSoloDown() { }
    protected virtual void OnMuteDown() { }
    protected virtual void OnRecArmDown() { }
    protected virtual void OnSelectDown() { }
    protected virtual void OnDrumDown() { }
    protected virtual void OnNoteDown() { }
    protected virtual void OnStopAllClipsDown() { }

    protected virtual void OnShiftDown() { }
    #endregion Down

    #region Up
    protected virtual void OnA1Up() { }
    protected virtual void OnB1Up() { }
    protected virtual void OnC1Up() { }
    protected virtual void OnD1Up() { }
    protected virtual void OnE1Up() { }
    protected virtual void OnF1Up() { }
    protected virtual void OnG1Up() { }
    protected virtual void OnH1Up() { }

    protected virtual void OnA2Up() { }
    protected virtual void OnB2Up() { }
    protected virtual void OnC2Up() { }
    protected virtual void OnD2Up() { }
    protected virtual void OnE2Up() { }
    protected virtual void OnF2Up() { }
    protected virtual void OnG2Up() { }
    protected virtual void OnH2Up() { }

    protected virtual void OnA3Up() { }
    protected virtual void OnB3Up() { }
    protected virtual void OnC3Up() { }
    protected virtual void OnD3Up() { }
    protected virtual void OnE3Up() { }
    protected virtual void OnF3Up() { }
    protected virtual void OnG3Up() { }
    protected virtual void OnH3Up() { }

    protected virtual void OnA4Up() { }
    protected virtual void OnB4Up() { }
    protected virtual void OnC4Up() { }
    protected virtual void OnD4Up() { }
    protected virtual void OnE4Up() { }
    protected virtual void OnF4Up() { }
    protected virtual void OnG4Up() { }
    protected virtual void OnH4Up() { }

    protected virtual void OnA5Up() { }
    protected virtual void OnB5Up() { }
    protected virtual void OnC5Up() { }
    protected virtual void OnD5Up() { }
    protected virtual void OnE5Up() { }
    protected virtual void OnF5Up() { }
    protected virtual void OnG5Up() { }
    protected virtual void OnH5Up() { }

    protected virtual void OnA6Up() { }
    protected virtual void OnB6Up() { }
    protected virtual void OnC6Up() { }
    protected virtual void OnD6Up() { }
    protected virtual void OnE6Up() { }
    protected virtual void OnF6Up() { }
    protected virtual void OnG6Up() { }
    protected virtual void OnH6Up() { }

    protected virtual void OnA7Up() { }
    protected virtual void OnB7Up() { }
    protected virtual void OnC7Up() { }
    protected virtual void OnD7Up() { }
    protected virtual void OnE7Up() { }
    protected virtual void OnF7Up() { }
    protected virtual void OnG7Up() { }
    protected virtual void OnH7Up() { }

    protected virtual void OnA8Up() { }
    protected virtual void OnB8Up() { }
    protected virtual void OnC8Up() { }
    protected virtual void OnD8Up() { }
    protected virtual void OnE8Up() { }
    protected virtual void OnF8Up() { }
    protected virtual void OnG8Up() { }
    protected virtual void OnH8Up() { }

    protected virtual void OnVolumeUp() { }
    protected virtual void OnPanUp() { }
    protected virtual void OnSendUp() { }
    protected virtual void OnDeviceUp() { }
    protected virtual void OnUpUp() { }
    protected virtual void OnDownUp() { }
    protected virtual void OnLeftUp() { }
    protected virtual void OnRightUp() { }

    protected virtual void OnClipStopUp() { }
    protected virtual void OnSoloUp() { }
    protected virtual void OnMuteUp() { }
    protected virtual void OnRecArmUp() { }
    protected virtual void OnSelectUp() { }
    protected virtual void OnDrumUp() { }
    protected virtual void OnNoteUp() { }
    protected virtual void OnStopAllClipsUp() { }

    protected virtual void OnShiftUp() { }
    #endregion Up

    protected List<SevenBitNumber> HeldNotes { get; set; } = [];

    protected bool IsDown(SevenBitNumber value) => HeldNotes.Contains(value);

    private void OnNoteOnEvent(object? _, NoteOnEventArgs e)
    {
        HeldNotes.Add(e.NoteOnEvent.NoteNumber);
        OnButtonPressed(e.NoteOnEvent.NoteNumber);
    }

    private void OnNoteOffEvent(object? _, NoteOffEventArgs e)
    {
        HeldNotes.Remove(e.NoteOffEvent.NoteNumber);
        OnButtonPressed(e.NoteOffEvent.NoteNumber, false);
    }

    protected void OnButtonPressed(SevenBitNumber button, bool down = true)
    {
        switch (button)
        {
            case ButtonRaw.A1:
                if (down) { OnA1Down(); } else { OnA1Up(); };
                break;
            case ButtonRaw.B1:
                if (down) { OnB1Down(); } else { OnB1Up(); };
                break;
            case ButtonRaw.C1:
                if (down) { OnC1Down(); } else { OnC1Up(); };
                break;
            case ButtonRaw.D1:
                if (down) { OnD1Down(); } else { OnD1Up(); };
                break;
            case ButtonRaw.E1:
                if (down) { OnE1Down(); } else { OnE1Up(); };
                break;
            case ButtonRaw.F1:
                if (down) { OnF1Down(); } else { OnF1Up(); };
                break;
            case ButtonRaw.G1:
                if (down) { OnG1Down(); } else { OnG1Up(); };
                break;
            case ButtonRaw.H1:
                if (down) { OnH1Down(); } else { OnH1Up(); };
                break;

            case ButtonRaw.A2:
                if (down) { OnA2Down(); } else { OnA2Up(); };
                break;
            case ButtonRaw.B2:
                if (down) { OnB2Down(); } else { OnB2Up(); };
                break;
            case ButtonRaw.C2:
                if (down) { OnC2Down(); } else { OnC2Up(); };
                break;
            case ButtonRaw.D2:
                if (down) { OnD2Down(); } else { OnD2Up(); };
                break;
            case ButtonRaw.E2:
                if (down) { OnE2Down(); } else { OnE2Up(); };
                break;
            case ButtonRaw.F2:
                if (down) { OnF2Down(); } else { OnF2Up(); };
                break;
            case ButtonRaw.G2:
                if (down) { OnG2Down(); } else { OnG2Up(); };
                break;
            case ButtonRaw.H2:
                if (down) { OnH2Down(); } else { OnH2Up(); };
                break;

            case ButtonRaw.A3:
                if (down) { OnA3Down(); } else { OnA3Up(); };
                break;
            case ButtonRaw.B3:
                if (down) { OnB3Down(); } else { OnB3Up(); };
                break;
            case ButtonRaw.C3:
                if (down) { OnC3Down(); } else { OnC3Up(); };
                break;
            case ButtonRaw.D3:
                if (down) { OnD3Down(); } else { OnD3Up(); };
                break;
            case ButtonRaw.E3:
                if (down) { OnE3Down(); } else { OnE3Up(); };
                break;
            case ButtonRaw.F3:
                if (down) { OnF3Down(); } else { OnF3Up(); };
                break;
            case ButtonRaw.G3:
                if (down) { OnG3Down(); } else { OnG3Up(); };
                break;
            case ButtonRaw.H3:
                if (down) { OnH3Down(); } else { OnH3Up(); };
                break;

            case ButtonRaw.A4:
                if (down) { OnA4Down(); } else { OnA4Up(); };
                break;
            case ButtonRaw.B4:
                if (down) { OnB4Down(); } else { OnB4Up(); };
                break;
            case ButtonRaw.C4:
                if (down) { OnC4Down(); } else { OnC4Up(); };
                break;
            case ButtonRaw.D4:
                if (down) { OnD4Down(); } else { OnD4Up(); };
                break;
            case ButtonRaw.E4:
                if (down) { OnE4Down(); } else { OnE4Up(); };
                break;
            case ButtonRaw.F4:
                if (down) { OnF4Down(); } else { OnF4Up(); };
                break;
            case ButtonRaw.G4:
                if (down) { OnG4Down(); } else { OnG4Up(); };
                break;
            case ButtonRaw.H4:
                if (down) { OnH4Down(); } else { OnH4Up(); };
                break;

            case ButtonRaw.A5:
                if (down) { OnA5Down(); } else { OnA5Up(); };
                break;
            case ButtonRaw.B5:
                if (down) { OnB5Down(); } else { OnB5Up(); };
                break;
            case ButtonRaw.C5:
                if (down) { OnC5Down(); } else { OnC5Up(); };
                break;
            case ButtonRaw.D5:
                if (down) { OnD5Down(); } else { OnD5Up(); };
                break;
            case ButtonRaw.E5:
                if (down) { OnE5Down(); } else { OnE5Up(); };
                break;
            case ButtonRaw.F5:
                if (down) { OnF5Down(); } else { OnF5Up(); };
                break;
            case ButtonRaw.G5:
                if (down) { OnG5Down(); } else { OnG5Up(); };
                break;
            case ButtonRaw.H5:
                if (down) { OnH5Down(); } else { OnH5Up(); };
                break;

            case ButtonRaw.A6:
                if (down) { OnA6Down(); } else { OnA6Up(); };
                break;
            case ButtonRaw.B6:
                if (down) { OnB6Down(); } else { OnB6Up(); };
                break;
            case ButtonRaw.C6:
                if (down) { OnC6Down(); } else { OnC6Up(); };
                break;
            case ButtonRaw.D6:
                if (down) { OnD6Down(); } else { OnD6Up(); };
                break;
            case ButtonRaw.E6:
                if (down) { OnE6Down(); } else { OnE6Up(); };
                break;
            case ButtonRaw.F6:
                if (down) { OnF6Down(); } else { OnF6Up(); };
                break;
            case ButtonRaw.G6:
                if (down) { OnG6Down(); } else { OnG6Up(); };
                break;
            case ButtonRaw.H6:
                if (down) { OnH6Down(); } else { OnH6Up(); };
                break;

            case ButtonRaw.A7:
                if (down) { OnA7Down(); } else { OnA7Up(); };
                break;
            case ButtonRaw.B7:
                if (down) { OnB7Down(); } else { OnB7Up(); };
                break;
            case ButtonRaw.C7:
                if (down) { OnC7Down(); } else { OnC7Up(); };
                break;
            case ButtonRaw.D7:
                if (down) { OnD7Down(); } else { OnD7Up(); };
                break;
            case ButtonRaw.E7:
                if (down) { OnE7Down(); } else { OnE7Up(); };
                break;
            case ButtonRaw.F7:
                if (down) { OnF7Down(); } else { OnF7Up(); };
                break;
            case ButtonRaw.G7:
                if (down) { OnG7Down(); } else { OnG7Up(); };
                break;
            case ButtonRaw.H7:
                if (down) { OnH7Down(); } else { OnH7Up(); };
                break;

            case ButtonRaw.A8:
                if (down) { OnA8Down(); } else { OnA8Up(); };
                break;
            case ButtonRaw.B8:
                if (down) { OnB8Down(); } else { OnB8Up(); };
                break;
            case ButtonRaw.C8:
                if (down) { OnC8Down(); } else { OnC8Up(); };
                break;
            case ButtonRaw.D8:
                if (down) { OnD8Down(); } else { OnD8Up(); };
                break;
            case ButtonRaw.E8:
                if (down) { OnE8Down(); } else { OnE8Up(); };
                break;
            case ButtonRaw.F8:
                if (down) { OnF8Down(); } else { OnF8Up(); };
                break;
            case ButtonRaw.G8:
                if (down) { OnG8Down(); } else { OnG8Up(); };
                break;
            case ButtonRaw.H8:
                if (down) { OnH8Down(); } else { OnH8Up(); };
                break;

            case SingleLEDButtonRaw.Volume:
                if (down) { OnVolumeDown(); } else { OnVolumeUp(); };
                break;
            case SingleLEDButtonRaw.Pan:
                if (down) { OnPanDown(); } else { OnPanUp(); };
                break;
            case SingleLEDButtonRaw.Send:
                if (down) { OnSendDown(); } else { OnSendUp(); };
                break;
            case SingleLEDButtonRaw.Device:
                if (down) { OnDeviceDown(); } else { OnDeviceUp(); };
                break;
            case SingleLEDButtonRaw.Up:
                if (down) { OnUpDown(); } else { OnUpUp(); };
                break;
            case SingleLEDButtonRaw.Down:
                if (down) { OnDownDown(); } else { OnDownUp(); };
                break;
            case SingleLEDButtonRaw.Left:
                if (down) { OnLeftDown(); } else { OnLeftUp(); };
                break;
            case SingleLEDButtonRaw.Right:
                if (down) { OnRightDown(); } else { OnRightUp(); };
                break;

            case SingleLEDButtonRaw.ClipStop:
                if (down) { OnClipStopDown(); } else { OnClipStopUp(); };
                break;
            case SingleLEDButtonRaw.Solo:
                if (down) { OnSoloDown(); } else { OnSoloUp(); };
                break;
            case SingleLEDButtonRaw.Mute:
                if (down) { OnMuteDown(); } else { OnMuteUp(); };
                break;
            case SingleLEDButtonRaw.RecArm:
                if (down) { OnRecArmDown(); } else { OnRecArmUp(); };
                break;
            case SingleLEDButtonRaw.Select:
                if (down) { OnSelectDown(); } else { OnSelectUp(); };
                break;
            case SingleLEDButtonRaw.Drum:
                if (down) { OnDrumDown(); } else { OnDrumUp(); };
                break;
            case SingleLEDButtonRaw.Note:
                if (down) { OnNoteDown(); } else { OnNoteUp(); };
                break;
            case SingleLEDButtonRaw.StopAllClips:
                if (down) { OnStopAllClipsDown(); } else { OnStopAllClipsUp(); };
                break;

            case SingleLEDButtonRaw.Shift:
                if (down) { OnShiftDown(); } else { OnShiftUp(); };
                break;
        }
    }
}
