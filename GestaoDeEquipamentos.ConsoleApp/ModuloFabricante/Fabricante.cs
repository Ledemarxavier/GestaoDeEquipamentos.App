using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeEquipamentos.ConsoleApp.ModuloFabricante
{
    public class Fabricante
    {
        public int id { get; set; }
        public string nome { get; set; }

        public string email { get; set; }

        public string telefone { get; set; }

        public Fabricante(string nome, string email, string telefone)
        {
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
        }

        public void DefinirId(int id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return nome;
        }
    }
}