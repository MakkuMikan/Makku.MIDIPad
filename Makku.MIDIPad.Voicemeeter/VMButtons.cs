using Makku.MIDIPad.Core;
using Melanchall.DryWetMidi.Common;
using System.Text.RegularExpressions;

namespace Makku.MIDIPad.Voicemeeter
{
    public class VMButtons : Buttons
    {
        public Buttons Base => this;

        public VMPads VMPads { get; set; } = [];

        public VMSingleLEDs VMSingleLEDs { get; set; } = [];

        public override Pads Pads => VMPads;

        public override SingleLEDs SingleLEDs => VMSingleLEDs;
    }

    public class VMPads : List<VMPad>
    {
        public static implicit operator Pads(VMPads pads)
        {
            return [.. pads];
        }
    }

    public class VMPad : Pad
    {
        private readonly VoicemeeterHelper _voicemeeterHelper;

        public Pad Base => this;

        public KeyValuePair<string, object>? LoadParameter { get; set; }

        public Dictionary<string, object> OnValues { get; set; } = [];
        public Dictionary<string, object> OffValues { get; set; } = [];

        public override bool Load()
        {
            if ((LoadParameter?.Value ?? OnValues.First().Value) is string strValue)
            {
                return _voicemeeterHelper.GetStringParameter(LoadParameter?.Key ?? OnValues.First().Key) == strValue;
            }
            else if ((LoadParameter?.Value ?? OnValues.First().Value) is float fltValue)
            {
                return _voicemeeterHelper.GetFloatParameter(LoadParameter?.Key ?? OnValues.First().Key) == fltValue;
            }
            else
            {
                return false;
            }
        }

        public override void WhenOn()
        {
            foreach (var value in OnValues)
            {
                if (value.Value is string intValue)
                {
                    _voicemeeterHelper.SetStringParameter(value.Key, intValue);
                }
                else if (value.Value is float floatValue)
                {
                    _voicemeeterHelper.SetFloatParameter(value.Key, floatValue);
                }
            }
        }

        public override void WhenOff()
        {
            foreach (var value in OffValues)
            {
                if (value.Value is string intValue)
                {
                    _voicemeeterHelper.SetStringParameter(value.Key, intValue);
                }
                else if (value.Value is float floatValue)
                {
                    _voicemeeterHelper.SetFloatParameter(value.Key, floatValue);
                }
            }
        }

        public VMPad(VoicemeeterHelper vmHelper, SevenBitNumber button, PadScheme scheme) : base(button, scheme)
        {
            _voicemeeterHelper = vmHelper;
        }

        public VMPad(VoicemeeterHelper vmHelper, SevenBitNumber button) : base(button)
        {
            _voicemeeterHelper = vmHelper;
        }

        public VMPad(VoicemeeterHelper vmHelper, SevenBitNumber button, SevenBitNumber onColour, SevenBitNumber offColour) : base(button, onColour, offColour)
        {
            _voicemeeterHelper = vmHelper;
        }

        public VMPad(VoicemeeterHelper vmHelper, SevenBitNumber button, SevenBitNumber colour) : base(button, colour)
        {
            _voicemeeterHelper = vmHelper;
        }
    }

    public static class VoicemeeterHelperExtensions
    {
        public static VMPad Pad(this VoicemeeterHelper vmHelper, SevenBitNumber button, PadScheme scheme, Dictionary<string, object> onValues, Dictionary<string, object> offValues)
        {
            return new VMPad(vmHelper, button, scheme)
            {
                OnValues = onValues,
                OffValues = offValues
            };
        }

        public static VMPad Pad(this VoicemeeterHelper vmHelper, SevenBitNumber button, SevenBitNumber colour, Dictionary<string, object> onValues, Dictionary<string, object> offValues)
        {
            return new VMPad(vmHelper, button, colour)
            {
                OnValues = onValues,
                OffValues = offValues
            };
        }

        public static VMPad Pad(this VoicemeeterHelper vmHelper, SevenBitNumber button, Dictionary<string, object> onValues, Dictionary<string, object> offValues)
        {
            return new VMPad(vmHelper, button)
            {
                OnValues = onValues,
                OffValues = offValues
            };
        }

        public static VMPad Pad(this VoicemeeterHelper vmHelper, SevenBitNumber button, PadScheme scheme, string[] parameters)
        {
            var onValues = parameters.ToDictionary(x => x, x => (object)1);
            var offValues = parameters.ToDictionary(x => x, x => (object)0);

            return new VMPad(vmHelper, button, scheme)
            {
                OnValues = onValues,
                OffValues = offValues
            };
        }

