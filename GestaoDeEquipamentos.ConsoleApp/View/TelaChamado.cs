using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace GestaoDeEquipamentos.ConsoleApp.View
{
    public class TelaChamado
    {
        private ChamadoRepository chamadoRepository;
        public EquipamentoRepository equipamentoRepository;

        public TelaChamado(EquipamentoRepository equipamentoRepository, ChamadoRepository chamadoRepository)
        {
            this.equipamentoRepository = equipamentoRepository;
            this.chamadoRepository = chamadoRepository;
        }

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
                        CadastrarChamado();
                        break;

                    case "2":
                        Listar();
                        break;

                    case "3":
                        EditarChamado();
                        break;

                    case "4":
                        DeletarChamado();
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

        public void CadastrarChamado()
        {
            Console.Clear();
            Console.WriteLine("Cadastro de Chamado");
            Console.WriteLine("-------------------");

            Chamado chamado = ObterDados();
            string erros = chamado.Validar();
            if (erros.Length > 0)
            {
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(erros);
                Console.ResetColor();

                Console.Write("\nDigite ENTER para continuar...");
                Console.ReadLine();
                CadastrarChamado();
                return;
            }
            chamadoRepository.CadastrarRegistro(chamado);
            Console.WriteLine("\nChamado cadastrado com sucesso!");
            Console.ReadLine();
        }

        public bool Listar()
        {
            Console.Clear();
            Console.WriteLine("Lista de Chamados");
            Console.WriteLine("-----------------");

            List<EntidadeBase> registros = chamadoRepository.SelecionarRegistros();
            List<Chamado> chamados = registros.OfType<Chamado>().ToList();

            if (chamados == null || chamados.Count == 0)
            {
                Console.WriteLine("\nNenhum chamado cadastrado!");
                Console.ReadLine();
                return false;
            }
            else
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-20} | {3,-20} | {4,-12} | {5,-10}",
                "ID", "Título", "Descrição", "Equipamento", "Data Abertura", "Dias Aberto");

                foreach (Chamado chamado in chamados)
                {
                    int diasAberto = (DateTime.Today - chamado.dataAbertura).Days;
                    Console.WriteLine("{0,-5} | {1,-20} | {2,-20} | {3,-20} | {4,-12:dd/MM/yyyy} | {5,-10}",
        chamado.id, chamado.titulo, chamado.descricao, chamado.equipamento.nome,
        chamado.dataAbertura, diasAberto);
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para Continuar...");
            Console.ReadLine();
            return true;
        }

        public Chamado ObterDados()
        {
            Console.Write("Digite o título do chamado: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a descrição do chamado: ");
            string descricao = Console.ReadLine();

            DateTime dataAbertura = DateTime.Now;

            List<EntidadeBase> registros = equipamentoRepository.SelecionarRegistros();
            List<Equipamento> equipamentos = registros.OfType<Equipamento>().ToList();

            if (equipamentos.Count == 0)
            {
                Console.WriteLine("\nNenhum equipamento cadastrado. Cadastre um equipamento primeiro");

                Console.ReadLine();
                return null;
            }

            Console.WriteLine("\nLista de equipamantos: ");
            foreach (var e in equipamentos)
            {
                Console.WriteLine($"ID: {e.id} | Nome: {e.nome}");
            }

            Console.Write("\nDigite o ID do equipamento que deseja selecionar: ");
            string input = Console.ReadLine();
            int idEquipamento = 0;
            int.TryParse(input, out idEquipamento);

            var idSelecionado = equipamentoRepository.SelecionarRegistroPorId(idEquipamento);

            if (idSelecionado != null)
            {
                bool jaUsado = false;

                List<Chamado> chamados = chamadoRepository.SelecionarRegistros().OfType<Chamado>().ToList();

                foreach (Chamado c in chamados)
                {
                    if (c.equipamento.id == idSelecionado.id)
                    {
                        jaUsado = true;
                        break;
                    }
                }

                if (jaUsado)
                {
                    Console.WriteLine("Este equipamento já está vinculado a um chamado. Selecione outro.");
                    return null;
                }
            }
            return new Chamado(titulo, descricao, (Equipamento)idSelecionado, dataAbertura);
        }

        public bool VisualizarEquipamentos()
        {
            Console.Clear();
            Console.WriteLine("Lista de Equipamentos Disponíveis");
            Console.WriteLine("---------------------------------");

            List<Equipamento> equipamentos = equipamentoRepository.SelecionarRegistros().OfType<Equipamento>().ToList();

            if (equipamentos == null)
            {
                Console.WriteLine("\nNenhum equipamento cadastrado no sistema!");
                Console.WriteLine("\nPressione ENTER para voltar...");
                return false;
            }
            else
            {
                Console.WriteLine("\n{0,-5} | {1,-20} | {2,-15} | {3,-15} | {4,-12}",
                    "ID", "Nome", "Nº Série", "Fabricante", "Data Fabricação");
                Console.WriteLine(new string('-', 80));

                foreach (Equipamento equipamento in equipamentos)
                {
                    Console.WriteLine("{0,-5} | {1,-20} | {2,-15} | {3,-15} | {4,-12:dd/MM/yyyy}",
                        equipamento.id,
                        equipamento.nome,
                        equipamento.numeroSerie,
                        equipamento.fabricante,
                        equipamento.dataFabricacao);
                }
            }
            return true;
        }

        public void EditarChamado()
        {
            Console.Clear();
            Console.WriteLine("Edição de Chamado");
            Console.WriteLine("-----------------");

            bool listaTemChamados = Listar();
            if (!listaTemChamados)
                return;

            Console.Write("\nDigite o ID do chamado a editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            try
            {
                var equipamentoExistente = equipamentoRepository.SelecionarRegistroPorId(id);
                if (equipamentoExistente == null)
                {
                    Console.WriteLine("Chamado não encontrado.");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("\nDigite os novos dados:");
                Chamado dadosAtualizados = ObterDados();

                chamadoRepository.EditarRegistro(id, dadosAtualizados);

                Console.WriteLine("\nChamado atualizado com sucesso!");
            }
            catch
            {
                Console.WriteLine($"\nErro ao atualizar o chamado.");
            }

            Console.ReadLine();
        }

        public void DeletarChamado()
        {
            Console.Clear();
            Console.WriteLine("Exclusão de chamado");
            Console.WriteLine("-----------------------");

            bool listaTemChamados = Listar();
            if (!listaTemChamados)
                return;

            Console.Write("\nDigite o ID do chamado a ser excluído: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            var chamadoDeletar = chamadoRepository.SelecionarRegistroPorId(idSelecionado);

            if (chamadoDeletar == null)
            {
                Console.WriteLine("Chamado não encontrado.");
            }
            else
            {
                bool sucesso = chamadoRepository.ExcluirRegistro(idSelecionado);
                if (sucesso)
                    Console.WriteLine("\nChamado excluído com sucesso!");
                else
                    Console.WriteLine("\nErro ao excluir o chamado.");
            }

            Console.ReadLine();
        }
    }
}