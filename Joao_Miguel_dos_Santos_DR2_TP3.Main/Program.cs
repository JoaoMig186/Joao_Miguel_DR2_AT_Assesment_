﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp3.Classes;
using Tp3.Repositorio;


namespace Joao_Miguel_DR2_TP3
{
    public class Program
    {
        private static IGerenciarJogador gerenciador;
        private static string salvarOuSair;
        static void Main(string[] args) 
        {
            Console.WriteLine("\nEscolha qual opção você deseja salvar. (L) Lista / (A) Arquivo.");
            string arquivoOuLista = Console.ReadLine();

            if (arquivoOuLista.ToUpper() == "L")
            {
                gerenciador = new GenrenciarJogadorEmMemoria();
                salvarOuSair = "Sair";
                Menu();
            }
            else if (arquivoOuLista.ToUpper() == "A")
            {
                salvarOuSair = "Salvar e sair";

                Console.WriteLine("Escolha um formato para salvar. \n[1] - .txt\n[2] - .json");
                string formato = Console.ReadLine();
                if (formato == "1")
                {
                    gerenciador = new GerenciarJogadorEmArquivo();
                }
                else if (formato == "2")
                {
                    gerenciador = new GerenciarJogadorJson();
                }
                else
                {
                    Console.WriteLine("\nOpção inválida. Reinicie o programa!");
                }

                List<Jogador> jogadores = gerenciador.ObterLista();
                if (jogadores.Count == 0)
                {
                    Console.WriteLine("Você ainda não cadastrou nenhum jogador.");
                }

                int startIndex = Math.Max(jogadores.Count - 5, 0); 

                for (int i = startIndex; i < jogadores.Count; i++)
                {
                    jogadores[i].ExibirJogador();
                }
                Menu();
            }
            else
            {
                Console.WriteLine("\nOpção inválida. Reinicie o programa!");
                Console.ReadLine();
            }
        }
        public static void Menu()
        {
            Console.WriteLine();
            Console.WriteLine("Menu:");
            Console.WriteLine("\n[1] - Cadastrar jogador");
            Console.WriteLine("[2] - Exibir jogador");
            Console.WriteLine("[3] - Procurar jogador");
            Console.WriteLine("[4] - Atualizar jogador");
            Console.WriteLine("[5] - Excluir jogador");
            Console.WriteLine("[6] - " + salvarOuSair);
            Console.WriteLine();
            Console.Write("Escreva a opção: ");
            string opcao = Console.ReadLine();
            Console.WriteLine();

            if (opcao == "1")
            {
                CadastrarJogador();
                Menu();
            }
            else if (opcao == "2")
            {
                Exibir();
                Menu();
            }
            else if (opcao == "3")
            {
                ProcurarJogador();
                Menu();
            }
            else if(opcao == "4")
            {
                AtualizarJogador();
                Menu();
            }
            else if (opcao == "5")
            {
                ExcluirJogador();
                Menu();
            }
            else if ((opcao == "6") && (salvarOuSair == "Salvar e sair"))
            {
                Salvar();
                Console.WriteLine("Obrigado por utilizar o programa! :)");
            }
            else if ((opcao == "6") && (salvarOuSair =="Sair"))
            {
                Console.WriteLine("Obrigado por utilizar o programa! :)");
            }
            else
            {
                Console.WriteLine("Você digitou uma opção inválida, tente novamente.");
                Menu();
            }
        }

        public static DateTime LerDataValida()
        {
            DateTime data;
            bool dataValida = false;

            do
            {
                string entradaData = Console.ReadLine();
                if (DateTime.TryParseExact(entradaData, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data))
                {
                    dataValida = true;
                }
                else
                {
                    Console.WriteLine("Data inválida. Tente novamente (Formato: dd/MM/yyyy): ");
                }
            } while (!dataValida);

            return data;
        }