        private static string[] ParseParameterPattern(string pattern)
        {
            // We will take in a pattern like "Strip[{1-3}].Mute" and return ["Strip[1].Mute", "Strip[2].Mute", "Strip[3].Mute"]
            // Or a pattern like "Strip[0].{A1,A2,A3} and return ["Strip[0].A1", "Strip[0].A2", "Strip[0].A3"]
            // Or a pattern like "Strip[0].A{1-3}" and return ["Strip[0].A1", "Strip[0].A2", "Strip[0].A3"]
            // You cannot, however, mix and match ranges and values like "Strip[{1-3,5}].Mute" as this is too complex for this simple parser

            List<string> outputs = [];

            var matches = Regex.Matches(pattern, @"(?<={).*?(?=})");

            foreach (Match match in matches)
            {
                if (match.Value.Split('-').Length > 1)
                {
                    var range = match.Value.Split('-').Select(int.Parse).ToArray();

                    for (int i = range[0]; i <= range[1]; i++)
                    {
                        outputs.Add(pattern.Replace($"{{{match.Value}}}", i.ToString()));
                    }
                }
                else if (match.Value.Split(',').Length > 1)
                {
                    var values = match.Value.Split(',');

                    foreach (var value in values)
                    {
                        outputs.Add(pattern.Replace($"{{{match.Value}}}", value));
                    }
                }
            }

            return [.. outputs];
        }

        public static VMPad Pad(this VoicemeeterHelper vmHelper, SevenBitNumber button, PadScheme scheme, string pattern)
        {
            var parameters = ParseParameterPattern(pattern);

            return Pad(vmHelper, button, scheme, parameters);
        }

        public static VMPad Pad0(this VoicemeeterHelper vmHelper, SevenBitNumber button, PadScheme scheme, string pattern)
        {
            var pad = Pad(vmHelper, button, scheme, pattern);

            pad.State = false;

            return pad;
        }

        public static VMPad Pad1(this VoicemeeterHelper vmHelper, SevenBitNumber button, PadScheme scheme, string pattern)
        {
            var pad = Pad(vmHelper, button, scheme, pattern);

            pad.State = true;

            return pad;
        }

        public static VMPad Pad(this VoicemeeterHelper vmHelper, SevenBitNumber button, PadScheme scheme, object onValue, object offValue, string[] parameters)
        {
            var onValues = parameters.ToDictionary(x => x, x => onValue);
            var offValues = parameters.ToDictionary(x => x, x => offValue);

            return new VMPad(vmHelper, button, scheme)
            {
                OnValues = onValues,
                OffValues = offValues
            };
        }

        public static VMPad Pad0(this VoicemeeterHelper vmHelper, SevenBitNumber button, PadScheme scheme, object onValue, object offValue, string[] parameters)
        {
            var pad = Pad(vmHelper, button, scheme, onValue, offValue, parameters);

            pad.State = false;

            return pad;
        }

        public static VMPad Pad1(this VoicemeeterHelper vmHelper, SevenBitNumber button, PadScheme scheme, object onValue, object offValue, string[] parameters)
        {
            var pad = Pad(vmHelper, button, scheme, onValue, offValue, parameters);

            pad.State = true;

            return pad;
        }

        public static VMPad Pad(this VoicemeeterHelper vmHelper, SevenBitNumber button, SevenBitNumber colour, string pattern)
        {
            var parameters = ParseParameterPattern(pattern);

            return Pad(vmHelper, button, colour, parameters);
        }

        public static VMPad Pad0(this VoicemeeterHelper vmHelper, SevenBitNumber button, SevenBitNumber colour, string pattern)
        {
            var pad = Pad(vmHelper, button, colour, pattern);

            pad.State = false;

            return pad;
        }

        public static VMPad Pad1(this VoicemeeterHelper vmHelper, SevenBitNumber button, SevenBitNumber colour, string pattern)
        {
            var pad = Pad(vmHelper, button, colour, pattern);

            pad.State = true;

            return pad;
        }

        public static VMPad Pad(this VoicemeeterHelper vmHelper, SevenBitNumber button, SevenBitNumber colour, string[] parameters)
        {
            var onValues = parameters.ToDictionary(x => x, x => (object)1);
            var offValues = parameters.ToDictionary(x => x, x => (object)0);

            return new VMPad(vmHelper, button, colour)
            {
                OnValues = onValues,
                OffValues = offValues
            };
        }

        public static VMPad Pad0(this VoicemeeterHelper vmHelper, SevenBitNumber button, SevenBitNumber colour, string[] parameters)
        {
            var pad = Pad(vmHelper, button, colour, parameters);

            pad.State = false;

            return pad;
        }

