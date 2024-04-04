using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class Banco_Conexao {

	private string linhaConexao = @"
	Server=sql10.freemysqlhosting.net;
	Port=3306;
	Database=sql10692791;
	Uid=sql10692791;
	Pwd=w2RYBidDvh;
	Pooling=false;
	old guids=true;
	AllowUserVariables=True;
	CharSet=latin1";

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