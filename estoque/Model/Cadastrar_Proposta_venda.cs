using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Cadastrar_Proposta_venda
    {
        public ObjectId _id { get; set; }
        public string Numero_proposta { get; set; }
        public DateTime Data_envio { get; set; }
        public string Cliente { get; set; }
        public string Nome_contato { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set;}
        public string Descrição { get; set; }
        public string Observações { get; set; }
        public double Valor { get; set; }
    }
}
