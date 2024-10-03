using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDIPad.Core
{
    public class Buttons
    {
        public virtual Pads Pads { get; set; } = [];
        public virtual SingleLEDs SingleLEDs { get; set; } = [];

        public bool IsPad(SevenBitNumber button)
        {
            return Pads.Any(x => x.Button == button);
        }

        public bool IsSingleLED(SevenBitNumber button)
        {
            return SingleLEDs.Any(x => x.Button == button);
        }

        public Pad GetPad(SevenBitNumber button)
        {
            return Pads.First(x => x.Button == button);
        }

        public SingleLED GetSingleLED(SevenBitNumber button)
        {
            return SingleLEDs.First(x => x.Button == button);
        }

        public bool Toggle(SevenBitNumber button)
        {
            if (Pads.FirstOrDefault(x => x.Button == button) is Pad pad)
            {
                return pad.Toggle();
            }
            else if (SingleLEDs.FirstOrDefault(x => x.Button == button) is SingleLED led)
            {
                return led.Toggle();
            }
            else
            {
                return false;
            }
        }
    }
}
