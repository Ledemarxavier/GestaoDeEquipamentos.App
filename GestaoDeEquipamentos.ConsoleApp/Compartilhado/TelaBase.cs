﻿namespace GestaoDeEquipamentos.ConsoleApp.Compartilhado
{
    public abstract class TelaBase
    {
        private string nomeEntidade;
        private BaseRepository repository;

        protected TelaBase(string nomeEntidade, BaseRepository repository)
        {
            this.nomeEntidade = nomeEntidade;
            this.repository = repository;
        }

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Gerenciamento de {nomeEntidade}");
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"1. Cadastrar {nomeEntidade}");
                Console.WriteLine($"2. Listar {nomeEntidade}");
                Console.WriteLine($"3. Editar {nomeEntidade}");
                Console.WriteLine($"4. Excluir {nomeEntidade}");
                Console.WriteLine("0. Voltar");
                Console.Write("Opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CadastrarRegistro();
                        break;

                    case "2":
                        ListarRegistros();

                        break;

                    case "3":
                        AtualizarRegistro();

                        break;

                    case "4":
                        DeletarRegistro();
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

        public void CadastrarRegistro()
        {
            Console.Clear();
            Console.WriteLine($"\nCadastro de {nomeEntidade}");
            Console.WriteLine("----------------------");

            EntidadeBase novoRegistro = ObterDados();

            if (novoRegistro == null)
                return;

            string erros = novoRegistro.Validar();

            if (erros.Length > 0)
            {
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(erros);
                Console.ResetColor();

                Console.Write("\nDigite ENTER para continuar...");
                Console.ReadLine();
                CadastrarRegistro();
                return;
            }
            repository.CadastrarRegistro(novoRegistro);
            Console.WriteLine($"\n{nomeEntidade} cadastrado com sucesso!");

            Console.ReadLine();
        }

        public abstract bool ListarRegistros();

        public void AtualizarRegistro()
        {
            Console.Clear();
            Console.WriteLine($"Edição de {nomeEntidade}");
            Console.WriteLine("---------------------");

            if (!ListarRegistros())
                return;

            Console.Write($"\nDigite o ID do {nomeEntidade} a editar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            EntidadeBase registroAtualizado = ObterDados();

            repository.EditarRegistro(idSelecionado, registroAtualizado);

            Console.WriteLine($"\n{nomeEntidade} atualizado com sucesso!");

            Console.ReadLine();
        }

        protected abstract EntidadeBase ObterDados();

        public void DeletarRegistro()
        {
            Console.Clear();
            Console.WriteLine($"Exclusão de {nomeEntidade}");
            Console.WriteLine("-----------------------");

            if (!ListarRegistros())

                return;

            Console.Write($"\nDigite o ID do {nomeEntidade} a ser excluído: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            repository.ExcluirRegistro(idSelecionado);

            Console.WriteLine($"\n{nomeEntidade} excluído com sucesso!");

            Console.ReadLine();
        }
    }
}