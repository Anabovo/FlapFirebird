namespace FlapFirebird;

public partial class MainPage : ContentPage
{
	const int gravidade=30;
	const int tempoEntreFrames=80;
	bool estaMorto=true;
	double larguraJanela=0;
	double alturaJanela=0;
	int velocidade=20;
	const int maxTempoPulando=1;
	int tempoPulando=0;
	bool estaPulando=false;
	const int forcaPulo=45;

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
			if (estaPulando)
			    AplicaPulo();
			else 
			    AplicaGravidade();
			    GerenciaCanos();
			if (VerificaColisao())
			{
				estaMorto=true;
				FrameGameOver.IsVisible=true;
				break;
			}
			await Task.Delay(tempoEntreFrames);
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

	bool VerificaColisao()
	{
		if(!estaMorto)
		{
			if (VerificaColisaoTeto() ||
			    VerificaColisaoChao())
			{
				return true;
			}
		}
		return false;
	}

	bool VerificaColisaoTeto()
	{
		var minY = -alturaJanela/2;
		if (ImagemFirebird.TranslationY <= minY)
		    return true;
		else
		    return false;
	}

	bool VerificaColisaoChao()
	{
	    var maxY= alturaJanela/2 - ImagemChao.HeightRequest;
		if(ImagemFirebird.TranslationY >= maxY)
		   return true;
		else
		   return false;
    }

	void AplicaPulo()
	{
		ImagemFirebird.TranslationY -= forcaPulo;
		tempoPulando++;
		if (tempoPulando >= maxTempoPulando)
		{
			estaPulando=false;
			tempoPulando=0;
		}
	}

	void OnGridClicked(object s, TappedEventArgs a)
	{
		estaPulando=true;
	}

}

