using Makku.APCMini.MK2.Constants;
using Makku.APCMini.MK2.Helpers;
using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDIPad.Voicemeeter.Helpers
{
    public class SLEDStates : Dictionary<SevenBitNumber, bool>
    {
        public bool IsOn(SevenBitNumber key)
        {
            if (!ContainsKey(key))
            {
                this[key] = false;
            }

            return this[key];
        }

        public SingleLEDState? Set(SevenBitNumber key, bool value)
        {
            if (ContainsKey(key) && this[key] == value)
            {
                return null;
            }

            this[key] = value;

            return value ? On(key) : Off(key);
        }

        private SingleLEDState On(SevenBitNumber key)
        {
            return SingleLEDState.SolidOn(key);
        }

        private SingleLEDState Off(SevenBitNumber key)
        {
            return SingleLEDState.SolidOff(key);
        }

        public SingleLEDState Toggle(SevenBitNumber key)
        {
            if (IsOn(key))
            {
                this[key] = false;
                return Off(key);
            }
            else
            {
                this[key] = true;
                return On(key);
            }
        }
    }
}
