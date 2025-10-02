using MauiAppMinhasCompras.Models;
using System.Threading.Tasks; // Namespace padr�o para Task (opcional neste arquivo)

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
    public EditarProduto() => InitializeComponent();

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto produto_anexado = BindingContext as Produto;
            if (produto_anexado != null)
            {
                // Cria inst�ncia de Produto a partir dos campos de entrada (XAML)
                Produto p = new Produto
                {
                    Id = produto_anexado.Id, // Mant�m o Id original para atualiza��o
                    Descricao = txt_descricao.Text,
                    Quantidade = Convert.ToDouble(txt_quantidade.Text),
                    Preco = Convert.ToDouble(txt_preco.Text)
                };

                // Insere o produto no banco usando a inst�ncia singleton App.Db (SQLiteDatabaseHelper)
                await App.Db.Update(p);

                // Feedback visual ao usu�rio
                await DisplayAlert("Sucesso!", "Registro Atualizado!", "Ok");
                await Navigation.PopAsync(); // Retorna � p�gina anterior na pilha de navega��o
            }
            else
            {
                throw new Exception("Produto inv�lido!");
            }
        }
        catch (Exception ex)
        {
            // Em caso de erro (parse, BD, etc.), exibe mensagem ao usu�rio
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }
}