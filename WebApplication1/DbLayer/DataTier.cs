using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.DbLayer
{
    public class DataTier
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoDB"].ConnectionString);

        //DataTier DtObj = new DataTier();
        string qry;

        public void Add_details(Records rd)
        {
            try
            {
                // qry = "insert into Sp_register(Name,Email,Experience) values(@name,@email,@exp)";
                SqlCommand cmd = new SqlCommand("Sp_register", con);

                cmd.CommandType = CommandType.StoredProcedure;

               // SqlCommand cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@name", rd.Name);
                cmd.Parameters.AddWithValue("@email", rd.Email);
                cmd.Parameters.AddWithValue("@exp", rd.Experience);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public DataSet get_record()

        {

            SqlCommand com = new SqlCommand("Sp_register_get", con);

            com.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;

        }

        public void update_record(Records rs)

        {

            SqlCommand com = new SqlCommand("Sp_register_Update", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", rs.Id);

            com.Parameters.AddWithValue("@Name", rs.Name);

            com.Parameters.AddWithValue("@Email", rs.Email);

            com.Parameters.AddWithValue("@exp", rs.Experience);

            con.Open();

            com.ExecuteNonQuery();

            con.Close();

        }

        // Get Record by id

        public DataSet get_recordbyid(int id)

        {

            SqlCommand com = new SqlCommand("Sp_register_byid", con);

            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Id", id);

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;

        }

        // Delete record

        public void deletedata(int id)

        {

            SqlCommand com = new SqlCommand("Sp_register_delete", con);

            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Id", id);

            con.Open();

            com.ExecuteNonQuery();

            con.Close();

        }



    }
}