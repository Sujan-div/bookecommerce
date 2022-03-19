using Dapper;
using bookecommercewebsite.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace bookecommercewebsite
{
    public static class Class1
    {
        public static SqlConnection MyMethod()
        {
            var connection = new SqlConnection(Dapper.Connection);
            connection.Open();
            return connection;
            
        }
        public static IEnumerable<T> RunQuery<T> (string sql,object parameter= null)
        {
            using (var con = MyMethod())
            {
                return con.Query<T>(sql, parameter);
            }

        }


    }
}
