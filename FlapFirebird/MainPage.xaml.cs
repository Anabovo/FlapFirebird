namespace FlapFirebird;

public partial class MainPage : ContentPage
{
    
	const int gravidade=5;
	const int tempoEntreFrames=100;

	
	bool estaMorto=true;
	double larguraJanela=0;
	double alturaJanela=0;
	int velocidade=20;


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
			GerenciaCanos();
		}
	}

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		larguraJanela = w;
		alturaJanela = h;
	}

	void GerenciaCanos()
	{
		ImagemCanoAlto.TranslationX-=velocidade;
		ImagemCanoBaixo.TranslationX-=velocidade;
		if (ImagemCanoBaixo.TranslationX<-larguraJanela)
	    {
			ImagemCanoBaixo.TranslationX=0;
			ImagemCanoAlto.TranslationX=0;
		}
	}

	void OnGameOverClicked(object s, TappedEventArgs a)
	{
		FrameGameOver.IsVisible=false;
		Inicializar();
		Desenhar();
	}
	 void Inicializar()
	 {
		estaMorto=false;
		ImagemFirebird.TranslationY=0;
	 }

}

