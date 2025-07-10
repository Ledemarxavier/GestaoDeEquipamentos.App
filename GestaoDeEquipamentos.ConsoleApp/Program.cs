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
            TelaPrincipal telaPrincipal = new TelaPrincipal();
            telaPrincipal.menuPrincipal();
            while (true)
            {
                telaPrincipal.ApresentarMenuPrincipal();

                ITela telaEscolhida = telaPrincipal.ObterTela();

                if (telaEscolhida == null)
                    break;

                char opcaoEscolhida = telaEscolhida.Menu();

                if (char.ToUpper(opcaoEscolhida) == 'S')
                    break;

                switch (opcaoEscolhida)
                {
                    case '1':
                        telaEscolhida.CadastrarRegistro();
                        break;

                    case '2':
                        telaEscolhida.ListarRegistros();

                        break;

                    case '3':
                        telaEscolhida.AtualizarRegistro();

                        break;

                    case '4':
                        telaEscolhida.DeletarRegistro();
                        break;
                }
            }
        }
    }
}