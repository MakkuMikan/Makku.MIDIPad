using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDIPad.Core
{
    public class Pads : List<Pad>
    {
        public bool Toggle(SevenBitNumber button)
        {
            var pad = this.FirstOrDefault(p => p.Button == button);
            if (pad == null)
            {
                pad = new Pad(button, PadScheme.Default);
                Add(pad);
            }

            return pad.Toggle();
        }
    }
}
