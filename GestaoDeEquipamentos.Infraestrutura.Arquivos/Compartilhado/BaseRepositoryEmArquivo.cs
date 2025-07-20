using GestaoDeEquipamentos.Dominio.Compartilhado;
using GestaoDeEquipamentos.Infraestrutura.Arquivos.Compartilhado;

namespace GestaoDeEquipamentos.Infraestrutura.Compartilhado
{
    public abstract class BaseRepositoryEmArquivo<Tipo> where Tipo : EntidadeBase<Tipo>
    {
        private List<Tipo> registros = new List<Tipo>();
        private int contadorIds = 0;

        protected ContextoDados contexto;

        protected BaseRepositoryEmArquivo(ContextoDados contexto)
        {
            this.contexto = contexto;

            this.registros = ObterRegistros();

            int maiorId = 0;

            foreach (Tipo registro in registros)
            {
                if (registro.id > maiorId)
                    maiorId = registro.id;
            }

            contadorIds = ++maiorId;
        }

        public void CadastrarRegistro(Tipo novoRegistro)
        {
            novoRegistro.id = ++contadorIds;

            registros.Add(novoRegistro);
            contexto.Salvar();
        }

        public bool EditarRegistro(int idSelecionado, Tipo registroAtualizado)
        {
            Tipo registroSelecionado = SelecionarRegistroPorId(idSelecionado);

            if (registroSelecionado == null)
                return false;

            registroSelecionado.AtualizarRegistro(registroAtualizado);

            contexto.Salvar();

            return true;
        }

        public bool ExcluirRegistro(int id)
        {
            var registro = SelecionarRegistroPorId(id);
            if (registro == null)
                return false;

             registros.Remove(registro);

            contexto.Salvar();
            return true;
        }

        public List<Tipo> SelecionarRegistros()
        {
            return registros;
        }

        public Tipo SelecionarRegistroPorId(int idSelecionado)
        {
            foreach (var r in registros)
            {
                if (r.id == idSelecionado)
                    return r;
            }
            return null;
        }

        protected abstract List<Tipo> ObterRegistros();
    }
}