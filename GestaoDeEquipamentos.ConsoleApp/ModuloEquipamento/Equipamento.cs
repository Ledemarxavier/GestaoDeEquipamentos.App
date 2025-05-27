using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento
{
    public class Equipamento : EntidadeBase
    {
        public string nome;
        public decimal preco;
        public string numeroSerie;
        public DateTime dataFabricacao;
        public Fabricante fabricante;

        public Equipamento(string nome, decimal preco, string numeroSerie, DateTime dataFabricacao, Fabricante fabricante)
        {
            this.nome = nome;
            this.preco = preco;
            this.numeroSerie = numeroSerie;
            this.dataFabricacao = dataFabricacao;
            this.fabricante = fabricante;
        }

        public override string Validar()
        {
            string erros = "";

            if (string.IsNullOrWhiteSpace(nome))
                erros += "O nome é obrigatório!\n";
            else if (nome.Length < 2)
                erros += "O nome deve conter mais que 1 caractere!\n";
            if (preco <= 0)
                erros += "O preço deve ser um valor numérico maior que zero!\n";
            if (string.IsNullOrWhiteSpace(numeroSerie))
                erros += "O número de série é obrigatório!\n";
            if (dataFabricacao == default(DateTime))
                erros += "A data de fabricação é obrigatória!\n";

            if (fabricante == null)
                erros += "Fabricante não encontrado ou inválido!\n";

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Equipamento equipamentoAtualizado = (Equipamento)registroAtualizado;

            this.nome = equipamentoAtualizado.nome;
            this.preco = equipamentoAtualizado.preco;
            this.numeroSerie = equipamentoAtualizado.numeroSerie;
            this.fabricante = equipamentoAtualizado.fabricante;
            this.dataFabricacao = equipamentoAtualizado.dataFabricacao;
        }
    }
}