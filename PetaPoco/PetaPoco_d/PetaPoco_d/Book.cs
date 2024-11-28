using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetaPoco_d
{
    [TableName("Books1")]
    [PrimaryKey("id_k", AutoIncrement = true)]
    public class Book
    {
        public int id_k { get; set; }
        public string nazwa { get; set; } = null;
        public string autor { get; set; } = null;
        public string gatunek { get; set; }
    }
}
