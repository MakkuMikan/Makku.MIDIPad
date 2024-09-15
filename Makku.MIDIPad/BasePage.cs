using Makku.APCMini.MK2;
using Makku.APCMini.MK2.Constants;
using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDIPad;

public abstract class BasePage
{
    private readonly APCMiniService apcMini;

    public BasePage(APCMiniService apcMini)
    {
        this.apcMini = apcMini;

        apcMini.
    }

    protected virtual void OnLoad() { }

    protected virtual void OnUnload()
    {
        
    }

    protected virtual void PadOn(SevenBitNumber value, SevenBitNumber colour)
    {
        apcMini.SetLED(PadState.OneHundredPercent, value, colour);
    }

    protected virtual void PadOff(SevenBitNumber value)
    {
        apcMini.ResetLED(value);
    }

    protected virtual void OnA1() { }
    protected virtual void OnB1() { }
    protected virtual void OnC1() { }
    protected virtual void OnD1() { }
    protected virtual void OnE1() { }
    protected virtual void OnF1() { }
    protected virtual void OnG1() { }
    protected virtual void OnH1() { }

    protected virtual void OnA2() { }
    protected virtual void OnB2() { }
    protected virtual void OnC2() { }
    protected virtual void OnD2() { }
    protected virtual void OnE2() { }
    protected virtual void OnF2() { }
    protected virtual void OnG2() { }
    protected virtual void OnH2() { }

    protected virtual void OnA3() { }
    protected virtual void OnB3() { }
    protected virtual void OnC3() { }
    protected virtual void OnD3() { }
    protected virtual void OnE3() { }
    protected virtual void OnF3() { }
    protected virtual void OnG3() { }
    protected virtual void OnH3() { }

    protected virtual void OnA4() { }
    protected virtual void OnB4() { }
    protected virtual void OnC4() { }
    protected virtual void OnD4() { }
    protected virtual void OnE4() { }
    protected virtual void OnF4() { }
    protected virtual void OnG4() { }
    protected virtual void OnH4() { }

    protected virtual void OnA5() { }
    protected virtual void OnB5() { }
    protected virtual void OnC5() { }
    protected virtual void OnD5() { }
    protected virtual void OnE5() { }
    protected virtual void OnF5() { }
    protected virtual void OnG5() { }
    protected virtual void OnH5() { }

    protected virtual void OnA6() { }
    protected virtual void OnB6() { }
    protected virtual void OnC6() { }
    protected virtual void OnD6() { }
    protected virtual void OnE6() { }
    protected virtual void OnF6() { }
    protected virtual void OnG6() { }
    protected virtual void OnH6() { }

    protected virtual void OnA7() { }
    protected virtual void OnB7() { }
    protected virtual void OnC7() { }
    protected virtual void OnD7() { }
    protected virtual void OnE7() { }
    protected virtual void OnF7() { }
    protected virtual void OnG7() { }
    protected virtual void OnH7() { }

    protected virtual void OnA8() { }
    protected virtual void OnB8() { }
    protected virtual void OnC8() { }
    protected virtual void OnD8() { }
    protected virtual void OnE8() { }
    protected virtual void OnF8() { }
    protected virtual void OnG8() { }
    protected virtual void OnH8() { }

    protected virtual void OnVolume() { }
    protected virtual void OnPan() { }
    protected virtual void OnSend() { }
    protected virtual void OnDevice() { }
    protected virtual void OnUp() { }
    protected virtual void OnDown() { }
    protected virtual void OnLeft() { }
    protected virtual void OnRight() { }

    protected virtual void OnClipStop() { }
    protected virtual void OnSolo() { }
    protected virtual void OnMute() { }
    protected virtual void OnRecArm() { }
    protected virtual void OnSelect() { }
    protected virtual void OnDrum() { }
    protected virtual void OnNote() { }
    protected virtual void OnStopAllClips() { }

    protected virtual void OnShift() { }

