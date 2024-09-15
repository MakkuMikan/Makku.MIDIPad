using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;

namespace Makku.MIDI
{
    public abstract class MIDIDeviceService : IDisposable
    {
        private readonly InputDevice _inputDevice;
        private readonly OutputDevice _outputDevice;

        public MIDIDeviceService(string deviceName, string? outputDeviceName = null)
        {
            _inputDevice = InputDevice.GetByName(deviceName);
            _outputDevice = OutputDevice.GetByName(outputDeviceName ?? deviceName);

            StartEventsListening();
            StartEventsSending();
        }

        protected void SendEvent(MidiEvent midiEvent)
        {
            _outputDevice.SendEvent(midiEvent);
        }

        protected virtual void StartEventsListening()
        {
            _inputDevice.EventReceived += (sender, e) =>
            {
                OnEventReceived((MidiDevice)sender!, e.Event);
            };

            _inputDevice.StartEventsListening();
        }

        protected virtual void StartEventsSending()
        {
            _outputDevice.EventSent += (sender, e) =>
            {
                OnEventSent((MidiDevice)sender!, e.Event);
            };
        }

        protected virtual void OnEventReceived(MidiDevice sender, MidiEvent midiEvent)
        {
        }

        protected virtual void OnEventSent(MidiDevice sender, MidiEvent midiEvent)
        {
        }

        public void Dispose()
        {
            _inputDevice.Dispose();
            _outputDevice.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
