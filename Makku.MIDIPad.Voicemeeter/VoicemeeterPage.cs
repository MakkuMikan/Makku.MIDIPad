﻿using Makku.MIDI.APCMiniMk2;
using Makku.MIDIPad.Core;
using Makku.MIDIPad.Voicemeeter.Helpers;
using Melanchall.DryWetMidi.Common;
using Makku.MIDI.Pad;
using Makku.MIDI.SingleLED;
using Makku.MIDI.ExtendedLED;
using Makku.MIDI.APCMiniMk2.Constants;

namespace Makku.MIDIPad.Voicemeeter;

public class VoicemeeterPage<TService> : ExtendedSingleBasePage<TService> where TService : IExtendedLEDService, ISingleLEDService, IPadService
{
    private VoicemeeterHelper VM;

    private readonly PadStates PadStates = [];
    private readonly SLEDStates SLEDStates = [];
    private readonly TService APCMini;
    private bool Disposed = false;

    private static PadScheme HeadphoneScheme = new()
    {
        OnState = new PadState
        {
            Colour = Colour.Orange,
            Behaviour = Behaviour.OneHundredPercent
        },
        OffState = new PadState
        {
            Colour = Colour.Orange,
            Behaviour = Behaviour.TenPercent
        }
    };

    private static PadScheme SpeakerScheme = new()
    {
        OnState = new PadState
        {
            Colour = Colour.Yellow,
            Behaviour = Behaviour.OneHundredPercent
        },
        OffState = new PadState
        {
            Colour = Colour.Yellow,
            Behaviour = Behaviour.TenPercent
        }
    };

    private static PadScheme MicScheme = new()
    {
        OnState = new PadState
        {
            Colour = Colour.LightBlue,
            Behaviour = Behaviour.OneHundredPercent
        },
        OffState = new PadState
        {
            Colour = Colour.LightBlue,
            Behaviour = Behaviour.TenPercent
        }
    };

    private static PadScheme AltMicScheme = new()
    {
        OnState = new PadState
        {
            Colour = Colour.BrightPurple,
            Behaviour = Behaviour.OneHundredPercent
        },
        OffState = new PadState
        {
            Colour = Colour.BrightPurple,
            Behaviour = Behaviour.TenPercent
        }
    };

    private static PadScheme RecordScheme = new()
    {
        OnState = new PadState
        {
            Colour = Colour.BrightMagenta,
            Behaviour = Behaviour.OneHundredPercent
        },
        OffState = new PadState
        {
            Colour = Colour.BrightMagenta,
            Behaviour = Behaviour.TenPercent
        }
    };

    private static PadScheme MuteScheme = new()
    {
        OnState = new PadState
        {
            Colour = Colour.BrightRed,
            Behaviour = Behaviour.OneHundredPercent
        },
        OffState = new PadState
        {
            Colour = Colour.BrightGreen,
            Behaviour = Behaviour.OneHundredPercent
        }
    };

