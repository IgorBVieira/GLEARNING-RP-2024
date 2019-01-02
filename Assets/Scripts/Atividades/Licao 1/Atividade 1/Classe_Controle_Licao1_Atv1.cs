﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;

public class Classe_Controle_Licao1_Atv1 : MonoBehaviour {

	public GameObject NPC;
	public Dropdown dpw_nomes_1, dpw_nascionalidades_1, dpw_nomes_2, dpw_nascionalidades_2, dpw_nomes_3, dpw_nascionalidades_3, dpw_nomes_4, dpw_nascionalidades_4, dpw_nomes_5, dpw_nascionalidades_5, dpw_nomes_6, dpw_nascionalidades_6;
	public List<GameObject> npcs = new List<GameObject>();
    public List<string> nomes_masculinos;
    public List<string> nomes_femininos;
    public List<string> sobrenomes;        
    public List<string> nascionalidades;

    public List<List<Vector3>> posicoesNPC = new List<List<Vector3>>();
    public List<Vector3> posicoes1 = new List<Vector3>();
    public List<Vector3> posicoes2 = new List<Vector3>();

    public int nivel = 1;

	MySqlCommand comando;
	MySqlDataReader dados;
	void Start () {
		Banco_Conexao conexao = new Banco_Conexao();
		Objeto_Player player = new Objeto_Player();	

		nomes_masculinos = new List<string>();
		nomes_femininos = new List<string>();
		sobrenomes = new List<string>();
		nascionalidades = new List<string>();
		
		posicoes1.Add(new Vector3(-35, 1, 0));
		posicoes1.Add(new Vector3(-31, 1, 0));
		posicoes1.Add(new Vector3(-26, 1, 0));
                              

		posicoes2.Add(new Vector3(-25, 3, 0));
		posicoes2.Add(new Vector3(-31, 3, 0));
		posicoes2.Add(new Vector3(-34, 3, 0));


        posicoesNPC.Add(posicoes1);
        posicoesNPC.Add(posicoes2);        

        try
        {
			conexao.conectarBanco();

			// PEGAR NIVEL
			conexao.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE="+player.Cpf+" AND COD_ATIVIDADE=1";
			comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
			dados = comando.ExecuteReader();
			
			if(dados.HasRows){
				while(dados.Read()){
					int n = (int)dados["NIVEL_ATIVIDADE"];
					if(n <= 10){
						nivel = 1;
					}else if(n <= 20){
						nivel = 2;
					}else{
						nivel = 3;
					}
				}
			}

			dados.Close();
			comando.Dispose();
			
			// PEGAR NOMES MASCULINOS E FEMININOS
			conexao.Sql = "SELECT CONTEUDO_TEXTO, CONTEUDO_TAG1 FROM TB_CONTEUDOS WHERE CONTEUDO_TIPO = 'Nome' AND (CONTEUDO_TAG1 = 'Masculino' OR CONTEUDO_TAG1 = 'Feminino');";
			comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
			dados = comando.ExecuteReader();
			
			if(dados.HasRows){
				while(dados.Read()){
					if(dados["CONTEUDO_TAG1"].ToString() == "Masculino"){
						nomes_masculinos.Add(dados["CONTEUDO_TEXTO"].ToString());						
					}else if(dados["CONTEUDO_TAG1"].ToString() == "Feminino")
                    {
						nomes_femininos.Add(dados["CONTEUDO_TEXTO"].ToString());
					}
				}
			}

			dpw_nomes_1.AddOptions(nomes_masculinos);
			dpw_nomes_1.AddOptions(nomes_femininos);
			dpw_nomes_2.AddOptions(nomes_masculinos);
			dpw_nomes_2.AddOptions(nomes_femininos);
			dpw_nomes_3.AddOptions(nomes_masculinos);
			dpw_nomes_3.AddOptions(nomes_femininos);
			dpw_nomes_4.AddOptions(nomes_masculinos);
			dpw_nomes_4.AddOptions(nomes_femininos);
			dpw_nomes_5.AddOptions(nomes_masculinos);
			dpw_nomes_5.AddOptions(nomes_femininos);
			dpw_nomes_6.AddOptions(nomes_masculinos);
			dpw_nomes_6.AddOptions(nomes_femininos);			

			dados.Close();
			comando.Dispose();

			// PEGAR SOBRENOMES
			conexao.Sql = "SELECT CONTEUDO_TEXTO FROM TB_CONTEUDOS WHERE CONTEUDO_TIPO = 'Sobrenome';";
			comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
			dados = comando.ExecuteReader();

			if(dados.HasRows){
				while(dados.Read()){
					sobrenomes.Add(dados["CONTEUDO_TEXTO"].ToString());
				}
			}

			dados.Close();
			comando.Dispose();

			// PEGAR NASCIONALIDADES
			conexao.Sql = "SELECT CONTEUDO_TEXTO FROM TB_CONTEUDOS WHERE CONTEUDO_TIPO = 'Nacionalidade';";
			comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
			dados = comando.ExecuteReader();

			if(dados.HasRows){
				while(dados.Read()){
					nascionalidades.Add(dados["CONTEUDO_TEXTO"].ToString());
				}
			}

			dpw_nascionalidades_1.AddOptions(nascionalidades);
			dpw_nascionalidades_2.AddOptions(nascionalidades);
			dpw_nascionalidades_3.AddOptions(nascionalidades);
			dpw_nascionalidades_4.AddOptions(nascionalidades);
			dpw_nascionalidades_5.AddOptions(nascionalidades);
			dpw_nascionalidades_6.AddOptions(nascionalidades);

			dados.Close();
			comando.Dispose();

			conexao.fecharBanco();
		}catch{
			
		}

		int nNpcs;

		switch(nivel){
			case 1:
				nNpcs = 2;
				break;
			case 2:
				nNpcs = 4;
				break;
			case 3:
				nNpcs = 6;
				break;
			default:
				nNpcs = 2;
				break;
		}

        int rPosicao1;
        int rPosicao2;
        // GERA NPCS
        for (int i = 0; i < nNpcs; i++){
            rPosicao1 = Random.Range(0, posicoesNPC.Count);
            
            if (posicoesNPC[rPosicao1].Count == 0)
            {
                posicoesNPC.RemoveAt(rPosicao1);
                rPosicao1 = Random.Range(0, posicoesNPC.Count);
            }
            
            rPosicao2 = Random.Range(0, posicoesNPC[rPosicao1].Count);
            
            GameObject npc = Instantiate(NPC, posicoesNPC[rPosicao1][rPosicao2], Quaternion.identity);
			Classe_NPC_Licao1_Atv1 c = npc.GetComponent<Classe_NPC_Licao1_Atv1>();			
			c.Id = i+1;
			npcs.Add(npc);
			posicoesNPC[rPosicao1].RemoveAt(rPosicao2);
            
        }
	}

	public void Metodo_Adicionar_Pontos_Licao1_Atv1(){
		Banco_Conexao conexao = new Banco_Conexao();
		Objeto_Player player = new Objeto_Player();

		try{
			conexao.conectarBanco();

			conexao.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE="+player.Cpf+" AND COD_ATIVIDADE=1";
			comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
			dados = comando.ExecuteReader();
			int n = 0;
			if(dados.HasRows){
				while(dados.Read()){
					n = (int)dados["NIVEL_ATIVIDADE"];
					print(n);					
				}
			}			

			dados.Close();
			comando.Dispose();

			conexao.Sql = "UPDATE TB_NIVEL_ATIVIDADE SET NIVEL_ATIVIDADE="+(n+1)+" WHERE COD_ESTUDANTE="+player.Cpf+" AND COD_ATIVIDADE=1";
			comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
			comando.ExecuteNonQuery();

			conexao.fecharBanco();
		}catch{

		}
	}
    
    public void Metodo_Voltar_Menu_Principal()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("telaPrincipal");
    }
}
