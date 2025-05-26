using GestaoDeEquipamentos.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.View;
using System;

namespace GestaoDeEquipamentos.ConsoleApp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            FabricanteRepository fabricanteRepository = new FabricanteRepository();
            EquipamentoRepository equipamentoRepository = new();
            ChamadoRepository chamadoRepository = new();

            TelaFabricante telaFabricante = new(fabricanteRepository);
            TelaEquipamento telaEquipamento = new(fabricanteRepository, equipamentoRepository);

            TelaChamado telaChamado = new(equipamentoRepository, chamadoRepository);

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
                        telaEquipamento.Menu();
                        break;

                    case "2":
                        telaChamado.Menu();
                        break;

                    case "3":
                        telaFabricante.Menu();
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