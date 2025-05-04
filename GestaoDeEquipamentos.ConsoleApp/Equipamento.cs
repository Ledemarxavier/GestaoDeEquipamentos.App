namespace GestaoDeEquipamentos.ConsoleApp
{
    public class Equipamento()
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string NumeroSerie { get; set; }
        public DateTime DataFabricacao { get; set; }
        public string Fabricante { get; set; }
    }
}