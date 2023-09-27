namespace Tp3.Classes
{
    public class Jogador
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Numero { get; set; }
        public DateTime DataAniversario { get; set; }
        public bool Aposntado { get; set; }

        private int idade;
        public int Idade
        {
            get
            {
                return CalcularIdade();
            }
            set
            {
                idade= value;
            }
        }

        public Jogador()
            {
                Id= Guid.NewGuid();
            }    

            public void ExibirJogador()
            {
            string status;
            

                if(Aposntado)
                {
                    status = "Aposentado";
                }
                else
                { 
                    status = "Em Atividade";
                }

                Console.WriteLine($"ID: {Id} - {Nome} {Numero} | {DataAniversario.ToString("dd-MM-yyyy")} | Idade: {CalcularIdade()} | status: {status}. ");

            }

            public int CalcularIdade()
            {
            DateTime hoje = DateTime.Today;
            int idade = hoje.Year - DataAniversario.Year;

            if (hoje.Month < DataAniversario.Month || (hoje.Month == DataAniversario.Month && hoje.Day < DataAniversario.Day))
            {
                idade--;
            }

            return idade;
        }
    }
}