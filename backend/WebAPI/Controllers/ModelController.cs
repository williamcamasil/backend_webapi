using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ModelController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {

            string query = @"
                    select spId, spNameModel, spNameBland, spPrice from model";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ConexionBD");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        //GET values from only one item in the table
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {

            string query = @"
                    select spId, spNameModel, spNameBland, spPrice from model where spId = " + id;
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ConexionBD");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        //sp = SmartPhone
        [HttpPost]
        public JsonResult Post(Model sp)
        {
            string query = @"
                    insert into model 
                    (spNameBland,spNameModel,spPrice)
                    values 
                    (
                        '" + sp.spNameBland + @"'
                        ,'" + sp.spNameModel + @"'
                        ,'" + sp.spPrice + @"'

                    )";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ConexionBD");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }



    }
}
