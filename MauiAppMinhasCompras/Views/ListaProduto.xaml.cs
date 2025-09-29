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
        tmp.ForEach(i => lista.Add(i)); // Adiciona cada produto � cole��o observ�vel
    }
    // Evento do ToolbarItem para navegar at� a p�gina de novo produto
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Navega��o empilhada para a p�gina NovoProduto (mant�m hist�rico)
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
        tmp.ForEach(i => lista.Add(i)); // Adiciona cada produto � cole��o observ�vel
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);
        string msg = $"O total � {soma:C}";
        DisplayAlert(" O total � ", msg, "Ok");
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {

    }
}