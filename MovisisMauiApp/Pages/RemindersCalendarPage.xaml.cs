namespace MovisisMauiApp.Pages;

public partial class RemindersCalendarPage : ContentPage
{
    public RemindersCalendarPage(RemindersCalendarViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}