using GestaoDeEquipamentos.Dominio.ModuloFabricante;
using GestaoDeEquipamentos.Dominio.Compartilhado;

namespace GestaoDeEquipamentos.Dominio.ModuloEquipamento
{
    public class Equipamento : EntidadeBase<Equipamento>
    {
        public string nome { get; set; }
        public decimal preco { get; set; }
        public string numeroSerie { get; set; }
        public Fabricante fabricante { get; set; }
        public DateTime dataFabricacao { get; set; }

        public Equipamento()
        { }

        public Equipamento(
            string nome,
            decimal preco,
            DateTime dataFabricacao,
            Fabricante fabricante
        ) : this()
        {
            this.nome = nome;
            this.preco = preco;
            this.fabricante = fabricante;
            this.dataFabricacao = dataFabricacao;
        }

        public Equipamento(
            string nome,
            decimal preco,
            string numeroSerie,
            Fabricante fabricante,
            DateTime dataFabricacao
        ) : this()
        {
            this.nome = nome;
            this.preco = preco;
            this.numeroSerie = numeroSerie;
            this.fabricante = fabricante;
            this.dataFabricacao = dataFabricacao;
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

        public override void AtualizarRegistro(Equipamento registroAtualizado)
        {
            this.nome = registroAtualizado.nome;
            this.preco = registroAtualizado.preco;
            this.numeroSerie = registroAtualizado.numeroSerie;
            this.fabricante = registroAtualizado.fabricante;
            this.dataFabricacao = registroAtualizado.dataFabricacao;
        }
    }
}