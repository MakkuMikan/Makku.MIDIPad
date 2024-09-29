using Makku.APCMini.MK2.Helpers;
using Makku.MIDI;
using Melanchall.DryWetMidi.Common;

namespace Makku.APCMini.MK2
{
    public interface IPadService : IMIDIDeviceService
    {
        void ResetAllPads();
        void ResetAllSLEDs();
        void ResetLED(MatrixPadState state);
        void ResetLED(SevenBitNumber value);
        void ResetSLED(SevenBitNumber value);
        void SetLED(FourBitNumber behaviour, SevenBitNumber value, SevenBitNumber colour);
        void SetLED(MatrixPadState state);
        void SetMultipleLEDs(FourBitNumber behaviour, SevenBitNumber[] values, SevenBitNumber colour);
        void SetMultipleSLEDs(SevenBitNumber behaviour, SevenBitNumber[] values);
        void SetSLED(SevenBitNumber value, SevenBitNumber behaviour);
        void SetSLED(SingleLEDState state);
    }
}