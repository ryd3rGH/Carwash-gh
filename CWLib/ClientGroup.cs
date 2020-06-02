using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CWLib
{
    public class ClientGroup
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Discount { get; set; }
        public bool? Visibility { get; set; }

        public static ClientGroup GetGroupByPersonId(string connStr, int personId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                ClientGroup group = new ClientGroup();

                using (SqlCommand findGroup = new SqlCommand($"select CLIENT_GROUPS.ID, CLIENT_GROUPS.GROUP_NAME, CLIENT_GROUPS.discount, CLIENT_GROUPS.VIS " +
                                                            $"from [CARWASH].[dbo].[PERSONS], [CARWASH].[dbo].[INDIVIDUAL_CLIENTS], [CARWASH].[dbo].[CLIENT_GROUPS] " +
                                                            $"where PERSONS.ID = INDIVIDUAL_CLIENTS.ID_PERSON "+
                                                            $"and INDIVIDUAL_CLIENTS.ID_GROUP = CLIENT_GROUPS.ID "+
                                                            $"and PERSONS.ID = {personId}", conn))
                {
                    using (SqlDataReader dr = findGroup.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            group.Id = dr.GetInt32(0);
                            group.Name = dr.GetString(1);
                            group.Discount = dr.GetInt32(2);
                            group.Visibility = dr.GetBoolean(3);
                        }
                    }
                }

                return group;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
