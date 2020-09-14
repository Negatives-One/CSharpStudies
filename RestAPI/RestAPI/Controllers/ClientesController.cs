using RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace RestAPI.Controllers
{
    public class ClientesController : ApiController
    {
        private string SenhaMySql = "Senha";
        private static List<Cliente> _clientes = new List<Cliente>();

        [HttpPatch]
        public string PatchCliente(int numCliente, string nome)
        {

            MySqlConnection con = new MySqlConnection("server=localhost;port=3306;User Id=root;database=loja;password=" + SenhaMySql);
            con.Open();
            MySqlCommand command = new MySqlCommand("update Clientes SET nome = ? WHERE cod_cliente = ?;" , con);
            command.Parameters.Add("@nome", MySqlDbType.VarChar, 30).Value = nome;
            command.Parameters.Add("@cod_cliente", MySqlDbType.Int32).Value = numCliente;
            command.ExecuteNonQuery();
            con.Close();
            return "Cliente Atualizado";
        }


        [HttpGet]
        public string GetCliente(int numCliente)
        {
            MySqlConnection con = new MySqlConnection("server=localhost;port=3306;User Id=root;database=loja;password="+SenhaMySql);
            con.Open();
            MySqlCommand command = new MySqlCommand("select nome from clientes where cod_cliente = ?;", con);
            command.Parameters.Add("cod_cliente", MySqlDbType.Int32).Value = numCliente;
            command.CommandType = System.Data.CommandType.Text;
            MySqlDataReader dr = command.ExecuteReader();
            dr.Read();
            string data = dr.GetString(0);
            con.Close();
            return data;
        }
        [HttpPost]
        public string PostCliente(string nome)
        {
            if (!string.IsNullOrEmpty(nome))
            {
                MySqlConnection con = new MySqlConnection("server=localhost;port=3306;User Id=root;database=loja;password=" + SenhaMySql);
                con.Open();
                MySqlCommand command = new MySqlCommand("insert into clientes(cod_cliente, nome) values(null, ?)", con);
                command.Parameters.Add("@nome", MySqlDbType.VarChar, 30).Value = nome;
                command.ExecuteNonQuery();
                con.Close();
            }
            return "Nome Cadastrado";
        }
        [HttpDelete]
        public string DeleteCliente(int numCliente)
        {
            MySqlConnection con = new MySqlConnection("server=localhost;port=3306;User Id=root;database=loja;password=" + SenhaMySql);
            con.Open();
            MySqlCommand command = new MySqlCommand("delete from clientes where cod_cliente = ?;", con);
            command.Parameters.Add("@cod_cliente", MySqlDbType.Int32).Value = numCliente;
            command.ExecuteNonQuery();
            con.Close();
            //if (_clientes.Count > 0 && _clientes.Contains(_clientes.First(x => x.Nome.Equals(nome))))
            //{
            //    _clientes.RemoveAt(_clientes.IndexOf(_clientes.First(x => x.Nome.Equals(nome))));
            //}
            return "Cliente Deletado";
        }
    }
}
