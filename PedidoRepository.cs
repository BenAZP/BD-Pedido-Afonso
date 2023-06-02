using Aula10DB.Database;
using Aula10DB.Models;
using Microsoft.Data.Sqlite;
namespace Aula10DB.Repositories;
class PedidoRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public PedidoRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<Pedido> GetAll()
    {
        var pedidos = new List<Pedido>();
        

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Pedidos";

        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            var pedidoid = reader.GetInt32(7);
            var empregadoid = reader.GetInt32(8);
            var datapedido = reader.GetString(9);
            var peso = reader.GetString(10);
            var codtransportadora = reader.GetInt32(11);
            var pedidoClienteid = reader.GetInt32(12);
            var pedido = ReaderToPedido(reader);
            pedidos.Add(pedido);
        }

        connection.Close();
        
        return pedidos;
    }

    public Pedido Save(Pedido pedido)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Pedidos VALUES($pedidoid, $empregadoid, $datapedido, $peso, $codtranspoortadora, $pedidoclienteid)";
        command.Parameters.AddWithValue("$pedidoid", pedido.PedidoId);
        command.Parameters.AddWithValue("$empregadoid", pedido.EmpregadoId);
        command.Parameters.AddWithValue("$datapedido", pedido.DataPedido);
        command.Parameters.AddWithValue("$peso", pedido.Peso);
        command.Parameters.AddWithValue("$codtransportadora", pedido.CodTransportadora);
        command.Parameters.AddWithValue("$pedidoclienteid", pedido.PedidoClienteId);
        command.ExecuteNonQuery();
        connection.Close();

        return pedido;
    }
    public Pedido GetById(int pedidoid)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Clientes WHERE (id = $id)";
        command.Parameters.AddWithValue("$id", pedidoid);

        var reader = command.ExecuteReader();
        reader.Read();

        var pedido = ReaderToPedido(reader);

        connection.Close(); 

        return pedido;
    }
    public bool ExitsById(int pedidoid)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(id) FROM Clientes WHERE (id = $id)";
        command.Parameters.AddWithValue("$id", pedidoid);

        var reader = command.ExecuteReader();
        reader.Read();
        var result = reader.GetBoolean(0);

        return result;
    }
private Pedido ReaderToPedido(SqliteDataReader reader)
    {
        var pedido = new Pedido(reader.GetInt32(7), reader.GetInt32(8), reader.GetString(9), reader.GetString(10), reader.GetInt32(11), reader.GetInt32(12));

        return pedido;
    }
}