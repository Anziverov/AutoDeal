using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoDeal.Models
{
    public sealed class User
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}