    public VoicemeeterPage(TService apcMini, Action<IBasePage> ChangePage) : base(apcMini, ChangePage)
    {
        APCMini = apcMini;

        VM = new();

        VMButtons = new()
        {
            VMPads = [
                VM.Pad(
                    Button.A8,
                    Colour.BrightGreen,
                    onValues: new() { ["Strip[2].Gain"] = -60, ["Strip[3].Gain"] = 0 },
                    offValues: new() { ["Strip[2].Gain"] = 0, ["Strip[3].Gain"] = -60 }
                ),

                VM.Pad(Button.A6, HeadphoneScheme, "Strip[{0,2,3}].{A1-A3}"),   VM.Pad(Button.B6, HeadphoneScheme, "Strip[1].{A1-A3}"), VM.Pad(Button.C6, HeadphoneScheme, "Strip[5].{A1-A3}"), VM.Pad(Button.D6, HeadphoneScheme, "Strip[6].{A1-A3}"), VM.Pad(Button.E6, HeadphoneScheme, "Strip[7].{A1-A3}"),
                VM.Pad(Button.A5, SpeakerScheme, "Strip[{0,2,3}].{A4,A5}"),     VM.Pad(Button.B5, SpeakerScheme, "Strip[1].{A4,A5}"),   VM.Pad(Button.C5, SpeakerScheme, "Strip[5].{A4,A5}"),   VM.Pad(Button.D5, SpeakerScheme, "Strip[6].{A4,A5}"),   VM.Pad(Button.E5, SpeakerScheme, "Strip[7].{A4,A5}"),
                VM.Pad(Button.A4, MicScheme, "Strip[{0,2,3}].B1"),              VM.Pad(Button.B4, MicScheme, "Strip[1].B1"),            VM.Pad(Button.C4, MicScheme, "Strip[5].B1"),            VM.Pad(Button.D4, MicScheme, "Strip[6].B1"),            VM.Pad(Button.E4, MicScheme, "Strip[7].B1"),
                VM.Pad(Button.A3, AltMicScheme, "Strip[{0,2,3}].B2"),           VM.Pad(Button.B3, AltMicScheme, "Strip[1].B2"),         VM.Pad(Button.C3, AltMicScheme, "Strip[5].B2"),         VM.Pad(Button.D3, AltMicScheme, "Strip[6].B2"),         VM.Pad(Button.E3, AltMicScheme, "Strip[7].B2"),
                VM.Pad(Button.A2, RecordScheme, "Strip[{0,2,3}].B3"),           VM.Pad(Button.B2, RecordScheme, "Strip[1].B3"),         VM.Pad(Button.C2, RecordScheme, "Strip[5].B3"),         VM.Pad(Button.D2, RecordScheme, "Strip[6].B3"),         VM.Pad(Button.E2, RecordScheme, "Strip[7].B3"),
                VM.Pad(Button.A1, MuteScheme, "Strip[{0,2,3}].Mute"),           VM.Pad(Button.B1, MuteScheme, "Strip[1].Mute"),         VM.Pad(Button.C1, MuteScheme, "Strip[5].Mute"),         VM.Pad(Button.D1, MuteScheme, "Strip[6].Mute"),         VM.Pad(Button.E1, MuteScheme, "Strip[7].Mute"),
            ]
        };
    }

    protected readonly VMButtons VMButtons;

    protected override Buttons Buttons => VMButtons.Base;

    #region Utilities
    private bool TogglePad(SevenBitNumber pad)
    {
        var state = PadStates.Toggle(pad);

        APCMini.SetLED(state);

        return PadStates.IsOn(pad);
    }

    private void SetPad(SevenBitNumber pad, bool state)
    {
        var newState = PadStates.Set(pad, state);

        if (newState != null)
        {
            APCMini.SetLED(newState);
        }
    }

    private void SetPad(SevenBitNumber pad, bool state, FourBitNumber behaviour)
    {
        var newState = PadStates.Set(pad, state);

        if (newState != null)
        {
            APCMini.SetLED(newState.WithBehaviour(behaviour));
        }
    }

    private bool ToggleSLED(SevenBitNumber led)
    {
        var state = SLEDStates.Toggle(led);

        APCMini.SetSLED(state);

        return SLEDStates.IsOn(led);
    }

    private void SetSLED(SevenBitNumber led, bool state)
    {
        var newState = SLEDStates.Set(led, state);

        if (newState != null)
        {
            APCMini.SetSLED(newState);
        }
    }
    #endregion Utilities

    #region Load
    public override void OnLoad()
    {
        VM ??= new();

        LoadValues();

        base.OnLoad();
    }

    protected override void OnUnload()
    {
        Disposed = true;

        VM.Dispose();

        base.OnUnload();
    }

    public override void Update()
    {
        if (Disposed) return;

        LoadValues();
    }

