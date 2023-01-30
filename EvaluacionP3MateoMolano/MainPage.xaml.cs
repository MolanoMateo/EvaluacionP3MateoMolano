using System.Text.Json;

namespace EvaluacionP3MateoMolano;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainPageViewModel();
	}

	
}

