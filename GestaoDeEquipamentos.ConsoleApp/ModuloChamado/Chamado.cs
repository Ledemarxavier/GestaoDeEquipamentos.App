using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloChamado
{
    public class Chamado
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public Equipamento equipamento { get; set; }
        public DateTime dataAbertura { get; set; }

        public Chamado(string titulo, string decricao, Equipamento equipamento, DateTime dataAbertura)
        {
            this.titulo = titulo;
            this.descricao = descricao;
            this.equipamento = equipamento;
            this.dataAbertura = dataAbertura;
        }

        public void DefinirId(int id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return titulo;
        }
    }
}