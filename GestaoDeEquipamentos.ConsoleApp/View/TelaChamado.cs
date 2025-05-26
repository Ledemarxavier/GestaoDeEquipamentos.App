using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

namespace GestaoDeEquipamentos.ConsoleApp.View
{
    public class TelaChamado : TelaBase
    {
        private ChamadoRepository chamadoRepository;
        public EquipamentoRepository equipamentoRepository;

        public TelaChamado(EquipamentoRepository equipamentoRepository, ChamadoRepository chamadoRepository)
            : base("Chamado", chamadoRepository)
        {
            this.equipamentoRepository = equipamentoRepository;
            this.chamadoRepository = chamadoRepository;
        }

        public override bool ListarRegistros()
        {
            Console.Clear();
            Console.WriteLine("Lista de Chamados");
            Console.WriteLine("-----------------");

            List<EntidadeBase> registros = chamadoRepository.SelecionarRegistros();
            List<Chamado> chamados = registros.OfType<Chamado>().ToList();

            if (chamados == null || chamados.Count == 0)
            {
                Console.WriteLine("\nNenhum chamado cadastrado!");
                Console.ReadLine();
                return false;
            }
            else
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-20} | {3,-20} | {4,-12} | {5,-10}",
                "ID", "Título", "Descrição", "Equipamento", "Data Abertura", "Dias Aberto");

                foreach (Chamado chamado in chamados)
                {
                    int diasAberto = (DateTime.Today - chamado.dataAbertura).Days;
                    Console.WriteLine("{0,-5} | {1,-20} | {2,-20} | {3,-20} | {4,-12:dd/MM/yyyy} | {5,-10}",
        chamado.id, chamado.titulo, chamado.descricao, chamado.equipamento.nome,
        chamado.dataAbertura, diasAberto);
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para Continuar...");
            Console.ReadLine();
            return true;
        }

        protected override Chamado ObterDados()
        {
            Console.Write("Digite o título do chamado: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a descrição do chamado: ");
            string descricao = Console.ReadLine();

            DateTime dataAbertura = DateTime.Now;

            List<EntidadeBase> registros = equipamentoRepository.SelecionarRegistros();
            List<Equipamento> equipamentos = registros.OfType<Equipamento>().ToList();

            if (equipamentos.Count == 0)
            {
                Console.WriteLine("\nNenhum equipamento cadastrado. Cadastre um equipamento primeiro");

                Console.ReadLine();
                return null;
            }

            Console.WriteLine("\nLista de equipamantos: ");
            foreach (var e in equipamentos)
            {
                Console.WriteLine($"ID: {e.id} | Nome: {e.nome}");
            }

            Console.Write("\nDigite o ID do equipamento que deseja selecionar: ");
            string input = Console.ReadLine();
            int idEquipamento = 0;
            int.TryParse(input, out idEquipamento);

            var idSelecionado = equipamentoRepository.SelecionarRegistroPorId(idEquipamento);

            if (idSelecionado != null)
            {
                bool jaUsado = false;

                List<Chamado> chamados = chamadoRepository.SelecionarRegistros().OfType<Chamado>().ToList();

                foreach (Chamado c in chamados)
                {
                    if (c.equipamento.id == idSelecionado.id)
                    {
                        jaUsado = true;
                        break;
                    }
                }

                if (jaUsado)
                {
                    Console.WriteLine("Este equipamento já está vinculado a um chamado. Selecione outro.");
                    return null;
                }
            }
            return new Chamado(titulo, descricao, (Equipamento)idSelecionado, dataAbertura);
        }

        public bool VisualizarEquipamentos()
        {
            Console.Clear();
            Console.WriteLine("Lista de Equipamentos Disponíveis");
            Console.WriteLine("---------------------------------");

            List<Equipamento> equipamentos = equipamentoRepository.SelecionarRegistros().OfType<Equipamento>().ToList();

            if (equipamentos == null)
            {
                Console.WriteLine("\nNenhum equipamento cadastrado no sistema!");
                Console.WriteLine("\nPressione ENTER para voltar...");
                return false;
            }
            else
            {
                Console.WriteLine("\n{0,-5} | {1,-20} | {2,-15} | {3,-15} | {4,-12}",
                    "ID", "Nome", "Nº Série", "Fabricante", "Data Fabricação");
                Console.WriteLine(new string('-', 80));

                foreach (Equipamento equipamento in equipamentos)
                {
                    Console.WriteLine("{0,-5} | {1,-20} | {2,-15} | {3,-15} | {4,-12:dd/MM/yyyy}",
                        equipamento.id,
                        equipamento.nome,
                        equipamento.numeroSerie,
                        equipamento.fabricante,
                        equipamento.dataFabricacao);
                }
            }
            return true;
        }
    }
}