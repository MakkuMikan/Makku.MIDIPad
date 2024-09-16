using Makku.APCMini.MK2.Constants;
using Makku.APCMini.MK2.Helpers;
using Makku.MIDI;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;

namespace Makku.APCMini.MK2
{
    public class APCMiniService : MIDIDeviceService
    {
        public APCMiniService() : base(GetInputDeviceName())
        {
            Console.WriteLine("APC Mini MK2 connected.");
        }

        public void SetLED(MatrixPadState state)
        {
            SendOnEvent(state);
        }

        public void SetLED(FourBitNumber behaviour, SevenBitNumber value, SevenBitNumber colour)
        {
            SetLED(new MatrixPadState(behaviour, value, colour));
        }

        public void SetMultipleLEDs(FourBitNumber behaviour, SevenBitNumber[] values, SevenBitNumber colour)
        {
            foreach (var value in values)
            {
                SetLED(new MatrixPadState(behaviour, value, colour));
            }
        }

        public void ResetLED(MatrixPadState state)
        {
            state.Behaviour = PadState.OneHundredPercent;
            state.Colour = Colour.Black;
            SendOnEvent(state);
        }

        public void ResetLED(SevenBitNumber value)
        {
            ResetLED(new MatrixPadState(PadState.OneHundredPercent, value, SevenBitNumber.MinValue));
        }

        public void ResetAllPads()
        {
            SetMultipleLEDs(PadState.OneHundredPercent,
            [
                Button.A1,
                Button.B1,
                Button.C1,
                Button.D1,
                Button.E1,
                Button.F1,
                Button.G1,
                Button.H1,
                Button.A2,
                Button.B2,
                Button.C2,
                Button.D2,
                Button.E2,
                Button.F2,
                Button.G2,
                Button.H2,
                Button.A3,
                Button.B3,
                Button.C3,
                Button.D3,
                Button.E3,
                Button.F3,
                Button.G3,
                Button.H3,
                Button.A4,
                Button.B4,
                Button.C4,
                Button.D4,
                Button.E4,
                Button.F4,
                Button.G4,
                Button.H4,
                Button.A5,
                Button.B5,
                Button.C5,
                Button.D5,
                Button.E5,
                Button.F5,
                Button.G5,
                Button.H5,
                Button.A6,
                Button.B6,
                Button.C6,
                Button.D6,
                Button.E6,
                Button.F6,
                Button.G6,
                Button.H6,
                Button.A7,
                Button.B7,
                Button.C7,
                Button.D7,
                Button.E7,
                Button.F7,
                Button.G7,
                Button.H7,
                Button.A8,
                Button.B8,
                Button.C8,
                Button.D8,
                Button.E8,
                Button.F8,
                Button.G8,
                Button.H8
            ], Colour.Black);
        }

        public void SetSLED(SingleLEDState state)
        {
            SendEvent(state);
        }

        public void SetSLED(SevenBitNumber value, SevenBitNumber behaviour)
        {
            SendEvent(new SingleLEDState(value, behaviour));
        }

        public void SetMultipleSLEDs(SevenBitNumber behaviour, SevenBitNumber[] values)
        {
            foreach (var value in values)
            {
                SetSLED(new SingleLEDState(value, behaviour));
            }
        }

        public void ResetSLED(SevenBitNumber value)
        {
            SendEvent(new SingleLEDState(value, SingleLEDButtonState.Off));
        }

        public void ResetAllSLEDs()
        {
            SetMultipleSLEDs(SingleLEDButtonState.Off,
            [
                SingleLEDButton.Volume,
                SingleLEDButton.Pan,
                SingleLEDButton.Send,
                SingleLEDButton.Device,
                SingleLEDButton.Up,
                SingleLEDButton.Down,
                SingleLEDButton.Left,
                SingleLEDButton.Right,

                SingleLEDButton.ClipStop,
                SingleLEDButton.Solo,
                SingleLEDButton.Mute,
                SingleLEDButton.RecArm,
                SingleLEDButton.Select,
                SingleLEDButton.Drum,
                SingleLEDButton.Note,
                SingleLEDButton.StopAllClips,

                SingleLEDButton.Shift
            ]);
        }

        private void SendOnEvent(MatrixPadState state)
        {
            SendEvent(state.ToNoteOnEvent());
        }

        private void SendEvent(SingleLEDState state)
        {
            SendEvent(state.ToNoteOnEvent());
        }

        private static string GetInputDeviceName()
        {
            var allDevices = InputDevice.GetAll();
            return allDevices.First(d => d.Name.Contains("apc", StringComparison.CurrentCultureIgnoreCase)).Name;
        }
    }

    public static class APCMiniServiceExtensions
    {
        public static IServiceCollection UseAPCMini(this IServiceCollection services)
        {
            services.AddSingleton<APCMiniService>();
            return services;
        }
    }
}
