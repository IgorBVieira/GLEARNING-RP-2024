using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class Banco_Conexao {

	private string linhaConexao = @"
	Server=ryk.h.filess.io;
	Port=3307;
	Database=glearning_tryaverage;
	Uid=glearning_tryaverage;
	Pwd=a72c51207b78e1b14486fab69142c41442f89a59;
	Pooling=false;
	old guids=true;
	AllowUserVariables=True;
	CharSet=utf8mb4";

  private static MySqlConnection conexaoBanco;
	private static string sql;

	public string Sql
	{
		get
		{
			return sql;
		}
		set
		{
			sql = value;
		}
	}

	public MySqlConnection ConexaoBanco
	{
		get
		{
			return conexaoBanco;
		}
		set
		{
			conexaoBanco = value;
		}
	}

	public void conectarBanco(){
		ConexaoBanco = new MySqlConnection(this.linhaConexao);
		ConexaoBanco.Open();
	}

	public void fecharBanco(){
		ConexaoBanco.Close();
	}
}