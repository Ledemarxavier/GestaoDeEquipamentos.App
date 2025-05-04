namespace GestaoDeEquipamentos.ConsoleApp
{
    public class Chamado
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Equipamento Equipamento { get; set; }
        public DateTime DataAbertura { get; set; }
    }
}