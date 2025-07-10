namespace GestaoDeEquipamentos.ConsoleApp.Compartilhado
{
    public abstract class EntidadeBase<Tipo>
    {
        public int id;

        public abstract void AtualizarRegistro(Tipo registroAtualizado);

        public abstract string Validar();
    }
}