using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using CWLib.Interfaces;

namespace CWLib
{
    public class CarwashOrder : OrderBase, IOrder
    {
        public int IdClientsCar { get; set; }
        public string ClientsCarPlate { get; set; }
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
                    
                    SqlCommand addOrder = new SqlCommand($"insert into [CARWASH].[dbo].[CARWASH_ORDERS] (ID_BOX, ID_PERSON, ID_PAYMENT_TYPE, ID_CLIENTS_CAR, START_TIME, EST_END_TIME, END_TIME, SUM, COMMENT, VIS, IS_DELETED, ID_ADMIN) " +
                                                        $"output inserted.ID " +
                                                        $"values ({Box.Id}, {PersonId}, {MoneyType.Id}, {IdClientsCar}, '{StartTime.ToString("yyyy-MM-ddThh:mm:ss")}', '{((DateTime)EstimatedEndTime).ToString("yyyy-MM-ddThh:mm:ss")}', null, {Sum}, '{Comment}', 'true', 'false', {AdmId})", conn);

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

                    Box.SetBoxBusyState(connStr, true, Id, true);
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

        public bool UpdateOrder(string connStr, int personId, string newName, string newPhone, string newEmail, 
            ClientGroup newGroup, Box newBox, List<ClientCar> newCars, List<Worker> newWorkers, 
            List<CarwashService> newServices, MoneyType newMoneyType, out string err)
        {
            try
            {
                EntityClient entClient = null;
                IndividualClient indClient = null;

                if (DBWorker.EntityClientCheck(connStr, personId))
                {
                    entClient = new EntityClient() { Id = personId };
                    entClient.GetClientInfoById(connStr);

                    entClient.UpdateEntityClient(connStr, newCars, newName, newPhone);
                }

                else
                {
                    indClient = new IndividualClient() { Id = personId };
                    indClient.GetClientInfoById(connStr);

                    indClient.UpdateIndividualClient(connStr, newGroup, newCars, newName, newPhone, newEmail);
                }               

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    int tempOrderDuration = 0; //новая длительность выполнения заказа
                    decimal tempOrderSum = 0; //новая сумма заказа

                    /* проверка и обновление услуг в заказе */
                    for (int i = 0; i < newServices.Count; i++)
                    {
                        tempOrderDuration += newServices[i].Duration.Minutes;
                        tempOrderSum += (decimal)(newServices[i].OrderServiceCount * newServices[i].UsedPrice);

                        bool serviceFlag = false;

                        for (int j=0; j<Services.Count; j++)
                        {
                            if (newServices[i].Id == Services[j].Id)
                            {
                                using (SqlCommand updateOrderService = new SqlCommand($"update [CARWASH].[dbo].[CARWASH_ORDER_SERVICES] " +
                                                                                        $"set COUNT = {newServices[i].OrderServiceCount}, PRICE = {newServices[i].UsedPrice} " +
                                                                                        $"where ID_ORDER = {Id}", conn))
                                    updateOrderService.ExecuteNonQuery();
                                
                                serviceFlag = true;
                            }                            
                        }
                        
                        if (!serviceFlag)
                        {
                            using (SqlCommand addServiceToOrder = new SqlCommand($"insert into [CARWASH].[dbo].[CARWASH_ORDER_SERVCES] " +
                                                                                    $"values ({Id}, {newServices[i].Id}, {UsingCustomCategories}, {Services[i].UsedCarClass}, " +
                                                                                    $"{newServices[i].UsedCarCategory}, {newServices[i].OrderServiceCount}, {newServices[i].UsedPrice}, 'true')", conn))
                                addServiceToOrder.ExecuteNonQuery();
                        }
                    }

                    /* проверить, не удалялись ли услуги в заказе */
                    for (int i = 0; i < Services.Count; i++)
                    {
                        bool delServiceFlag = false;
                        int delServiceIndex = 0;

                        for (int j = 0; j < newServices.Count; j++)
                        {
                            if (Services[i].Id == newServices[j].Id)
                            {
                                delServiceFlag = true;
                                delServiceIndex = i;
                            }                                
                        }

                        if (!delServiceFlag)
                        {
                            using (SqlCommand delOrderService = new SqlCommand($"update [CARWASH].[dbo].[CARWASH_ORDER_SERVICES] " +
                                                                                $"set VIS = 'false' " +
                                                                                $"where ID_ORDER = {Id} and ID_SERVICE = {Services[delServiceIndex].Id}", conn))
                                delOrderService.ExecuteNonQuery();
                        }
                    }

                    /* проверка и обновление работников в заказе */
                    for (int i = 0; i < newWorkers.Count; i++)
                    {
                        bool addFlag = false;

                        for (int j = 0; j < Workers.Count; j++)
                        {
                            if (newWorkers[i].Id == Workers[j].Id)
                            {
                                addFlag = true;
                                break;
                            }
                        }

                        if (!addFlag)
                        {
                            int tempId = 0;
                            using (SqlCommand findId = new SqlCommand($"select ID from [CARWASH].[dbo].[WORKERS] where ID_PERSON = {newWorkers[i].Id}", conn))
                                tempId = (int)findId.ExecuteScalar();

                            using (SqlCommand addWorker = new SqlCommand($"insert into [CARWASH].[dbo].[CARWASH_ORDER_WORKERS] " +
                                                                            $"values ({Id}, {tempId}, 'true')", conn))
                                addWorker.ExecuteNonQuery();
                        }
                    }

                    /* проверить не удалялись ли работники из заказа */
                    for (int i = 0; i < Workers.Count; i++)
                    {
                        bool delFlag = true;

                        for (int j = 0; j < newWorkers.Count; j++)
                        {
                            if (Workers[i].Id == newWorkers[j].Id)
                            {
                                delFlag = false;
                                break;
                            }
                        }

                        if (delFlag)
                        {
                            using (SqlCommand delWorker = new SqlCommand($"delete from [CARWASH].[dbo].[CARWASH_ORDER_WORKERS] " +
                                                                            $"where ID_ORDER = {Id} and ID_WORKER IN (SELECT ID FROM [CARWASH].[dbo].[WORKERS] WHERE ID_PERSON = {Workers[i].Id})", conn))
                                delWorker.ExecuteNonQuery();
                        }
                    }

                    string updateStr = string.Empty;

                    /* изменение статуса боксов */

                    if (newBox.Id != Box.Id)
                    {
                        newBox.SetBoxBusyState(connStr, false, Id, true);
                        Box.SetBoxBusyState(connStr, true, Id, true);

                        updateStr += updateStr == string.Empty ? $"ID_BOX = {newBox.Id}" : $", ID_BOX = {newBox.Id}";
                    }

                    /* проверка и обновление типа оплаты заказа */
                    if (newMoneyType.Id != MoneyType.Id)                    
                        updateStr += updateStr == string.Empty ? $"ID_PAYMENT_TYPE = {newMoneyType.Id}" : $", ID_PAYMENT_TYPE = {newMoneyType.Id}";                    

                    /* проверка и обновление инфо о заказе */

                    if (EstimatedEndTime != StartTime.AddMinutes(tempOrderDuration))
                        updateStr += updateStr == string.Empty ? $"EST_END_TIME = '{StartTime.AddMinutes(tempOrderDuration).ToString("yyyy-MM-ddThh:mm:ss")}'" : $", EST_END_TIME = '{StartTime.AddMinutes(tempOrderDuration).ToString("yyyy-MM-ddThh:mm:ss")}'";

                    if (Sum != tempOrderSum)
                        updateStr += updateStr == string.Empty ? $"SUM = {tempOrderSum}" : $", SUM = {tempOrderSum}";

                    if (updateStr != string.Empty)
                    {
                        using (SqlCommand updateOrderInfo = new SqlCommand($"update [CARWASH].[dbo].[CARWASH_ORDERS] " +
                                                                                        $"set {updateStr} " +
                                                                                        $"where ID = {Id}", conn))


                            updateOrderInfo.ExecuteNonQuery();
                    }                                     
                }

                err = null;
                return true;
            }
            catch (Exception ex)
            {
                err = ex.ToString();
                return false;
            }
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

                        using (SqlCommand findPlate = new SqlCommand($"select PLATE from [CARWASH].[dbo].[CLIENT_CARS] where ID = {orders[i].IdClientsCar}", conn))
                            orders[i].ClientsCarPlate = (string)findPlate.ExecuteScalar();
                    }
                }                
            }

            return orders;
        }
    }
}
