using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app23Receiver.Models
{
    public class UserClass
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }    
        public string Surname { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
    }
}
