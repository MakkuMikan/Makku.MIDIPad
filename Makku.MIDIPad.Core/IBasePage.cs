using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDIPad.Core
{
    public interface IBasePage : IDisposable
    {
        void OnLoad();
        void Update();
    }
}
