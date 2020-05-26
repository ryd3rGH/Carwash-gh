using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CWLib
{
    public class CarwashService : ServiceBase
    {
        public int? Id { get; set; }
        public ServiceType Type { get; set; }
        public List<CarCategory> CategoryPrices { get; set; }
        public List<CarClass> ClassPrices { get; set; }
        public int UsedCarClass { get; set; }
        public int UsedCarCategory { get; set; }
        public int? OrderServiceCount { get; set; }
        public decimal? UsedPrice { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public CarwashService(int? id, string name, string descr, TimeSpan duration, bool vis)
                             : base(name, descr, duration, vis)
        {
            Id = id;
        }

        public static List<CarwashService> GetServicesByOrderId(string connStr, int orderId)
        {
            List<CarwashService> services = new List<CarwashService>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand findServices = new SqlCommand($"select c.ID, c.NAME, c.DURATION, s.ID_CLASS, s.ID_CATEGORY, s.PRICE, s.COUNT " +
                                                                $"from[CARWASH].[dbo].[CARWASH_SERVICES] c join[CARWASH].[dbo].[CARWASH_ORDER_SERVICES] s " +
                                                                $"on c.ID = s.ID_SERVICE " +
                                                                $"where s.ID_ORDER = {orderId}", conn))
                {
                    using (SqlDataReader dr = findServices.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            services.Add(new CarwashService(dr.GetInt32(0), dr.GetString(1), null, new TimeSpan(0, dr.GetInt32(2), 0), true)
                            {
                                UsedCarClass = dr.GetInt32(3),
                                UsedCarCategory = dr.GetInt32(4),
                                UsedPrice = dr.GetDecimal(5),
                                OrderServiceCount = dr.GetInt32(6)
                            });
                        }
                    }
                }
            }

            return services;
        }

        public static List<CarwashService> GetServices(string connStr, int typeId, bool IsCustomCategoriesUses)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                List<CarwashService> services = new List<CarwashService>();
                conn.Open();

                SqlCommand findServices = new SqlCommand($"select * from [CARWASH].[dbo].[CARWASH_SERVICES] " +
                                                         $"where ID_TYPE = {typeId} and VIS = 'true' ORDER BY NAME", conn);

                SqlDataReader dr = findServices.ExecuteReader();
                while (dr.Read())
                {
                    services.Add(new CarwashService(dr.GetInt32(0), 
                        dr.GetString(1), 
                        dr.GetValue(3) != DBNull.Value ? dr.GetString(3) : null, 
                        TimeSpan.FromMinutes(Convert.ToDouble(dr.GetInt32(4))), 
                        dr.GetBoolean(5)));
                }
                dr.Close();                          

                if (services != null && services.Count > 0)
                {
                    for (int i=0; i<services.Count; i++)
                    {
                        SqlCommand findTypeName = new SqlCommand($"select NAME from [CARWASH].[dbo].[SERVICE_CATEGORIES] where ID = {typeId}", conn);
                        services[i].Type = new ServiceType() { Id = typeId, Name = findTypeName.ExecuteScalar().ToString(), IsCarwashService = true };

                        if (IsCustomCategoriesUses)
                        {
                            services[i].CategoryPrices = new List<CarCategory>();
                            SqlCommand findPrices = new SqlCommand($"select c.ID, c.NAME, p.PRICE " +
                                                                   $"from [CARWASH].[dbo].[CAR_CATEGORIES] c join [CARWASH].[dbo].[CSERVICE_CATEGORY_PRICE] p " +
                                                                   $"on c.ID = p.ID_CLASS " +
                                                                   $"where p.ID_SERVICE = {services[i].Id}", conn);

                            SqlDataReader dr2 = findPrices.ExecuteReader();
                            while (dr2.Read())
                            {
                                services[i].CategoryPrices.Add(new CarCategory()
                                {
                                    Id = dr2.GetInt32(0),
                                    Name = dr2.GetString(1),
                                    Price = dr2.GetValue(2) != DBNull.Value ? Convert.ToDecimal(dr2.GetValue(2)) : 0
                                });
                            }
                            dr2.Close();
                        }
                        else
                        {
                            services[i].ClassPrices = new List<CarClass>();
                            SqlCommand findPrices = new SqlCommand($"select p.ID, p.PRICE " +
                                                                   $"from [CARWASH].[dbo].[CAR_CATEGORIES] c join [CARWASH].[dbo].[CSERVICE_CLASS_PRICE] p " +
                                                                   $"on c.ID = p.ID_CLASS " +
                                                                   $"where p.ID_SERVICE = {services[i].Id}", conn);

                            SqlDataReader dr2 = findPrices.ExecuteReader();
                            while (dr2.Read())
                            {
                                services[i].ClassPrices.Add(new CarClass() 
                                { 
                                    Id = dr2.GetInt32(0), 
                                    Price = dr2.GetValue(1) != DBNull.Value ? Convert.ToDecimal(dr2.GetValue(1)) : 0
                                });
                            }
                            dr2.Close();
                        }
                    }
                }

                return services;
            }
        }

        public bool AddNewService(string connStr, bool IsCustomCategoriesUsed, out string err)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    bool res = false;
                    conn.Open();

                    SqlCommand addService = new SqlCommand($"insert into [CARWASH].[dbo].[CARWASH_SERVICES] (NAME, ID_TYPE, DESCR, DURATION, VIS) " +
                                                           $"output INSERTED.ID " +
                                                           $"values ('{Name}', {Type.Id}, '{Description}', {Duration.Minutes}, '{Visibility}')", conn);

                    Id = Convert.ToInt32(addService.ExecuteScalar());

                    if (CategoryPrices != null || ClassPrices != null)
                    {
                        if (IsCustomCategoriesUsed && CategoryPrices != null)
                        {
                            for (int i=0; i<CategoryPrices.Count; i++)
                            {
                                SqlCommand addServicePrice = new SqlCommand($"insert into [CARWASH].[dbo].[CSERVICE_CATEGORY_PRICE] " +
                                                                            $"values ({Id}, {CategoryPrices[i].Id}, {CategoryPrices[i].Price})", conn);
                                addServicePrice.ExecuteNonQuery();
                            }

                            res = true;
                        }

                        else if (!IsCustomCategoriesUsed && ClassPrices != null)
                        {
                            for (int i=0; i<ClassPrices.Count; i++)
                            {
                                SqlCommand addServicePrice = new SqlCommand($"insert into [CARWASH].[dbo].[CSERVICE_CLASS_PRICE] " +
                                                                            $"values ({Id}, {ClassPrices[i].Id}, {ClassPrices[i].Price})", conn);
                            }

                            res = true;
                        }
                        else
                            res = false;
                    }
                    err = null;
                    return res;
                }
                catch (Exception ex)
                {
                    err = ex.ToString();
                    return false;
                }
            }
        }

        public bool UpdateService(string connStr, bool isCustomCategoriesUsed, string newName, string newDescr, TimeSpan newDuration, ServiceType newType, List<CarCategory> newPrices = null, List<CarClass> newCPrices = null)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    string updateCommandString = string.Empty;
                    if (!Name.Equals(newName))
                        updateCommandString += $"NAME = '{newName}' ";

                    if (!Description.Equals(newDescr))
                        updateCommandString += updateCommandString != string.Empty ? $", DESCR = '{newDescr}' " : $"DESCR = '{newDescr}' ";

                    if (Duration != newDuration)
                        updateCommandString += updateCommandString != string.Empty ? $", DURATION = {newDuration.Minutes} " : $"DURATION = {newDuration.Minutes} ";

                    if (Type.Id != newType.Id)
                        updateCommandString += updateCommandString != string.Empty ? $", ID_TYPE = {newType.Id}" : $"ID_TYPE = {newType.Id}";

                    if (updateCommandString != string.Empty)
                    {
                        using (SqlCommand updateServiceInfo = new SqlCommand($"update [CARWASH].[dbo].[CARWASH_SERVICES] " +
                                                                         $"set {updateCommandString} " +
                                                                         $"where ID = {Id}", conn))
                            updateServiceInfo.ExecuteNonQuery();
                    }                    

                    if (isCustomCategoriesUsed)
                    {
                        for (int i = 0; i < CategoryPrices.Count; i++)
                        {
                            if (CategoryPrices[i].Price != newPrices[i].Price)
                            {
                                using (SqlCommand updatePrice = new SqlCommand($"update [CARWASH].[dbo].[CSERVICE_CATEGORY_PRICE] " +
                                                                               $"set PRICE = {CategoryPrices[i].Price} " +
                                                                               $"where ID_SERVICE = {Id} " +
                                                                               $"and ID_CLASS = {CategoryPrices[i].Id}", conn))
                                    updatePrice.ExecuteNonQuery();
                            }
                        }
                    }

                    else
                    {
                        for (int i = 0; i < CategoryPrices.Count; i++)
                        {
                            if (ClassPrices[i].Price != newCPrices[i].Price)
                            {
                                using (SqlCommand updatePrice = new SqlCommand($"update [CARWASH].[dbo].[CSERVICE_CLASS_PRICE] " +
                                                                               $"set PRICE = {ClassPrices[i].Price} " +
                                                                               $"where ID_SERVICE = {Id} " +
                                                                               $"and ID_CLASS = {ClassPrices[i].Id}", conn))
                                    updatePrice.ExecuteNonQuery();
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
        }

        public bool DeleteService(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                try
                {
                    SqlCommand deleteService = new SqlCommand($"update [CARWASH].[dbo].[CARWASH_SERVICES] " +
                                                                        $"set VIS = 'false' " +
                                                                        $"where ID = {Id}", conn);
                    deleteService.ExecuteNonQuery();

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
