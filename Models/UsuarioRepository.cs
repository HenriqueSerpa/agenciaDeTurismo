using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using MySqlConnector;

namespace atv02_henriqueSerpa.Models
{
    public class UsuarioRepository
    {
        private const string DadosConexao = "Database=Turismo;Data Source=localhost;User Id=root;";

        public void testarConexao()
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
            conexao.Open();
            conexao.Close();
        }

        public Usuario validarLogin(Usuario novoAcesso)
        {
            //1 - Conectar a aplicação com o Banco de Dados
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
            conexao.Open();
            //2 - Inicializar variável nulo
            Usuario usuarioCadastrado = null;
            //3 - Query
            string Query = "SELECT * FROM Usuario WHERE Login=@Login and Senha=@Senha";
            //4 - Preparar comando
            MySqlCommand comando = new MySqlCommand(Query,conexao);
            //5- SQL Injection
            comando.Parameters.AddWithValue("@Login",novoAcesso.Login);
            comando.Parameters.AddWithValue("@Senha",novoAcesso.Senha);
            //6 - Executar comando para ler os registros
            MySqlDataReader Reader = comando.ExecuteReader();
            //7 - Define a variável como objeto Usuario. Busca se o usuario está cadastrado
            if (Reader.Read())
            {
                usuarioCadastrado = new Usuario();
                usuarioCadastrado.Id = Reader.GetInt32("Id");
                if(!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                {
                    usuarioCadastrado.Nome = Reader.GetString("Nome");
                }
                if(!Reader.IsDBNull(Reader.GetOrdinal("Login")))
                {
                    usuarioCadastrado.Login = Reader.GetString("Login");
                }
                if(!Reader.IsDBNull(Reader.GetOrdinal("Senha")))
                {
                    usuarioCadastrado.Senha = Reader.GetString("Senha");
                }
                usuarioCadastrado.DataNascimento = Reader.GetDateTime("DataNascimento");
            }

            //8 - Fecha conexão
            conexao.Close();
            //9 - Retorna usuário
            return usuarioCadastrado;
        }

        public void Inserir(Usuario novoUsuario)
        {
            //1 - Conectar a aplicação com o Banco de Dados
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
            conexao.Open();
            //2 - Query
            string Query = "INSERT INTO Usuario (Nome, Login, Senha, DataNascimento) VALUES (@Nome, @Login, @Senha, @DataNascimento)";
            //3 - Prepara Comando 
            MySqlCommand comando = new MySqlCommand(Query,conexao);
            //4 - SQL Injection
            comando.Parameters.AddWithValue("@Nome", novoUsuario.Nome);
            comando.Parameters.AddWithValue("@Login", novoUsuario.Login);
            comando.Parameters.AddWithValue("@Senha", novoUsuario.Senha);
            comando.Parameters.AddWithValue("@DataNascimento", novoUsuario.DataNascimento);
            //5 - Executar comando
            comando.ExecuteNonQuery();
            // 6 - Fechar conexão
            conexao.Close();
        }

        public List<Usuario> Listar()
        {
            //1 - Conectar a aplicação com o Banco de Dados
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
            conexao.Open();
            //2 - Criar lista
            List<Usuario> listaUsuario = new List <Usuario>();
            //3 - Query
            string Query = "SELECT * FROM Usuario";
            //4 - Prepara Comando 
            MySqlCommand comando = new MySqlCommand(Query,conexao);
            //5 - Executar comando para ler os registros
            MySqlDataReader Reader = comando.ExecuteReader();
            //6 - Percurso nos registros na TABLE Usuario e adiciona à lista criada
            while(Reader.Read())
            {
                Usuario usuarioEncontrado = new Usuario();
                usuarioEncontrado.Id = Reader.GetInt32("Id");
                if(!Reader.IsDBNull(Reader.GetOrdinal("Nome")))
                {
                    usuarioEncontrado.Nome = Reader.GetString("Nome");
                }
                if(!Reader.IsDBNull(Reader.GetOrdinal("Login")))
                {
                    usuarioEncontrado.Login = Reader.GetString("Login");
                }
                if(!Reader.IsDBNull(Reader.GetOrdinal("Senha")))
                {
                    usuarioEncontrado.Senha = Reader.GetString("Senha");
                }
                usuarioEncontrado.DataNascimento = Reader.GetDateTime("DataNascimento");

                listaUsuario.Add(usuarioEncontrado);
            }
            //7 - Fechar conexão
            conexao.Close();
            //8 - Retorna Lista com os registros adicionados na lista 
            return listaUsuario;
        }

        public void Atualizar(Usuario usuario)
        {
            //1 - Conectar a aplicação com o Banco de Dados
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
            conexao.Open();
            //2 - Query
            string Query = "UPDATE Usuario SET Nome=@Nome, Login=@Login, Senha=@Senha, DataNascimento=@DataNascimento WHERE Id=@Id";
            //3 - Prepara Comando 
            MySqlCommand comando = new MySqlCommand(Query,conexao);
            //4 - SQL Injection
            comando.Parameters.AddWithValue("@Nome", usuario.Nome);
            comando.Parameters.AddWithValue("@Login", usuario.Login);
            comando.Parameters.AddWithValue("@Senha", usuario.Senha);
            comando.Parameters.AddWithValue("@DataNascimento", usuario.DataNascimento);
            comando.Parameters.AddWithValue("@Id", usuario.Id);
            //5 - Executar comando
            comando.ExecuteNonQuery();
            // 6 - Fechar conexão
            conexao.Close();
        }

        public void Remover(Usuario removerUsuario)
        {
            //1 - Conectar a aplicação com o Banco de Dados
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
            conexao.Open();
            //2 - Query
            string Query = "DELETE FROM Usuario WHERE Id=@Id";
            //3 - Prepara Comando 
            MySqlCommand comando = new MySqlCommand(Query,conexao);
            //4 - SQL Injector
            comando.Parameters.AddWithValue("@Id", removerUsuario.Id);
            //5 - Executar comando
            comando.ExecuteNonQuery();
            // 6 - Fechar conexão
            conexao.Close();
        }

    }
}