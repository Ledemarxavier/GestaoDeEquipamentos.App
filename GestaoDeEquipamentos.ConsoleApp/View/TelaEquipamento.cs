using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (equipamento.nome.Length > 10)
            {
                Console.WriteLine("Nome deve ter no máximo 10 caracteres.");
            }
            else if (equipamento.preco <= 0)
            {
                Console.WriteLine("Preço deve ser maior que zero.");
            }
            else
            {
                equipamentoRepository.InserirEquipamento(equipamento);
                Console.WriteLine("\nEquipamento cadastrado com sucesso!");
            }

            Console.ReadLine();
        }

        public Equipamento ObterDados()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            decimal preco;
            while (true)
            {
                Console.Write("Preço: ");
                if (decimal.TryParse(Console.ReadLine(), out preco) && preco > 0)
                    break;
                Console.WriteLine("Entrada inválida. Digite um valor numérico maior que zero.");
            }

            Console.Write("Número de Série: ");
            string numeroSerie = Console.ReadLine();

            DateTime dataFabricacao;
            while (true)
            {
                Console.Write("Data de Fabricação (dd/mm/aaaa): ");

                if (DateTime.TryParse(Console.ReadLine(), out dataFabricacao))
                    break;
                Console.WriteLine("Data inválida. Digite novamente.");
            }
            var fabricantes = fabricanteRepository.Listar();

            if (fabricantes.Count == 0)
            {
                Console.WriteLine("Nenhum fabricante cadastrado. Cadastre um fabricante primeiro.");
                return null;
            }
            Console.WriteLine("\nLista de fabricantes: ");
            foreach (var f in fabricantes)
            {
                Console.WriteLine($"ID: {f.id} | Nome: {f.nome}");
            }
            Console.Write("Digite o id do fabricante: ");

            int idFabricante = Convert.ToInt32(Console.ReadLine());
            Fabricante fabricanteSelecionado = fabricanteRepository.ObterPorId(idFabricante);

            if (fabricanteSelecionado == null)
            {
                Console.WriteLine("Fabricante não encontrado.");
                return null;
            }

            return new Equipamento(nome, preco, numeroSerie, dataFabricacao, fabricanteSelecionado);
        }

        public bool ListarEquipamentos()
        {
            Console.Clear();
            Console.WriteLine("Lista de Equipamentos");
            Console.WriteLine("---------------------");

            var equipamentos = equipamentoRepository.Listar();

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
                        e.fabricante, e.dataFabricacao);
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
                var equipamentoExistente = equipamentoRepository.ObterPorId(id);
                if (equipamentoExistente == null)
                {
                    Console.WriteLine("Equipamento não encontrado.");
                    Console.ReadLine();
                }

                Console.WriteLine("\nDigite os novos dados:");
                Equipamento dadosAtualizados = ObterDados();

                equipamentoRepository.EditarEquipamento(id, dadosAtualizados);

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

            var equipamentoDeletar = equipamentoRepository.ObterPorId(idSelecionado);

            if (equipamentoDeletar == null)
            {
                Console.WriteLine("Equipamento não encontrado.");
            }
            else
            {
                bool sucesso = equipamentoRepository.ExcluirEquipamento(idSelecionado);
                if (sucesso)
                    Console.WriteLine("\nEquipamento excluído com sucesso!");
                else
                    Console.WriteLine("\nErro ao excluir o equipamento.");
            }

            Console.ReadLine();
        }
    }
}