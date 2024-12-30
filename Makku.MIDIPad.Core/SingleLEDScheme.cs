using Makku.MIDI.APCMiniMk2.Constants;
using Melanchall.DryWetMidi.Common;

namespace Makku.MIDIPad.Core
{
    public struct SingleLEDScheme
    {
        public SevenBitNumber OnBehaviour { get; set; }
        public SevenBitNumber OffBehaviour { get; set; }
        public SingleLEDTouchBehaviour TouchBehaviour { get; set; }

        public static SingleLEDScheme Default => new()
        {
            OnBehaviour = SingleLEDButtonState.On,
            OffBehaviour = SingleLEDButtonState.Off,
            TouchBehaviour = SingleLEDTouchBehaviour.Toggle
        };

        public enum SingleLEDTouchBehaviour
        {
            Toggle,
            Hold
        }
    }
}
