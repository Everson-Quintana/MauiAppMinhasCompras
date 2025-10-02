using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage

{
    // Cole��o observ�vel que alimenta a ListView/CollectionView no XAML.
    // ObservableCollection notifica automaticamente a UI sobre inser��es/remo��es.
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    // Construtor: inicializa componentes XAML e configura a fonte de dados da listagem.
    public ListaProduto()
    {
        InitializeComponent();
        // Associa a cole��o observ�vel � propriedade ItemsSource do controle de lista (definido no XAML).
        lst_produtos.ItemsSource = lista; // Define a fonte de dados da ListView
    }

    // M�todo do ciclo de vida da p�gina chamado quando a p�gina passa a ser vis�vel.
    // Usado aqui para carregar os dados do banco sempre que a p�gina aparece.
    protected async override void OnAppearing()
    {
        // Consulta todos os produtos do banco de dados de forma ass�ncrona.
        List<Produto> tmp = await App.Db.GetAll(); // Consulta todos os produtos do banco de dados

        // Adiciona cada produto retornado � cole��o observ�vel para atualiza��o autom�tica da UI.
        tmp.ForEach(i => lista.Add(i)); // Adiciona cada produto � cole��o observ�vel
    }

    // Evento do ToolbarItem para navegar at� a p�gina de novo produto.
    // Usa Navigation.PushAsync para empilhar a navega��o (preserva hist�rico).
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Navega��o empilhada para a p�gina NovoProduto (mant�m hist�rico)
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            // Em caso de falha na navega��o, exibe uma mensagem de erro ao usu�rio.
            DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    // Tratador do evento TextChanged do controle de busca.
    // Executa uma pesquisa no banco a cada altera��o do texto e atualiza a lista exibida.
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Valor atual do campo de pesquisa
        string q = e.NewTextValue;

        // Limpa a cole��o atual antes de inserir os resultados da busca
        lista.Clear();

        // Realiza a busca ass�ncrona no banco (m�todo customizado App.Db.Search)
        List<Produto> tmp = await App.Db.Search(q); // Consulta todos os produtos do banco de dados

        // Popula a cole��o observ�vel com os resultados da busca
        tmp.ForEach(i => lista.Add(i)); // Adiciona cada produto � cole��o observ�vel
    }

    // Evento do ToolbarItem que calcula o total acumulado dos produtos atualmente exibidos.
    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        // Soma a propriedade Total de cada item da cole��o
        double soma = lista.Sum(i => i.Total);

        // Formata a mensagem com s�mbolo monet�rio conforme cultura atual
        string msg = $"O total � {soma:C}";

        // Exibe o resultado em um alerta
        DisplayAlert(" O total � ", msg, "Ok");
    }

    // Handler vazio para MenuItem � adicionar a l�gica do menu contextual aqui.
    private void MenuItem_Clicked(object sender, EventArgs e)
    {
        // TODO: implementar a��o do MenuItem (ex: deletar, editar, compartilhar)
    }
}