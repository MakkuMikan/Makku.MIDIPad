using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;

namespace Makku.MIDI
{
    public abstract class MIDIDeviceService : IMIDIDeviceService
    {
        private readonly InputDevice _inputDevice;
        private readonly OutputDevice _outputDevice;

        public event EventHandler<NoteOnEventArgs> NoteOnEvent;
        public event EventHandler<NoteOffEventArgs> NoteOffEvent;

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
            _inputDevice.EventReceived += OnEventReceived;

            _inputDevice.StartEventsListening();
        }

        protected virtual void StartEventsSending()
        {
            _outputDevice.EventSent += OnEventSent;
        }

        protected void OnEventReceived(object? sender, MidiEventReceivedEventArgs midiEvent)
        {
            switch (midiEvent.Event)
            {
                case NoteOnEvent noteOnEvent:
                    NoteOnEvent?.Invoke(this, new NoteOnEventArgs(noteOnEvent));
                    break;
                case NoteOffEvent noteOffEvent:
                    NoteOffEvent?.Invoke(this, new NoteOffEventArgs(noteOffEvent));
                    break;
            }
        }

        protected virtual void OnEventSent(object? sender, MidiEventSentEventArgs midiEvent)
        {
            switch (midiEvent.Event)
            {
                case NoteOnEvent noteOnEvent:
                    Console.WriteLine(noteOnEvent.Channel);
                    Console.WriteLine(noteOnEvent.NoteNumber);
                    Console.WriteLine(noteOnEvent.Velocity);
                    break;
            }
        }

        public void Dispose()
        {
            _inputDevice.EventReceived -= OnEventReceived;
            _inputDevice.StopEventsListening();
            _outputDevice.EventSent -= OnEventSent;

            _inputDevice.Dispose();
            _outputDevice.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
