using Windows.ApplicationModel.Search.Core;

namespace FlapFirebird;

public partial class MainPage : ContentPage
{
	const int gravidade=30;
	const int tempoEntreFrames=75;
	bool estaMorto=true;
	double larguraJanela=0;
	double alturaJanela=0;
	int velocidade=20;
	const int maxTempoPulando=2;
	int tempoPulando=0;
	bool estaPulando=false;
	const int forcaPulo=45;
	const int aberturaMinima=200;
	int score=0;

	public MainPage()
	{
		InitializeComponent();
		
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
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
		if (ImagemCanoBaixo.TranslationX<= -larguraJanela)
	    {
			var alturaMax=-10;
			var alturaMin=-ImagemCanoBaixo.HeightRequest;
			ImagemCanoAlto.TranslationY=Random.Shared.Next((int)alturaMin, (int)alturaMax);
			ImagemCanoBaixo.TranslationY=ImagemCanoAlto.TranslationY=aberturaMinima;
			ImagemCanoBaixo.TranslationX=0;
			ImagemCanoAlto.TranslationX=0;
			score++;
			labelScore.Text = "Score:" + score.ToString("D3");
			if (score % 2 == 0)
			    velocidade++;
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
			    VerificaColisaoChao() ||
				VerificaColisaoCanoalto() ||
				VerificaColisaoCanoBaixo())
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

	bool VerificaColisaoCanoalto()
	{
	   var posHPardal = (larguraJanela/2)-(ImagemFirebird.WidthRequest/2);
	   var posVPardal = (larguraJanela/2)-(ImagemFirebird.HeightRequest/2);

	   if(posHPardal >= Math.Abs(ImagemCanoAlto.TranslationX)-ImagemCanoAlto.WidthRequest&&
	      posHPardal <= Math.Abs(ImagemCanoAlto.TranslationX)+ImagemCanoAlto.WidthRequest&&
		  posVPardal <= ImagemCanoAlto.HeightRequest + ImagemCanoAlto.TranslationY)
		  {
			return true;
		  }
          else
		  {
			return false;
		  }
	}

	bool VerificaColisaoCanoBaixo()
	{
		var posHPardal = (larguraJanela/2)-(ImagemFirebird.WidthRequest/2);
		var posVPardal = (alturaJanela/2)+(ImagemFirebird.HeightRequest/2)+ImagemFirebird.TranslationY;
		var yMaxCano = ImagemCanoAlto.HeightRequest+ImagemCanoAlto.TranslationY+aberturaMinima;
		if(posHPardal >= Math.Abs(ImagemCanoBaixo.TranslationX)-ImagemCanoBaixo.WidthRequest&&
		   posHPardal <= Math.Abs(ImagemCanoBaixo.TranslationX)+ImagemCanoBaixo.WidthRequest&&
		   posVPardal >= yMaxCano)
		   {
			return true;
		   }
		   else
		   {
			return false;
		   }
	}





}

