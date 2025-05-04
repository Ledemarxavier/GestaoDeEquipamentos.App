using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeEquipamentos.ConsoleApp
{
    public class View
    {
        public void MenuPrincipal()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Sistema de Gestão de Equipamentos e Chamados");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("1. Gerenciar Equipamentos");
                Console.WriteLine("2. Gerenciar Chamados");
                Console.WriteLine("0. Sair");
                Console.Write("Opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":

                        break;

                    case "2":

                        break;

                    case "0":
                        return;

                    default:

                        break;
                }
            }
        }
    }
}