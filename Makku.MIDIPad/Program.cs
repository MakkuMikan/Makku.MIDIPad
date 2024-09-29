using Makku.MIDI.APCMiniMk2;
using Makku.MIDIPad;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.UseAPCMini();

var host = builder.Build();
host.Run();
