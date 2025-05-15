using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloChamado
{
    public class ChamadoRepository
    {
        private List<Chamado> chamados = new List<Chamado>();
        private int nextId = 1;

        public void InserirChamado(Chamado chamado)
        {
            chamado.id = nextId++;
            chamados.Add(chamado);
        }

        public List<Chamado> ListarChamados()
        {
            return chamados;
        }

        public Chamado ObterPorId(int id)
        {
            foreach (Chamado chamado in chamados)
            {
                if (chamado.id == id)
                {
                    return chamado;
                }
            }
            return null;
        }

        public bool AtualizarChamado(int idSelecionado, Chamado chamadoAtualizado)
        {
            Chamado chamadoSelecionado = ObterPorId(idSelecionado);
            {
                Chamado chamado = ObterPorId(chamadoAtualizado.id);

                if (chamado != null)
                    return false;
                {
                    chamadoSelecionado.titulo = chamadoAtualizado.titulo;
                    chamadoSelecionado.descricao = chamadoAtualizado.descricao;
                    chamadoSelecionado.equipamento = chamadoAtualizado.equipamento;
                    chamadoSelecionado.dataAbertura = chamadoAtualizado.dataAbertura;
                    return true;
                }
            }
        }

        public bool ExcluirChamado(int id)
        {
            var chamado = ObterPorId(id);
            if (chamado == null)
                return false;

            return chamados.Remove(chamado);
        }
    }
}