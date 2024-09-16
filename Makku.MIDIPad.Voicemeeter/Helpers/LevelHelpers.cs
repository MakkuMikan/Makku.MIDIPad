using Makku.MIDIPad.Voicemeeter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.Voicemeeter.Core.Helpers
{
    public static class LevelHelpers
    {
        public static int GetBusIndex(VoicemeeterType vmType, string busName)
        {
            return vmType switch
            {
                VoicemeeterType.Voicemeeter => busName switch
                {
                    "A1" => 0,
                    "A2" => 1,
                    "B1" => 2,
                    _ => -1
                },
                VoicemeeterType.VoicemeeterBanana => busName switch
                {
                    "A1" => 0,
                    "A2" => 1,
                    "A3" => 2,
                    "B1" => 3,
                    "B2" => 4,
                    _ => -1
                },
                VoicemeeterType.VoicemeeterPotato => busName switch
                {
                    "A1" => 0,
                    "A2" => 1,
                    "A3" => 2,
                    "A4" => 3,
                    "A5" => 4,
                    "B1" => 5,
                    "B2" => 6,
                    "B3" => 7,
                    _ => -1
                },
                _ => -1
            };
        }

        public static (int Left, int Right) GetLevelIndexes(VoicemeeterType vmType, string channel, bool output = false)
        {
            return vmType switch
            {
                VoicemeeterType.Voicemeeter => channel switch
                {
                    "A1" => output ? (0, 1) : (0, 1),
                    "A2" => output ? (4, 5) : (2, 3),
                    "B1" => output ? (8, 9) : (4, 5),
                    _ => (-1, -1)
                },
                VoicemeeterType.VoicemeeterBanana => channel switch
                {
                    "A1" => output ? (0, 1) : (0, 1),
                    "A2" => output ? (8, 9) : (2, 3),
                    "A3" => output ? (16, 17) : (4, 5),
                    "B1" => output ? (24, 25) : (6, 7),
                    "B2" => output ? (32, 33) : (14, 15),
                    _ => (-1, -1)
                },
                VoicemeeterType.VoicemeeterPotato => channel switch
                {
                    "A1" => output ? (0, 1) : (0, 1),
                    "A2" => output ? (8, 9) : (2, 3),
                    "A3" => output ? (16, 17) : (4, 5),
                    "A4" => output ? (24, 25) : (6, 7),
                    "A5" => output ? (32, 33) : (8, 9),
                    "B1" => output ? (40, 41) : (10, 11),
                    "B2" => output ? (48, 49) : (18, 19),
                    "B3" => output ? (56, 57) : (26, 27),
                    _ => (-1, -1)
                },
                _ => (-1, -1)
            };
        }
    }
}
