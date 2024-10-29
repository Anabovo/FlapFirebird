using Windows.ApplicationModel.Search.Core;

namespace FlapFirebird;

public partial class MainPage : ContentPage
{
	int velocidade=20;
	const int gravidade=30;
	const int forcaPulo=45;
	const int maxTempoPulando=2;


	const int aberturaMinima=200;


    int score=0;
	const int tempoEntreFrames=75;
	double larguraJanela=0;
	double alturaJanela=0;
	bool estaMorto=true;
	bool estaPulando=false;
	int tempoPulando=0;
	
	//---------------------------------------------------------------------------------------
	
	
	public MainPage()
	{
		InitializeComponent();
		
	}

	//-----------------------------------------------------------------------------------------

	protected override void OnAppearing()
	{
		base.OnAppearing();
	}

	//-----------------------------------------------------------------------------------------


    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        larguraJanela = width;
        alturaJanela = height;
        if (height > 0)
        {
            ImagemCanoAlto.HeightRequest  = height - ImagemChao.HeightRequest;
            ImagemCanoBaixo.HeightRequest = height - ImagemChao.HeightRequest;
        }
    }
	//-------------------------------------------------------------------------------------------
	
	void Inicializar()
	{
		estaMorto=false;
		ImagemFirebird.TranslationY=0;
	}
	//---------------------------------------------------------------------------------------------------------
	
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
				SoundHelper.Play("gameover.wav");
				labelGameOverScore.Text = "Você passou por\n" + score + " canos";
				FrameGameOver.IsVisible=true;
				break;
			}
			await Task.Delay(tempoEntreFrames);
		}
	}
	
	//---------------------------------------------------------------------------------------------------------------


	void GerenciaCanos()
	{
		ImagemCanoAlto.TranslationX -= velocidade;
		ImagemCanoBaixo.TranslationX -= velocidade;
		if (ImagemCanoBaixo.TranslationX < -larguraJanela)
	    {
			ImagemCanoBaixo.TranslationX=0;
			ImagemCanoAlto.TranslationX=0;

			var alturaMax=-10;
			var alturaMin=-ImagemCanoBaixo.HeightRequest;

			ImagemCanoAlto.TranslationY=Random.Shared.Next((int)alturaMin, (int)alturaMax);
			ImagemCanoBaixo.TranslationY=ImagemCanoAlto.TranslationY=aberturaMinima;
			
			score++;
			labelScore.Text = "Score:" + score.ToString("D3");
			if (score % 4 == 0)
			    velocidade++;
		}
	}
	
	//-------------------------------------------------------------------------------------------------------


    void AplicaGravidade()
	{
		ImagemFirebird.TranslationY += gravidade;
	}
    //--------------------------------------------------------------------------------------------------------
	
	void AplicaPulo()
	{
		ImagemFirebird.TranslationY -= forcaPulo;
		tempoPulando++;
		if (tempoPulando >= maxTempoPulando)
		{
			tempoPulando=0;
			
		}
	}

	//----------------------------------------------------------------------------------------------------------
	
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

	//-----------------------------------------------------------------------------------------------------------------------
	
	void OnGameOverClicked(object s, TappedEventArgs a)
	{
		FrameGameOver.IsVisible=false;
		Inicializar();
		Desenhar();
	}

    //-----------------------------------------------------------------------------------------------------------------------------------

	bool VerificaColisaoCano()
    {
    if (VerificaColisaoCanoBaixo() || VerificaColisaoCanoalto())
      return true;
    else
       return false;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
	

	bool VerificaColisaoCanoalto()
	{
	   var posHPardal = (larguraJanela - 50) - (ImagemFirebird.WidthRequest / 2);
	   var posVPardal = (alturaJanela / 2) - (ImagemFirebird.HeightRequest / 2) + ImagemFirebird.TranslationY;

	   if(
		  posHPardal >= Math.Abs(ImagemCanoAlto.TranslationX) - ImagemCanoAlto.WidthRequest &&
	      posHPardal <= Math.Abs(ImagemCanoAlto.TranslationX) + ImagemCanoAlto.WidthRequest &&
		  posVPardal <= ImagemCanoAlto.HeightRequest + ImagemCanoAlto.TranslationY
		 )
		  {
			return true;
		  }
          else
		  {
			return false;
		  }
	}

	//-------------------------------------------------------------------------------------------------------------------
	
	bool VerificaColisaoCanoBaixo()
	{
		var posHPardal = larguraJanela - 50 - ImagemFirebird.WidthRequest / 2;
		var posVPardal = (alturaJanela / 2) + (ImagemFirebird.HeightRequest / 2) + ImagemFirebird.TranslationY;
		var yMaxCano = ImagemCanoAlto.HeightRequest + ImagemCanoAlto.TranslationY + aberturaMinima;
		if(
			posHPardal >= Math.Abs(ImagemCanoBaixo.TranslationX) - ImagemCanoBaixo.WidthRequest &&
		    posHPardal <= Math.Abs(ImagemCanoBaixo.TranslationX) + ImagemCanoBaixo.WidthRequest &&
		    posVPardal >= yMaxCano
		  )
		   {
			return true;
		   }
		   else
		   {
			return false;
		   }
	}
	
	//--------------------------------------------------------------------------------------------------------------------------------
	
	bool VerificaColisaoChao()
	{
	    var maxY = alturaJanela / 2 - ImagemChao.HeightRequest;
		if(ImagemFirebird.TranslationY >= maxY - ImagemChao.HeightRequest)
		   return true;
		else
		   return false;
    }

	//------------------------------------------------------------------------------------------------------------------------
	
	bool VerificaColisaoTeto()
	{
		var minY = alturaJanela / 2;
		if (ImagemFirebird.TranslationY <= 
		.minY)
		    return true;
		else
		    return false;
	}

	//----------------------------------------------------------------------------------------------------------------------------------
	void OnGridClicked(object s, TappedEventArgs a)
	{
		estaPulando=true;
	}

	

	





}