    protected void OnButtonPressed(int button)
    {
        switch ((SevenBitNumber)button)
        {
            case ButtonRaw.A1:
                OnA1();
                break;
            case ButtonRaw.B1:
                OnB1();
                break;
            case ButtonRaw.C1:
                OnC1();
                break;
            case ButtonRaw.D1:
                OnD1();
                break;
            case ButtonRaw.E1:
                OnE1();
                break;
            case ButtonRaw.F1:
                OnF1();
                break;
            case ButtonRaw.G1:
                OnG1();
                break;
            case ButtonRaw.H1:
                OnH1();
                break;

            case ButtonRaw.A2:
                OnA2();
                break;
            case ButtonRaw.B2:
                OnB2();
                break;
            case ButtonRaw.C2:
                OnC2();
                break;
            case ButtonRaw.D2:
                OnD2();
                break;
            case ButtonRaw.E2:
                OnE2();
                break;
            case ButtonRaw.F2:
                OnF2();
                break;
            case ButtonRaw.G2:
                OnG2();
                break;
            case ButtonRaw.H2:
                OnH2();
                break;

            case ButtonRaw.A3:
                OnA3();
                break;
            case ButtonRaw.B3:
                OnB3();
                break;
            case ButtonRaw.C3:
                OnC3();
                break;
            case ButtonRaw.D3:
                OnD3();
                break;
            case ButtonRaw.E3:
                OnE3();
                break;
            case ButtonRaw.F3:
                OnF3();
                break;
            case ButtonRaw.G3:
                OnG3();
                break;
            case ButtonRaw.H3:
                OnH3();
                break;

            case ButtonRaw.A4:
                OnA4();
                break;
            case ButtonRaw.B4:
                OnB4();
                break;
            case ButtonRaw.C4:
                OnC4();
                break;
            case ButtonRaw.D4:
                OnD4();
                break;
            case ButtonRaw.E4:
                OnE4();
                break;
            case ButtonRaw.F4:
                OnF4();
                break;
            case ButtonRaw.G4:
                OnG4();
                break;
            case ButtonRaw.H4:
                OnH4();
                break;

            case ButtonRaw.A5:
                OnA5();
                break;
            case ButtonRaw.B5:
                OnB5();
                break;
            case ButtonRaw.C5:
                OnC5();
                break;
            case ButtonRaw.D5:
                OnD5();
                break;
            case ButtonRaw.E5:
                OnE5();
                break;
            case ButtonRaw.F5:
                OnF5();
                break;
            case ButtonRaw.G5:
                OnG5();
                break;
            case ButtonRaw.H5:
                OnH5();
                break;

            case ButtonRaw.A6:
                OnA6();
                break;
            case ButtonRaw.B6:
                OnB6();
                break;
            case ButtonRaw.C6:
                OnC6();
                break;
            case ButtonRaw.D6:
                OnD6();
                break;
            case ButtonRaw.E6:
                OnE6();
                break;
            case ButtonRaw.F6:
                OnF6();
                break;
            case ButtonRaw.G6:
                OnG6();
                break;
            case ButtonRaw.H6:
                OnH6();
                break;

            case ButtonRaw.A7:
                OnA7();
                break;
            case ButtonRaw.B7:
                OnB7();
                break;
            case ButtonRaw.C7:
                OnC7();
                break;
            case ButtonRaw.D7:
                OnD7();
                break;
            case ButtonRaw.E7:
                OnE7();
                break;
            case ButtonRaw.F7:
                OnF7();
                break;
            case ButtonRaw.G7:
                OnG7();
                break;
            case ButtonRaw.H7:
                OnH7();
                break;

            case ButtonRaw.A8:
                OnA8();
                break;
            case ButtonRaw.B8:
                OnB8();
                break;
            case ButtonRaw.C8:
                OnC8();
                break;
            case ButtonRaw.D8:
                OnD8();
                break;
            case ButtonRaw.E8:
                OnE8();
                break;
            case ButtonRaw.F8:
                OnF8();
                break;
            case ButtonRaw.G8:
                OnG8();
                break;
            case ButtonRaw.H8:
                OnH8();
                break;

            case SingleLEDButtonRaw.Volume:
                OnVolume();
                break;
            case SingleLEDButtonRaw.Pan:
                OnPan();
                break;
            case SingleLEDButtonRaw.Send:
                OnSend();
                break;
            case SingleLEDButtonRaw.Device:
                OnDevice();
                break;
            case SingleLEDButtonRaw.Up:
                OnUp();
                break;
            case SingleLEDButtonRaw.Down:
                OnDown();
                break;
            case SingleLEDButtonRaw.Left:
                OnLeft();
                break;
            case SingleLEDButtonRaw.Right:
                OnRight();
                break;

            case SingleLEDButtonRaw.ClipStop:
                OnClipStop();
                break;
            case SingleLEDButtonRaw.Solo:
                OnSolo();
                break;
            case SingleLEDButtonRaw.Mute:
                OnMute();
                break;
            case SingleLEDButtonRaw.RecArm:
                OnRecArm();
                break;
            case SingleLEDButtonRaw.Select:
                OnSelect();
                break;
            case SingleLEDButtonRaw.Drum:
                OnDrum();
                break;
            case SingleLEDButtonRaw.Note:
                OnNote();
                break;
            case SingleLEDButtonRaw.StopAllClips:
                OnStopAllClips();
                break;

            case SingleLEDButtonRaw.Shift:
                OnShift();
                break;
        }
    }
}
