namespace GestaoDeEquipamentos.ConsoleApp.Compartilhado
{
    public abstract class TelaBase<T> where T : EntidadeBase<T>
    {
        private string nomeEntidade;
        private BaseRepository<T> repository;

        protected TelaBase(string nomeEntidade, BaseRepository<T> repository)
        {
            this.nomeEntidade = nomeEntidade;
            this.repository = repository;
        }

        public virtual char Menu()
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

                char operacaoEscolhida = Convert.ToChar(Console.ReadLine()!);

                return operacaoEscolhida;
            }
        }

        public void CadastrarRegistro()
        {
            Console.Clear();
            Console.WriteLine($"\nCadastro de {nomeEntidade}");
            Console.WriteLine("----------------------");

            T novoRegistro = ObterDados();

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

            T registroAtualizado = ObterDados();

            repository.EditarRegistro(idSelecionado, registroAtualizado);

            Console.WriteLine($"\n{nomeEntidade} atualizado com sucesso!");

            Console.ReadLine();
        }

        protected abstract T ObterDados();

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