using Makku.APCMini.MK2;
using Makku.APCMini.MK2.Constants;
using Makku.APCMini.MK2.Helpers;
using Makku.MIDIPad.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Makku.MIDI;
using Microsoft.Extensions.DependencyInjection;

namespace Makku.MIDIPad.Voicemeeter;

public class SoundboardPage(MIDIDeviceService service, Navigator navigator) : BasePage(service, navigator)
{
    private readonly PartialMatrixPadState _soundboardInactive = MatrixPadState.FromColour(Colour.Orange).WithBehaviour(PadState.TenPercent);
    private readonly PartialMatrixPadState _soundboardActive = MatrixPadState.FromColour(Colour.Orange).WithBehaviour(PadState.OneHundredPercent);

    private VoicemeeterHelper Voicemeeter;
    private const string SoundboardPath = @"C:\Users\makku\Documents\VM Sounds";
    
    private APCMiniService _apcMiniService = service as APCMiniService;

    #region Navigation
    protected override void OnShiftDown()
    {
        SLEDBlink(SingleLEDButton.Left);
    }

    protected override void OnShiftUp()
    {
        SLEDOff(SingleLEDButton.Left);
    }

    protected override void OnLeftDown()
    {
        ChangePage<VoicemeeterPage>();
    }
    #endregion Navigation

    public override void Update()
    {
        if (Disposed) return;

        base.Update();
    }

    public override void OnLoad()
    {
        Voicemeeter = new VoicemeeterHelper();

        APCMini.SetMultipleLEDs(
            (Melanchall.DryWetMidi.Common.FourBitNumber)_soundboardInactive.Behaviour!, [
                Button.A8,
                Button.B8,
                Button.C8,
                Button.D8,
                Button.E8,
                Button.F8,
                Button.G8,
                Button.H8,
                Button.A7
            ],
            (Melanchall.DryWetMidi.Common.SevenBitNumber)_soundboardInactive.Colour!);

        base.OnLoad();
    }

    private void PlaySound(string filename)
    {
        if (!filename.Contains(":\\"))
        {
            filename = Path.Combine(SoundboardPath, filename);
        }

        Voicemeeter.SetStringParameter("Recorder.Load", filename);
        Voicemeeter.SetFloatParameter("Recorder.Play", 1);
    }

    protected override void OnA8Down()
    {
        APCMini.SetLED(_soundboardActive.WithValue(Button.A8));
        PlaySound("perfect-fart.wav");
    }

    protected override void OnA8Up()
    {
        APCMini.SetLED(_soundboardInactive.WithValue(Button.A8));
    }

    protected override void OnB8Down()
    {
        APCMini.SetLED(_soundboardActive.WithValue(Button.B8));
        PlaySound("vine-boom.wav");
    }

    protected override void OnB8Up()
    {
        APCMini.SetLED(_soundboardInactive.WithValue(Button.B8));
    }

    protected override void OnC8Down()
    {
        APCMini.SetLED(_soundboardActive.WithValue(Button.C8));
        PlaySound("taco-bell-bong.wav");
    }

    protected override void OnC8Up()
    {
        APCMini.SetLED(_soundboardInactive.WithValue(Button.C8));
    }

    protected override void OnD8Down()
    {
        APCMini.SetLED(_soundboardActive.WithValue(Button.D8));
        PlaySound("emotional-damage.wav");
    }

    protected override void OnD8Up()
    {
        APCMini.SetLED(_soundboardInactive.WithValue(Button.D8));
    }

    protected override void OnE8Down()
    {
        APCMini.SetLED(_soundboardActive.WithValue(Button.E8));
        PlaySound("fart-with-extra-reverb.wav");
    }

    protected override void OnE8Up()
    {
        APCMini.SetLED(_soundboardInactive.WithValue(Button.E8));
    }

    protected override void OnF8Down()
    {
        APCMini.SetLED(_soundboardActive.WithValue(Button.F8));
        PlaySound("minecraft-cave-sound.wav");
    }

    protected override void OnF8Up()
    {
        APCMini.SetLED(_soundboardInactive.WithValue(Button.F8));
    }

    protected override void OnG8Down()
    {
        APCMini.SetLED(_soundboardActive.WithValue(Button.G8));
        PlaySound("dial-up.wav");
    }

    protected override void OnG8Up()
    {
        APCMini.SetLED(_soundboardInactive.WithValue(Button.G8));
    }

    protected override void OnH8Down()
    {
        APCMini.SetLED(_soundboardActive.WithValue(Button.H8));
        PlaySound("metal-pipe.wav");
    }

    protected override void OnH8Up()
    {
        APCMini.SetLED(_soundboardInactive.WithValue(Button.H8));
    }

    protected override void OnA7Down()
    {
        APCMini.SetLED(_soundboardActive.WithValue(Button.A7));
        PlaySound("aughh.wav");
    }

    protected override void OnA7Up()
    {
        APCMini.SetLED(_soundboardInactive.WithValue(Button.A7));
    }
}

public static class SoundboardPageServiceExtensions
{
    public static IServiceCollection AddSoundboardPage(this IServiceCollection services)
        => services.AddScoped<SoundboardPage>();
}