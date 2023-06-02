using Microsoft.Data.Sqlite;
using Aula10DB.Database;
using Aula10DB.Repositories;
using Aula10DB.Models;
var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);
var clienteRepository = new ClienteRepository(databaseConfig);
var pedidoRepository = new  PedidoRepository(databaseConfig);

var modelName = args[0];
var modelAction = args[1];

if(modelName == "Cliente")
{
    if(modelAction == "Listar")
    {
        Console.WriteLine("Cliente Listar");
        foreach (var cliente in clienteRepository.GetAll())
        {
            Console.WriteLine($"{cliente.ClienteId}, {cliente.Endereco}, {cliente.Cidade}, {cliente.Regiao}, {cliente.CodigoPostal}, {cliente.Pais}, {cliente. Telefone}");
        }
    }

    if(modelAction == "Inserir")
    {
        Console.WriteLine("Cliente Inserir");
        var id = Convert.ToInt32(args[2]);
        string endereco = args[3];
        string cidade = args[4];
        string regiao = args[5];
        string codigopostal = args[6];
        string pais = args[7];
        string telefone = args[8];
        var cliente = new Cliente(id, endereco, cidade, regiao, codigopostal, pais, telefone);
        clienteRepository.Save(cliente);
    }

    if(modelAction == "Apresentar")
    {
        Console.WriteLine("Cliente Apresentar");
        var id = Convert.ToInt32(args[2]);
        if(clienteRepository.ExitsById(id))
        {
            var cliente = clienteRepository.GetById(id);
            Console.WriteLine($"{cliente.ClienteId} | {cliente.Endereco} | {cliente.Cidade}  | {cliente.Regiao}| {cliente.CodigoPostal}| {cliente.Pais}| {cliente.Telefone}");
        } 
        else 
        {
            Console.WriteLine($"O cliente com Id {id} não existe.");
        }
    }
}

if(modelName == "Pedido")
{
    if(modelAction == "Listar")
    {
        Console.WriteLine("Pedido Listar");
        foreach (var pedido in pedidoRepository.GetAll())
        {
            Console.WriteLine($"{pedido.PedidoId}, {pedido.EmpregadoId}, {pedido.DataPedido}, {pedido.Peso}, {pedido.CodTransportadora}, {pedido.PedidoClienteId}");
        }
    }

    if(modelAction == "Inserir")
    {
        Console.WriteLine("Pedido Inserir");
        var pedidoid = Convert.ToInt32(args[9]);
        int empregadoid = Convert.ToInt32(args[10]);
        string datapedido = args[11];
        string peso = args[12];
        int codtransportadora = Convert.ToInt32(args[13]);
        int pedidoClienteid = Convert.ToInt32(args[14]);
        var pedido = new Pedido (pedidoid, empregadoid, datapedido, peso, codtransportadora, pedidoClienteid);
        pedidoRepository.Save(pedido);
    }

    if(modelAction == "Apresentar")
    {
        Console.WriteLine("Pedido Apresentar");
        var pedidoid = Convert.ToInt32(args[2]);
        if(pedidoRepository.ExitsById(pedidoid))
        {
            var pedido = pedidoRepository.GetById(pedidoid);
            Console.WriteLine($"{pedido.PedidoId} | {pedido.EmpregadoId} | {pedido.DataPedido}  | {pedido.Peso}| {pedido.CodTransportadora}| {pedido.PedidoClienteId}");
        } 
        else 
        {
            Console.WriteLine($"O pedido com Id {pedidoid} não existe.");
        }
    }
}
