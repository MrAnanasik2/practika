using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRAKTIKA1.Models
{
    public class autorize
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Money_id { get; set; }
        public int Country_id { get; set; }
        public string Role { get; set; }
    }
}
