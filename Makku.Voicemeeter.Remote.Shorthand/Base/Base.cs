using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.Voicemeeter.Remote.Shorthand.Base
{
    public class Base
    {
        public string Prefix { get; set; }

        public Base(string prefix)
        {
            Prefix = prefix;
        }
    }
}
