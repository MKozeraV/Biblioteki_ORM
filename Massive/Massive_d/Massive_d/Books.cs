using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Massive;
namespace Massive_d
{
    public class Books : DynamicModel
    {

        public Books()
            : base("Dyplomowa","Books1","id_k")
        {

        }
    }
}
