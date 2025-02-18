namespace MovisisMauiApp.Pages;

public partial class StartPage : ContentPage
{
    public StartPage(StartViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}