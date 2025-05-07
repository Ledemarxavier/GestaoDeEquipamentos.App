using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeEquipamentos.ConsoleApp.View
{
    public class TelaEquipamento
    {
        public static EquipamentoRepository equipamentoRepository = new EquipamentoRepository();

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
            else if (equipamento.fabricante.Length > 15)
            {
                Console.WriteLine("Fabricante deve ter no máximo 15 caracteres.");
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
            Equipamento equipamento = new Equipamento();

            Console.Write("Nome: ");
            equipamento.nome = Console.ReadLine();

            Console.Write("Preço: ");
            while (true)
            {
                try
                {
                    string entrada = Console.ReadLine();
                    decimal preco = Convert.ToDecimal(entrada);

                    equipamento.preco = preco;
                    break;
                }
                catch
                {
                    Console.WriteLine("Entrada inválida. Digite um valor numérico.");
                }
            }

            Console.Write("Número de Série: ");
            equipamento.numeroSerie = Console.ReadLine();

            Console.Write("Data de Fabricação (dd/mm/aaaa): ");
            while (true)
            {
                try
                {
                    string entrada = Console.ReadLine();
                    DateTime data = Convert.ToDateTime(entrada);
                    equipamento.dataFabricacao = data;
                    break;
                }
                catch
                {
                    Console.WriteLine("Data inválida. Digite novamente.");
                }
            }
            Console.Write("Fabricante: ");
            equipamento.fabricante = Console.ReadLine();

            return equipamento;
        }

        public void ListarEquipamentos()
        {
            Console.Clear();
            Console.WriteLine("Lista de Equipamentos");
            Console.WriteLine("---------------------");

            var equipamentos = equipamentoRepository.Listar();

            if (equipamentos == null || equipamentos.Count == 0)
            {
                Console.WriteLine("\nNenhum equipamento cadastrado.");
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
            return;
        }

        public void AtualizarEquipamento()
        {
            Console.Clear();
            Console.WriteLine("Edição de Equipamento");
            Console.WriteLine("---------------------");

            ListarEquipamentos();

            Console.Write("\nDigite o ID do equipamento a editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            try
            {
                var equipamentoExistente = equipamentoRepository.ObterPorId(id);
                if (equipamentoExistente == null)
                {
                    Console.WriteLine("Equipamento não encontrado.");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("\nDigite os novos dados:");
                Equipamento dadosAtualizados = ObterDados();

                equipamentoRepository.EditarEquipamento(id, dadosAtualizados);

                Console.WriteLine("\nEquipamento atualizado com sucesso!");
            }
            catch
            {
                Console.WriteLine($"\nErro ao excluir o equipamento.");
            }

            Console.ReadLine();
        }

        public void DeletarEquipamento()
        {
            Console.Clear();
            Console.WriteLine("Exclusão de Equipamento");
            Console.WriteLine("-----------------------");

            ListarEquipamentos();

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