namespace MovisisMauiApp.Pages;

public partial class ReminderUpdatePage : ContentPage
{
    private ReminderUpdateViewModel? _viewModel => BindingContext as ReminderUpdateViewModel;
    public ReminderUpdatePage(ReminderUpdateViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnTappedAttachImages(object sender, TappedEventArgs e)
    {
        var images = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Pick Image",
            FileTypes = FilePickerFileType.Images
        });
        var imageSource = images.FullPath.ToString();
        attachImage.Source = imageSource;
        _viewModel.AttachmentFileName = imageSource;
    }
}