using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CWLib
{
    /* Пользователи */
    public class User
    {
        public int? Id { get; set; }
        public string LoginName { get; set; }
        public string Pass { get; set; }

        public override string ToString()
        {
            return LoginName;
        }

        public static string FindNameByUserName(string connStr, int personId)
        {
            string name = string.Empty;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand findName = new SqlCommand($"select NAME from [CARWASH].[dbo].[PERSONS] where ID = {personId}", conn))
                {
                    name = (string)findName.ExecuteScalar();
                }
            }

            return name;
        }
    }    
}
