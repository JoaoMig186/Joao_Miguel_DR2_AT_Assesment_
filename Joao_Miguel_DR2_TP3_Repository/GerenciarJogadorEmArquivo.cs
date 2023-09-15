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
        string path = @"C:\Arquivos\ArquivoJogadores.txt";
        public void ArmazenarJogador(Jogador jogador)
        {
            File.AppendAllLines(path, new string[] {$"{jogador.Id}; {jogador.Nome}; {jogador.Numero}; {jogador.Aposntado}; {jogador.DataAniversario.ToString("dd/MM/yyyy")}; {jogador.CalcularIdade()}"});
        }

        public List<Jogador> ObterLista()
        {
            List<Jogador> jogadores = new List<Jogador>();
            if (File.Exists(path))
            {
                string[] linhas = File.ReadAllLines(path);
                foreach (string linha in linhas)
                {
                    string[] partes = linha.Split(';');

                    if (partes.Length == 6)
                    {
                            Jogador jogador = new Jogador()
                            {

                                Nome = partes[1].Trim().ToString(),
                                Numero = int.Parse(partes[2].Trim()),
                                Aposntado = Convert.ToBoolean(partes[3].Trim()),
                                DataAniversario = DateTime.ParseExact(partes[4].Trim(), "d/M/yyyy", null),
                                Idade = int.Parse(partes[5].Trim())

                            };
                            jogadores.Add(jogador);
                        }
                }
            }
            return jogadores;
        }

        public List<Jogador> ProcurarJogador(string nome)
        {
            //return _jogador.Where(x => x.Nome == nome).ToList();
            return null;
        }

    }
}

