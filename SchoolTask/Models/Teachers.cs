using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolTask.Models
{
    public class Teachers
    {
        [Key]
        public Guid TeacherID { get; set; }
        public string TeacherName { get; set; }
    }
}
