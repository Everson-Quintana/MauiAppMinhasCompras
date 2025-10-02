using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage

{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
    // Construtor: inicializa componentes XAML
    public ListaProduto()
    {
        InitializeComponent();
        lst_produtos.ItemsSource = lista; // Define a fonte de dados da ListView
    }
    protected async override void OnAppearing()
    {

        try
        {
            List<Produto> tmp = await App.Db.GetAll(); // Consulta todos os produtos do banco de dados
            tmp.ForEach(i => lista.Add(i)); // Adiciona cada produto à coleção observável
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }
    // Evento do ToolbarItem para navegar até a página de novo produto
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Navegação empilhada para a página NovoProduto (mantém histórico)
            await Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            // Exibe alerta em caso de erro ao tentar navegar
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;
            lista.Clear();
            List<Produto> tmp = await App.Db.Search(q); // Consulta todos os produtos do banco de dados
            tmp.ForEach(i => lista.Add(i)); // Adiciona cada produto à coleção observável
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            double soma = lista.Sum(i => i.Total);
            string msg = $"O total é {soma:C}";
            await DisplayAlert("Total dos produtos: ", msg, "Ok");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecionado = (MenuItem)sender;
            Produto? p = selecionado.BindingContext as Produto;
            if (p == null)
            {
                await DisplayAlert("Ops", "Produto não encontrado.", "Ok");
                return;
            }
            bool confirm = await DisplayAlert("Tem certeza?", $"Confirma a exclusão do produto {p.Descricao}?", "Sim", "Não");

            if (confirm)
            {
                await App.Db.Delete(p.Id);
                lista.Remove(p);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto? p = e.SelectedItem as Produto;
            if (p == null)
            {
                return;
            }
            Navigation.PushAsync(new Views.EditarProduto { BindingContext = p });
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "Ok");
        }
    }
}