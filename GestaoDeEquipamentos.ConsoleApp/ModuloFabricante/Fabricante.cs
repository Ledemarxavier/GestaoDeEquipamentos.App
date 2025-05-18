using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

        public string Validar()
        {
            string erros = "";

            if (string.IsNullOrWhiteSpace(nome))
                erros += "O nome é obrigatório!\n";
            else if (nome.Length < 2)
                erros += "O nome deve conter mais que 1 caractere!\n";

            if (!MailAddress.TryCreate(email, out _))
                erros += "O email deve conter um formato válido \"nome@provedor.com\"!\n";

            if (string.IsNullOrWhiteSpace(telefone))
                erros += "O telefone é obrigatório!\n";
            else if (telefone.Length < 9)
                erros += "O telefone deve conter no mínimo 9 caracteres!\n";

            return erros;
        }

        public void Atualizar(Fabricante fabricanteAtualizado)
        {
            this.nome = fabricanteAtualizado.nome;
            this.email = fabricanteAtualizado.email;
            this.telefone = fabricanteAtualizado.telefone;
        }

        public override string ToString()
        {
            return nome;
        }
    }
}