        private static void Salvar()
        { 
            if (gerenciador is GerenciarJogadorEmArquivo temp)
            {
                temp.EscreverArquivo();
            }
            else if(gerenciador is GerenciarJogadorJson tempJ)
            { 
                tempJ.EscreverArquivo();
            }
        }

        private static void CadastrarJogador()
        {
            Jogador jogador = new Jogador();

            Console.Write("Nome: ");
            jogador.Nome = Console.ReadLine();
            Console.Write("Número da camisa: ");
            jogador.Numero = Convert.ToInt32(Console.ReadLine());
            Console.Write("É aposentado?(s/n) ");
            string status = Console.ReadLine();
            while(status.ToLower() != "s" && status.ToLower() != "n")
            {
                Console.WriteLine("Opção inválida");
                Console.Write("É aposentado?(s/n) ");
                status = Console.ReadLine();
            }

            if (status.ToLower() == "s")
            {
                jogador.Aposntado = true;
            }
            else
            {
                jogador.Aposntado = false;   
            }

            Console.Write("Data de nascimento (dd/mm/aaaa): ");
            jogador.DataAniversario = LerDataValida();
            
            gerenciador.ArmazenarJogador(jogador);
        }
        private static void Exibir()
        {
            foreach (var item in gerenciador.ObterLista())
            {
                item.ExibirJogador();
            }
        }

        private static void ProcurarJogador()
        {
            Console.Write("Entre com o nome completo da pessoa que você deseja procurar: ");
            string jogadorProcurado = Console.ReadLine();

            var jogador = gerenciador.ProcurarJogador(jogadorProcurado);

            Console.WriteLine();
            foreach (var item in jogador)
            {
                item.ExibirJogador();
            }
        }

        private static void AtualizarJogador()
        {
            Console.Write("Coloque o nome do jogador que você deseja atualizar: ");
            string nome = Console.ReadLine();
            var listajogadores = gerenciador.ProcurarJogador(nome);
            Console.WriteLine("Pesquisando...");

            foreach (var item in listajogadores)
            {
                item.ExibirJogador();

                Console.WriteLine("Atualizar este jogador? 'S' para sim e 'N' para não.");
                string opcao = Console.ReadLine();

                if (opcao.ToUpper() == "S")
                { 
                    Jogador jogador = new Jogador();
                    gerenciador.ExcluirJogadores(item.Nome);
                    Console.Write("Nome: ");
                    jogador.Nome = Console.ReadLine();
                    Console.Write("Número da camisa: ");
                    jogador.Numero = Convert.ToInt32(Console.ReadLine());
                    Console.Write("É aposentado?(s/n) ");
                    string status = Console.ReadLine();
                    while (status.ToLower() != "s" && status.ToLower() != "n")
                    {
                        Console.WriteLine("Opção inválida");
                        Console.Write("É aposentado?(s/n) ");
                        status = Console.ReadLine();
                    }

                    if (status.ToLower() == "s")
                    {
                        jogador.Aposntado = true;
                    }
                    else
                    {
                        jogador.Aposntado = false;
                    }

                    Console.Write("Data de nascimento (dd/mm/aaaa): ");
                    jogador.DataAniversario = LerDataValida();

                    gerenciador.ArmazenarJogador(jogador);

                } 
            } 
        } 

        private static void ExcluirJogador()
        {
            Console.Write("Coloque o nome do jogador que você deseja excluir: ");
            string nome = Console.ReadLine();
            var listajogadores = gerenciador.ProcurarJogador(nome);
            Console.WriteLine("Pesquisando...");

            foreach (var item in listajogadores)
            {
                item.ExibirJogador();

                Console.WriteLine("Exlucir este jogador? 'S' para sim e 'N' para não.");
                string opcao = Console.ReadLine();

                if (opcao.ToUpper() == "S")
                {
                    gerenciador.ExcluirJogadores(item.Nome);
                    Console.WriteLine("O jogador '" + item.Nome +"' foi excluído.");
                }

            }
        }

    }
}
