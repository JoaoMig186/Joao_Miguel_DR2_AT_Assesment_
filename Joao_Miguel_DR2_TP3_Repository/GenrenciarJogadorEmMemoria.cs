using Tp3.Classes;

namespace Tp3.Repositorio
{
    public class GenrenciarJogadorEmMemoria : IGerenciarJogador
    {
        private List<Jogador> _jogador = new List<Jogador>();

        public void ArmazenarJogador(Jogador jogador)
        {
            _jogador.Add(jogador);
        }

        public List<Jogador> ObterLista()
        {
            return _jogador;
        }

        public List<Jogador> ProcurarJogador(string nome)
        {
            return _jogador.Where(x => x.Nome == nome).ToList();
        }

        }
}