    protected void LoadValues(bool force = false)
    {
        if (!force && !VM.ParametersIsDirty()) return;

        List<MatrixPadState> padStates = [
            PadStates.Set(Pads.InputA1Mute, VM.GetFloatParameter("Strip[0].Mute") == 1),
            PadStates.Set(Pads.InputA2Mute, VM.GetFloatParameter("Strip[1].Mute") == 1),
            PadStates.Set(Pads.InputB1Mute, VM.GetFloatParameter("Strip[5].Mute") == 1),
            PadStates.Set(Pads.InputB2Mute, VM.GetFloatParameter("Strip[6].Mute") == 1),
            PadStates.Set(Pads.InputB3Mute, VM.GetFloatParameter("Strip[7].Mute") == 1),

            PadStates.Set(Pads.InputA2Record, VM.GetFloatParameter("Strip[0].B3") == 1),
            PadStates.Set(Pads.InputA1Record, VM.GetFloatParameter("Strip[1].B3") == 1),
            PadStates.Set(Pads.InputB1Record, VM.GetFloatParameter("Strip[5].B3") == 1),
            PadStates.Set(Pads.InputB2Record, VM.GetFloatParameter("Strip[6].B3") == 1),
            PadStates.Set(Pads.InputB3Record, VM.GetFloatParameter("Strip[7].B3") == 1),

            PadStates.Set(Pads.InputA2AltMic, VM.GetFloatParameter("Strip[0].B2") == 1),
            PadStates.Set(Pads.InputA1AltMic, VM.GetFloatParameter("Strip[1].B2") == 1),
            PadStates.Set(Pads.InputB1AltMic, VM.GetFloatParameter("Strip[5].B2") == 1),
            PadStates.Set(Pads.InputB2AltMic, VM.GetFloatParameter("Strip[6].B2") == 1),
            PadStates.Set(Pads.InputB3AltMic, VM.GetFloatParameter("Strip[7].B2") == 1),

            PadStates.Set(Pads.InputA2Mic, VM.GetFloatParameter("Strip[0].B1") == 1),
            PadStates.Set(Pads.InputA1Mic, VM.GetFloatParameter("Strip[1].B1") == 1),
            PadStates.Set(Pads.InputB1Mic, VM.GetFloatParameter("Strip[5].B1") == 1),
            PadStates.Set(Pads.InputB2Mic, VM.GetFloatParameter("Strip[6].B1") == 1),
            PadStates.Set(Pads.InputB3Mic, VM.GetFloatParameter("Strip[7].B1") == 1),

            PadStates.Set(Pads.InputA2Speakers, VM.GetFloatParameter("Strip[0].A5") == 1),
            PadStates.Set(Pads.InputA1Speakers, VM.GetFloatParameter("Strip[1].A5") == 1),
            PadStates.Set(Pads.InputB1Speakers, VM.GetFloatParameter("Strip[5].A5") == 1),
            PadStates.Set(Pads.InputB2Speakers, VM.GetFloatParameter("Strip[6].A5") == 1),
            PadStates.Set(Pads.InputB3Speakers, VM.GetFloatParameter("Strip[7].A5") == 1),

            PadStates.Set(Pads.InputA2Headphones, VM.GetFloatParameter("Strip[0].A2") == 1),
            PadStates.Set(Pads.InputA1Headphones, VM.GetFloatParameter("Strip[1].A2") == 1),
            PadStates.Set(Pads.InputB1Headphones, VM.GetFloatParameter("Strip[5].A2") == 1),
            PadStates.Set(Pads.InputB2Headphones, VM.GetFloatParameter("Strip[6].A2") == 1),
            PadStates.Set(Pads.InputB3Headphones, VM.GetFloatParameter("Strip[7].A2") == 1),

            PadStates.Set(Pads.NvidiaBroadcastToggle, VM.GetFloatParameter("Strip[2].Gain") == -60),
        ];

        List<SingleLEDState> singleLEDStates = [
            SLEDStates.Set(SLEDs.OutputA1Mute, VM.GetFloatParameter("Bus[0].Mute") == 1),
            SLEDStates.Set(SLEDs.OutputA2Mute, VM.GetFloatParameter("Bus[1].Mute") == 1),
            SLEDStates.Set(SLEDs.OutputA3Mute, VM.GetFloatParameter("Bus[2].Mute") == 1),
            SLEDStates.Set(SLEDs.OutputA4Mute, VM.GetFloatParameter("Bus[3].Mute") == 1),
            SLEDStates.Set(SLEDs.OutputA5Mute, VM.GetFloatParameter("Bus[4].Mute") == 1),
        ];

        foreach (var padState in padStates.Where(x => x != null))
        {
            APCMini.SetLED(padState);
        }

        foreach (var singleLEDState in singleLEDStates.Where(x => x != null))
        {
            APCMini.SetSLED(singleLEDState);
        }
    }
    #endregion Load

