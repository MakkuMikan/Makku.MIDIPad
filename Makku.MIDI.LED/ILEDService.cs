using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using System.Drawing;

namespace Makku.MIDI.LED
{
    /// <summary>
    /// A service for controlling RGB LEDs.
    /// </summary>
    public interface ILEDService : IMIDIService
    {
        SevenBitNumber[] AllLEDs { get; }
        SevenBitNumber DefaulSevenBitNumber { get; }

        /// <summary>
        /// Sets the LED state.
        /// </summary>
        /// <param name="state">The state to set.</param>
        void SetLED(LEDState state);

        /// <summary>
        /// Resets the LED state.
        /// </summary>
        /// <param name="state">The state to reset.</param>
        void ResetLED(SevenBitNumber ident);

        /// <summary>
        /// Resets all LEDs.
        /// </summary>
        void ResetAllLEDs();

        public class LEDState(SevenBitNumber led, SevenBitNumber color)
        {
            public SevenBitNumber LED { get; set; } = led;

            public SevenBitNumber Color { get; set; } = color;
        }
    }
}
