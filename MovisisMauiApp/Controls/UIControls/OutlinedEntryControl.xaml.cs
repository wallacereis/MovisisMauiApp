namespace MovisisMauiApp.Controls.UIControls;

public partial class OutlinedEntryControl : Grid
{
	public OutlinedEntryControl()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
           propertyName: nameof(Text),
           returnType: typeof(string),
           declaringType: typeof(OutlinedEntryControl),
           defaultValue: null,
           defaultBindingMode: BindingMode.TwoWay);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set { SetValue(TextProperty, value); }
    }

    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
          propertyName: nameof(Placeholder),
          returnType: typeof(string),
          declaringType: typeof(OutlinedEntryControl),
          defaultValue: null,
          defaultBindingMode: BindingMode.TwoWay);

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set { SetValue(PlaceholderProperty, value); }
    }

    public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
          propertyName: nameof(IsPassword),
          returnType: typeof(bool),
          declaringType: typeof(OutlinedEntryControl),
          defaultValue: false,
          defaultBindingMode: BindingMode.TwoWay);

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set { SetValue(IsPasswordProperty, value); }
    }

    private void txtEntry_Focused(object sender, FocusEventArgs e)
    {
        lblPlaceholder.FontSize = 15;
        lblPlaceholder.TranslateTo(0, -20, 80, easing: Easing.Linear);
    }

    private void txtEntry_Unfocused(object sender, FocusEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(Text))
        {
            lblPlaceholder.FontSize = 15;
            lblPlaceholder.TranslateTo(0, -20, 80, easing: Easing.Linear);
        }
        else
        {
            lblPlaceholder.FontSize = 18;
            lblPlaceholder.TranslateTo(0, 0, 80, easing: Easing.Linear);
        }
    }

    private void OnTapped(object sender, TappedEventArgs e)
    {
        txtEntry.Focus();
    }
}