    #region Navigation
    protected override void OnShiftDown()
    {
        SLEDBlink(SingleLEDButton.Right);
    }

    protected override void OnShiftUp()
    {
        SLEDOff(SingleLEDButton.Right);
    }

    private void GoToSoundboard()
    {
        ChangePage(new SoundboardPage(APCMini, ChangePage));
    }
    #endregion Navigation

    #region Input A1
    protected override void OnB1Down()
    {
        Console.WriteLine("Would toggle Input 2 Mute");

        var newState = TogglePad(Pads.InputA1Mute);

        VM.SetFloatParameter($"Strip[0].Mute", newState ? 1 : 0);

        Console.WriteLine($"Input 2 is now {(newState ? "muted" : "unmuted")}");
    }

    protected override void OnB2Down()
    {
        Console.WriteLine("Would toggle Input 2 Record");

        var newState = TogglePad(Pads.InputA1Record);

        VM.SetFloatParameter($"Strip[0].B3", newState ? 1 : 0);

        Console.WriteLine($"Input 2 is now {(newState ? "recording" : "not recording")}");
    }

    protected override void OnB3Down()
    {
        Console.WriteLine("Would toggle Input 2 Alt Mic");

        var newState = TogglePad(Pads.InputA1AltMic);

        VM.SetFloatParameter($"Strip[0].B2", newState ? 1 : 0);

        Console.WriteLine($"Input 2 is now {(newState ? "using alt mic" : "not using alt mic")}");
    }

    protected override void OnB4Down()
    {
        Console.WriteLine("Would toggle Input 2 Mic");

        var newState = TogglePad(Pads.InputA1Mic);

        VM.SetFloatParameter($"Strip[0].B1", newState ? 1 : 0);

        Console.WriteLine($"Input 2 is now {(newState ? "using mic" : "not using mic")}");
    }

    protected override void OnB5Down()
    {
        Console.WriteLine("Would toggle Input 2 Speakers");

        var newState = TogglePad(Pads.InputA1Speakers);

        VM.SetFloatParameter($"Strip[0].A5", newState ? 1 : 0);

        Console.WriteLine($"Input 2 is now {(newState ? "using speakers" : "not using speakers")}");
    }

    protected override void OnB6Down()
    {
        Console.WriteLine("Would toggle Input 2 Headphones");

        var newState = TogglePad(Pads.InputA1Headphones);

        VM.SetFloatParameter($"Strip[0].A1", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[0].A2", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[0].A3", newState ? 1 : 0);

        Console.WriteLine($"Input 2 is now {(newState ? "using headphones" : "not using headphones")}");
    }

    protected override void OnPanDown()
    {
        SetSLED(SLEDs.InputA1PTM, true);
        SetPad(Pads.InputA1Mute, true, PadState.PulsingHalfs);
        VM.SetFloatParameter($"Strip[0].Mute", 1);
    }

