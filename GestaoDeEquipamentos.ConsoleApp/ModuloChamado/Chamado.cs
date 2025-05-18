using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using System.Net.Mail;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloChamado
{
    public class Chamado
    {
        public int id { get; private set; }
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

        public string Validar()
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

        public void Atualizar(Chamado chamadoAtualizado)
        {
            this.titulo = chamadoAtualizado.titulo;
            this.descricao = chamadoAtualizado.descricao;
            this.equipamento = chamadoAtualizado.equipamento;
            this.dataAbertura = chamadoAtualizado.dataAbertura;
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