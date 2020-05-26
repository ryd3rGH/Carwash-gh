using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CWLib
{
    public class MoneyType
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public static List<MoneyType> GetMoneyTypes(string connStr)
        {
            List<MoneyType> types = new List<MoneyType>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand findMoneyTypes = new SqlCommand($"select ID, NAME from [CARWASH].[dbo].[PAYMENT_CATEGORIES] " +
                                                                    $"where VISIBILITY = 'true'", conn))
                {
                    using (SqlDataReader dr = findMoneyTypes.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            types.Add(new MoneyType() { Id = dr.GetInt32(0), Name = dr.GetString(1) });
                        }
                    }
                }

                return types;
            }
        }
    }
}
