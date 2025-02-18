namespace MovisisMauiApp.Pages;

public partial class ReminderCreatePage : ContentPage
{
    private ReminderCreateViewModel? _viewModel => BindingContext as ReminderCreateViewModel;
    public ReminderCreatePage(ReminderCreateViewModel viewModel)
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
        _viewModel.AttachmentFileName = images.FullPath;
    }
}