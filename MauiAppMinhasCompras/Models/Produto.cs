using SQLite; // Atributos do sqlite-net, como PrimaryKey e AutoIncrement

namespace MauiAppMinhasCompras.Models
{
    // Modelo que representa a tabela Produto no SQLite
    public class Produto
    {   string _descricao;
        // Chave primária com auto incremento
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Campos que serão mapeados como colunas na tabela
        public string Descricao
        {
            get => _descricao;
            set
            {
                if (value == null)
                {
                    throw new Exception("Por favor, preencha a descrição.");
                }
                _descricao = value;
            }
        }
        public double Quantidade { get; set; }
        public double Preco { get; set; }
        public double Total { get => Quantidade * Preco; }
    }
}