using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeEquipamentos.ConsoleApp.View
{
    public class TelaFabricante
    {
        public FabricanteRepository fabricanteRepository;

        public TelaFabricante(FabricanteRepository fabricanteRepository)
        {
            this.fabricanteRepository = fabricanteRepository;
        }

        public void MenuFabricantes()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gerenciamento de Fabricantes");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("1. Cadastrar Fabricante");
                Console.WriteLine("2. Listar Fabricantes");
                Console.WriteLine("3. Editar Frabricante");
                Console.WriteLine("4. Excluir Fabricante");
                Console.WriteLine("0. Voltar");
                Console.Write("Opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CadastrarFabricante();
                        break;

                    case "2":
                        ListarFabricantes();

                        break;

                    case "3":
                        AtualizarFabricante();

                        break;

                    case "4":
                        DeletarFabricante();
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

        public void CadastrarFabricante()
        {
            Console.Clear();
            Console.WriteLine("\nCadastro de Fabricantes");
            Console.WriteLine("----------------------");

            Fabricante fabricante = ObterDados();

            string erros = fabricante.Validar();

            if (erros.Length > 0)
            {
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(erros);
                Console.ResetColor();

                Console.Write("\nDigite ENTER para continuar...");
                Console.ReadLine();
                CadastrarFabricante();
                return;
            }
            fabricanteRepository.InserirFabricante(fabricante);
            Console.WriteLine("\nFabricante cadastrado com sucesso!");
            Console.ReadLine();
        }

        public Fabricante ObterDados()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("email: ");

            string email = Console.ReadLine();

            Console.Write("telefone: ");
            string telefone = Console.ReadLine();

            return new Fabricante(nome, email, telefone);
        }

        public bool ListarFabricantes()
        {
            Console.Clear();
            Console.WriteLine("Lista de Fabricantes");
            Console.WriteLine("---------------------");

            var fabricante = fabricanteRepository.Listar();

            if (fabricante == null || fabricante.Count == 0)
            {
                Console.WriteLine("\nNenhum fabricante cadastrado.");
                Console.ReadLine();
                return false;
            }
            else
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-10} | {3,-15}",
                    "Id", "Nome", "Email", "Telefone");

                foreach (var f in fabricante)
                {
                    Console.WriteLine("{0,-5} | {1,-20} | {2,-10:C2} | {3,-15}",
                        f.id, f.nome, f.email, f.telefone);
                }
            }

            Console.WriteLine("\nPressione a tecla ENTER para continuar...");
            Console.ReadLine();
            return true;
        }

        public void AtualizarFabricante()
        {
            Console.Clear();
            Console.WriteLine("Edição de Frabricante");
            Console.WriteLine("---------------------");

            if (!ListarFabricantes())
                return;

            Console.Write("\nDigite o ID do fabricante a editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            try
            {
                var fabricanteExistente = fabricanteRepository.ObterPorId(id);
                if (fabricanteExistente == null)
                {
                    Console.WriteLine("Fabricante não encontrado.");
                    Console.ReadLine();
                }

                Console.WriteLine("\nDigite os novos dados:");
                Fabricante dadosAtualizados = ObterDados();

                fabricanteRepository.EditarFabricante(id, dadosAtualizados);

                Console.WriteLine("\nFabricante atualizado com sucesso!");
            }
            catch
            {
                Console.WriteLine($"\nErro ao atualizar o fabricante.");
            }

            Console.ReadLine();
        }

        public void DeletarFabricante()
        {
            Console.Clear();
            Console.WriteLine("Exclusão de Fabricante");
            Console.WriteLine("-----------------------");

            if (!ListarFabricantes())

                return;

            Console.Write("\nDigite o ID do fabricante a ser excluído: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            var fabricanteDeletar = fabricanteRepository.ObterPorId(idSelecionado);

            if (fabricanteDeletar == null)
            {
                Console.WriteLine("Fabricante não encontrado.");
            }
            else
            {
                bool sucesso = fabricanteRepository.ExcluirFabricante(idSelecionado);

                Console.WriteLine("\nFabricante excluído com sucesso!");
            }

            Console.ReadLine();
        }
    }
}