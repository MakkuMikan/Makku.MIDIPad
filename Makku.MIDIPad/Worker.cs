using Makku.MIDI.APCMiniMk2;
using Makku.MIDI.APCMiniMk2.Constants;
using Makku.MIDIPad.Core;
using Makku.MIDIPad.Voicemeeter;

namespace Makku.MIDIPad
{
    public class Worker(ILogger<Worker> logger, APCMiniService apcMini) : BackgroundService
    {
        private IBasePage? MainPage { get; set; }

        public void SetPage(IBasePage page)
        {
            MainPage?.Dispose();

            MainPage = page;
            MainPage.OnLoad();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            apcMini.ResetAllLEDs();
            apcMini.ResetAllSingleLEDs();

            SetPage(new VoicemeeterPage<APCMiniService>(apcMini, SetPage));

            while (!stoppingToken.IsCancellationRequested)
            {
                MainPage?.Update();

                await Task.Delay(100, stoppingToken);
            }

            MainPage?.Dispose();

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker stopped.");
            }

            apcMini.ResetAllLEDs();
            apcMini.ResetAllSingleLEDs();

            apcMini.SetSingleLED(SingleLEDButton.Shift, SingleLEDButtonState.On);

            apcMini.Dispose();
        }
    }
}