    protected override void OnPanUp()
    {
        SetSLED(SLEDs.InputA1PTM, false);
        SetPad(Pads.InputA1Mute, false);
        VM.SetFloatParameter($"Strip[0].Mute", 0);
    }
    #endregion Input A1

    #region Input A2
    protected override void OnA1Down() // Input A1 Mute
    {
        Console.WriteLine("Would toggle Input 1 Mute");

        var newState = TogglePad(Pads.InputA2Mute);

        VM.SetFloatParameter($"Strip[1].Mute", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[2].Mute", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[3].Mute", newState ? 1 : 0);

        Console.WriteLine($"Input 1 is now {(newState ? "muted" : "unmuted")}");
    }

    protected override void OnA2Down() // Input A1 Record
    {
        Console.WriteLine("Would toggle Input 1 Record");

        var newState = TogglePad(Pads.InputA2Record);

        VM.SetFloatParameter($"Strip[1].B3", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[2].B3", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[3].B3", newState ? 1 : 0);

        Console.WriteLine($"Input 1 is now {(newState ? "recording" : "not recording")}");
    }

    protected override void OnA3Down() // Input A1 Alt Mic
    {
        Console.WriteLine("Would toggle Input 1 Alt Mic");

        var newState = TogglePad(Pads.InputA2AltMic);

        VM.SetFloatParameter($"Strip[1].B2", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[2].B2", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[3].B2", newState ? 1 : 0);

        Console.WriteLine($"Input 1 is now {(newState ? "using alt mic" : "not using alt mic")}");
    }

    protected override void OnA4Down() // Input A1 Mic
    {
        Console.WriteLine("Would toggle Input 1 Mic");

        var newState = TogglePad(Pads.InputA2Mic);

        VM.SetFloatParameter($"Strip[1].B1", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[2].B1", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[3].B1", newState ? 1 : 0);

        Console.WriteLine($"Input 1 is now {(newState ? "using mic" : "not using mic")}");
    }

    protected override void OnA5Down() // Input A1 Speakers
    {
        Console.WriteLine("Would toggle Input 1 Speakers");

        var newState = TogglePad(Pads.InputA2Speakers);

        VM.SetFloatParameter($"Strip[1].A5", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[2].A5", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[3].A5", newState ? 1 : 0);

        Console.WriteLine($"Input 1 is now {(newState ? "using speakers" : "not using speakers")}");
    }

    protected override void OnA6Down() // Input A1 Headphones
    {
        Console.WriteLine("Would toggle Input 1 Headphones");

        var newState = TogglePad(Pads.InputA2Headphones);

        VM.SetFloatParameter($"Strip[1].A1", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[1].A2", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[1].A3", newState ? 1 : 0);

        VM.SetFloatParameter($"Strip[2].A1", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[2].A2", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[2].A3", newState ? 1 : 0);

        VM.SetFloatParameter($"Strip[3].A1", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[3].A2", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[3].A3", newState ? 1 : 0);

        Console.WriteLine($"Input 1 is now {(newState ? "using headphones" : "not using headphones")}");
    }

    protected override void OnVolumeDown()
    {
        SetSLED(SLEDs.InputA2PTM, true);
        SetPad(Pads.InputA2Mute, true, PadState.PulsingHalfs);
        VM.SetFloatParameter($"Strip[1].Mute", 1);
        VM.SetFloatParameter($"Strip[2].Mute", 1);
        VM.SetFloatParameter($"Strip[3].Mute", 1);
    }

    protected override void OnVolumeUp()
    {
        SetSLED(SLEDs.InputA2PTM, false);
        SetPad(Pads.InputA2Mute, false);
        VM.SetFloatParameter($"Strip[1].Mute", 0);
        VM.SetFloatParameter($"Strip[2].Mute", 0);
        VM.SetFloatParameter($"Strip[3].Mute", 0);
    }
    #endregion Input A2

