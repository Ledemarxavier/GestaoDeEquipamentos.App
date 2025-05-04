namespace GestaoDeEquipamentos.ConsoleApp
{
    public class Equipamento()
    {
        public int id { get; set; }
        public string nome { get; set; }
        public decimal preco { get; set; }
        public string numeroSerie { get; set; }
        public DateTime dataFabricacao { get; set; }
        public string fabricante { get; set; }
    }
}