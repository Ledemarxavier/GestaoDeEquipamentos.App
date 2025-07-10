using GestaoDeEquipamentos.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using GestaoDeEquipamentos.ConsoleApp.View;
using System.Security.Cryptography.X509Certificates;

namespace GestaoDeEquipamentos.ConsoleApp.Compartilhado
{
    public class TelaPrincipal
    {
        private char opcaoEscolhida;
        private FabricanteRepository fabricanteRepository;
        private EquipamentoRepository equipamentoRepository;
        private ChamadoRepository chamadoRepository;

        private TelaFabricante telaFabricante;
        private TelaEquipamento telaEquipamento;
        private TelaChamado telaChamado;

        public void menuPrincipal()
        {
            fabricanteRepository = new FabricanteRepository();

            equipamentoRepository = new EquipamentoRepository();
            chamadoRepository = new ChamadoRepository();

            telaFabricante = new TelaFabricante(fabricanteRepository);

            telaEquipamento = new TelaEquipamento(fabricanteRepository,
                equipamentoRepository

            );
            telaChamado = new TelaChamado(chamadoRepository, equipamentoRepository);
        }

        public void ApresentarMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("Sistema de Gestão de Equipamentos e Chamados");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("1. Gerenciar Equipamentos");
            Console.WriteLine("2. Gerenciar Chamados");
            Console.WriteLine("3. Gerenciar Fabricantes");
            Console.WriteLine("S. Sair");

            Console.WriteLine();

            Console.Write("Escolha uma das opções: ");
            opcaoEscolhida = Console.ReadLine()[0];
        }

        public ITela? ObterTela()
        {
            if (opcaoEscolhida == '1')
                return telaEquipamento;
            else if (opcaoEscolhida == '2')
                return telaChamado;
            else if (opcaoEscolhida == '3')
                return telaFabricante;

            return null;
        }
    }
}