namespace FlapFirebird;

public partial class MainPage : ContentPage
{
    
	const int gravidade=5;
	const int tempoEntreFrames=1000;
	bool estaMorto=false;

	public MainPage()
	{
		InitializeComponent();
		
	}

	void AplicaGravidade()
	{
		ImagemFirebird.TranslationY+=gravidade;
	}

	async Task Desenhar()
	{
		while (! estaMorto)
		{
			AplicaGravidade();
			await Task.Delay(tempoEntreFrames);
		}
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		Desenhar();
    }

}

