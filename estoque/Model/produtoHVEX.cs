using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class produtoHVEX
    {
        public ObjectId _id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string setor { get; set; }
        public string codHVEX { get; set; }
        public int quantidade { get; set; }
        public string preco { get; set; }
        public List<Estoque> ProductCollection { get; set; }
    }
}
