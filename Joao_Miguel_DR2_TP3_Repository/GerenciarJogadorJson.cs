using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp3.Classes;
using Newtonsoft.Json;

namespace Tp3.Repositorio
{
    public class GerenciarJogadorJson : IGerenciarJogador
    {
        private List<Jogador> jogadores = new List<Jogador>();

        private string path = @"C:../../../../Files/db_jogadores.json";

        public GerenciarJogadorJson() 
        {
            LerArquivo();
        }

        private void LerArquivo()
        {
            using(var file = File.Open(path, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using(var stream = new StreamReader(file))
                { 
                    var json = stream.ReadToEnd();

                    this.jogadores = JsonConvert.DeserializeObject<List<Jogador>>(json);

                    stream.Close();
                }
            }
            if (this.jogadores == null)
            {
                this.jogadores = new List<Jogador>();
            }
        }

        public void EscreverArquivo()
        {
            if(this.jogadores == null)
            {
                return;
            }

            using (var file = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using(var stream = new StreamWriter(file))
                {
                    var json = JsonConvert.SerializeObject(this.jogadores, Formatting.None);
                    json = json.Replace(Environment.NewLine, "");

                    stream.WriteLine(json); 
                    
                    stream.Close();
                }
            }
            if(this.jogadores == null)
            {
                this.jogadores = new List<Jogador>();
            }
        }
        public void SalvarDados(string path, List<Jogador> jogador)
        {
            string json = JsonConvert.SerializeObject(jogador);
            File.WriteAllText(path, json);
        }


        public void ArmazenarJogador(Jogador jogador)
        {
            jogadores.Add(jogador);
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
            var jogador = jogadores.Find(x => x.Nome == nome);
            jogadores.Remove(jogador);
            SalvarDados("../../../../Files/db_jogadores.json", jogadores);
        }

    }
}
