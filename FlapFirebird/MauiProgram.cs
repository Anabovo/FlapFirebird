using Microsoft.Extensions.Logging;

namespace FlapFirebird;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("Gamer.ttf", "Gamer");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

}
