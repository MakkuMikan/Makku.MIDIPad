using Makku.APCMini.MK2.Constants;
using Makku.APCMini.MK2.Helpers;
using Makku.MIDI;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using Microsoft.Extensions.DependencyInjection;

namespace Makku.APCMini.MK2
{
    public class APCMiniService : MIDIDeviceService
    {
        public APCMiniService() : base(GetInputDeviceName())
        {
            Console.WriteLine("APC Mini MK2 connected.");
        }

        public event EventHandler<NoteOnEventArgs> NoteOnEvent;

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

        public void SetMultipleLEDs(FourBitNumber behaviour, int[] values, SevenBitNumber colour)
        {
            foreach (var value in values)
            {
                SetLED(new MatrixPadState(behaviour, (SevenBitNumber)value, colour));
            }
        }

        public void ResetLED(MatrixPadState state)
        {
            SendOffEvent(state);
        }

        public void ResetLED(SevenBitNumber value)
        {
            ResetLED(new MatrixPadState(PadState.OneHundredPercent, value, SevenBitNumber.MinValue));
        }

        private void SendOnEvent(MatrixPadState state)
        {
            SendEvent(state.ToNoteOnEvent());
        }

        private void SendOffEvent(MatrixPadState state)
        {
            SendEvent(state.ToNoteOffEvent());
        }

        protected override void OnEventReceived(MidiDevice sender, MidiEvent midiEvent)
        {
            Console.WriteLine(midiEvent);
        }

        protected override void OnEventSent(MidiDevice sender, MidiEvent midiEvent)
        {
            Console.WriteLine(midiEvent);
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
