using Makku.APCMini.MK2;
using Makku.APCMini.MK2.Constants;
using Makku.MIDI;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Makku.MIDIPad.Core;

public abstract class BaseDynamicPage<TService> : IDisposable where TService : IPadService
{
    private readonly TService APCMini;
    protected readonly Action<BasePage> ChangePage;
    protected bool Disposed = false;

    private readonly List<PageButton> Buttons;

    public BaseDynamicPage(TService apcMini, Action<BasePage> changePage)
    {
        APCMini = apcMini;
        ChangePage = changePage;

        Buttons = GetPageButtons();
    }

    public virtual void OnLoad()
    {
        APCMini.NoteOnEvent += OnNoteOnEvent;
        APCMini.NoteOffEvent += OnNoteOffEvent;
    }

    protected virtual void OnUnload()
    {
        APCMini.NoteOnEvent -= OnNoteOnEvent;
        APCMini.NoteOffEvent -= OnNoteOffEvent;
    }

    public void Dispose()
    {
        OnUnload();
        APCMini.ResetAllPads();
        APCMini.ResetAllSLEDs();
        GC.SuppressFinalize(this);
    }

    protected virtual List<PageButton> GetPageButtons() => [];

    public virtual void Update()
    {
        foreach (var button in Buttons)
        {
            if (button.GetState())
            {
                PadOn(button.Button, Colour.Green);
            }
            else
            {
                PadOff(button.Button);
            }
        }
    }

    protected virtual void PadOn(SevenBitNumber value, SevenBitNumber colour)
    {
        APCMini.SetLED(PadState.OneHundredPercent, value, colour);
    }

    protected virtual void PadOff(SevenBitNumber value)
    {
        APCMini.ResetLED(value);
    }

    public virtual void SLEDOn(SevenBitNumber value)
    {
        APCMini.SetSLED(value, SingleLEDButtonState.On);
    }

    public virtual void SLEDOff(SevenBitNumber value)
    {
        APCMini.SetSLED(value, SingleLEDButtonState.Off);
    }

    public virtual void SLEDBlink(SevenBitNumber value)
    {
        APCMini.SetSLED(value, SingleLEDButtonState.Blinking);
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
        var pageButton = Buttons.FirstOrDefault(b => b.Button == button);
        if (pageButton == null)
        {
            return;
        }

        if (down)
        {
            pageButton.OnDown?.Invoke();
        }
        else
        {
            pageButton.OnUp?.Invoke();
        }
    }
}
