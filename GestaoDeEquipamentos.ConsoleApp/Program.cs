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
        }
    }
}