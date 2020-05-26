using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Text;

namespace CWLib
{
    public class CashboxOperation
    {
        private int Id { get; set; }
        public int? OrderId { get; private set; }
        public decimal Sum { get; private set; }
        public DateTime OperationDateTime { get; private set; }
        public MoneyType MoneyType { get; private set; }
        public bool IsConsumption { get; private set; } //расход - true
        public string Description { get; private set; }
        public bool IsDeleted { get; private set; }

        public CashboxOperation(int? orderId, decimal sum, DateTime operationDateTime, MoneyType moneyType, bool isConsumption, string description)
        {
            OrderId = orderId;
            Sum = sum;
            OperationDateTime = operationDateTime;
            MoneyType = moneyType;
            IsConsumption = isConsumption;
            Description = description;
            IsDeleted = false;
        }

        public CashboxOperation(int? orderId, decimal sum, DateTime operationDateTime, MoneyType moneyType, bool isConsumption, string description, bool isDeleted, int id)
        {
            OrderId = orderId;
            Sum = sum;
            OperationDateTime = operationDateTime;
            MoneyType = moneyType;
            IsConsumption = isConsumption;
            Description = description;
            IsDeleted = isDeleted;
            Id = id;
        }

        public bool AddCashboxOperation(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    if (OrderId != null)
                    {
                        using (SqlCommand addOperation = new SqlCommand($"insert into [CARWASH].[dbo].[CASHBOX_OPERATIONS] (ID_ORDER, SUM, OPERATION_DATETIME, ID_MONEY_TYPE, DESCR, IS_DELETED, IS_CONSUMPTION) " +
                                                                    $"output inserted.ID " +
                                                                    $"values ({OrderId}, {Sum}, '{OperationDateTime.ToString("yyyy-MM-ddThh:mm:ss")}', {MoneyType.Id}, '{Description}', '{IsDeleted}', '{IsConsumption}')", conn))

                        {
                            Id = (int)addOperation.ExecuteScalar();
                        }
                    }
                    
                    else
                    {
                        using (SqlCommand addOperation = new SqlCommand($"insert into [CARWASH].[dbo].[CASHBOX_OPERATIONS] (SUM, OPERATION_DATETIME, ID_MONEY_TYPE, DESCR, IS_DELETED, IS_CONSUMPTION) " +
                                                                    $"output inserted.ID " +
                                                                    $"values ({Sum}, '{OperationDateTime.ToString("yyyy-MM-ddThh:mm:ss")}', {MoneyType.Id}, '{Description}', '{IsDeleted}', '{IsConsumption}')", conn))

                        {
                            Id = (int)addOperation.ExecuteScalar();
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

        public static List<CashboxOperation> GetOperations(string connStr)
        {
            List<CashboxOperation> operations = new List<CashboxOperation>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand getOperations = new SqlCommand("select c.ID_ORDER, c.SUM, c.OPERATION_DATETIME, p.ID, p.NAME, c.IS_CONSUMPTION, c.DESCR, c.IS_DELETED, c.ID "+
                                                                 "from [CARWASH].[dbo].[CASHBOX_OPERATIONS] c join [CARWASH].[dbo].[PAYMENT_CATEGORIES] p " +
                                                                 "on c.ID_MONEY_TYPE = p.ID " +
                                                                 "order by c.OPERATION_DATETIME desc", conn))
                {
                    using (SqlDataReader dr = getOperations.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int? orderId = dr.GetValue(0) != DBNull.Value ? (int?)dr.GetInt32(0) : null;

                            operations.Add(new CashboxOperation(orderId, dr.GetDecimal(1), dr.GetDateTime(2), 
                                                                new MoneyType() { Id = dr.GetInt32(3), Name = dr.GetString(4) }, 
                                                                dr.GetBoolean(5), dr.GetString(6), dr.GetBoolean(7), dr.GetInt32(8)));
                        }
                    }
                }
            }

            return operations;
        }

        public static List<CashboxOperation> GetOperations(string connStr, string startDate, string endDate)
        {
            List<CashboxOperation> operations = new List<CashboxOperation>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand getOperations = new SqlCommand($"select c.ID_ORDER, c.SUM, c.OPERATION_DATETIME, p.ID, p.NAME, c.IS_CONSUMPTION, c.DESCR, c.IS_DELETED, c.ID " +
                                                                 $"from [CARWASH].[dbo].[CASHBOX_OPERATIONS] c join [CARWASH].[dbo].[PAYMENT_CATEGORIES] p " +
                                                                 $"on c.ID_MONEY_TYPE = p.ID " +
                                                                 $"where c.OPERATION_DATETIME between '{startDate}' and '{endDate}'" +
                                                                 $"order by c.OPERATION_DATETIME desc", conn))
                {
                    using (SqlDataReader dr = getOperations.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int? orderId = dr.GetValue(0) != DBNull.Value ? (int?)dr.GetInt32(0) : null;

                            operations.Add(new CashboxOperation(orderId, dr.GetDecimal(1), dr.GetDateTime(2),
                                                                new MoneyType() { Id = dr.GetInt32(3), Name = dr.GetString(4) },
                                                                dr.GetBoolean(5), dr.GetString(6), dr.GetBoolean(7), dr.GetInt32(8)));
                        }
                    }
                }
            }

            return operations;
        }

        public bool DeleteOperation(string connStr)
        {
            try
            {
                if (OrderId != null)
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();

                        using (SqlCommand deleteOperation = new SqlCommand($"update [CARWASH].[dbo].[CASHBOX_OPERATIONS] " +
                                                                           $"set IS_DELETED = 'true', DESCR = DESCR + ', удалено' " +
                                                                           $"where ID_ORDER = {OrderId}", conn))
                        {
                            deleteOperation.ExecuteNonQuery();
                        }
                    }

                    CarwashOrder.DeleteOrder(connStr, (int)OrderId); 
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();

                        using (SqlCommand deleteOperation = new SqlCommand($"update [CARWASH].[dbo].[CASHBOX_OPERATIONS] " +
                                                                           $"set IS_DELETED = 'true', DESCR = DESCR + ', удалено' " +
                                                                           $"where ID = {Id}", conn))
                        {
                            deleteOperation.ExecuteNonQuery();
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

        public static bool DeleteOperation(string connStr, int orderId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    using (SqlCommand deleteOperation = new SqlCommand($"update [CARWASH].[dbo].[CASHBOX_OPERATIONS] " +
                                                                       $"set IS_DELETED = 'true', DESCR = DESCR + ', удалено' " +
                                                                       $"where ID_ORDER = {orderId}", conn))
                    {
                        deleteOperation.ExecuteNonQuery();
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
