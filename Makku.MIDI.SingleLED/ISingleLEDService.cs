using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Makku.MIDI.SingleLED
{
    /// <summary>
    /// A service for controlling a single LED.
    /// A single LED is an LED that can only be one color.
    /// It can have different behaviors.
    /// </summary>
    public interface ISingleLEDService : IMIDIService
    {
        /// <summary>
        /// A list of all SingleLEDs.
        /// </summary>
        SevenBitNumber[] AllSingleLEDs { get; }

        /// <summary>
        /// The default behavior of the LED.
        /// </summary>
        SevenBitNumber DefaultSingleLEDBehaviour { get; }

        /// <summary>
        /// Sets the LED state.
        /// </summary>
        /// <param name="state">The state to set.</param>
        void SetSingleLED(SingleLEDState state);

        /// <summary>
        /// Resets the LED state.
        /// </summary>
        /// <param name="ident">The identifier of the LED.</param>
        void ResetSingleLED(SevenBitNumber ident);

        /// <summary>
        /// Resets all LEDs.
        /// </summary>
        void ResetAllSingleLEDs();

        public class SingleLEDState(SevenBitNumber identifier, SevenBitNumber behaviour)
        {
            public SevenBitNumber Identifier { get; } = identifier;

            public SevenBitNumber Behaviour { get; } = behaviour;
        }
    }
}
