using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernate_d
{
    class Book
    {
        public virtual int id_k { get; set; }
        public virtual string nazwa { get; set; }
        public virtual string autor {  get; set; }
        public virtual string gatunek { get; set; }
    }
}
