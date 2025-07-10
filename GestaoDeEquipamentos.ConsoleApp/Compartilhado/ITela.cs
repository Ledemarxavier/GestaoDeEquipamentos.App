namespace GestaoDeEquipamentos.ConsoleApp.Compartilhado;

public interface ITela
{
    char Menu();

    void CadastrarRegistro();

    void AtualizarRegistro();

    void DeletarRegistro();

    bool ListarRegistros();
}