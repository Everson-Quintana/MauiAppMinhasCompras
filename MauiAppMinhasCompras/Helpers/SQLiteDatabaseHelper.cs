using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    // Classe auxiliar para manipulação do banco SQLite de produtos
    public class SQLiteDatabaseHelper
    {
        // Conexão assíncrona com o banco de dados SQLite
        readonly SQLiteAsyncConnection _conn;

        // Construtor: inicializa a conexão e garante a existência da tabela Produto
        public SQLiteDatabaseHelper(string Path)
        {
            _conn = new SQLiteAsyncConnection(Path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        // Insere um novo produto na tabela Produto
        public Task<int> Insert(Produto p) 
        { 
            return _conn.InsertAsync(p);
        }

        // Atualiza um produto existente na tabela Produto
        // Corrigido: usa ExecuteAsync para comandos UPDATE (não retorna lista)
        public Task<int> Update(Produto p) 
        {
            string sql = "UPDATE Produto SET Descricao = ?, Quantidade = ?, Preco = ? WHERE Id = ?";
            // Ordem dos parâmetros corrigida: Descricao, Quantidade, Preco, Id
            // O que mudou:
            // 1. Adicionamos a cláusula WHERE que estava faltando
            // 2. Esta correção permite que a atualização aconteça apenas no registro 
            //    específico com o Id correspondente
            // 3. Sem o WHERE, o SQLite retornava erro de sintaxe próximo ao Id
            return _conn.ExecuteAsync(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        }

        // Remove um produto pelo Id
        public Task<int> Delete(int Id) 
        { 
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == Id);
        }

        // Retorna todos os produtos cadastrados
        public Task<List<Produto>> GetAll() 
        { 
            return _conn.Table<Produto>().ToListAsync();
        }

        // Busca produtos pela descrição (contendo o termo informado)
        // Corrigido: SQL válido e uso de parâmetro para evitar SQL Injection
        public Task<List<Produto>> Search(string q) 
        {
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE ?";
            // O parâmetro deve ser passado com os % para o LIKE
            return _conn.QueryAsync<Produto>(sql, $"%{q}%");
        }
    }
}
