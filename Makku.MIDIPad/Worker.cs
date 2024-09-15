using Makku.APCMini.MK2;
using Makku.APCMini.MK2.Constants;
using Makku.APCMini.MK2.Helpers;

namespace Makku.MIDIPad
{
    public class Worker(ILogger<Worker> logger, APCMiniService apcMini) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation(apcMini.ToString());
                }

                apcMini.SetMultipleLEDs(
                    PadState.OneHundredPercent,
                    [0, 1, 2, 3, 4, 5, 6, 7],
                    Colour.BrightGreen
                );

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
