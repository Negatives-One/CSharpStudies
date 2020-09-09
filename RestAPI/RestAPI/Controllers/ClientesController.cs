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
        MySqlConnection con = new MySqlConnection("server=localhost;port=3306;User Id=root;database=loja;password=*esperto*2");
        private static List<Cliente> _clientes = new List<Cliente>();

        [HttpGet]
        public string GetCliente(int coluna)
        {
            con.Open();
            MySqlCommand command = new MySqlCommand("select * from clientes", con);
            command.CommandType = System.Data.CommandType.Text;
            MySqlDataReader dados = command.ExecuteReader();
            dados.Read();
            string data = dados.GetString(coluna);
            con.Close();
            return data;
        }
        [HttpPost]
        public void PostCliente(string nome)
        {
            if (!string.IsNullOrEmpty(nome))
            {
                con.Open();
                MySqlCommand command = new MySqlCommand("insert into clientes(cod_cliente, nome) values(null, ?)", con);
                command.Parameters.Add("@nome", MySqlDbType.VarChar, 30).Value = nome;
                command.ExecuteNonQuery();
                con.Close();
                if (CheckRepeat() == false)
                {
                    _clientes.Add(new Cliente(nome));
                }
            }
        }
        [HttpDelete]
        public void DeleteCliente(string nome)
        {
            if (_clientes.Count > 0 && _clientes.Contains(_clientes.First(x => x.Nome.Equals(nome))))
            {
                _clientes.RemoveAt(_clientes.IndexOf(_clientes.First(x => x.Nome.Equals(nome))));
            }
        }

        private bool CheckRepeat()
        {
            for (int i = 0; i < _clientes.Count; i++)
            {
                for (int j = i; j < _clientes.Count; j++)
                {
                    if (_clientes[i] == _clientes[j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
