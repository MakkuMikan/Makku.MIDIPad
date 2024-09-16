using Makku.APCMini.MK2.Constants;
using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDIPad.Voicemeeter.Helpers
{
    public static class SLEDs
    {
        public static readonly SevenBitNumber InputA2PTM = SingleLEDButton.Volume;
        public static readonly SevenBitNumber InputA1PTM = SingleLEDButton.Pan;
        public static readonly SevenBitNumber InputB1PTM = SingleLEDButton.Send;
        public static readonly SevenBitNumber InputB2PTM = SingleLEDButton.Device;
        public static readonly SevenBitNumber InputB3PTM = SingleLEDButton.Up;

        public static readonly SevenBitNumber OutputA1Mute = SingleLEDButton.Left;
        public static readonly SevenBitNumber OutputA2Mute = SingleLEDButton.Down;
        public static readonly SevenBitNumber OutputA3Mute = SingleLEDButton.Right;

        public static readonly SevenBitNumber RestartVM = SingleLEDButton.ClipStop;

        public static readonly SevenBitNumber Reverb = SingleLEDButton.RecArm;
        public static readonly SevenBitNumber DeepFry = SingleLEDButton.Select;
        public static readonly SevenBitNumber OutputA4Mute = SingleLEDButton.Drum;
        public static readonly SevenBitNumber OutputA5Mute = SingleLEDButton.Note;

        public static readonly SevenBitNumber StopSoundpad = SingleLEDButton.StopAllClips;
    }
}
