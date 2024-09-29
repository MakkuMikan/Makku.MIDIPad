using Makku.MIDIPad.Core;
using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDIPad.Voicemeeter.Buttons;

public class VMButton : PageButton
{
    protected readonly VoicemeeterHelper _voicemeeterHelper;

    public string ParameterName { get; }
    public virtual float OnValue { get; }
    public virtual float OffValue { get; }

    protected bool IsTwoState { get; } = true;

    protected virtual void onDown()
    {
        if (IsTwoState)
        {
            _voicemeeterHelper.SetFloatParameter(ParameterName, GetState() ? OffValue : OnValue);
        }
        else
        {
            _voicemeeterHelper.SetFloatParameter(ParameterName, OnValue);
        }
    }

    protected virtual void onUp()
    {
        if (!IsTwoState)
        {
            _voicemeeterHelper.SetFloatParameter(ParameterName, OffValue);
        }
    }

    public VMButton(VoicemeeterHelper vmHelper, SevenBitNumber button, string parameterName)
    {
        _voicemeeterHelper = vmHelper;
        ParameterName = parameterName;

        OnDown = onDown;
        OnUp = onUp;
    }

    public VMButton(VoicemeeterHelper vmHelper, SevenBitNumber button, string parameterName, bool isTwoState)
    {
        _voicemeeterHelper = vmHelper;
        ParameterName = parameterName;
        IsTwoState = isTwoState;

        OnDown = onDown;
        OnUp = onUp;
    }

    public VMButton(VoicemeeterHelper vmHelper, SevenBitNumber button, string parameterName, float onValue, float offValue)
    {
        _voicemeeterHelper = vmHelper;
        ParameterName = parameterName;
        OnValue = onValue;
        OffValue = offValue;

        OnDown = onDown;
        OnUp = onUp;
    }

    public VMButton(VoicemeeterHelper vmHelper, SevenBitNumber button, string parameterName, float onValue, float offValue, bool isTwoState)
    {
        _voicemeeterHelper = vmHelper;
        ParameterName = parameterName;
        OnValue = onValue;
        OffValue = offValue;
        IsTwoState = isTwoState;

        OnDown = onDown;
        OnUp = onUp;
    }
}

public static class VMHelperExtensions
{
    public static VMButton Button(this VoicemeeterHelper vmHelper, SevenBitNumber button, string parameterName)
    {
        return new VMButton(vmHelper, button, parameterName);
    }

    public static VMButton HoldButton(this VoicemeeterHelper vmHelper, SevenBitNumber button, string parameterName)
    {
        return new VMButton(vmHelper, button, parameterName, false);
    }

    public static VMButton Button(this VoicemeeterHelper vmHelper, SevenBitNumber button, string parameterName, float onValue, float offValue)
    {
        return new VMButton(vmHelper, button, parameterName, onValue, offValue);
    }

    public static VMButton HoldButton(this VoicemeeterHelper vmHelper, SevenBitNumber button, string parameterName, float onValue, float offValue)
    {
        return new VMButton(vmHelper, button, parameterName, onValue, offValue, false);
    }

    public static PageButton RawButton(this VoicemeeterHelper vmHelper, SevenBitNumber button, Action onDown, Action onUp, Func<bool> getState)
    {
        return new PageButton(button, onDown, onUp, getState);
    }

    public static PageButton RawButton(this VoicemeeterHelper vmHelper, SevenBitNumber button, Action onDown, Func<bool> getState)
    {
        return new PageButton(button, onDown, getState);
    }

    public static PageButton RawButton(this VoicemeeterHelper vmHelper, SevenBitNumber button, Action on, Action off, string getStateParameter, float onValue)
    {
        return new PageButton(button, () => { if (vmHelper.GetFloatParameter(getStateParameter) == onValue) { on(); } else { off(); } }, null, () => vmHelper.GetFloatParameter(getStateParameter) == onValue);
    }
}