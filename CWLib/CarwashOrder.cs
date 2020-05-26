using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using CWLib.Interfaces;

namespace CWLib
{
    public class CarwashOrder : OrderBase, IOrder
    {
        public int IdClientsCar { get; set; }
        public bool? IsShineWeather { get; set; }
        public List<Discount> Discounts { get; set; }
        public List<CarwashService> Services { get; set; }
        public bool UsingCustomCategories { get; set; } //использование пользовательских категорий
        private int OrderDuration { get; set; }

        public CarwashOrder() { }

        public bool AddOrder(string connStr, out int? orderId, out string err)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    StartTime = DateTime.Now;

                    OrderDuration = 0;

                    if (Services.Count > 0)
                    {
                        for (int i = 0; i < Services.Count; i++)
                            OrderDuration += Services[i].Duration.Minutes;

                        EstimatedEndTime = StartTime.AddMinutes(OrderDuration);
                    }
                    
                    SqlCommand addOrder = new SqlCommand($"insert into [CARWASH].[dbo].[CARWASH_ORDERS] (ID_BOX, ID_PERSON, ID_PAYMENT_TYPE, ID_CLIENTS_CAR, START_TIME, EST_END_TIME, END_TIME, SUM, COMMENT, VIS, IS_DELETED) " +
                                                        $"output inserted.ID " +
                                                        $"values ({Box.Id}, {PersonId}, {MoneyType.Id}, {IdClientsCar}, '{StartTime.ToString("yyyy-MM-ddThh:mm:ss")}', '{((DateTime)EstimatedEndTime).ToString("yyyy-MM-ddThh:mm:ss")}', null, {Sum}, '{Comment}', 'true', 'false')", conn);

                    Id = Convert.ToInt32(addOrder.ExecuteScalar());
                    orderId = (int)Id;

                    if (Workers.Count > 0)
                    {
                        for (int i = 0; i < Workers.Count; i++)
                        {
                            SqlCommand addOrderWorker = new SqlCommand($"insert into [CARWASH].[dbo].[CARWASH_ORDER_WORKERS] (ID_ORDER, ID_WORKER, VIS) " +
                                                                       $"values ({Id}, {Workers[i].Id}, 'true')", conn);

                            addOrderWorker.ExecuteNonQuery();
                        }
                    }

                    if (Services.Count > 0)
                    {
                        for (int i = 0; i < Services.Count; i++)
                        {
                            SqlCommand addOrderService = new SqlCommand($"insert into [CARWASH].[dbo].[CARWASH_ORDER_SERVICES] (ID_ORDER, ID_SERVICE, CUSTOM_CATEGORIES, ID_CLASS, ID_CATEGORY, COUNT, PRICE, VIS) " +
                                                                        $"values ({Id}, {Services[i].Id}, '{UsingCustomCategories}', {Services[i].UsedCarClass}, {Services[i].UsedCarCategory}, {Services[i].OrderServiceCount}, {Services[i].UsedPrice}, 'true')", conn);

                            addOrderService.ExecuteNonQuery();
                        }
                    }

                    /* Оплата заказа */
                    CashboxOperation operation = new CashboxOperation((int)Id, Sum, StartTime, MoneyType, false, $"Оплата заказа №{Id}");
                    operation.AddCashboxOperation(connStr);

                    err = null;
                    return true;
                }
            }
            catch (Exception ex)
            {
                err = ex.ToString();
                orderId = null;
                return false;
            }
        }

        public bool EndOrder(string connStr)
        {
            try
            {
                DateTime endTime = DateTime.Now;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    using (SqlCommand endOrder = new SqlCommand($"update [CARWASH].[dbo].[CARWASH_ORDERS] " +
                                                                $"set END_TIME = '{endTime.ToString("yyyy-MM-ddThh:mm:ss")}' " +
                                                                $"where ID = {Id}", conn))
                    {
                        endOrder.ExecuteNonQuery();
                    }

                    for (int i = 0; i < Workers.Count; i++) //null reference
                        Workers[i].SetWorkerBusyState(connStr, false);

                    Box.SetBoxBusyState(connStr, true);
                }

                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool DeleteOrder(string connStr)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    using (SqlCommand deleteOrder = new SqlCommand($"update [CARWASH].[dbo].[CARWASH_ORDERS] " +
                                                                    $"set VIS = 'false', COMMENT = 'Заказ был удален {DateTime.Now.ToString()}', IS_DELETED = 'true' " +
                                                                    $"where ID = {Id}", conn))
                    {
                        deleteOrder.ExecuteNonQuery();
                    }
                }

                CashboxOperation.DeleteOperation(connStr, (int)Id);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteOrder(string connStr, int orderId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    using (SqlCommand deleteOrder = new SqlCommand($"update [CARWASH].[dbo].[CARWASH_ORDERS] " +
                                                                    $"set VIS = 'false', COMMENT = 'Заказ был удален {DateTime.Now.ToString()}', IS_DELETED = 'true' " +
                                                                    $"where ID = {orderId}", conn))
                    {
                        deleteOrder.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateOrder()
        {
            throw new NotImplementedException();
        }

        public List<IOrder> GetOrders()
        {
            throw new NotImplementedException();
        }

        public static List<CarwashOrder> GetActiveOrders(string connStr)
        {
            List<CarwashOrder> orders = new List<CarwashOrder>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                /* Поиск активных заказов */
                using (SqlCommand findOrders = new SqlCommand($"select * from [CARWASH].[dbo].[CARWASH_ORDERS] " +
                                                                $"where END_TIME is null " +
                                                                $"and VIS = 'true' " +
                                                                $"order by ID desc", conn))
                {
                    using (SqlDataReader dr = findOrders.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            orders.Add(new CarwashOrder()
                            {
                                Id = dr.GetInt32(0),
                                Box = new Box() { Id = dr.GetInt32(1) },
                                PersonId = dr.GetInt32(2),
                                MoneyType = new MoneyType() { Id = dr.GetInt32(3) },
                                IdClientsCar = dr.GetInt32(4),
                                StartTime = dr.GetDateTime(5),
                                EstimatedEndTime = dr.GetDateTime(6),
                                Sum = dr.GetDecimal(8),
                                Comment = dr.GetString(9)
                            });
                        }
                    }    
                }

                
                if (orders.Count > 0)
                {
                    for (int i = 0; i < orders.Count; i++)
                    {
                        /* Поиск услуг в активных заказах */
                        orders[i].Services = CarwashService.GetServicesByOrderId(connStr, (int)orders[i].Id);

                        /* Поиск сотрудников в активных заказах */
                        orders[i].Workers = Worker.GetWorkersInOrder(connStr, (int)orders[i].Id);
                    }
                }

                
            }

            return orders;
        }        
    }
}
