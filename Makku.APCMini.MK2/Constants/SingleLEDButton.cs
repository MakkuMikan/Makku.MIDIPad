using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.APCMini.MK2.Constants
{
    public static class SingleLEDButton
    {
        public static readonly SevenBitNumber Volume = new(SingleLEDButtonRaw.Volume);
        public static readonly SevenBitNumber Pan = new(SingleLEDButtonRaw.Pan);
        public static readonly SevenBitNumber Send = new(SingleLEDButtonRaw.Send);
        public static readonly SevenBitNumber Device = new(SingleLEDButtonRaw.Device);
        public static readonly SevenBitNumber Up = new(SingleLEDButtonRaw.Up);
        public static readonly SevenBitNumber Down = new(SingleLEDButtonRaw.Down);
        public static readonly SevenBitNumber Left = new(SingleLEDButtonRaw.Left);
        public static readonly SevenBitNumber Right = new(SingleLEDButtonRaw.Right);

        public static readonly SevenBitNumber ClipStop = new(SingleLEDButtonRaw.ClipStop);
        public static readonly SevenBitNumber Solo = new(SingleLEDButtonRaw.Solo);
        public static readonly SevenBitNumber Mute = new(SingleLEDButtonRaw.Mute);
        public static readonly SevenBitNumber RecArm = new(SingleLEDButtonRaw.RecArm);
        public static readonly SevenBitNumber Select = new(SingleLEDButtonRaw.Select);
        public static readonly SevenBitNumber Drum = new(SingleLEDButtonRaw.Drum);
        public static readonly SevenBitNumber Note = new(SingleLEDButtonRaw.Note);
        public static readonly SevenBitNumber StopAllClips = new(SingleLEDButtonRaw.StopAllClips);

        public static readonly SevenBitNumber Shift = new(SingleLEDButtonRaw.Shift);
    }
}
