using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace WebAPIDemo.Controllers
{
    public class EmployeesController : ApiController
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        [HttpGet]
        public List<Employee> GetData()
        {
            Employee objemp = new Employee();
            List<Employee> EmpList = new List<Employee>();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SpSelEmployeeData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
           
            while (dr.Read())
            {

                objemp.ID = Convert.ToInt32(dr["Id"]);
                objemp.Name = dr["Name"].ToString();
                objemp.City = dr["City"].ToString();
                objemp.IsActive = Convert.ToBoolean(dr["IsActive"]);
                EmpList.Add(objemp);

            }
            con.Close();
            return EmpList;


        }

        [HttpGet]
        public Employee GetData(int id)
        {
            Employee objemp = new Employee();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SpSelEmployeeDataByID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                objemp.ID = Convert.ToInt32(dr["Id"]);
                objemp.Name = dr["Name"].ToString();
                objemp.City = dr["City"].ToString();
                objemp.IsActive = Convert.ToBoolean(dr["IsActive"]);
            }
            con.Close();
            return objemp;


        }
        [HttpPost]
        public string post(Employee emp)
        {
            string message = "";
            int result = 0;
            Employee objemp = new Employee();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SpInsEmpoyeeData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", emp.ID);
            cmd.Parameters.AddWithValue("@Name",emp.Name);
            cmd.Parameters.AddWithValue("@City",emp.City);
            cmd.Parameters.AddWithValue("@IsActive",emp.IsActive);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();

            if (result==1)
            {

                message = "Record Added Successfuly";
            }
            else
            {
                message = "Data not saved";
            }
            return message;
           
        }

        [HttpPut]
        public string put(Employee emp)
        {
            int result;
            string message;
            Employee objemp = new Employee();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SpUpdateEmpoyeeData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", emp.ID);
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@City", emp.City);
            cmd.Parameters.AddWithValue("@IsActive", emp.IsActive);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();

            if (result==1)
            {

                message = "Record Updated Successfuly";
            }
            else
            {
                message = "Data not saved";
            }
            return message;
            
        }
        //
        [HttpDelete]
        public string delete(Employee emp)
        {
            int result;
            string message;
            
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SpDeleteEmployeeData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", emp.ID);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();

            if (result==1)
            {

                message = "Record deleted Successfuly";
            }
            else
            {
                message = "Data not saved";
            }
            return message;

        }

    }
}
