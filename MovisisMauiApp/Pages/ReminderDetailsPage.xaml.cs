namespace MovisisMauiApp.Pages;

public partial class ReminderDetailsPage : ContentPage
{
	public ReminderDetailsPage(ReminderDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}