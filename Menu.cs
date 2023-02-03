using System.Data;
using static System.Console;
using System.Data.SqlClient;

namespace CrudAdoNet;
public static class Ui
{
    public static void MenuStart()
    {
        WriteLine("Bem vindo ao CRUD do Banco de Dados ");
        WriteLine($"Selecione a opção abaixo: ");
        WriteLine($"1 - Listar todos os Clientes");
        WriteLine($"2 - Alterar os dados de um Cliente");
        WriteLine($"3 - Inserir um novo cliente");
        WriteLine($"4 - Deletar um cliente");
        WriteLine($"5 - Limpar o Chat");
        WriteLine($"6 - Encerrar programa");
        int optionSelected = int.Parse(Console.ReadLine());
        SelectOption(optionSelected);
    }

    public static void SelectOption(int optionSelected)
    {
        switch (optionSelected)
        {
            case 1:
                WriteLine("================== LISTANDO TODOS OS CLIENTES ==================");
                ListAllClients().ForEach(clienteIndex => WriteLine($"Id: {clienteIndex.id} - Nome: {clienteIndex.nome} - Email: {clienteIndex.email}"));
                MenuStart();
                break;

            case 2:
                WriteLine("================== ALTERANDO CLIENTE ==================");
                WriteLine("Digite o ID do cliente");
                int id = int.Parse(Console.ReadLine());

                WriteLine("Digite o NOVO NOME do cliente ");
                string nomet = Console.ReadLine();

                WriteLine("Digite o NOVO Email do cliente");
                string emailt = Console.ReadLine();

                AlterClient(id, nomet, emailt);
                MenuStart();
                break;

            case 3:
                WriteLine("================== INSERINDO CLIENTE ==================");
                WriteLine("Digite o NOME do cliente ");
                string nome = Console.ReadLine();
                WriteLine("Digite o Email do cliente");
                string email = Console.ReadLine();
                InsertClient(nome, email);
                MenuStart();
                break;
            case 4:
                WriteLine("================== REMOVENDO CLIENTE ==================");
                WriteLine("Digite o ID do cliente: ");
                int idRemove = int.Parse(Console.ReadLine());
                var temporaryClient = SelectClient(idRemove);
                DeleteClient(idRemove);
                WriteLine($"O cliente {temporaryClient.nome} foi Excluído");
                MenuStart();
                break;
            case 5:
                Clear();
                MenuStart();
                break;
            case 6:
                System.Environment.Exit(1);
                break;
            default:
                MenuStart();
                break;
        }
    }

    public static string StringConnection()
    {
        //Hardcoding  aqui é temporário
        return "Server=SeuServidorAqui,PortaDoServidor;Database=BancaDeDadosAqui;User Id=SeuUsuarioAqui;Password=SuaSenhaAqui;";
    }

    public static List<(int id, string nome, string email)> ListAllClients()
    {
        using (SqlConnection connection = new SqlConnection(StringConnection()))
        {
            connection.Open();
            using (SqlCommand connectionCommand = connection.CreateCommand())
            {
                connectionCommand.CommandText = "select [Id],[Nome],[Email] from [Clientes] order by [Id] asc";
                using (SqlDataReader commandObjectReturn = connectionCommand.ExecuteReader())
                {
                    List<(int id, string nome, string email)> listaDeClientes = new List<(int id, string nome, string email)>();

                    while (commandObjectReturn.Read())
                    {
                        listaDeClientes.Add((id: commandObjectReturn.GetInt32(0), nome: commandObjectReturn.GetString(1), email: commandObjectReturn.GetString(2)));
                    }
                    return listaDeClientes;
                }
            }
        }
    }
    public static void InsertClient(string nome, string email)
    {
        using (SqlConnection connection = new SqlConnection(StringConnection()))
        {
            connection.Open();
            using (SqlCommand connectionCommand = connection.CreateCommand())
            {
                connectionCommand.CommandText = " insert into [Clientes]([Nome],[Email]) values(@nome,@email) ";
                connectionCommand.Parameters.Add("@nome", System.Data.SqlDbType.NVarChar).Value = nome;
                connectionCommand.Parameters.Add("@email", System.Data.SqlDbType.NVarChar).Value = email;
                connectionCommand.ExecuteNonQuery();
            }
        }
    }

    public static void AlterClient(int id, string nome, string email)
    {
        using (SqlConnection connection = new SqlConnection(StringConnection()))
        {
            connection.Open();
            using (SqlCommand connectinCommand = connection.CreateCommand())
            {
                connectinCommand.CommandText = "update clientes set [nome] = @nome, [email] = @email WHERE [Id] = @id";
                connectinCommand.Parameters.Add("@nome", System.Data.SqlDbType.NVarChar).Value = nome;
                connectinCommand.Parameters.Add("@email", System.Data.SqlDbType.NVarChar).Value = email;
                connectinCommand.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                connectinCommand.ExecuteNonQuery();
            }

        }
    }
    public static void DeleteClient(int id)
    {
        using (SqlConnection connection = new SqlConnection(StringConnection()))
        {
            connection.Open();
            using (SqlCommand connectionCommand = connection.CreateCommand())
            {
                connectionCommand.CommandText = "delete from [Clientes] where [id] = @id ";
                connectionCommand.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                connectionCommand.ExecuteNonQuery();
            }
        }
    }

    public static (int id, string nome, string email) SelectClient(int id)
    {
        using (SqlConnection connection = new SqlConnection(StringConnection()))
        {
            connection.Open();
            using (SqlCommand connectionCommand = connection.CreateCommand())
            {
                connectionCommand.CommandText = "select * from [Clientes] where [id] = @id";
                connectionCommand.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                SqlDataReader commandObjectReturn = connectionCommand.ExecuteReader();

                commandObjectReturn.Read();

                (int id, string nome, string email) rowObjectReturn = (id: commandObjectReturn.GetInt32(0), nome: commandObjectReturn.GetString(1), email: commandObjectReturn.GetString(2));
                return rowObjectReturn;
            }
        }
    }

}
