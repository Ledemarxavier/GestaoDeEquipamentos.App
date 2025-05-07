using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeEquipamentos.ConsoleApp.View
{
    public class TelaChamado
    {
        public void MenuChamados()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gerenciamento de Chamados");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("1. Cadastrar Chamado");
                Console.WriteLine("2. Listar Chamados");
                Console.WriteLine("3. Editar Chamado");
                Console.WriteLine("4. Excluir Chamado");
                Console.WriteLine("0. Voltar");
                Console.Write("Opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":

                        break;

                    case "2":

                        break;

                    case "3":

                        break;

                    case "4":

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
    }
}