using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{

    //This is an AD User
    public class User
    {
        public string Handle { get; set; }
        public string  Token { get; set; }

        public bool IsAuthenticated { get; set; }
    }
}
