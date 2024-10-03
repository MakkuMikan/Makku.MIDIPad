using Makku.MIDI.LED;
using Makku.MIDI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Makku.MIDI.Pad;
using Makku.MIDI.ExtendedLED;
using Melanchall.DryWetMidi.Common;

namespace Makku.MIDIPad.Core
{
    public abstract class ExtendedBasePage<TService>(TService apcMini, Action<IBasePage> changePage) : BasePage<TService>(apcMini, changePage) where TService : IMIDIService, ILEDService, IPadService, IExtendedLEDService
    {
        protected override IExtendedLEDService.ExtendedLEDState GetLEDState(Pad pad, bool on)
        {
            var state = on ? pad.Scheme.OnState : pad.Scheme.OffState;

            return new(pad.Button, state.Colour, state.Behaviour);
        }

        protected override void OnButtonPressed(SevenBitNumber button, bool down = true)
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
}