    #region Input B1
    protected override void OnC1Down()
    {
        Console.WriteLine("Would toggle Input 3 Mute");

        var newState = TogglePad(Pads.InputB1Mute);

        VM.SetFloatParameter($"Strip[5].Mute", newState ? 1 : 0);

        Console.WriteLine($"Input 3 is now {(newState ? "muted" : "unmuted")}");
    }

    protected override void OnC2Down()
    {
        Console.WriteLine("Would toggle Input 3 Record");

        var newState = TogglePad(Pads.InputB1Record);

        VM.SetFloatParameter($"Strip[5].B3", newState ? 1 : 0);

        Console.WriteLine($"Input 3 is now {(newState ? "recording" : "not recording")}");
    }

    protected override void OnC3Down()
    {
        Console.WriteLine("Would toggle Input 3 Alt Mic");

        var newState = TogglePad(Pads.InputB1AltMic);

        VM.SetFloatParameter($"Strip[5].B2", newState ? 1 : 0);

        Console.WriteLine($"Input 3 is now {(newState ? "using alt mic" : "not using alt mic")}");
    }

    protected override void OnC4Down() // Input B1 Mic (push not toggle)
    {
        Console.WriteLine("Would toggle Input 3 Mic");

        SetPad(Pads.InputB1Mic, true, PadState.PulsingHalfs);
        VM.SetFloatParameter($"Strip[5].B1", 1);

        Console.WriteLine("Input 3 is now using mic");
    }

    protected override void OnC4Up() // Input B1 Mic (push not toggle)
    {
        Console.WriteLine("Would toggle Input 3 Mic");

        SetPad(Pads.InputB1Mic, false);
        VM.SetFloatParameter($"Strip[5].B1", 0);

        Console.WriteLine("Input 3 is now not using mic");
    }

    protected override void OnC5Down()
    {
        Console.WriteLine("Would toggle Input 3 Speakers");

        var newState = TogglePad(Pads.InputB1Speakers);

        VM.SetFloatParameter($"Strip[5].A5", newState ? 1 : 0);

        Console.WriteLine($"Input 3 is now {(newState ? "using speakers" : "not using speakers")}");
    }

    protected override void OnC6Down()
    {
        Console.WriteLine("Would toggle Input 3 Headphones");

        var newState = TogglePad(Pads.InputB1Headphones);

        VM.SetFloatParameter($"Strip[5].A1", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[5].A2", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[5].A3", newState ? 1 : 0);

        Console.WriteLine($"Input 3 is now {(newState ? "using headphones" : "not using headphones")}");
    }

    protected override void OnSendDown()
    {
        SetSLED(SLEDs.InputB1PTM, true);
        SetPad(Pads.InputB1Mute, true, PadState.PulsingHalfs);
        VM.SetFloatParameter($"Strip[5].Mute", 1);
    }

    protected override void OnSendUp()
    {
        SetSLED(SLEDs.InputB1PTM, false);
        SetPad(Pads.InputB1Mute, false);
        VM.SetFloatParameter($"Strip[5].Mute", 0);
    }
    #endregion Input B1

    #region Input B2
    protected override void OnD1Down()
    {
        Console.WriteLine("Would toggle Input 4 Mute");

        var newState = TogglePad(Pads.InputB2Mute);

        VM.SetFloatParameter($"Strip[6].Mute", newState ? 1 : 0);

        Console.WriteLine($"Input 4 is now {(newState ? "muted" : "unmuted")}");
    }

    protected override void OnD2Down()
    {
        Console.WriteLine("Would toggle Input 4 Record");

        var newState = TogglePad(Pads.InputB2Record);

        VM.SetFloatParameter($"Strip[6].B3", newState ? 1 : 0);

        Console.WriteLine($"Input 4 is now {(newState ? "recording" : "not recording")}");
    }

