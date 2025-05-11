using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloFabricante
{
    public class FabricanteRepository()
    {
        public List<Fabricante> fabricantes = new List<Fabricante>();
        public int nextId = 1;

        public void InserirFabricante(Fabricante fabricante)
        {
            fabricante.id = nextId++;
            fabricantes.Add(fabricante);
        }

        public List<Fabricante> Listar()
        {
            if (fabricantes != null)
            {
                return fabricantes;
            }
            return null;
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

            fabricanteSelecionado.nome = fabricanteAtualizado.nome;
            fabricanteSelecionado.email = fabricanteAtualizado.email;
            fabricanteSelecionado.telefone = fabricanteAtualizado.telefone;

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