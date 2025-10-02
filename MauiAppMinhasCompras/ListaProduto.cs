using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage

{
    // Coleção observável que alimenta a ListView/CollectionView no XAML.
    // ObservableCollection notifica automaticamente a UI sobre inserções/remoções.
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    // Construtor: inicializa componentes XAML e configura a fonte de dados da listagem.
    public ListaProduto()
    {
        InitializeComponent();
        // Associa a coleção observável à propriedade ItemsSource do controle de lista (definido no XAML).
        lst_produtos.ItemsSource = lista; // Define a fonte de dados da ListView
    }

    // Método do ciclo de vida da página chamado quando a página passa a ser visível.
    // Usado aqui para carregar os dados do banco sempre que a página aparece.
    protected async override void OnAppearing()
    {
        // Consulta todos os produtos do banco de dados de forma assíncrona.
        List<Produto> tmp = await App.Db.GetAll(); // Consulta todos os produtos do banco de dados

        // Adiciona cada produto retornado à coleção observável para atualização automática da UI.
        tmp.ForEach(i => lista.Add(i)); // Adiciona cada produto à coleção observável
    }

    // Evento do ToolbarItem para navegar até a página de novo produto.
    // Usa Navigation.PushAsync para empilhar a navegação (preserva histórico).
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Navegação empilhada para a página NovoProduto (mantém histórico)
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            // Em caso de falha na navegação, exibe uma mensagem de erro ao usuário.
            DisplayAlert("Ops", ex.Message, "Ok");
        }
    }

    // Tratador do evento TextChanged do controle de busca.
    // Executa uma pesquisa no banco a cada alteração do texto e atualiza a lista exibida.
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Valor atual do campo de pesquisa
        string q = e.NewTextValue;

        // Limpa a coleção atual antes de inserir os resultados da busca
        lista.Clear();

        // Realiza a busca assíncrona no banco (método customizado App.Db.Search)
        List<Produto> tmp = await App.Db.Search(q); // Consulta todos os produtos do banco de dados

        // Popula a coleção observável com os resultados da busca
        tmp.ForEach(i => lista.Add(i)); // Adiciona cada produto à coleção observável
    }

    // Evento do ToolbarItem que calcula o total acumulado dos produtos atualmente exibidos.
    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        // Soma a propriedade Total de cada item da coleção
        double soma = lista.Sum(i => i.Total);

        // Formata a mensagem com símbolo monetário conforme cultura atual
        string msg = $"O total é {soma:C}";

        // Exibe o resultado em um alerta
        DisplayAlert(" O total é ", msg, "Ok");
    }

    // Handler vazio para MenuItem — adicionar a lógica do menu contextual aqui.
    private void MenuItem_Clicked(object sender, EventArgs e)
    {
        // TODO: implementar ação do MenuItem (ex: deletar, editar, compartilhar)
    }
}