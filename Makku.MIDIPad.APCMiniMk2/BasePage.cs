using Makku.MIDI;
using Makku.MIDI.ExtendedLED;
using Makku.MIDI.Pad;
using Makku.MIDI.SingleLED;
using Makku.MIDIPad.Core;
using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDIPad.APCMiniMk2
{
    public class BasePage<TService, TIdent, TColor, TBehaviour, TSingleLEDBehaviour>(TService service, Action<BasePage<TIdent, TColor, TService>> changePage)
        : BasePage<TIdent, TColor, TService>(service, changePage)
        where TService : IMIDIService,
                         IExtendedLEDService<TIdent, TColor, TBehaviour>,
                         ISingleLEDService<TIdent, TSingleLEDBehaviour>,
                         IPadService<TIdent>
    {
        public override void Dispose()
        {
            Service.ResetAllLEDs();
            Service.ResetAllSingleLEDs();

            GC.SuppressFinalize(this);
            base.Dispose();
        }
    }
}