        public static VMPad Pad1(this VoicemeeterHelper vmHelper, SevenBitNumber button, SevenBitNumber colour, string[] parameters)
        {
            var pad = Pad(vmHelper, button, colour, parameters);

            pad.State = true;

            return pad;
        }

        public static VMPad Pad(this VoicemeeterHelper vmHelper, SevenBitNumber button, SevenBitNumber colour, object onValue, object offValue, string[] parameters)
        {
            var onValues = parameters.ToDictionary(x => x, x => onValue);
            var offValues = parameters.ToDictionary(x => x, x => offValue);

            return new VMPad(vmHelper, button, colour)
            {
                OnValues = onValues,
                OffValues = offValues
            };
        }

        public static VMPad Pad0(this VoicemeeterHelper vmHelper, SevenBitNumber button, SevenBitNumber colour, object onValue, object offValue, string[] parameters)
        {
            var pad = Pad(vmHelper, button, colour, onValue, offValue, parameters);

            pad.State = false;

            return pad;
        }

        public static VMPad Pad1(this VoicemeeterHelper vmHelper, SevenBitNumber button, SevenBitNumber colour, object onValue, object offValue, string[] parameters)
        {
            var pad = Pad(vmHelper, button, colour, onValue, offValue, parameters);

            pad.State = true;

            return pad;
        }

        public static VMPad Pad(this VoicemeeterHelper vmHelper, SevenBitNumber button, string pattern)
        {
            var parameters = ParseParameterPattern(pattern);

            return Pad(vmHelper, button, parameters);
        }

        public static VMPad Pad0(this VoicemeeterHelper vmHelper, SevenBitNumber button, string pattern)
        {
            var pad = Pad(vmHelper, button, pattern);

            pad.State = false;

            return pad;
        }

        public static VMPad Pad1(this VoicemeeterHelper vmHelper, SevenBitNumber button, string pattern)
        {
            var pad = Pad(vmHelper, button, pattern);

            pad.State = true;

            return pad;
        }

        public static VMPad Pad(this VoicemeeterHelper vmHelper, SevenBitNumber button, string[] parameters)
        {
            var onValues = parameters.ToDictionary(x => x, x => (object)1);
            var offValues = parameters.ToDictionary(x => x, x => (object)0);

            return new VMPad(vmHelper, button)
            {
                OnValues = onValues,
                OffValues = offValues
            };
        }

        public static VMPad Pad0(this VoicemeeterHelper vmHelper, SevenBitNumber button, string[] parameters)
        {
            var pad = Pad(vmHelper, button, parameters);

            pad.State = false;

            return pad;
        }

        public static VMPad Pad1(this VoicemeeterHelper vmHelper, SevenBitNumber button, string[] parameters)
        {
            var pad = Pad(vmHelper, button, parameters);

            pad.State = true;

            return pad;
        }

        public static VMPad Pad(this VoicemeeterHelper vmHelper, SevenBitNumber button, object onValue, object offValue, string[] parameters)
        {
            var onValues = parameters.ToDictionary(x => x, x => onValue);
            var offValues = parameters.ToDictionary(x => x, x => offValue);

            return new VMPad(vmHelper, button)
            {
                OnValues = onValues,
                OffValues = offValues
            };
        }

        public static VMPad Pad0(this VoicemeeterHelper vmHelper, SevenBitNumber button, object onValue, object offValue, string[] parameters)
        {
            var pad = Pad(vmHelper, button, onValue, offValue, parameters);

            pad.State = false;

            return pad;
        }

        public static VMPad Pad1(this VoicemeeterHelper vmHelper, SevenBitNumber button, object onValue, object offValue, string[] parameters)
        {
            var pad = Pad(vmHelper, button, onValue, offValue, parameters);

            pad.State = true;

            return pad;
        }

        public static VMSingleLED SLED(this VoicemeeterHelper vmHelper, SevenBitNumber button, SingleLEDScheme scheme)
        {
            return new VMSingleLED(vmHelper, button, scheme);
        }

        public static VMSingleLED SLED(this VoicemeeterHelper vmHelper, SevenBitNumber button)
        {
            return new VMSingleLED(vmHelper, button);
        }

        public static VMSingleLED SLED(this VoicemeeterHelper vmHelper, SevenBitNumber button, Dictionary<string, object> onValues, Dictionary<string, object> offValues)
        {
            return new VMSingleLED(vmHelper, button)
            {
                OnValues = onValues,
                OffValues = offValues
            };
        }

