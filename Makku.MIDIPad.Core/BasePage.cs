using Makku.MIDI;
using Makku.MIDI.LED;
using Makku.MIDI.Pad;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Makku.MIDIPad.Core;

public abstract class BasePage<TService> : IBasePage where TService : IMIDIService, ILEDService, IPadService
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
        if (Disposed)
        {
            return;
        }

        foreach (var pad in Buttons.Pads)
        {
            var newState = pad.Load();

            if (newState != pad.State)
            {
                pad.State = newState;

                // No need to trigger the WhenOn/WhenOff events here, as we are only updating the state of the pads, not from pad input.
            }
        }

        foreach (var led in Buttons.SingleLEDs)
        {
            var newState = led.Load();

            if (newState != led.State)
            {
                led.State = newState;

                // No need to trigger the WhenOn/WhenOff events here, as we are only updating the state of the pads, not from pad input.
            }
        }
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

    protected virtual ILEDService.LEDState GetLEDState(Pad pad, bool on)
    {
        return new(pad.Button, on ? pad.Scheme.OnState.Colour : pad.Scheme.OffState.Colour);
    }

    protected virtual void OnButtonPressed(SevenBitNumber button, bool down = true)
    {
        if (Buttons.GetPad(button) is Pad pad)
        {
            if (pad.Toggle())
            {
                if (pad.State)
                {
                    Service.SetLED(GetLEDState(pad, true));
                    pad.WhenOn();
                }
                else
                {
                    Service.SetLED(GetLEDState(pad, false));
                    pad.WhenOff();
                }
            }
        }
    }
}
