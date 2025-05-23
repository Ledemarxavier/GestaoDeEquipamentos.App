using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeEquipamentos.ConsoleApp.Compartilhado
{
    public class BaseRepository
    {
        private List<EntidadeBase> registros = new List<EntidadeBase>();
        private int contadorRegistros = 0;

        public void CadastrarRegistro(EntidadeBase novoRegistro)
        {
            contadorRegistros++;
            novoRegistro.id = contadorRegistros;

            registros.Add(novoRegistro);
        }

        public bool EditarRegistro(int idSelecionado, EntidadeBase registroAtualizado)
        {
            EntidadeBase registroSelecionado = SelecionarRegistroPorId(idSelecionado);

            if (registroSelecionado == null)
                return false;

            registroSelecionado.AtualizarRegistro(registroAtualizado);

            return true;
        }

        public bool ExcluirRegistro(int id)
        {
            var registro = SelecionarRegistroPorId(id);
            if (registro == null)
                return false;

            return registros.Remove(registro);
        }

        public List<EntidadeBase> SelecionarRegistros()
        {
            return registros;
        }

        public EntidadeBase SelecionarRegistroPorId(int idSelecionado)
        {
            foreach (var r in registros)
            {
                if (r.id == idSelecionado)
                    return r;
            }
            return null;
        }
    }
}