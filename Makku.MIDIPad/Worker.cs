using Makku.APCMini.MK2;
using Makku.APCMini.MK2.Constants;
using Makku.APCMini.MK2.Helpers;
using Makku.MIDIPad.Core;
using Makku.MIDIPad.Voicemeeter;

namespace Makku.MIDIPad
{
    public class Worker(ILogger<Worker> logger, APCMiniService apcMini) : BackgroundService
    {
        private BasePage? MainPage { get; set; }

        public void SetPage(BasePage page)
        {
            MainPage?.Dispose();

            MainPage = page;
            MainPage.OnLoad();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            apcMini.ResetAllPads();
            apcMini.ResetAllSLEDs();

            SetPage(new VoicemeeterPage(apcMini, SetPage));

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

            apcMini.ResetAllPads();
            apcMini.ResetAllSLEDs();

            apcMini.SetSLED(SingleLEDButton.Shift, SingleLEDButtonState.On);

            apcMini.Dispose();
        }
    }
}
