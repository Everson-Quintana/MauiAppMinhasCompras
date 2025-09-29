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
        List<Produto> tmp = await App.Db.GetAll(); // Consulta todos os produtos do banco de dados
        tmp.ForEach(i => lista.Add(i)); // Adiciona cada produto à coleção observável
    }
    // Evento do ToolbarItem para navegar até a página de novo produto
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Navegação empilhada para a página NovoProduto (mantém histórico)
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            // Exibe alerta em caso de erro ao tentar navegar
            DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue;
        lista.Clear();
        List<Produto> tmp = await App.Db.Search(q); // Consulta todos os produtos do banco de dados
        tmp.ForEach(i => lista.Add(i)); // Adiciona cada produto à coleção observável
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);
        string msg = $"O total é {soma:C}";
        DisplayAlert(" O total é ", msg, "Ok");
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {

    }
}