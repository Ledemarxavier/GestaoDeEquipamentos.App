namespace GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento
{
    public class EquipamentoRepository()
    {
        public List<Equipamento> equipamentos = new List<Equipamento>();
        public int nextId = 1;

        public void InserirEquipamento(Equipamento equipamento)
        {
            equipamento.id = nextId++;
            equipamentos.Add(equipamento);
        }

        public List<Equipamento> Listar()
        {
            if (equipamentos != null)
            {
                return equipamentos;
            }
            return null;
        }

        public Equipamento ObterPorId(int id)
        {
            foreach (var equipamento in equipamentos)
            {
                if (equipamento.id == id)
                    return equipamento;
            }
            return null;
        }

        public bool EditarEquipamento(int idSelecionado, Equipamento equipamentoAtualizado)
        {
            Equipamento equipamentoSelecionado = ObterPorId(idSelecionado);

            if (equipamentoSelecionado == null)
                return false;

            equipamentoSelecionado.nome = equipamentoAtualizado.nome;
            equipamentoSelecionado.preco = equipamentoAtualizado.preco;
            equipamentoSelecionado.numeroSerie = equipamentoAtualizado.numeroSerie;
            equipamentoSelecionado.dataFabricacao = equipamentoAtualizado.dataFabricacao;
            equipamentoSelecionado.fabricante = equipamentoAtualizado.fabricante;

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