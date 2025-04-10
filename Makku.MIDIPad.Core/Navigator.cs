using Microsoft.Extensions.DependencyInjection;

namespace Makku.MIDIPad.Core;

public class Navigator(IServiceProvider services) : IDisposable
{
    private BasePage? CurrentPage { get; set; }

    public void SetPage(BasePage page)
    {
        CurrentPage = page;
    }

    public void SetPage<TPage>() where TPage : BasePage
    {
        using var scope = services.CreateScope();
        var page = scope.ServiceProvider.GetRequiredService<TPage>();
        SetPage(page);
    }

    public void Update()
    {
        CurrentPage?.Update();
    }

    public void Dispose()
    {
        CurrentPage?.Dispose();
    }
}

public static class NavigatorServiceExtensions
{
    public static IServiceCollection AddNavigator(this IServiceCollection services)
        => services.AddSingleton<Navigator>();
}