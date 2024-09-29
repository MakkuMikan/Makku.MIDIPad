using Melanchall.DryWetMidi.Core;
using System.Drawing;

namespace Makku.MIDI.LED
{
    /// <summary>
    /// A service for controlling RGB LEDs.
    /// <typeparamref name="TIdent"/> The type of the LED identifier.
    /// </summary>
    public interface ILEDService<TIdent, TColor> : IMIDIService
    {
        TIdent[] AllLEDs { get; }
        TColor DefaultColor { get; }

        /// <summary>
        /// Sets the LED state.
        /// </summary>
        /// <param name="state">The state to set.</param>
        void SetLED(LEDState state);

        /// <summary>
        /// Resets the LED state.
        /// </summary>
        /// <param name="state">The state to reset.</param>
        void ResetLED(TIdent ident);

        /// <summary>
        /// Resets all LEDs.
        /// </summary>
        void ResetAllLEDs();

        public class LEDState(TIdent led, TColor color)
        {
            public TIdent LED { get; set; } = led;

            public TColor Color { get; set; } = color;
        }
    }
}
