using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotalWorkingHoursController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TotalWorkingHoursController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult GetTotalWorkingHours()
        {
            string query = @"
                            SELECT  TeacherName,ClassName ,
                            MONTH(SessionStartTime) as month,
                            Year(SessionStartTime) as Year,
                            sum( cast(DATEDIFF(HOUR,cast(SessionStartTime as time),cast(SessionEndTime as time ))as float))  as WorkingHour
                            FROM classsession
                            inner join Teachers on ClassSession.TeacherID = Teachers.TeacherID
                            inner join Classes on ClassSession.ClassID = Classes.ClassID
                            GROUP BY  TeacherName,ClassName,
                            MONTH(SessionStartTime),
                            Year(SessionStartTime)

                            order by TeacherName,
                            Year(SessionStartTime)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SchoolAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
     

    }
}
