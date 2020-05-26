using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CWLib
{
    public class ServiceType
    {
        public bool IsCarwashService { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public bool AddCategory(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    SqlCommand newServiceCategory = new SqlCommand($"insert into [CARWASH].[dbo].[SERVICE_CATEGORIES] " +
                                                                    $"values ('{Name}', 'true', 'true')", conn);
                    newServiceCategory.ExecuteNonQuery();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static List<ServiceType> GetCWTypes(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                List<ServiceType> types = new List<ServiceType>();
                SqlCommand findTypes = new SqlCommand("select * from [CARWASH].[dbo].[SERVICE_CATEGORIES] " +
                                                        "where IS_CARWASH = 'true' and VISIBILITY = 'true'", conn);

                SqlDataReader dr = findTypes.ExecuteReader();
                while (dr.Read())
                {
                    types.Add(new ServiceType() { Id = dr.GetInt32(0), Name = dr.GetString(1), IsCarwashService = true });
                }
                dr.Close();

                return types;
            }
        }
    }
}
