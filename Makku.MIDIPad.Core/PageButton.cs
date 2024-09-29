using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDIPad.Core
{
    public class PageButton
    {
        public virtual SevenBitNumber Button { get; }
        public virtual Action OnDown { get; protected set; }
        public virtual Action? OnUp { get; protected set; }
        public virtual Func<bool> GetState { get; protected set; }

        public PageButton(SevenBitNumber button, Action onDown, Action? onUp, Func<bool> getState)
        {
            Button = button;
            OnDown = onDown;
            OnUp = onUp;
            GetState = getState;
        }

        public PageButton(SevenBitNumber button, Action onDown, Func<bool> getState)
        {
            Button = button;
            OnDown = onDown;
            GetState = getState;
        }

        public PageButton(SevenBitNumber button, Func<bool> getState)
        {
            Button = button;
            GetState = getState;
        }

        public PageButton() { }
    }
}