    protected override void OnD3Down()
    {
        Console.WriteLine("Would toggle Input 4 Alt Mic");

        var newState = TogglePad(Pads.InputB2AltMic);

        VM.SetFloatParameter($"Strip[6].B2", newState ? 1 : 0);

        Console.WriteLine($"Input 4 is now {(newState ? "using alt mic" : "not using alt mic")}");
    }

    protected override void OnD4Down()
    {
        Console.WriteLine("Would toggle Input 4 Mic");

        var newState = TogglePad(Pads.InputB2Mic);

        VM.SetFloatParameter($"Strip[6].B1", newState ? 1 : 0);

        Console.WriteLine($"Input 4 is now {(newState ? "using mic" : "not using mic")}");
    }

    protected override void OnD5Down()
    {
        Console.WriteLine("Would toggle Input 4 Speakers");

        var newState = TogglePad(Pads.InputB2Speakers);

        VM.SetFloatParameter($"Strip[6].A5", newState ? 1 : 0);

        Console.WriteLine($"Input 4 is now {(newState ? "using speakers" : "not using speakers")}");
    }

    protected override void OnD6Down()
    {
        Console.WriteLine("Would toggle Input 4 Headphones");

        var newState = TogglePad(Pads.InputB2Headphones);

        VM.SetFloatParameter($"Strip[6].A1", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[6].A2", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[6].A3", newState ? 1 : 0);

        Console.WriteLine($"Input 4 is now {(newState ? "using headphones" : "not using headphones")}");
    }

    protected override void OnDeviceDown()
    {
        SetSLED(SLEDs.InputB2PTM, true);
        SetPad(Pads.InputB2Mute, true, PadState.PulsingHalfs);
        VM.SetFloatParameter($"Strip[6].Mute", 1);
    }

    protected override void OnDeviceUp()
    {
        SetSLED(SLEDs.InputB2PTM, false);
        SetPad(Pads.InputB2Mute, false);
        VM.SetFloatParameter($"Strip[6].Mute", 0);
    }
    #endregion Input B2

    #region Input B3
    protected override void OnE1Down()
    {
        Console.WriteLine("Would toggle Input 5 Mute");

        var newState = TogglePad(Pads.InputB3Mute);

        VM.SetFloatParameter($"Strip[7].Mute", newState ? 1 : 0);

        Console.WriteLine($"Input 5 is now {(newState ? "muted" : "unmuted")}");
    }

    protected override void OnE2Down()
    {
        Console.WriteLine("Would toggle Input 5 Record");

        var newState = TogglePad(Pads.InputB3Record);

        VM.SetFloatParameter($"Strip[7].B3", newState ? 1 : 0);

        Console.WriteLine($"Input 5 is now {(newState ? "recording" : "not recording")}");
    }

    protected override void OnE3Down()
    {
        Console.WriteLine("Would toggle Input 5 Alt Mic");

        var newState = TogglePad(Pads.InputB3AltMic);

        VM.SetFloatParameter($"Strip[7].B2", newState ? 1 : 0);

        Console.WriteLine($"Input 5 is now {(newState ? "using alt mic" : "not using alt mic")}");
    }

    protected override void OnE4Down()
    {
        Console.WriteLine("Would toggle Input 5 Mic");

        var newState = TogglePad(Pads.InputB3Mic);

        VM.SetFloatParameter($"Strip[7].B1", newState ? 1 : 0);

        Console.WriteLine($"Input 5 is now {(newState ? "using mic" : "not using mic")}");
    }

    protected override void OnE5Down()
    {
        Console.WriteLine("Would toggle Input 5 Speakers");

        var newState = TogglePad(Pads.InputB3Speakers);

        VM.SetFloatParameter($"Strip[7].A5", newState ? 1 : 0);

        Console.WriteLine($"Input 5 is now {(newState ? "using speakers" : "not using speakers")}");
    }

