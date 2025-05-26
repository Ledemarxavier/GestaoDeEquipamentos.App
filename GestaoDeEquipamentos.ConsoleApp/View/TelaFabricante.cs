using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

using GestaoDeEquipamentos.ConsoleApp.Compartilhado;

namespace GestaoDeEquipamentos.ConsoleApp.View
{
    public class TelaFabricante : TelaBase
    {
        private FabricanteRepository fabricanteRepository;

        public TelaFabricante(FabricanteRepository fabricanteRepository)
        : base("Fabricante", fabricanteRepository)
        {
            this.fabricanteRepository = fabricanteRepository;
        }

        protected override Fabricante ObterDados()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("email: ");

            string email = Console.ReadLine();

            Console.Write("telefone: ");
            string telefone = Console.ReadLine();

            return new Fabricante(nome, email, telefone);
        }

        public override bool ListarRegistros()
        {
            Console.Clear();
            Console.WriteLine("Lista de Fabricantes");
            Console.WriteLine("---------------------");

            List<EntidadeBase> registros = fabricanteRepository.SelecionarRegistros();

            List<Fabricante> fabricantes = registros.OfType<Fabricante>().ToList();

            if (fabricantes == null || fabricantes.Count == 0)
            {
                Console.WriteLine("\nNenhum fabricante cadastrado.");
                Console.ReadLine();
                return false;
            }
            else
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-10} | {3,-15}",
                    "Id", "Nome", "Email", "Telefone");

                foreach (Fabricante f in fabricantes)
                {
                    Console.WriteLine("{0,-5} | {1,-20} | {2,-10} | {3,-15}",
                        f.id, f.nome, f.email, f.telefone);
                }
            }

            Console.WriteLine("\nPressione a tecla ENTER para continuar...");
            Console.ReadLine();
            return true;
        }
    }
}