using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolTask.Models
{
    public class Classes
    {
        [Key]
        public Guid ClassID { get; set; }
        public string ClassName { get; set; }
    }
}
