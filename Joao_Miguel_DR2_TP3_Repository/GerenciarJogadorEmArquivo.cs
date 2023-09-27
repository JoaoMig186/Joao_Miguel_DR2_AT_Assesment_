using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp3.Classes;

namespace Tp3.Repositorio
{
    public class GerenciarJogadorEmArquivo : IGerenciarJogador
    {
        string path = @"../../../../Files/ArquivoJogadores.txt";

        private List<Jogador> jogadores= new List<Jogador>();
        public GerenciarJogadorEmArquivo() 
        {
            LerArquivo();
        }
        public void ArmazenarJogador(Jogador jogador)
        {
            jogadores.Add(jogador); 
        }
        public void EscreverArquivo() 
        {
            string[] linhas = jogadores.Select(jogador => $"{jogador.Id}; {jogador.Nome}; {jogador.Numero}; {jogador.Aposntado}; {jogador.DataAniversario.ToString("dd/MM/yyyy")}; {jogador.CalcularIdade()}").ToArray();
            File.WriteAllLines(path, linhas);
        }

        private void LerArquivo() 
        {
            if (jogadores.Count == 0 && File.Exists(path))
            {
                string[] linhas = File.ReadAllLines(path);
                foreach (string linha in linhas)
                {
                    string[] partes = linha.Split(';');

                    if (partes.Length == 6)
                    {
                        Jogador jogador = new Jogador();


                        jogador.Nome = partes[1].Trim().ToString();
                        if (int.TryParse(partes[2].Trim().ToString(), out int numero))
                        {
                            jogador.Numero = numero;
                        }
                        else
                        {
                            Console.WriteLine("Erro de converção NUMERO");
                        }

                        if (bool.TryParse(partes[3].Trim().ToString(), out bool aposntado))
                        {
                            jogador.Aposntado = aposntado;
                        }
                        else
                        {
                            Console.WriteLine("Erro de converção APOSENTADO");
                        }

                        if (DateTime.TryParseExact(partes[4].Trim(), "d/M/yyyy", null, DateTimeStyles.None, out DateTime dataAniversario))
                        {
                            jogador.DataAniversario = dataAniversario;
                        }
                        else
                        {
                            Console.WriteLine("Erro de converção DATA ANIVERSÁRIO");
                        }

                        if (int.TryParse(partes[5].Trim(), out int idade))
                        {
                            jogador.Idade = idade;
                        }
                        else
                        {
                            Console.WriteLine("Erro de converção IDADE");
                        }
                        jogadores.Add(jogador);
                    }
                }
            }
        }

        public List<Jogador> ObterLista()
        {
            return jogadores;
        }

        public List<Jogador> ProcurarJogador(string nome)
        {
            return jogadores.Where(x => x.Nome == nome).ToList();
        }
        public void ExcluirJogadores(string nome)
        {
            var jog = jogadores.Find(x => x.Nome == nome);
            jogadores.Remove(jog);
            
        }

    }
}

