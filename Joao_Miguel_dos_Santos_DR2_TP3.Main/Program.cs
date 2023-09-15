using System;
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
        private static IGerenciarJogador gerenciador = new GerenciarJogadorEmArquivo();
        static void Main(string[] args) 
        {
            Menu();
        }
        public static void Menu()
        {
            Console.WriteLine();
            Console.WriteLine("Menu:");
            Console.WriteLine("\n[1] - Cadastrar jogador");
            Console.WriteLine("[2] - Exibir jogador");
            Console.WriteLine("[3] - Procurar jogador");
            Console.WriteLine("[4] - Sair");
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
            else if (opcao == "4")
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

    }
}
