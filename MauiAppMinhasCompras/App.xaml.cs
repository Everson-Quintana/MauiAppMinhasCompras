
using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        // Instância estática do helper SQLite para reuso em toda a aplicação
        static SQLiteDatabaseHelper _db;

        // Propriedade pública para acessar o DB com inicialização preguiçosa (lazy)
        public static SQLiteDatabaseHelper Db
        {
            get
            {
                if (_db == null)
                {
                    // Monta o caminho do arquivo em LocalApplicationData e instancia o helper
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "banco_sqlite_compras.db3");
                    _db = new SQLiteDatabaseHelper(path);
                }
                return _db;
            }
        }

        // Construtor da aplicação: inicializa componentes e define a página principal
        public App()
        {
            InitializeComponent();
            // Usa NavigationPage para permitir navegação empilhada entre páginas
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}