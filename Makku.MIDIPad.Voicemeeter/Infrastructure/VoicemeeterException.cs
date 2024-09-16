using AtgDev.Voicemeeter;

namespace Makku.MIDIPad.Voicemeeter.Infrastructure
{
    [Serializable]
    public class VoicemeeterException : Exception
    {
        const string Prefix = "[Voicemeeter]";

        private static string ProcessMessage(RemoteApiWrapper remoteApi, string? message)
        {
            List<string> messageChunks = [Prefix];

            var typeResult = remoteApi.GetVoicemeeterType(out int typeValue);

            if (typeResult != -1)
            {
                messageChunks.Add($"[{(VoicemeeterType)typeValue}]");

                remoteApi.GetVoicemeeterVersion(out int version);

                messageChunks.Add(VoicemeeterHelper.GetVersionString(version));
            }
            else
            {
                messageChunks.Add("[Client not found]");
            }

            if (message != null)
            {
                messageChunks.Add(message);
            }

            return string.Join(" ", messageChunks);
        }

        public VoicemeeterException()
        {
        }

        public VoicemeeterException(string? message) : base(message)
        {
        }

        public VoicemeeterException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public VoicemeeterException(RemoteApiWrapper remoteApi, string? message) : base(ProcessMessage(remoteApi, message))
        {
        }
    }
}