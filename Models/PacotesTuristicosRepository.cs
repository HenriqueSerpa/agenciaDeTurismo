using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using MySqlConnector;

namespace atv02_henriqueSerpa.Models
{
    public class PacotesTuristicosRepository
    {
        private const string DadosConexao = "Database=Turismo;Data Source=localhost;User Id=root;";

         public void testarConexao()
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
            conexao.Open();
            conexao.Close();
        }

        public void RegistrarPacote(PacotesTuristicos novoPacote)
        {
            //1 - Conectar a aplicação com o Banco de Dados
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
            conexao.Open();
            //2 - Query
            string Query = "INSERT INTO PacotesTuristicos (Nome, Origem, Destino, Atrativos, Saida, Retorno, Usuario) VALUES (@Nome, @Origem, @Destino, @Atrativos, @Saida, @Retorno, @IdUsuario)";
            //3 - Prepara Comando 
            MySqlCommand comando = new MySqlCommand(Query,conexao);
            //4 - SQL Injection
            comando.Parameters.AddWithValue("@Nome", novoPacote.Nome);
            comando.Parameters.AddWithValue("@Origem", novoPacote.Origem);
            comando.Parameters.AddWithValue("@Destino", novoPacote.Destino);
            comando.Parameters.AddWithValue("@Atrativos", novoPacote.Atrativos);
            comando.Parameters.AddWithValue("@Saida", novoPacote.Saida);
            comando.Parameters.AddWithValue("@Retorno", novoPacote.Retorno);
            comando.Parameters.AddWithValue("@IdUsuario", novoPacote.Usuario);
            //5 - Executar comando
            comando.ExecuteNonQuery();
            // 6 - Fechar conexão
            conexao.Close();
        }

        public List<PacotesTuristicos> Listar()
        {
            //1 - Conectar a aplicação com o Banco de Dados
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
            conexao.Open();
            //2 - Criar lista
            List<PacotesTuristicos> listaPacotes = new List <PacotesTuristicos>();
            //3 - Query
            string Query = "SELECT * FROM PacotesTuristicos";
            //4 - Prepara Comando 
            MySqlCommand comando = new MySqlCommand(Query,conexao);
            //5 - Executar comando para ler os registros
            MySqlDataReader Reader = comando.ExecuteReader();
            //6 - Percurso nos registros na TABLE Usuario e adiciona à lista criada
            while(Reader.Read())
            {
                PacotesTuristicos pacoteEncontrado = new PacotesTuristicos();
                pacoteEncontrado.Id = Reader.GetInt32("Id");
                if(!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                {
                    pacoteEncontrado.Nome = Reader.GetString("Nome");
                }
                if(!Reader.IsDBNull(Reader.GetOrdinal("Origem")))
                {
                    pacoteEncontrado.Origem = Reader.GetString("Origem");
                }
                if(!Reader.IsDBNull(Reader.GetOrdinal("Destino")))
                {
                    pacoteEncontrado.Destino = Reader.GetString("Destino");
                }
                if(!Reader.IsDBNull(Reader.GetOrdinal("Atrativos")))
                {
                    pacoteEncontrado.Atrativos = Reader.GetString("Atrativos");
                }
                pacoteEncontrado.Saida = Reader.GetDateTime("Saida");
                pacoteEncontrado.Retorno = Reader.GetDateTime("Retorno");
                pacoteEncontrado.Usuario = Reader.GetInt32("Usuario");
                
                listaPacotes.Add(pacoteEncontrado);
            }
            
            //7 - Fechar conexão
            conexao.Close();
            //8 - Retorna Lista com os registros adicionados na lista 
            return listaPacotes;
            
        }

        public void Alterar(PacotesTuristicos pacote)
        {
            //1 - Conectar a aplicação com o Banco de Dados
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
            conexao.Open();
            //2 - Query
            string Query = "UPDATE PacotesTuristicos SET Nome=@Nome, Origem=@Origem, Destino=@Destino, Atrativos=@Atrativos, Saida=@Saida, Retorno=@Retorno WHERE Id=@Id;";
            //3 - Prepara Comando 
            MySqlCommand comando = new MySqlCommand(Query,conexao);
            //4 - SQL Injection
            comando.Parameters.AddWithValue("@Nome", pacote.Nome);
            comando.Parameters.AddWithValue("@Origem", pacote.Origem);
            comando.Parameters.AddWithValue("@Destino", pacote.Destino);
            comando.Parameters.AddWithValue("@Atrativos", pacote.Atrativos);
            comando.Parameters.AddWithValue("@Saida", pacote.Saida);
            comando.Parameters.AddWithValue("@Retorno", pacote.Retorno);
            comando.Parameters.AddWithValue("@Id", pacote.Id);
            //5 - Executar comando
            comando.ExecuteNonQuery();
            // 6 - Fechar conexão
            conexao.Close();
        }

        public PacotesTuristicos BuscarId(int Id)
        {
            //1 - Conectar a aplicação com o Banco de Dados
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
            conexao.Open();
            //2 - Criar um PacotesTuristicos vazio
            PacotesTuristicos pacoteBuscado = new PacotesTuristicos();
            //3 - Query
            string Query = "SELECT * FROM PacotesTuristicos WHERE Id=@Id;";
            //4 - Prepara Comando 
            MySqlCommand comando = new MySqlCommand(Query,conexao);
            //5 - SQL Injection
            comando.Parameters.AddWithValue("@Id", Id);
            //6 - Executar comando, recuperando os registros
            MySqlDataReader Reader = comando.ExecuteReader();
            //7 - Percorre os registros
            if(Reader.Read())
            {
                pacoteBuscado.Id = Reader.GetInt32("Id");
                if(!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                {
                    pacoteBuscado.Nome = Reader.GetString("Nome");
                }
                if(!Reader.IsDBNull(Reader.GetOrdinal("Origem")))
                {
                    pacoteBuscado.Origem = Reader.GetString("Origem");
                }
                if(!Reader.IsDBNull(Reader.GetOrdinal("Destino")))
                {
                    pacoteBuscado.Destino = Reader.GetString("Destino");
                }
                if(!Reader.IsDBNull(Reader.GetOrdinal("Atrativos")))
                {
                    pacoteBuscado.Atrativos = Reader.GetString("Atrativos");
                }
                pacoteBuscado.Saida = Reader.GetDateTime("Saida");
                pacoteBuscado.Retorno = Reader.GetDateTime("Retorno");
                pacoteBuscado.Usuario = Reader.GetInt32("Usuario");
            }
            //8 - Fechar conexão
            conexao.Close();
            //9 - Retorna pacoteBuscado
            return pacoteBuscado;
        }

        public void Remover(PacotesTuristicos removerPacote)
        {
            //1 - Conectar a aplicação com o Banco de Dados
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
            conexao.Open();
            //2 - Query
            string Query = "DELETE FROM PacotesTuristicos WHERE Id=@Id;";
            //3 - Prepara Comando 
            MySqlCommand comando = new MySqlCommand(Query,conexao);
            //4 - SQL Injector
            comando.Parameters.AddWithValue("@Id", removerPacote.Id);
            //5 - Executar comando
            comando.ExecuteNonQuery();
            // 6 - Fechar conexão
            conexao.Close();
        }

    }
}