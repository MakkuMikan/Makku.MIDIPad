using Makku.APCMini.MK2.Constants;
using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDIPad.Voicemeeter.Helpers
{
    public static class Pads
    {
        #region Mute Pads
        public static readonly SevenBitNumber InputA1Mute = Button.B1;
        public static readonly SevenBitNumber InputA2Mute = Button.A1;
        public static readonly SevenBitNumber InputB1Mute = Button.C1;
        public static readonly SevenBitNumber InputB2Mute = Button.D1;
        public static readonly SevenBitNumber InputB3Mute = Button.E1;
        #endregion Mute Pads

        #region Record Pads
        public static readonly SevenBitNumber InputA1Record = Button.B2;
        public static readonly SevenBitNumber InputA2Record = Button.A2;
        public static readonly SevenBitNumber InputB1Record = Button.C2;
        public static readonly SevenBitNumber InputB2Record = Button.D2;
        public static readonly SevenBitNumber InputB3Record = Button.E2;
        #endregion Record Pads

        #region Alt Mic Pads
        public static readonly SevenBitNumber InputA1AltMic = Button.B3;
        public static readonly SevenBitNumber InputA2AltMic = Button.A3;
        public static readonly SevenBitNumber InputB1AltMic = Button.C3;
        public static readonly SevenBitNumber InputB2AltMic = Button.D3;
        public static readonly SevenBitNumber InputB3AltMic = Button.E3;
        #endregion Alt Mic Pads

        #region Mic Pads
        public static readonly SevenBitNumber InputA1Mic = Button.B4;
        public static readonly SevenBitNumber InputA2Mic = Button.A4;
        public static readonly SevenBitNumber InputB1Mic = Button.C4;
        public static readonly SevenBitNumber InputB2Mic = Button.D4;
        public static readonly SevenBitNumber InputB3Mic = Button.E4;
        #endregion Mic Pads

        #region Speakers Pads
        public static readonly SevenBitNumber InputA1Speakers = Button.B5;
        public static readonly SevenBitNumber InputA2Speakers = Button.A5;
        public static readonly SevenBitNumber InputB1Speakers = Button.C5;
        public static readonly SevenBitNumber InputB2Speakers = Button.D5;
        public static readonly SevenBitNumber InputB3Speakers = Button.E5;
        #endregion Speakers Pads

        #region Headphones Pads
        public static readonly SevenBitNumber InputA1Headphones = Button.B6;
        public static readonly SevenBitNumber InputA2Headphones = Button.A6;
        public static readonly SevenBitNumber InputB1Headphones = Button.C6;
        public static readonly SevenBitNumber InputB2Headphones = Button.D6;
        public static readonly SevenBitNumber InputB3Headphones = Button.E6;
        #endregion Headphones Pads

        #region Nvidia Broadcast Toggle Pads
        public static readonly SevenBitNumber NvidiaBroadcastToggle = Button.A8;
        #endregion Nvidia Broadcast Toggle Pads
    }
}
