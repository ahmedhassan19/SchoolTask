using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolTask.Models
{
    public class ClassSession
    {
        [Key]
        public Guid  SessionID { get; set; }
        public string SessionName { get; set; }
        public DateTime SessionStartTime { get; set; }
        public DateTime SessionEndTime { get; set; }

        [ForeignKey("Classes")]
        public Guid classID { get; set; }
        public Classes classes { get; set; }
       
        [ForeignKey("Teachers")]
        public Guid teacherID { get; set; }
        public Teachers teacher { get; set; }

    }


}
