namespace GestaoDeEquipamentos.ConsoleApp
{
    public class Chamado
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public Equipamento equipamento { get; set; }
        public DateTime dataAbertura { get; set; }
    }
}