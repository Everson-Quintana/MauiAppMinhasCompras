namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    // Construtor: inicializa componentes XAML
    public ListaProduto()
    {
        InitializeComponent();
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
}