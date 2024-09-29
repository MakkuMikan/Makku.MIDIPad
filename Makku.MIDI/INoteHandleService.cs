
namespace Makku.MIDI
{
    public interface INoteHandleService : IDisposable
    {
        event EventHandler<NoteOffEventArgs> NoteOffEvent;
        event EventHandler<NoteOnEventArgs> NoteOnEvent;
    }
}