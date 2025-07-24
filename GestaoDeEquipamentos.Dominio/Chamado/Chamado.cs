using GestaoDeEquipamentos.Dominio.Compartilhado;
using GestaoDeEquipamentos.Dominio.ModuloEquipamento;

namespace GestaoDeEquipamentos.Dominio.ModuloChamado
{
    public class Chamado : EntidadeBase<Chamado>
    {
        public string titulo { get; set; }

        public string descricao { get; set; }

        public DateTime dataAbertura { get; set; }

        public Equipamento equipamento { get; set; }

        public Chamado()
        { }

        public Chamado(string titulo, string descricao, DateTime dataAbertura, Equipamento equipamento) : this()
        {
            this.titulo = titulo;
            this.descricao = descricao;
            this.dataAbertura = dataAbertura;
            this.equipamento = equipamento;
        }

        public override string Validar()
        {
            string erros = "";

            if (string.IsNullOrWhiteSpace(titulo))
                erros += "O título é obrigatório!\n";
            if (string.IsNullOrWhiteSpace(descricao))
                erros += "A descricao é obrigatória!\n";
            if (dataAbertura == default(DateTime))
                erros += "A data de abertura é obrigatória!\n";
            if (equipamento == null)
                erros += "É necessário selecionar um equipamento válido!\n";

            return erros;
        }

        public override void AtualizarRegistro(Chamado registroAtualizado)
        {
            Chamado equipamentoAtualizado = (Chamado)registroAtualizado;

            this.titulo = equipamentoAtualizado.titulo;
            this.descricao = equipamentoAtualizado.descricao;
            this.dataAbertura = equipamentoAtualizado.dataAbertura;
            this.equipamento = equipamentoAtualizado.equipamento;
        }
    }
}