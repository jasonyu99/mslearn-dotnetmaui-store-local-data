using People.Models;

namespace People;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
		InitializeComponent();
        BindingContext = viewModel;
    }

}

