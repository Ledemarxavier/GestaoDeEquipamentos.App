using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;

namespace GestaoDeEquipamentos.ConsoleApp.View
{
    public class TelaEquipamento : TelaBase
    {
        private EquipamentoRepository equipamentoRepository;
        private FabricanteRepository fabricanteRepository;

        public TelaEquipamento(FabricanteRepository fabricanteRepository, EquipamentoRepository equipamentoRepository)
        : base("Equipamento", equipamentoRepository)
        {
            this.fabricanteRepository = fabricanteRepository;
            this.equipamentoRepository = equipamentoRepository;
        }

        protected override Equipamento ObterDados()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Preço: ");
            string entradaPreco = Console.ReadLine();
            decimal preco;
            decimal.TryParse(entradaPreco, out preco);

            Console.Write("Número de Série: ");
            string numeroSerie = Console.ReadLine();

            Console.Write("Data de Fabricação (dd/mm/aaaa): ");
            string entradaData = Console.ReadLine();
            DateTime dataFabricacao;
            DateTime.TryParse(entradaData, out dataFabricacao);

            List<EntidadeBase> registros = fabricanteRepository.SelecionarRegistros();
            List<Fabricante> fabricantes = registros.OfType<Fabricante>().ToList();

            if (fabricantes.Count == 0)
            {
                Console.WriteLine("Nenhum fabricante cadastrado. Cadastre um fabricante primeiro.");
                Console.ReadLine();
                return null;
            }

            Console.WriteLine("\nLista de fabricantes: ");
            foreach (var f in fabricantes)
            {
                Console.WriteLine($"ID: {f.id} | Nome: {f.nome}");
            }
            Console.Write("Digite o id do fabricante: ");
            string entradaIdFabricante = Console.ReadLine();

            int idFabricante = 0;
            int.TryParse(entradaIdFabricante, out idFabricante);
            EntidadeBase registro = fabricanteRepository.SelecionarRegistroPorId(idFabricante);

            return new Equipamento(nome, preco, numeroSerie, dataFabricacao, (Fabricante)registro);
        }

        public override bool ListarRegistros()
        {
            Console.Clear();
            Console.WriteLine("Lista de Equipamentos");
            Console.WriteLine("---------------------");

            List<EntidadeBase> registros = equipamentoRepository.SelecionarRegistros();

            List<Equipamento> equipamentos = registros.OfType<Equipamento>().ToList();

            if (equipamentos == null || equipamentos.Count == 0)
            {
                Console.WriteLine("\nNenhum equipamento cadastrado.");
                Console.ReadLine();
                return false;
            }
            else
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-10} | {3,-15} | {4,-15} | {5,-12}",
                    "Id", "Nome", "Preço", "Número de Série", "Fabricante", "Data Fabricação");

                foreach (var e in equipamentos)
                {
                    Console.WriteLine("{0,-5} | {1,-20} | {2,-10:C2} | {3,-15} | {4,-15} | {5,-12:dd/MM/yyyy}",
                        e.id, e.nome, e.preco, e.numeroSerie,
                        e.fabricante.nome, e.dataFabricacao);
                }
            }

            Console.WriteLine("\nPressione a tecla ENTER para continuar...");
            Console.ReadLine();
            return true;
        }
    }
}