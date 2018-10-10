using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Estoque
    {
        public ObjectId _id { get; set; }
        public string tipo { get; set; }
     //   public string codigo { get; set; }
        public string setor { get; set; }   
        public string partNumb { get; set; }
        public string produto { get; set; }
        public string descricao { get; set; }
        public string fabricante { get; set; }
        public string NF1 { get; set; }
        public string fornecedor { get; set; }
        public DateTime dataCompra { get; set; }
        public double valorNF { get; set; }
        public string responsavel { get; set; }
        public double Preco { get; set; }
        public string codHVEX { get; set; }
        public int quantidade { get; set; }
    }
}
