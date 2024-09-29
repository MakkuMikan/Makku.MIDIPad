using Makku.MIDI;
using Makku.MIDI.LED;
using Makku.MIDI.Pad;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Makku.MIDIPad.Core;

public abstract class BasePage<TIdent, TColor, TService> : IBasePage where TService : IMIDIService, ILEDService<TIdent, TColor>, IPadService<TIdent>
{
    protected readonly TService Service;
    protected readonly Action<IBasePage> ChangePage;
    protected bool Disposed = false;

    protected virtual Buttons Buttons { get; } = new();

    public BasePage(TService apcMini, Action<IBasePage> changePage)
    {
        Service = apcMini;
        ChangePage = changePage;
    }

    public virtual void OnLoad()
    {
        Service.NoteOnEvent += OnNoteOnEvent;
        Service.NoteOffEvent += OnNoteOffEvent;
    }

    protected virtual void OnUnload()
    {
        Service.NoteOnEvent -= OnNoteOnEvent;
        Service.NoteOffEvent -= OnNoteOffEvent;
    }

    public virtual void Dispose()
    {
        OnUnload();
        GC.SuppressFinalize(this);
    }

    public virtual void Update()
    {
        // Override this method to update the values
    }

    protected List<SevenBitNumber> HeldNotes { get; set; } = [];

    protected bool IsDown(SevenBitNumber value) => HeldNotes.Contains(value);

    private void OnNoteOnEvent(object? _, NoteOnEventArgs e)
    {
        HeldNotes.Add(e.NoteOnEvent.NoteNumber);
        OnButtonPressed(e.NoteOnEvent.NoteNumber);
    }

    private void OnNoteOffEvent(object? _, NoteOffEventArgs e)
    {
        HeldNotes.Remove(e.NoteOffEvent.NoteNumber);
        OnButtonPressed(e.NoteOffEvent.NoteNumber, false);
    }

    protected void OnButtonPressed(SevenBitNumber button, bool down = true)
    {
        if (Buttons.GetPad(button) is Pad pad)
        {
            if (pad.Toggle())
            {
                if (pad.State)
                {
                    
                }
            }
        }
        else if (Buttons.IsSingleLED(button))
        {
            if (Buttons.SingleLEDs.FirstOrDefault(x => x.Button == button) is SingleLED led)
            {
                led.Toggle();
            }
        }
    }
}
