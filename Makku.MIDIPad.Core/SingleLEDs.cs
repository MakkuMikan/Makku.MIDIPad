using Makku.MIDI.APCMiniMk2.Constants;
using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDIPad.Core
{
    public class SingleLEDs : List<SingleLED>
    {
        public bool Toggle(SevenBitNumber button)
        {
            var led = this.FirstOrDefault(p => p.Button == button);
            if (led == null)
            {
                led = new SingleLED(button);
                Add(led);
            }

            return led.Toggle();
        }
    }
}
