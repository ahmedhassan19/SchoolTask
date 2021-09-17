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
    public class TotalClassFreeTimeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TotalClassFreeTimeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult GetTotalClassFreeTime()
        {
            string query = @"
                            SELECT  ClassName ,
                            day(SessionStartTime) as day,
                            MONTH(SessionStartTime) as month,
                            Year(SessionStartTime) as Year,
                            6 - sum( cast(DATEDIFF(HOUR,cast(SessionStartTime as time),cast(SessionStartTime as time ))as float))  as ClassFreeTime
                            FROM classsession
                            inner join Classes on ClassSession.ClassID = Classes.ClassID
                            GROUP BY  ClassName,
                            day(SessionStartTime),
                            MONTH(SessionStartTime),
                            Year(SessionStartTime)

                            order by ClassName,
                            day(SessionStartTime),
                            MONTH(SessionStartTime),
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
