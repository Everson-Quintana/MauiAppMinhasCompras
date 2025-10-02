using MauiAppMinhasCompras.Models;
using System.Threading.Tasks; // Namespace padrão para Task (opcional neste arquivo)

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
                // Cria instância de Produto a partir dos campos de entrada (XAML)
                Produto p = new Produto
                {
                    Id = produto_anexado.Id, // Mantém o Id original para atualização
                    Descricao = txt_descricao.Text,
                    Quantidade = Convert.ToDouble(txt_quantidade.Text),
                    Preco = Convert.ToDouble(txt_preco.Text)
                };

                // Insere o produto no banco usando a instância singleton App.Db (SQLiteDatabaseHelper)
                await App.Db.Update(p);

                // Feedback visual ao usuário
                await DisplayAlert("Sucesso!", "Registro Atualizado!", "Ok");
                await Navigation.PopAsync(); // Retorna à página anterior na pilha de navegação
            }
            else
            {
                throw new Exception("Produto inválido!");
            }
        }
        catch (Exception ex)
        {
            // Em caso de erro (parse, BD, etc.), exibe mensagem ao usuário
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }
}