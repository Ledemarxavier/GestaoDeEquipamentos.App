using GestaoDeEquipamentos.ConsoleApp.ModuloChamado;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento
{
    public class EquipamentoRepository
    {
        private List<Equipamento> equipamentos = new List<Equipamento>();
        private int nextId = 1;

        public void InserirEquipamento(Equipamento equipamento)
        {
            equipamento.DefinirId(nextId++);
            equipamentos.Add(equipamento);
        }

        public List<Equipamento> Listar()
        {
            return equipamentos;
        }

        public Equipamento ObterPorId(int id)
        {
            foreach (Equipamento equipamento in equipamentos)
            {
                if (equipamento.id == id)
                {
                    return equipamento;
                }
            }
            return null;
        }

        public bool EditarEquipamento(int idSelecionado, Equipamento equipamentoAtualizado)
        {
            Equipamento equipamentoSelecionado = ObterPorId(idSelecionado);

            if (equipamentoSelecionado == null)
                return false;

            equipamentoSelecionado.Atualizar(equipamentoAtualizado);

            return true;
        }

        public bool ExcluirEquipamento(int id)
        {
            var equipamento = ObterPorId(id);
            if (equipamento == null)
                return false;

            return equipamentos.Remove(equipamento);
        }
    }
}