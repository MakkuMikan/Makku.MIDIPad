
namespace Makku.MIDI
{
    public interface IMIDIDeviceService : IDisposable
    {
        event EventHandler<NoteOffEventArgs> NoteOffEvent;
        event EventHandler<NoteOnEventArgs> NoteOnEvent;
    }
}