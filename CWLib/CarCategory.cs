using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;

namespace CWLib
{
    /* Категории авто */
    public class CarCategory : ICloneable
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public List<int> Classes { get; set; }
        public decimal? Price { get; set; }

        public bool AddCategory(string connStr)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand addCat = new SqlCommand($"insert into [CARWASH].[dbo].[CAR_CATEGORIES] (NAME, VIS) " +
                                                        $"output inserted.ID " +
                                                        $"values ('{Name}', 'true')", conn);

                    Id = Convert.ToInt32(addCat.ExecuteScalar());
                    if (Id != null)
                    {
                        for (int i = 0; i < Classes.Count; i++)
                        {
                            SqlCommand addCatClass = new SqlCommand($"insert into [CARWASH].[dbo].[CAR_CLASS_CATEGORY] (CATEGORY_ID, CLASS_ID) " +
                                                                    $"values ({Id}, {Classes[i]})", conn);
                            addCatClass.ExecuteNonQuery();
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public object Clone()
        {
            return new CarCategory() { Id = this.Id, Name = this.Name, Price = this.Price, Classes = this.Classes };
        }

        public bool DeleteCategory(string connStr)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand delCat = new SqlCommand($"update [CARWASH].[dbo].[CAR_CATEGORIES] " +
                                                        $"set VIS = 'false' " +
                                                        $"where ID={Id}", conn);
                    delCat.ExecuteNonQuery();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCategory(string connStr, string newName, List<int> newClasses)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                try
                {
                    if (!Name.Equals(newName)) 
                    {
                        using (SqlCommand updateName = new SqlCommand($"update [CARWASH].[dbo].[CAR_CATEGORIES] " +
                                                                        $"set NAME = '{newName}' " +
                                                                        $"where ID = {Id}", conn))
                            updateName.ExecuteNonQuery();
                    }

                    if (!Classes.SequenceEqual(newClasses))
                    {
                        using (SqlCommand deleteOldClasses = new SqlCommand($"delete from [CARWASH].[dbo].[CAR_CLASS_CATEGORY] " +
                                                                            $"where CATEGORY_ID = {Id}", conn))
                            deleteOldClasses.ExecuteNonQuery();

                        for (int i=0; i<newClasses.Count; i++)
                        {
                            using (SqlCommand addNewClasses = new SqlCommand($"insert into [CARWASH].[dbo].[CAR_CLASS_CATEGORY] (CATEGORY_ID, CLASS_ID) " +
                                                                             $"values ({Id}, {newClasses[i]})", conn))
                                addNewClasses.ExecuteNonQuery();
                        }
                    }
                        

                    return true;
                }

                catch
                {
                    return false;
                }
            }
        }
    }
}
