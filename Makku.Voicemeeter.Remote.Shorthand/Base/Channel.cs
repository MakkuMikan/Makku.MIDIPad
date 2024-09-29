using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.Voicemeeter.Common.Base
{
    public abstract class Channel
    {
        public static virtual int Index { get; set; }

        ///<summary>
        ///Mono
        ///Set to 1 for mono, 0 for stereo
        ///</summary>
        public string Mono => $"Strip"
    }
}