    protected override void OnE6Down()
    {
        Console.WriteLine("Would toggle Input 5 Headphones");

        var newState = TogglePad(Pads.InputB3Headphones);

        VM.SetFloatParameter($"Strip[7].A1", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[7].A2", newState ? 1 : 0);
        VM.SetFloatParameter($"Strip[7].A3", newState ? 1 : 0);

        Console.WriteLine($"Input 5 is now {(newState ? "using headphones" : "not using headphones")}");
    }

    protected override void OnUpDown()
    {
        SetSLED(SLEDs.InputB3PTM, true);
        SetPad(Pads.InputB3Mute, true, PadState.PulsingHalfs);
        VM.SetFloatParameter($"Strip[7].Mute", 1);
    }

    protected override void OnUpUp()
    {
        SetSLED(SLEDs.InputB3PTM, false);
        SetPad(Pads.InputB3Mute, false);
        VM.SetFloatParameter($"Strip[7].Mute", 0);
    }
    #endregion Input B3

    #region Output A1
    protected override void OnLeftDown()
    {
        var newState = ToggleSLED(SLEDs.OutputA1Mute);

        if (newState)
        {
            VM.SetFloatParameter($"Bus[0].Mute", 1);
        }
        else
        {
            VM.SetFloatParameter($"Bus[0].Mute", 0);
        }

        Console.WriteLine($"Output 1 is now {(newState ? "muted" : "unmuted")}");
    }
    #endregion Output A1

    #region Output A2
    protected override void OnDownDown()
    {
        var newState = ToggleSLED(SLEDs.OutputA2Mute);

        if (newState)
        {
            VM.SetFloatParameter($"Bus[1].Mute", 1);
        }
        else
        {
            VM.SetFloatParameter($"Bus[1].Mute", 0);
        }

        Console.WriteLine($"Output 2 is now {(newState ? "muted" : "unmuted")}");
    }
    #endregion Output A2

    #region Output A3
    protected override void OnRightDown()
    {
        if (IsDown(SingleLEDButton.Shift))
        {
            GoToSoundboard();
        }
        else
        {
            Console.WriteLine("Would toggle Bus 3 Mute");

            var newState = ToggleSLED(SLEDs.OutputA3Mute);

            if (newState)
            {
                VM.SetFloatParameter($"Bus[2].Mute", 1);
            }
            else
            {
                VM.SetFloatParameter($"Bus[2].Mute", 0);
            }

            Console.WriteLine($"Bus 3 is now {(newState ? "muted" : "unmuted")}");
        }
    }
    #endregion Output A3

    #region Output A4
    protected override void OnDrumDown()
    {
        Console.WriteLine("Would toggle Bus 4 Mute");

        var newState = ToggleSLED(SLEDs.OutputA4Mute);

        VM.SetFloatParameter($"Bus[3].Mute", newState ? 1 : 0);

        Console.WriteLine($"Bus 4 is now {(newState ? "muted" : "unmuted")}");
    }
    #endregion Output A4

    #region Output A5
    protected override void OnNoteDown()
    {
        Console.WriteLine("Would toggle Bus 5 Mute");

        var newState = ToggleSLED(SLEDs.OutputA5Mute);

        VM.SetFloatParameter($"Bus[4].Mute", newState ? 1 : 0);

        Console.WriteLine($"Bus 5 is now {(newState ? "muted" : "unmuted")}");
    }
    #endregion Output A5

    #region Nvidia Broadcast Toggle
    protected override void OnA8Down()
    {
        Console.WriteLine("Would toggle Nvidia Broadcast");

        var newState = TogglePad(Pads.NvidiaBroadcastToggle);

        VM.SetFloatParameter("Strip[2].Gain", newState ? -60 : 0);
        VM.SetFloatParameter("Strip[3].Gain", newState ? 0 : -60);

        Console.WriteLine($"Nvidia Broadcast is now {(newState ? "disabled" : "enabled")}");
    }
    #endregion Nvidia Broadcast Toggle
}