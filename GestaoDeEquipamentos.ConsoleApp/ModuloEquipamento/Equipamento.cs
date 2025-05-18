using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using System.Net.Mail;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento
{
    public class Equipamento
    {
        public int id { get; private set; }
        public string nome { get; set; }
        public decimal preco { get; set; }
        public string numeroSerie { get; set; }
        public DateTime dataFabricacao { get; set; }
        public Fabricante fabricante { get; set; }

        public Equipamento(string nome, decimal preco, string numeroSerie, DateTime dataFabricacao, Fabricante fabricante)
        {
            this.nome = nome;
            this.preco = preco;
            this.numeroSerie = numeroSerie;
            this.dataFabricacao = dataFabricacao;
            this.fabricante = fabricante;
        }

        public string Validar()
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

        public void Atualizar(Equipamento equipamentoAtualizado)
        {
            this.nome = equipamentoAtualizado.nome;
            this.preco = equipamentoAtualizado.preco;
            this.numeroSerie = equipamentoAtualizado.numeroSerie;
            this.dataFabricacao = equipamentoAtualizado.dataFabricacao;
            this.fabricante = equipamentoAtualizado.fabricante;
        }

        public void DefinirId(int id)
        {
            this.id = id;
        }
    }
}