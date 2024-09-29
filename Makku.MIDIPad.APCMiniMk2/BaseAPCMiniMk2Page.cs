using Makku.MIDI.APCMiniMk2;
using Makku.MIDIPad.Core;
using Melanchall.DryWetMidi.Common;

namespace Makku.MIDIPad.APCMiniMk2
{
    public class BaseAPCMiniMk2Page(APCMiniService apcMini, Action<BasePage<SevenBitNumber, SevenBitNumber, APCMiniService>> changePage) : BasePage<APCMiniService>(apcMini, changePage)
    {
    }
}
