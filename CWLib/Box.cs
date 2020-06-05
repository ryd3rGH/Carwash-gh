using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CWLib
{
    public class Box
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool? State { get; set; } /* true - свободен, false - занят */
        public bool? TechState { get; set; }
        public int? OrderId { get; set; }
        public byte? OrderType { get; set; }

        public Box()
        {

        }      

        public Box(int id, string name, bool? state, bool? techState, int? orderId, byte? orderType)
        {
            Id = id;
            Name = name;
            State = state;
            TechState = techState;
            OrderId = orderId;
            OrderType = orderType;
        }

        public Box AddBox(string name, bool techState, string connStr, out bool res)
        {
            Box box = new Box();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand addBox = new SqlCommand("insert into [CARWASH].[dbo].[BOXES] (BOX_NAME, STATE, TECH_STATE, ID_ORDER, ORDER_TYPE, VIS) " +
                                                        "output inserted.ID " +
                                                        "values (@name, 'true', @tech, null, null, 'true')", conn);

                    SqlParameter boxName = new SqlParameter("@name", System.Data.SqlDbType.VarChar);
                    boxName.Value = name;
                    addBox.Parameters.Add(boxName);

                    SqlParameter techStateParam = new SqlParameter("@tech", System.Data.SqlDbType.Bit);
                    techStateParam.Value = techState;
                    addBox.Parameters.Add(techStateParam);

                    Id = Convert.ToInt32(addBox.ExecuteScalar());
                }

                box.Name = name;
                box.State = true;
                box.TechState = techState;
                box.OrderId = null;
                box.OrderType = null;

                res = true;
                return box;
            }
            catch (Exception ex)
            {
                res = false;
                return null;
            }
        }

        public override string ToString()
        {
            return Name;
        }        

        public bool ChangeTechState(string connStr)
        {
            bool newState = TechState == true ? false : true;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    SqlCommand changeTechState = new SqlCommand($"update [CARWASH].[dbo].[BOXES] " +
                                                                $"set [TECH_STATE]='{newState}' " +
                                                                $"where [ID]={Id}", conn);
                    changeTechState.ExecuteNonQuery();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool DeleteBox(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    SqlCommand delBox = new SqlCommand($"update [CARWASH].[dbo].[BOXES] " +
                                                        $"set [VIS]='false' " +
                                                        $"where [ID] = {Id}", conn);
                    delBox.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static List<Box> GetBoxes(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                List<Box> boxes = new List<Box>();

                SqlCommand findBoxes = new SqlCommand($"select * from [CARWASH].[dbo].[BOXES] " +
                                                        $"where VIS = 'true' and " +
                                                        $"TECH_STATE = 'true' and " +
                                                        $"STATE = 'true'", conn);

                using (SqlDataReader dr = findBoxes.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        boxes.Add(new Box(dr.GetInt32(0), dr.GetString(1), dr.GetBoolean(2), dr.GetBoolean(3), dr.GetValue(4) == DBNull.Value ? null : (int?)dr.GetValue(4), null));
                    }
                }

                return boxes;
            }
        }

        public bool GetBoxInfoById(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                
                try
                {
                    using (SqlCommand findInfo = new SqlCommand($"select * from [CARWASH].[dbo].[BOXES] where ID = {Id}", conn))
                    {
                        using (SqlDataReader dr = findInfo.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Name = dr.GetString(1);
                                State = dr.GetBoolean(2);
                                TechState = dr.GetBoolean(3);
                                OrderId = dr.GetValue(4) == DBNull.Value ? null : (int?)dr.GetValue(4);
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

        public bool SetBoxBusyState(string connStr, bool state, int? orderId, bool isCarwash)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    byte IsCarwash = (byte)(isCarwash == true ? 0 : 1);

                    using (SqlCommand setIsBusy = new SqlCommand($"update [CARWASH].[dbo].[BOXES] " +
                                                                 $"set STATE = '{state}', ID_ORDER = {orderId}, ORDER_TYPE = { IsCarwash }" +
                                                                 $"where ID = {Id}", conn))
                    {
                        setIsBusy.ExecuteNonQuery();
                    }

                    State = state;

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool UpdateBox(string connStr, string newBoxName, bool newTechState)
        {
            Name = newBoxName;
            TechState = newTechState;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand updateBox = new SqlCommand($"update [CARWASH].[dbo].[BOXES] " +
                                                                 $"set BOX_NAME = '{Name}', TECH_STATE = '{TechState}' " +
                                                                 $"where ID = {Id}", conn))
                        updateBox.ExecuteNonQuery();

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
