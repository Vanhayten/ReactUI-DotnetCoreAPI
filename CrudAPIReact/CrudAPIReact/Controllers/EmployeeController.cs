using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using CrudAPIReact.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CrudAPIReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string sql = @"select  EmployeeId,  EmployeeName, Department, convert(varchar(10),  DateOfJoining, 120 ) as DateOfJoining, PhotoFileName from dbo.Employee";
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
        public JsonResult Post(Employee employee)
        {
            string sql = @"insert into dbo.Employee (EmployeeName, Department, DateOfJoining, photoFileName) values(@EmployeeName, @Department, @DateOfJoining, @PhotoFileName) ";
            DataTable Table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader Reader;
            using (SqlConnection myCon = new SqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (SqlCommand cmd = new SqlCommand(sql, myCon))
                {
                    cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@PhotoFileName", employee.PhotoFileName);
                    cmd.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
                    Reader = cmd.ExecuteReader();
                    Table.Load(Reader);
                    Reader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Employee employee)
        {
            string sql = @"update dbo.Employee set EmployeeName = @EmployeeName, Department = @Department, DateOfJoining = @DateOfJoining  where EmployeeId = @EmployeeId";
            DataTable Table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader Reader;
            using (SqlConnection myCon = new SqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (SqlCommand cmd = new SqlCommand(sql, myCon))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                    cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
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
            string sql = @"delete from dbo.Employee where EmployeeId = @EmployeeId";
            DataTable Table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader Reader;
            using (SqlConnection myCon = new SqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (SqlCommand cmd = new SqlCommand(sql, myCon))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", id);
                    Reader = cmd.ExecuteReader();
                    Table.Load(Reader);
                    Reader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postFile = httpRequest.Files[0];
                var fileName = postFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;
                using(var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postFile.CopyTo(stream);
                }

                return new JsonResult(fileName);
            }
            catch (System.Exception)
            {

                return new JsonResult("anonynous.png");
            }
        }
    }
}
