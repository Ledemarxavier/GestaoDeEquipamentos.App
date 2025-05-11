namespace GestaoDeEquipamentos.ConsoleApp.View
{
    using System;

    namespace GestaoDeEquipamentos.ConsoleApp
    {
        public class Program
        {
            private static void Main(string[] args)
            {
                TelaChamado telaChamado = new TelaChamado();
                TelaEquipamento telaEquipamento = new TelaEquipamento();
                TelaFabricante telaFabricante = new TelaFabricante();

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Sistema de Gestão de Equipamentos e Chamados");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("1. Gerenciar Equipamentos");
                    Console.WriteLine("2. Gerenciar Chamados");
                    Console.WriteLine("3. Gerenciar Fabricantes");
                    Console.WriteLine("0. Sair");
                    Console.Write("Opção: ");

                    string opcao = Console.ReadLine();

                    switch (opcao)
                    {
                        case "1":
                            telaEquipamento.MenuEquipamentos();
                            break;

                        case "2":
                            telaChamado.MenuChamados();
                            break;

                        case "3":
                            telaFabricante.MenuFabricantes();
                            break;

                        case "0":
                            return;

                        default:
                            Console.WriteLine("Opção inválida!");
                            Console.ReadLine();
                            break;
                    }
                }
            }
        }
    }
}