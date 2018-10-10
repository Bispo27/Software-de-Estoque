using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RegistroEntrada
    {
        public ObjectId _id { get; set; }
        public DateTime data { get; set; }
        public string codigo { get; set; }
        public string NF { get; set; }
        public int quantidade { get; set; }
        public double preco { get; set; }
        public string responsavel { get; set; }
    }
}
