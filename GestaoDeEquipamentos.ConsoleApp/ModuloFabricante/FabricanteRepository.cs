using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloFabricante
{
    public class FabricanteRepository
    {
        private List<Fabricante> fabricantes = new List<Fabricante>();
        private int nextId = 1;

        public void InserirFabricante(Fabricante fabricante)
        {
            fabricante.DefinirId(nextId++);
            fabricantes.Add(fabricante);
        }

        public List<Fabricante> Listar()
        {
            if (fabricantes == null)
                return new List<Fabricante>();

            return fabricantes;
        }

        public Fabricante ObterPorId(int id)
        {
            foreach (var f in fabricantes)
            {
                if (f.id == id)
                    return f;
            }
            return null;
        }

        public bool EditarFabricante(int idSelecionado, Fabricante fabricanteAtualizado)
        {
            Fabricante fabricanteSelecionado = ObterPorId(idSelecionado);

            if (fabricanteSelecionado == null)
                return false;

            fabricanteSelecionado.Atualizar(fabricanteAtualizado);

            return true;
        }

        public bool ExcluirFabricante(int id)
        {
            var fabricante = ObterPorId(id);
            if (fabricante == null)
                return false;

            return fabricantes.Remove(fabricante);
        }
    }
}