using Makku.APCMini.MK2;
using Makku.APCMini.MK2.Constants;
using Makku.APCMini.MK2.Helpers;
using Makku.MIDI;
using Makku.MIDIPad.Core;
using Makku.MIDIPad.Voicemeeter;

namespace Makku.MIDIPad
{
    public class Worker(ILogger<Worker> logger, MIDIDeviceService service, Navigator navigator) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            service.Reset();

            navigator.SetPage<VoicemeeterPage>();

            while (!stoppingToken.IsCancellationRequested)
            {
                navigator?.Update();

                await Task.Delay(100, stoppingToken);
            }

            navigator?.Dispose();

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker stopped.");
            }
            
            service.Reset();
            service.Dispose();
        }
    }
}
