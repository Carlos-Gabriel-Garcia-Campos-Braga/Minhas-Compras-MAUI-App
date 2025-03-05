using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
	{
		try
		{
			Produto p = new Produto
			{
				Descricao = txt_Descricao.Text,
				Preco = Convert.ToDouble(txt_Preco.Text),
				Quantidade = Convert.ToDouble(txt_Quantidade.Text)
			};

			await App.DataBase.InsertProduct(p);
			await DisplayAlert("Sucesso", "Registro Inserido", "Ok");
			await Navigation.PopAsync();
		}
		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "Fechar");
		}
		}
	}