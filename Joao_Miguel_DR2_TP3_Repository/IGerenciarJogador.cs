using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp3.Classes;

namespace Tp3.Repositorio
{
    public interface IGerenciarJogador
    {
        void ArmazenarJogador(Jogador obj);
        List<Jogador> ObterLista();
        List<Jogador> ProcurarJogador(string name);
    }
}
