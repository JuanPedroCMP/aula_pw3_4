namespace aula_4.Models
{
    public class Pessoa
    {

        public string Nome { get; set; }
        public int Idade { get; set; }      

        public static List<Pessoa> Lista
        {
            get
            {
                var lista = new List<Pessoa>
                {
                    new() { Nome = "Helder", Idade = 115 },
                    new() { Nome = "Alex", Idade = 35 },
                    new() { Nome = "Benir", Idade = 18 },
                };

                return lista;
            }

        }
    }
}
