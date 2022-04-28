using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using CrudAPIReact.Models;


namespace CrudAPIReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string sql = @"select DepartmentId, DepartmentName from dbo.Department";
            DataTable Table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader Reader;
            using (SqlConnection myCon = new SqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (SqlCommand cmd = new SqlCommand(sql, myCon))
                {
                    Reader = cmd.ExecuteReader();
                    Table.Load(Reader);
                    Reader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(Table);
        }

        [HttpPost]
        public JsonResult Post(Department department)
        {
            string sql = @"insert into dbo.Department values(@DepartmentName)";
            DataTable Table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader Reader;
            using (SqlConnection myCon = new SqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (SqlCommand cmd = new SqlCommand(sql, myCon))
                {
                    cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                    Reader = cmd.ExecuteReader();
                    Table.Load(Reader);
                    Reader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Department department)
        {
            string sql = @"update dbo.Department set DepartmentName = @DepartmentName where DepartmentId = @DepartmentId";
            DataTable Table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader Reader;
            using (SqlConnection myCon = new SqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (SqlCommand cmd = new SqlCommand(sql, myCon))
                {
                    cmd.Parameters.AddWithValue("@DepartmentId", department.DepartmentId);
                    cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                    Reader = cmd.ExecuteReader();
                    Table.Load(Reader);
                    Reader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string sql = @"delete from dbo.Department where DepartmentId = @DepartmentId";
            DataTable Table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader Reader;
            using (SqlConnection myCon = new SqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (SqlCommand cmd = new SqlCommand(sql, myCon))
                {
                    cmd.Parameters.AddWithValue("@DepartmentId", id);
                    Reader = cmd.ExecuteReader();
                    Table.Load(Reader);
                    Reader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted successfully");
        }
    }
}
