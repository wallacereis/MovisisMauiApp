namespace MovisisMauiApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
            .UseLocalNotification()
            .RegisterFonts()
			.RegisterAppServices()
			.RegisterDependencyInjection();

        return builder.Build();
    }
}
