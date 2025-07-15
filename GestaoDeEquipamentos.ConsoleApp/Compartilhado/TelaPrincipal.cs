using GestaoDeEquipamentos.ConsoleApp.View;
using GestaoDeEquipamentos.Infraestrutura.Arquivos.Compartilhado;
using GestaoDeEquipamentos.Infraestrutura.Arquivos.ModuloFabricante;
using GestaoDeEquipamentos.Infraestrutura.ModuloChamado;
using GestaoDeEquipamentos.Infraestrutura.ModuloEquipamento;
using GestaoDeEquipamentos.Infraestrutura.ModuloFabricante;
using System.Security.Cryptography.X509Certificates;

namespace GestaoDeEquipamentos.ConsoleApp.Compartilhado
{
    public class TelaPrincipal
    {
        private ContextoDados contextoDados;
        private char opcaoEscolhida;
        private FabricanteRepositoryEmArquivo fabricanteRepository;
        private EquipamentoRepositoryEmArquivo equipamentoRepository;
        private ChamadoRepositoryEmArquivo chamadoRepository;

        private TelaFabricante telaFabricante;
        private TelaEquipamento telaEquipamento;
        private TelaChamado telaChamado;

        public void menuPrincipal()
        {
            contextoDados = new ContextoDados(true);
            fabricanteRepository = new FabricanteRepositoryEmArquivo(contextoDados);

            equipamentoRepository = new EquipamentoRepositoryEmArquivo(contextoDados);
            chamadoRepository = new ChamadoRepositoryEmArquivo(contextoDados);

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