using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDI.APCMiniMk2.Constants
{
    public static class SingleLEDButtonRaw
    {
        public const byte Volume = 0x64;
        public const byte Pan = 0x65;
        public const byte Send = 0x66;
        public const byte Device = 0x67;
        public const byte Up = 0x68;
        public const byte Down = 0x69;
        public const byte Left = 0x6A;
        public const byte Right = 0x6B;

        public const byte ClipStop = 0x70;
        public const byte Solo = 0x71;
        public const byte Mute = 0x72;
        public const byte RecArm = 0x73;
        public const byte Select = 0x74;
        public const byte Drum = 0x75;
        public const byte Note = 0x76;
        public const byte StopAllClips = 0x77;

        public const byte Shift = 0x7A;
    }
}
