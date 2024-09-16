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
    public class PadStates : Dictionary<SevenBitNumber, bool>
    {
        #region Colours
        private static readonly PartialMatrixPadState MuteActive = MatrixPadState.FromColour(Colour.BrightRed).WithBehaviour(PadState.OneHundredPercent);
        private static readonly PartialMatrixPadState MuteInactive = MatrixPadState.FromColour(Colour.BrightGreen).WithBehaviour(PadState.OneHundredPercent);

        private static readonly PartialMatrixPadState RecordActive = MatrixPadState.FromColour(Colour.BrightMagenta).WithBehaviour(PadState.OneHundredPercent);
        private static readonly PartialMatrixPadState RecordInactive = MatrixPadState.FromColour(Colour.BrightMagenta).WithBehaviour(PadState.TenPercent);

        private static readonly PartialMatrixPadState AltMicActive = MatrixPadState.FromColour(Colour.BrightYellow).WithBehaviour(PadState.OneHundredPercent);
        private static readonly PartialMatrixPadState AltMicInactive = MatrixPadState.FromColour(Colour.BrightYellow).WithBehaviour(PadState.TenPercent);

        private static readonly PartialMatrixPadState MicActive = MatrixPadState.FromColour(Colour.BrightBlue).WithBehaviour(PadState.OneHundredPercent);
        private static readonly PartialMatrixPadState MicInactive = MatrixPadState.FromColour(Colour.BrightBlue).WithBehaviour(PadState.TenPercent);

        private static readonly PartialMatrixPadState SpeakersActive = MatrixPadState.FromColour(Colour.BrightLimeGreen).WithBehaviour(PadState.OneHundredPercent);
        private static readonly PartialMatrixPadState SpeakersInactive = MatrixPadState.FromColour(Colour.BrightLimeGreen).WithBehaviour(PadState.TenPercent);

        private static readonly PartialMatrixPadState HeadphonesActive = MatrixPadState.FromColour(Colour.BrightOrange).WithBehaviour(PadState.OneHundredPercent);
        private static readonly PartialMatrixPadState HeadphonesInactive = MatrixPadState.FromColour(Colour.BrightOrange).WithBehaviour(PadState.TenPercent);

        private static readonly PartialMatrixPadState NvidiaBroadcastToggleActive = MatrixPadState.FromColour(Colour.BrightGreen).WithBehaviour(PadState.OneHundredPercent);
        private static readonly PartialMatrixPadState NvidiaBroadcastToggleInactive = MatrixPadState.FromColour(Colour.BrightGreen).WithBehaviour(PadState.TenPercent);
        #endregion Colours

        #region Pad Groups

        private static readonly SevenBitNumber[] MutePads = [
            Pads.InputA1Mute,
            Pads.InputA2Mute,
            Pads.InputB1Mute,
            Pads.InputB2Mute,
            Pads.InputB3Mute,
        ];

        private static readonly SevenBitNumber[] RecordPads = [
            Pads.InputA2Record,
            Pads.InputA1Record,
            Pads.InputB1Record,
            Pads.InputB2Record,
            Pads.InputB3Record,
        ];

        private static readonly SevenBitNumber[] AltMicPads = [
            Pads.InputA2AltMic,
            Pads.InputA1AltMic,
            Pads.InputB1AltMic,
            Pads.InputB2AltMic,
            Pads.InputB3AltMic,
        ];

        private static readonly SevenBitNumber[] MicPads = [
            Pads.InputA2Mic,
            Pads.InputA1Mic,
            Pads.InputB1Mic,
            Pads.InputB2Mic,
            Pads.InputB3Mic,
        ];

        private static readonly SevenBitNumber[] SpeakersPads = [
            Pads.InputA2Speakers,
            Pads.InputA1Speakers,
            Pads.InputB1Speakers,
            Pads.InputB2Speakers,
            Pads.InputB3Speakers,
        ];

        private static readonly SevenBitNumber[] HeadphonesPads = [
            Pads.InputA2Headphones,
            Pads.InputA1Headphones,
            Pads.InputB1Headphones,
            Pads.InputB2Headphones,
            Pads.InputB3Headphones,
        ];

        private static readonly SevenBitNumber[] NvidiaBroadcastTogglePads = [
            Pads.NvidiaBroadcastToggle,
        ];
        #endregion Pad Groups

        public MatrixPadState? Set(SevenBitNumber key, bool value)
        {
            if (ContainsKey(key) && this[key] == value)
            {
                return null;
            }

            this[key] = value;
            
            if (value)
            {
                return On(key);
            }
            else
            {
                return Off(key);
            }
        }

        public bool IsOn(SevenBitNumber key)
        {
            return this[key];
        }

        private MatrixPadState On(SevenBitNumber key)
        {
            if (MutePads.Contains(key))
            {
                return MuteActive.WithValue(key);
            }
            else if (RecordPads.Contains(key))
            {
                return RecordActive.WithValue(key);
            }
            else if (AltMicPads.Contains(key))
            {
                return AltMicActive.WithValue(key);
            }
            else if (MicPads.Contains(key))
            {
                return MicActive.WithValue(key);
            }
            else if (SpeakersPads.Contains(key))
            {
                return SpeakersActive.WithValue(key);
            }
            else if (HeadphonesPads.Contains(key))
            {
                return HeadphonesActive.WithValue(key);
            }
            else if (NvidiaBroadcastTogglePads.Contains(key))
            {
                return NvidiaBroadcastToggleActive.WithValue(key);
            }
            else
            {
                throw new ArgumentException("Invalid key");
            }
        }

        private MatrixPadState Off(SevenBitNumber key)
        {
            if (MutePads.Contains(key))
            {
                return MuteInactive.WithValue(key);
            }
            else if (RecordPads.Contains(key))
            {
                return RecordInactive.WithValue(key);
            }
            else if (AltMicPads.Contains(key))
            {
                return AltMicInactive.WithValue(key);
            }
            else if (MicPads.Contains(key))
            {
                return MicInactive.WithValue(key);
            }
            else if (SpeakersPads.Contains(key))
            {
                return SpeakersInactive.WithValue(key);
            }
            else if (HeadphonesPads.Contains(key))
            {
                return HeadphonesInactive.WithValue(key);
            }
            else if (NvidiaBroadcastTogglePads.Contains(key))
            {
                return NvidiaBroadcastToggleInactive.WithValue(key);
            }
            else
            {
                throw new ArgumentException("Invalid key");
            }
        }

        public MatrixPadState Toggle(SevenBitNumber key)
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
