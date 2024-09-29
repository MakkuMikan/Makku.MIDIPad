using Makku.MIDI.APCMiniMk2.Constants;
using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDIPad.Core
{
    public class Pad
    {
        public SevenBitNumber Button { get; set; }
        public bool State { get; set; }
        public PadScheme Scheme { get; set; }

        public bool Toggle()
        {
            State = !State;
            return State;
        }

        public Pad(SevenBitNumber button, PadState onState, PadState offState)
        {
            Button = button;
            Scheme = new PadScheme
            {
                OnState = onState,
                OffState = offState
            };
        }

        public Pad(SevenBitNumber button, PadScheme scheme)
        {
            Button = button;
            Scheme = scheme;
        }

        public Pad(SevenBitNumber button)
        {
            Button = button;
            Scheme = PadScheme.Default;
        }

        public Pad(SevenBitNumber button, SevenBitNumber onColour, SevenBitNumber offColour)
        {
            Button = button;
            Scheme = new PadScheme
            {
                OnState = new PadState
                {
                    Colour = onColour,
                    Behaviour = Behaviour.OneHundredPercent
                },
                OffState = new PadState
                {
                    Colour = offColour,
                    Behaviour = Behaviour.OneHundredPercent
                }
            };
        }

        public Pad(SevenBitNumber button, SevenBitNumber colour)
        {
            Button = button;
            Scheme = new PadScheme
            {
                OnState = new PadState
                {
                    Colour = colour,
                    Behaviour = Behaviour.OneHundredPercent
                },
                OffState = new PadState
                {
                    Colour = colour,
                    Behaviour = Behaviour.TenPercent
                }
            };
        }

        public Pad(SevenBitNumber button, SevenBitNumber onColour, FourBitNumber onBehaviour, SevenBitNumber offColour, FourBitNumber offBehaviour)
        {
            Button = button;
            Scheme = new PadScheme
            {
                OnState = new PadState
                {
                    Colour = onColour,
                    Behaviour = onBehaviour
                },
                OffState = new PadState
                {
                    Colour = offColour,
                    Behaviour = offBehaviour
                }
            };
        }
    }
}
