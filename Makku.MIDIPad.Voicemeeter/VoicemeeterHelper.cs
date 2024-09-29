using AtgDev.Voicemeeter;
using AtgDev.Voicemeeter.Utils;
using Makku.MIDIPad.Voicemeeter.Infrastructure;
using Makku.Voicemeeter.Core.Helpers;

namespace Makku.MIDIPad.Voicemeeter
{
    public sealed class VoicemeeterHelper: IDisposable
    {
        private readonly object _lock = new();
        private readonly RemoteApiWrapper _remoteApi;
        public VoicemeeterType Type { get; }
        public string Version { get; }
        public bool Connected => _remoteApi.IsParametersDirty() >= 0;

        public VoicemeeterHelper()
        {
            _remoteApi = new RemoteApiWrapper(PathHelper.GetDllPath());

            lock (_lock)
            {
                _remoteApi.Login();

                lock (_lock)
                    Type = GetVoicemeeterType();
                Version = GetVoicemeeterVersion();
            }
        }

        #region Methods
        #region General Information
        public VoicemeeterType GetVoicemeeterType()
        {
            var result = _remoteApi.GetVoicemeeterType(out int value);

            lock (_lock)
                return result switch
                {
                    0 => (VoicemeeterType)value,
                    -1 => throw new VoicemeeterException(_remoteApi, "Voicemeeter is not running"),
                    -2 => throw new VoicemeeterException(_remoteApi, "No server"),
                    _ => throw new VoicemeeterException(_remoteApi, "Unknown error")
                };
        }

        public string GetVoicemeeterVersion()
        {
            var result = _remoteApi.GetVoicemeeterVersion(out int version);

            lock (_lock)
                return result switch
                {
                    0 => GetVersionString(version),
                    -1 => throw new VoicemeeterException(_remoteApi, "Voicemeeter is not running"),
                    -2 => throw new VoicemeeterException(_remoteApi, "No server"),
                    _ => throw new VoicemeeterException(_remoteApi, "Unknown error")
                };
        }
        #endregion General Information

        #region Levels
        public float GetLevel(LevelType type, int channel)
        {
            if (!IsValidChannel(type, channel))
            {
                throw new ArgumentException("Invalid channel");
            }

            lock (_lock)
            {
                float level;

                var result = type switch
                {
                    LevelType.PreFader => _remoteApi.GetLevel(0, channel, out level),
                    LevelType.PostFader => _remoteApi.GetLevel(1, channel, out level),
                    LevelType.PostMute => _remoteApi.GetLevel(2, channel, out level),
                    LevelType.Output => _remoteApi.GetLevel(3, channel, out level),
                    _ => throw new ArgumentException("Invalid level type")
                };

                return result switch
                {
                    0 => level,
                    -1 => throw new Exception("Falied to get level"),
                    -2 => throw new Exception("Could not connect to Voicemeeter"),
                    -3 => throw new Exception("Level not available"),
                    -4 => throw new Exception("Out of range"),
                    _ => throw new Exception("Unknown error")
                };
            }
        }

        public (float, float) GetStereoLevel(string channel, LevelType type)
        {
            var (leftIndex, rightIndex) = LevelHelpers.GetLevelIndexes(Type, channel);

            lock (_lock)
                return (GetLevel(type, leftIndex), GetLevel(type, rightIndex));
        }
        #endregion Levels

        #region Parameters
        public bool ParametersIsDirty()
        {
            lock (_lock)
                return _remoteApi.IsParametersDirty() switch
                {
                    0 => false,
                    1 => true,
                    -1 => false,
                    -2 => throw new VoicemeeterException(_remoteApi, "Voicemeeter not running"),
                    _ => throw new VoicemeeterException(_remoteApi, "Unknown Error")
                };
        }

