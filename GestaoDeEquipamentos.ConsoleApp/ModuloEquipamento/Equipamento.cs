using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

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

        public void DefinirId(int id)
        {
            this.id = id;
        }
    }
}