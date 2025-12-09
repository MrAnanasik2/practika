using Mysqlx.Prepare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRAKTIKA1.Models
{
    public class Money
    {
        public int Mony_id { get; set; }
        public string Mony_type { get; set; }

        public override string ToString()
        {
            return Mony_type; // чтобы в ComboBox отображался текст
        }

    }
}
