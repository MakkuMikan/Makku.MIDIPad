using Makku.APCMini.MK2;
using Makku.Discord;
using Makku.MIDIPad;
using Makku.MIDIPad.Core;
using Makku.MIDIPad.Voicemeeter;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.UseAPCMini();

builder.Services.AddNavigator();

builder.Services.AddVoicemeeterPage();
builder.Services.AddSoundboardPage();
builder.Services.AddDiscordService();

var host = builder.Build();
host.Run();
