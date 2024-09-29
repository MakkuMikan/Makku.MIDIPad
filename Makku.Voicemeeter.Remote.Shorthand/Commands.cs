using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.Voicemeeter.Remote.Shorthand
{
    public static partial class Voicemeeter
    {
        public static class Commands
        {
            /// <summary>
            /// Shutdown
            /// Set to 1 to shutdown Voicemeeter
            /// Version 1
            /// </summary>
            public static readonly string Shutdown = "Command.Shutdown";

            /// <summary>
            /// Show
            /// Set to 1 to show Voicemeeter
            /// Version 1
            /// </summary>
            public static readonly string Show = "Command.Show";

            /// <summary>
            /// Restart
            /// Set to 1 to restart the audio engine
            /// Version 1
            /// </summary>
            public static readonly string Restart = "Command.Restart";

            /// <summary>
            /// Eject
            /// Set to 1 to eject the cassette
            /// Version 1
            /// </summary>
            public static readonly string Eject = "Command.Eject";

            /// <summary>
            /// Reset
            /// Set to 1 to reset all configurations
            /// Version 1
            /// </summary>
            public static readonly string Reset = "Command.Reset";

            /// <summary>
            /// Save
            /// Set to the complete filename (xml) to save
            /// Version 1
            /// </summary>
            public static readonly string Save = "Command.Save";

            /// <summary>
            /// Load
            /// Set to the complete filename (xml) to load
            /// Version 1
            /// </summary>
            public static readonly string Load = "Command.Load";

            /// <summary>
            /// Lock
            /// Set to 0 or 1 to (un)lock the GUI (Menu option)
            /// Version 1
            /// </summary>
            public static readonly string Lock = "Command.Lock";

            /// <summary>
            /// Button State
            /// Set to 0 or 1 to change the macro button state
            /// Version 1
            /// </summary>
            public static string ButtonState(int index) => $"Command.Button[{index}].State";

            /// <summary>
            /// Button State Only
            /// Set to 0 or 1 to change the button state only
            /// Version 1
            /// </summary>
            public static string ButtonStateOnly(int index) => $"Command.Button[{index}].StateOnly";

            /// <summary>
            /// Button Trigger
            /// Set to 0 or 1 to change the trigger enable state
            /// Version 1
            /// </summary>
            public static string ButtonTrigger(int index) => $"Command.Button[{index}].Trigger";

            /// <summary>
            /// Button Color
            /// Set to 0 to 8 to change the button color
            /// Version 1
            /// </summary>
            public static string ButtonColor(int index) => $"Command.Button[{index}].Color";

            /// <summary>
            /// Dialog Show VBAN-Chat
            /// Set to 0 or 1 to show the VBAN-Chat dialog
            /// Version 1
            /// </summary>
            public static readonly string DialogShowVBANCHAT = "Command.DialogShow.VBANCHAT";

            /// <summary>
            /// Save BUSEQ
            /// Set to the complete filename (xml) to save BUSEQ
            /// Version 2
            /// </summary>
            public static string SaveBUSEQ(int index) => $"Command.SaveBUSEQ[{index}]";

            /// <summary>
            /// Load BUSEQ
            /// Set to the complete filename (xml) to load BUSEQ
            /// Version 2
            /// </summary>
            public static string LoadBUSEQ(int index) => $"Command.LoadBUSEQ[{index}]";

            /// <summary>
            /// Save Strip EQ
            /// Set to the complete filename (xml) to save Strip EQ
            /// Version 3
            /// </summary>
            public static string SaveStripEQ(int index) => $"Command.SaveStripEQ[{index}]";

            /// <summary>
            /// Load Strip EQ
            /// Set to the complete filename (xml) to load Strip EQ
            /// Version 3
            /// </summary>
            public static string LoadStripEQ(int index) => $"Command.LoadStripEQ[{index}]";

            /// <summary>
            /// Preset Recall
            /// Set to 1 to recall the preset scene
            /// Version 1
            /// </summary>
            public static string PresetRecall(int index) => $"Command.Preset[{index}].Recall";
        }
    }
}
