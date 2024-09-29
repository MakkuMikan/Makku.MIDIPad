using Makku.MIDI.APCMiniMk2.Constants;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using Microsoft.Extensions.DependencyInjection;
using Makku.MIDI.Pad;

using ILEDService = Makku.MIDI.LED.ILEDService<Melanchall.DryWetMidi.Common.SevenBitNumber, Melanchall.DryWetMidi.Common.SevenBitNumber>;
using IExtendedLEDService = Makku.MIDI.ExtendedLED.IExtendedLEDService<Melanchall.DryWetMidi.Common.SevenBitNumber, Melanchall.DryWetMidi.Common.SevenBitNumber, Melanchall.DryWetMidi.Common.FourBitNumber>;
using ISingleLEDService = Makku.MIDI.SingleLED.ISingleLEDService<Melanchall.DryWetMidi.Common.SevenBitNumber, Melanchall.DryWetMidi.Common.SevenBitNumber>;
using IPadService = Makku.MIDI.Pad.IPadService<Melanchall.DryWetMidi.Common.SevenBitNumber>;
using Makku.MIDI.LED;

namespace Makku.MIDI.APCMiniMk2
{
    public class APCMiniService : MIDIDeviceService, IExtendedLEDService, ISingleLEDService, IPadService
    {
        public SevenBitNumber DefaultColor { get; } = Colour.Black;
        public FourBitNumber DefaultLEDBehaviour { get; } = Behaviour.OneHundredPercent;
        public SevenBitNumber DefaultSingleLEDBehaviour { get; } = SingleLEDButtonState.Off;

        public APCMiniService() : base(GetInputDeviceName())
        {
            Console.WriteLine("APC Mini MK2 connected.");
        }

        public event EventHandler<PadPressedEventArgs<SevenBitNumber>>? PadPressedEvent;
        public event EventHandler<PadReleasedEventArgs<SevenBitNumber>>? PadReleasedEvent;

        public void SetLED(IExtendedLEDService.ExtendedLEDState state)
        {
            var midiEvent = new NoteOnEvent(state.LED, state.Color)
            {
                Channel = state.Behaviour
            };

            SendEvent(midiEvent);
        }

        public void SetLED(ILEDService.LEDState state)
        {
            var midiEvent = new NoteOnEvent(state.LED, state.Color)
            {
                Channel = DefaultLEDBehaviour
            };

            SendEvent(midiEvent);
        }

        public void ResetLED(SevenBitNumber led)
        {
            var midiEvent = new NoteOnEvent(led, DefaultColor)
            {
                Channel = Behaviour.OneHundredPercent
            };

            SendEvent(midiEvent);
        }

        public void ResetAllLEDs()
        {
            foreach (var led in AllLEDs)
            {
                ResetLED(led);
            }
        }

        public void SetSingleLED(ISingleLEDService.SingleLEDState state)
        {
            var midiEvent = new NoteOnEvent(state.Identifier, state.Behaviour);

            SendEvent(midiEvent);
        }

        public void SetSingleLED(SevenBitNumber led, SevenBitNumber behaviour)
        {
            var midiEvent = new NoteOnEvent(led, behaviour);

            SendEvent(midiEvent);
        }

        public void ResetSingleLED(SevenBitNumber led)
        {
            var midiEvent = new NoteOnEvent(led, DefaultSingleLEDBehaviour);

            SendEvent(midiEvent);
        }

        public void ResetAllSingleLEDs()
        {
            foreach (var led in AllSingleLEDs)
            {
                ResetSingleLED(led);
            }
        }

        private static string GetInputDeviceName()
        {
            var allDevices = InputDevice.GetAll();
            return allDevices.First(d => d.Name.Contains("apc", StringComparison.CurrentCultureIgnoreCase)).Name;
        }

        public PadPressedEventArgs<SevenBitNumber>? GetPadFromNoteOn(NoteOnEventArgs e)
        {
            return new PadPressedEventArgs<SevenBitNumber>(e.NoteOnEvent.NoteNumber, e.NoteOnEvent.Velocity);
        }

        public PadReleasedEventArgs<SevenBitNumber>? GetPadFromNoteOff(NoteOffEventArgs e)
        {
            return new PadReleasedEventArgs<SevenBitNumber>(e.NoteOffEvent.NoteNumber);
        }

        #region All LEDs
        public SevenBitNumber[] AllLEDs { get; } =
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
        ];

        public SevenBitNumber[] AllSingleLEDs { get; } =
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
        ];

        #endregion All LEDs
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
