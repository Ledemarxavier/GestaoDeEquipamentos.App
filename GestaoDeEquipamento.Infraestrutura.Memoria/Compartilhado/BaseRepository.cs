using GestaoDeEquipamentos.Dominio.Compartilhado;

namespace GestaoDeEquipamentos.Infraestrutura.Compartilhado
{
    public class BaseRepository<Tipo> where Tipo : EntidadeBase<Tipo>
    {
        private List<Tipo> registros = new List<Tipo>();
        private int contadorIds = 0;

        public void CadastrarRegistro(Tipo novoRegistro)
        {
            novoRegistro.id = ++contadorIds;

            registros.Add(novoRegistro);
        }

        public bool EditarRegistro(int idSelecionado, Tipo registroAtualizado)
        {
            Tipo registroSelecionado = SelecionarRegistroPorId(idSelecionado);

            if (registroSelecionado == null)
                return false;

            registroSelecionado.AtualizarRegistro(registroAtualizado);

            return true;
        }

        public bool ExcluirRegistro(int id)
        {
            var registro = SelecionarRegistroPorId(id);
            if (registro == null)
                return false;

            return registros.Remove(registro);
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
    }
}