        public static VMSingleLED SLED(this VoicemeeterHelper vmHelper, SevenBitNumber button, SingleLEDScheme scheme, Dictionary<string, object> onValues, Dictionary<string, object> offValues)
        {
            return new VMSingleLED(vmHelper, button, scheme)
            {
                OnValues = onValues,
                OffValues = offValues
            };
        }

        public static VMSingleLED SLED(this VoicemeeterHelper vmHelper, SevenBitNumber button, string pattern)
        {
            var parameters = ParseParameterPattern(pattern);

            return SLED(vmHelper, button, parameters);
        }

        public static VMSingleLED SLED(this VoicemeeterHelper vmHelper, SevenBitNumber button, string[] parameters)
        {
            var onValues = parameters.ToDictionary(x => x, x => (object)1);
            var offValues = parameters.ToDictionary(x => x, x => (object)0);

            return new VMSingleLED(vmHelper, button)
            {
                OnValues = onValues,
                OffValues = offValues
            };
        }

        public static VMSingleLED SLED(this VoicemeeterHelper vmHelper, SevenBitNumber button, object onValue, object offValue, string[] parameters)
        {
            var onValues = parameters.ToDictionary(x => x, x => onValue);
            var offValues = parameters.ToDictionary(x => x, x => offValue);

            return new VMSingleLED(vmHelper, button)
            {
                OnValues = onValues,
                OffValues = offValues
            };
        }

        public static VMSingleLED SLED(this VoicemeeterHelper vmHelper, SevenBitNumber button, SingleLEDScheme scheme, string pattern)
        {
            var parameters = ParseParameterPattern(pattern);

            return SLED(vmHelper, button, scheme, parameters);
        }

        public static VMSingleLED SLED(this VoicemeeterHelper vmHelper, SevenBitNumber button, SingleLEDScheme scheme, string[] parameters)
        {
            var onValues = parameters.ToDictionary(x => x, x => (object)1);
            var offValues = parameters.ToDictionary(x => x, x => (object)0);

            return new VMSingleLED(vmHelper, button, scheme)
            {
                OnValues = onValues,
                OffValues = offValues
            };
        }
    }

    public class VMSingleLEDs : List<VMSingleLED>
    {
        public static implicit operator SingleLEDs(VMSingleLEDs leds)
        {
            return [.. leds];
        }
    }

    public class VMSingleLED : SingleLED
    {
        private readonly VoicemeeterHelper _voicemeeterHelper;

        public SingleLED Base => this;

        public KeyValuePair<string, object>? LoadParameter { get; set; }

        public Dictionary<string, object> OnValues { get; set; } = [];
        public Dictionary<string, object> OffValues { get; set; } = [];

        public List<Action> OnActions { get; set; } = [];
        public List<Action> OffActions { get; set; } = [];

        public override bool Load()
        {
            if ((LoadParameter?.Value ?? OnValues.First().Value) is string strValue)
            {
                return _voicemeeterHelper.GetStringParameter(LoadParameter?.Key ?? OnValues.First().Key) == strValue;
            }
            else if ((LoadParameter?.Value ?? OnValues.First().Value) is float fltValue)
            {
                return _voicemeeterHelper.GetFloatParameter(LoadParameter?.Key ?? OnValues.First().Key) == fltValue;
            }
            else
            {
                return false;
            }
        }

        public override void WhenOn()
        {
            foreach (var value in OnValues)
            {
                if (value.Value is string intValue)
                {
                    _voicemeeterHelper.SetStringParameter(value.Key, intValue);
                }
                else if (value.Value is float floatValue)
                {
                    _voicemeeterHelper.SetFloatParameter(value.Key, floatValue);
                }
            }

            foreach (var action in OnActions)
            {
                action();
            }
        }

        public override void WhenOff()
        {
            foreach (var value in OffValues)
            {
                if (value.Value is string intValue)
                {
                    _voicemeeterHelper.SetStringParameter(value.Key, intValue);
                }
                else if (value.Value is float floatValue)
                {
                    _voicemeeterHelper.SetFloatParameter(value.Key, floatValue);
                }
            }

            foreach (var action in OffActions)
            {
                action();
            }
        }

        public VMSingleLED(VoicemeeterHelper vmHelper, SevenBitNumber button, SingleLEDScheme scheme) : base(button, scheme)
        {
            _voicemeeterHelper = vmHelper;
        }

        public VMSingleLED(VoicemeeterHelper vmHelper, SevenBitNumber button) : base(button)
        {
            _voicemeeterHelper = vmHelper;
        }

        public VMSingleLED WithOnAction(Action action)
        {
            OnActions.Add(action);

            return this;
        }

        public VMSingleLED WithOffAction(Action action)
        {
            OffActions.Add(action);

            return this;
        }
    }
}