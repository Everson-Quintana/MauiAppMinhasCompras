namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    // Construtor: inicializa componentes XAML
    public ListaProduto()
    {
        InitializeComponent();
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
}