namespace MovisisMauiApp.Pages;

public partial class RemindersPage : ContentPage
{
    private RemindersViewModel? _viewModel => BindingContext as RemindersViewModel;
    public RemindersPage(RemindersViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnDateSelected(object sender, DateChangedEventArgs e)
    {
        await _viewModel.GetRemindersExpirationDateAsync(e.NewDate);
    }

    private async void OnFilterTapped(object sender, TappedEventArgs e)
    {
        imgFilter.Source = "nofilter";
        borderFilter.IsVisible = !borderFilter.IsVisible;
        if (!borderFilter.IsVisible)
        {
            imgFilter.Source = "filter";
            await _viewModel.GetAllRemindersAsync(null);
        }
    }
}