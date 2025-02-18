using MovisisMauiApp.Services.Notifications;

namespace MovisisMauiApp.Extensions
{
    public static class ConfigExtensions
    {
        #region REGISTER FONTS
        public static MauiAppBuilder RegisterFonts(this MauiAppBuilder builder)
        {
            return builder.ConfigureFonts(fonts =>
            {
                fonts.AddFont("FontAwesome6FreeBrands.otf", "FontAwesomeBrands");
                fonts.AddFont("FontAwesome6FreeRegular.otf", "FontAwesomeRegular");
                fonts.AddFont("FontAwesome6FreeSolid.otf", "FontAwesomeSolid");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("materialdesignicons-webfont.ttf", "MaterialDesignIcons");
                fonts.AddFont("Roboto-Medium.ttf", "Roboto-Medium");
                fonts.AddFont("Roboto-Regular.ttf", "Roboto-Regular");
                fonts.AddFont("UIFontIcons.ttf", "FontIcons");
            });
        }
        #endregion

        #region REGISTER APP SERVICES
        public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<AppShellOnBoarding>();           
            builder.Services.AddSingleton<AppShellHome>();

            return builder;
        }
        #endregion

        #region REGISTER DEPENDENCY INJECTION        
        public static MauiAppBuilder RegisterDependencyInjection(this MauiAppBuilder builder)
        {
            // Scoped objects are the same within a request, but different across different requests.
            builder.Services.AddScoped(typeof(IFlurlAPI<>), typeof(FlurlAPI<>));
            builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            builder.Services.AddScoped(typeof(IReminderService), typeof(ReminderService));
            builder.Services.AddScoped(typeof(IReminderAttachmentService), typeof(ReminderAttachmentService));
            builder.Services.AddScoped(typeof(INavigationService), typeof(NavigationService));
            builder.Services.AddScoped(typeof(ILocalNotificationService), typeof(LocalNotificationService));

            // Singleton objects are created as a single instance throughout the application. It creates the instance for the first time and reuses the same object in the all calls.
            /*-------------- OnBoarding Dependency Injection --------------*/
            builder.Services.AddSingleton<StartViewModel>();
            builder.Services.AddSingleton<StartPage>();

            /*-------------- Login Dependency Injection --------------*/
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<LoginPage>();

            /*-------------- Home Dependency Injection --------------*/
            builder.Services.AddSingleton<RemindersViewModel>();
            builder.Services.AddSingleton<RemindersPage>();

            builder.Services.AddSingleton<ReminderDetailsViewModel>();
            builder.Services.AddSingleton<ReminderDetailsPage>();

            builder.Services.AddSingleton<ReminderCreateViewModel>();
            builder.Services.AddSingleton<ReminderCreatePage>();

            builder.Services.AddSingleton<ReminderUpdateViewModel>();
            builder.Services.AddSingleton<ReminderUpdatePage>();

            builder.Services.AddSingleton<RemindersCalendarPage>();
            builder.Services.AddSingleton<RemindersCalendarViewModel>();

            // Transient objects lifetime services are created each time they are requested. This lifetime works best for lightweight, stateless services.

            return builder;
        }
        #endregion
    }
}

