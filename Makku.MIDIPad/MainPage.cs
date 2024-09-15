using Makku.APCMini.MK2;
using Makku.APCMini.MK2.Constants;
using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makku.MIDIPad;

public class MainPage(APCMiniService apcMini) : BasePage(apcMini)
{
    private static readonly SevenBitNumber MuteBaseColour = Colour.BrightGreen;
    private static readonly SevenBitNumber MuteActiveColour = Colour.Red;

    protected override void OnLoad()
    {

    }
}
