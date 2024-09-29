using Melanchall.DryWetMidi.Core;

namespace Makku.MIDI.SingleLED
{
    /// <summary>
    /// A service for controlling a single LED.
    /// A single LED is an LED that can only be one color.
    /// It can have different behaviors.
    /// </summary>
    /// <typeparam name="TIdent">The type of the LED identifier.</typeparam>
    /// <typeparam name="TBehaviour">The type of the LED behavior.</typeparam>
    public interface ISingleLEDService<TIdent, TBehaviour> : IMIDIService
    {
        /// <summary>
        /// A list of all SingleLEDs.
        /// </summary>
        TIdent[] AllSingleLEDs { get; }

        /// <summary>
        /// The default behavior of the LED.
        /// </summary>
        TBehaviour DefaultSingleLEDBehaviour { get; }

        /// <summary>
        /// Sets the LED state.
        /// </summary>
        /// <param name="state">The state to set.</param>
        void SetSingleLED(SingleLEDState state);

        /// <summary>
        /// Resets the LED state.
        /// </summary>
        /// <param name="ident">The identifier of the LED.</param>
        void ResetSingleLED(TIdent ident);

        /// <summary>
        /// Resets all LEDs.
        /// </summary>
        void ResetAllSingleLEDs();

        public class SingleLEDState(TIdent identifier, TBehaviour behaviour)
        {
            public TIdent Identifier { get; } = identifier;

            public TBehaviour Behaviour { get; } = behaviour;
        }
    }
}
