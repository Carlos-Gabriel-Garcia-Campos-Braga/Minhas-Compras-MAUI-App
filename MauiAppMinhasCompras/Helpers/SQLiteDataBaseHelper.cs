    using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDataBaseHelper
    {
        readonly SQLiteAsyncConnection connect;
        public SQLiteDataBaseHelper(string path)
        {
            connect = new SQLiteAsyncConnection(path);
            connect.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> InsertProduct(Produto Produto)
        {
            return connect.InsertAsync(Produto);
        }

        public Task<List<Produto>> UpdateProduct(Produto Produto) 
        {
            string sql = "UPDATE Produto SET Descricao=? , Quantidade=?, Preco=? WHERE Id=?";
            return connect.QueryAsync<Produto>(sql,
                Produto.Descricao, Produto.Quantidade, Produto.Preco,Produto.Id);   
        }

        public Task<int> DeleteProduct(int ID) 
        {
            return connect.Table<Produto>().DeleteAsync(i => i.Id == ID);
        }

        public Task<List<Produto>> GetAll()
        {
            return connect.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> SearchProduct(string Query)  
        {
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%" + Query + "%'";
            return connect.QueryAsync<Produto>(sql);
        }
    }
}
