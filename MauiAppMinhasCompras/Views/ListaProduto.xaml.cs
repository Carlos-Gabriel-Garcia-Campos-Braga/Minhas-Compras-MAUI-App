using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
namespace MauiAppMinhasCompras.Views;


public partial class ListaProduto : ContentPage
{

	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
	public ListaProduto()
	{
		InitializeComponent();

		ls_Produtos.ItemsSource = lista;
	}

    protected async override void OnAppearing()
    {
		try
		{
			lista.Clear();

			List<Produto> temp = await App.DataBase.GetAll();

			temp.ForEach(i => lista.Add(i));
		}
		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "Fechar");
		}
        
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());
		}
		catch (Exception ex) 
		{
			DisplayAlert("Ops", ex.Message, "Fechar");
		}
    }

    private async void txt_Search_TextChanged(object sender, TextChangedEventArgs e)
    {
		try
		{
            string q = e.NewTextValue;

			ls_Produtos.IsRefreshing = true;

            lista.Clear();
			
            List<Produto> temp = await App.DataBase.SearchProduct(q);

            temp.ForEach(i => lista.Add(i));
        }
		catch(Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "Fechar");
		}
        finally
        {
            ls_Produtos.IsRefreshing = false;
        }
			
    }

	private void ToolbarItem_Clicked_1(object sender, EventArgs e)
	{
		double soma = lista.Sum(i => i.Total);

		string msg = $"O total e {soma:C}";	

		DisplayAlert("Total", msg, "Fechar");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			MenuItem selecionado = sender as MenuItem;

			Produto p = selecionado.BindingContext as Produto;

			bool confirm = await DisplayAlert("Tem certeza?",
				$"Remover {p.Descricao}?", "Sim", "Nao"
				);

			if (confirm)
			{
				await App.DataBase.DeleteProduct(p.Id);
				lista.Remove(p);
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "Fechar");
		}
    }

    private void ls_Produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		try
		{
			Produto p = e.SelectedItem as Produto;

			Navigation.PushAsync(new Views.EditarProduto
			{
				BindingContext = p,
			});
		}
		catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "Fechar");
		}

    }

    private async void ls_Produtos_Refreshing(object sender, EventArgs e)
    {
        try
		{
            List<Produto> temp = await App.DataBase.GetAll();

            temp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Fechar");
        }
		finally
		{
				ls_Produtos.IsRefreshing = false;
		}
    }
}