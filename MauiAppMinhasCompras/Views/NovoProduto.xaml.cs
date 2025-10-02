using MauiAppMinhasCompras.Models; // Modelo Produto utilizado para criar inst�ncia
using System.Threading.Tasks; // Namespace padr�o para Task (opcional neste arquivo)

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    // Construtor da p�gina: inicializa os componentes XAML
    public NovoProduto()
    {
        InitializeComponent();
    }

    // Manipulador do clique no ToolbarItem de salvar (ass�ncrono)
    private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            // Cria inst�ncia de Produto a partir dos campos de entrada (XAML)
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            // Insere o produto no banco usando a inst�ncia singleton App.Db (SQLiteDatabaseHelper)
            await App.Db.Insert(p);

            // Feedback visual ao usu�rio
            await DisplayAlert("Sucesso!", "Registro Inserido!", "Ok");
        }
        catch (Exception ex)
        {
            // Em caso de erro (parse, BD, etc.), exibe mensagem ao usu�rio
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }
}