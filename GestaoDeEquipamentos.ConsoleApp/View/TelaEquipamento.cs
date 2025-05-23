using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System;

//using static System.Net.Mime.MediaTypeNames;

namespace GestaoDeEquipamentos.ConsoleApp.View
{
    public class TelaEquipamento
    {
        private EquipamentoRepository equipamentoRepository;
        private FabricanteRepository fabricanteRepository;

        public TelaEquipamento(FabricanteRepository fabricanteRepository, EquipamentoRepository equipamentoRepository)
        {
            this.fabricanteRepository = fabricanteRepository;
            this.equipamentoRepository = equipamentoRepository;
        }

        public void MenuEquipamentos()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gerenciamento de Equipamentos");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("1. Cadastrar Equipamento");
                Console.WriteLine("2. Listar Equipamentos");
                Console.WriteLine("3. Editar Equipamento");
                Console.WriteLine("4. Excluir Equipamento");
                Console.WriteLine("0. Voltar");
                Console.Write("Opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CadastrarEquipamento();
                        break;

                    case "2":
                        ListarEquipamentos();

                        break;

                    case "3":
                        AtualizarEquipamento();

                        break;

                    case "4":
                        DeletarEquipamento();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public void CadastrarEquipamento()
        {
            Console.Clear();
            Console.WriteLine("\nCadastro de Equipamentos");
            Console.WriteLine("----------------------");

            Equipamento equipamento = ObterDados();

            if (equipamento == null)
                return;

            string erros = equipamento.Validar();

            if (erros.Length > 0)
            {
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(erros);
                Console.ResetColor();

                Console.Write("\nDigite ENTER para continuar...");
                Console.ReadLine();
                CadastrarEquipamento();
                return;
            }
            equipamentoRepository.CadastrarRegistro(equipamento);
            Console.WriteLine("\nEquipamento cadastrado com sucesso!");

            Console.ReadLine();
        }

        public Equipamento ObterDados()
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

        public bool ListarEquipamentos()
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

        public void AtualizarEquipamento()
        {
            Console.Clear();
            Console.WriteLine("Edição de Equipamento");
            Console.WriteLine("---------------------");

            bool temEquipamentos = ListarEquipamentos();
            if (!temEquipamentos)
                return;

            Console.Write("\nDigite o ID do equipamento a editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            try
            {
                var equipamentoExistente = equipamentoRepository.SelecionarRegistroPorId(id);
                if (equipamentoExistente == null)
                {
                    Console.WriteLine("Equipamento não encontrado.");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("\nDigite os novos dados:");
                Equipamento dadosAtualizados = ObterDados();

                equipamentoRepository.EditarRegistro(id, dadosAtualizados);

                Console.WriteLine("\nEquipamento atualizado com sucesso!");
            }
            catch
            {
                Console.WriteLine($"\nErro ao atualizar o equipamento.");
            }

            Console.ReadLine();
        }

        public void DeletarEquipamento()
        {
            Console.Clear();
            Console.WriteLine("Exclusão de Equipamento");
            Console.WriteLine("-----------------------");

            bool temEquipamentos = ListarEquipamentos();
            if (!temEquipamentos)
                return;

            Console.Write("\nDigite o ID do equipamento a ser excluído: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            var equipamentoDeletar = equipamentoRepository.SelecionarRegistroPorId(idSelecionado);

            if (equipamentoDeletar == null)
            {
                Console.WriteLine("Equipamento não encontrado.");
            }
            else
            {
                bool sucesso = equipamentoRepository.ExcluirRegistro(idSelecionado);
                if (sucesso)
                    Console.WriteLine("\nEquipamento excluído com sucesso!");
                else
                    Console.WriteLine("\nErro ao excluir o equipamento.");
            }

            Console.ReadLine();
        }
    }
}