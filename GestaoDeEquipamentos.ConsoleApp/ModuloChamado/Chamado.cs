using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloChamado
{
    public class Chamado : EntidadeBase
    {
        public string titulo { get; set; }

        public string descricao { get; set; }
        public Equipamento equipamento { get; set; }
        public DateTime dataAbertura { get; set; }

        public Chamado(string titulo, string descricao, Equipamento equipamento, DateTime dataAbertura)
        {
            this.titulo = titulo;
            this.descricao = descricao;
            this.equipamento = equipamento;
            this.dataAbertura = dataAbertura;
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

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Chamado equipamentoAtualizado = (Chamado)registroAtualizado;

            this.titulo = equipamentoAtualizado.titulo;
            this.descricao = equipamentoAtualizado.descricao;
            this.equipamento = equipamentoAtualizado.equipamento;

            this.dataAbertura = equipamentoAtualizado.dataAbertura;
        }
    }
}