        public string GetStringParameter(string parameterName)
        {
            lock (_lock)
                return _remoteApi.GetParameter(parameterName, out string value) switch
                {
                    0 => value,
                    -1 => throw new VoicemeeterException(_remoteApi, "Error getting Parameter"),
                    -2 => throw new VoicemeeterException(_remoteApi, "Voicemeeter not running"),
                    -3 => throw new VoicemeeterException(_remoteApi, "Parameter not found"),
                    -5 => throw new VoicemeeterException(_remoteApi, "Structure mismatch"),
                    _ => throw new VoicemeeterException(_remoteApi, "Unknown Error")
                };
        }

        public float GetFloatParameter(string parameterName)
        {
            lock (_lock)
                return _remoteApi.GetParameter(parameterName, out float value) switch
                {
                    0 => value,
                    -1 => throw new VoicemeeterException(_remoteApi, "Error getting Parameter"),
                    -2 => throw new VoicemeeterException(_remoteApi, "Voicemeeter not running"),
                    -3 => throw new VoicemeeterException(_remoteApi, "Parameter not found"),
                    -5 => throw new VoicemeeterException(_remoteApi, "Structure mismatch"),
                    _ => throw new VoicemeeterException(_remoteApi, "Unknown Error")
                };
        }

        public int SetStringParameter(string parameterName, string value)
        {
            lock (_lock)
                return _remoteApi.SetParameter(parameterName, value) switch
                {
                    0 => 0,
                    -1 => throw new VoicemeeterException(_remoteApi, "Error setting Parameter"),
                    -2 => throw new VoicemeeterException(_remoteApi, "Voicemeeter not running"),
                    -3 => throw new VoicemeeterException(_remoteApi, "Parameter not found"),
                    _ => throw new VoicemeeterException(_remoteApi, "Unknown Error")
                };
        }

        public int SetFloatParameter(string parameterName, float value)
        {
            lock (_lock)
                return _remoteApi.SetParameter(parameterName, value) switch
                {
                    0 => 0,
                    -1 => throw new VoicemeeterException(_remoteApi, "Error setting Parameter"),
                    -2 => throw new VoicemeeterException(_remoteApi, "Voicemeeter not running"),
                    -3 => throw new VoicemeeterException(_remoteApi, "Parameter not found"),
                    _ => throw new VoicemeeterException(_remoteApi, "Unknown Error")
                };
        }
        #endregion Parameters

        #region Macro Buttons
        // TODO: Implement Macro Buttons
        #endregion Macro Buttons
        #endregion Methods

        #region Utilities
        public static string GetVersionString(int version)
        {
            var v1 = (version & 0xFF000000) >> 24;
            var v2 = (version & 0x00FF0000) >> 16;
            var v3 = (version & 0x0000FF00) >> 8;
            var v4 = version & 0x000000FF;

            return $"{v1}.{v2}.{v3}.{v4}";
        }

        private bool IsValidChannel(LevelType type, int channel)
        {
            return channel >= 0 && Type switch
            {
                VoicemeeterType.Voicemeeter => type switch
                {
                    LevelType.PreFader => channel <= 11,
                    LevelType.PostFader => channel <= 11,
                    LevelType.PostMute => channel <= 11,
                    LevelType.Output => channel <= 15,
                    _ => false
                },
                VoicemeeterType.VoicemeeterBanana => type switch
                {
                    LevelType.PreFader => channel <= 21,
                    LevelType.PostFader => channel <= 21,
                    LevelType.PostMute => channel <= 21,
                    LevelType.Output => channel <= 39,
                    _ => false
                },
                VoicemeeterType.VoicemeeterPotato => type switch
                {
                    LevelType.PreFader => channel <= 39,
                    LevelType.PostFader => channel <= 39,
                    LevelType.PostMute => channel <= 39,
                    LevelType.Output => channel <= 63,
                    _ => false
                },
                _ => false
            };
        }
        #endregion Utilities

        public void Dispose()
        {
            lock (_lock)
            {
                _remoteApi.Logout();
            }
        }
    }
}
