using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Text;

namespace CWLib
{
    /* Работники */
    public class Worker : PersonBase
    {
        public DateTime? BirthDate { get; set; }
        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public DateTime? PassportDate { get; set; }
        public string RegistrationAdress { get; set; }
        public string FactAdress { get; set; }
        public WorkersCategory WorkerCategory { get; set; }
        public User User { get; set; }
        public bool IsOnWork { get; set; }
        public bool IsBusy { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public void GetWorkerInfoById(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand findInfo = new SqlCommand($"select * " +
                                                        $"from [CARWASH].[dbo].[PERSONS] p join [CARWASH].[dbo].[WORKERS] w " +
                                                        $"on p.ID = w.ID_PERSON " +
                                                        $"where  p.ID = {Id}", conn);

                SqlDataReader dr = findInfo.ExecuteReader();
                while (dr.Read())
                {
                    Name = dr.GetValue(1) != DBNull.Value ? dr.GetString(1) : null;
                    Phone = dr.GetValue(2) != DBNull.Value ? dr.GetString(2) : null;
                    Email = dr.GetValue(3) != DBNull.Value ? dr.GetString(3) : null;
                    BirthDate = dr.GetValue(10) != DBNull.Value ? dr.GetDateTime(10) : new DateTime?();
                    PassportSeries = dr.GetValue(11) != DBNull.Value ? dr.GetString(11) : null;
                    PassportNumber = dr.GetValue(12) != DBNull.Value ? dr.GetString(12) : null;
                    PassportDate = dr.GetValue(13) != DBNull.Value ? dr.GetDateTime(13) : new DateTime?();
                    RegistrationAdress = dr.GetValue(14) != DBNull.Value ? dr.GetString(14) : null;
                    FactAdress = dr.GetValue(15) != DBNull.Value ? dr.GetString(15) : null;
                    WorkerCategory = new WorkersCategory() { GroupId = dr.GetInt32(16) };
                    IsOnWork = dr.GetBoolean(17);
                }
                dr.Close();

                SqlCommand findGroupName = new SqlCommand($"select GROUP_NAME " +
                                                            $"from [CARWASH].[dbo].[WORKERS_CATEGORIES] " +
                                                            $"where ID = {WorkerCategory.GroupId}", conn);

                WorkerCategory.GroupName = (string)findGroupName.ExecuteScalar();
            }
        }

        public bool AddWorker(string connStr, out string er)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    SqlCommand addPerson = new SqlCommand($"insert into [CARWASH].[dbo].[PERSONS] (NAME, PHONE, EMAIL, COMMENT, IS_ENTITY, IS_WORKER, VIS) " +
                                                            $"output INSERTED.ID " +
                                                            $"values ('{Name}', '{Phone}', '{Email}', null, 'false', 'true', 'true')", conn);

                    Id = Convert.ToInt32(addPerson.ExecuteScalar());

                    string bdateStr = BirthDate != null ? $"'" + ((DateTime)BirthDate).ToString("yyyy-MM-ddThh:mm:ss") + "'" : "NULL";
                    string pdateStr = PassportDate != null ? $"'" + ((DateTime)PassportDate).ToString("yyyy-MM-ddThh:mm:ss") + "'" : "NULL";

                    SqlCommand addWorker = new SqlCommand($"insert into [CARWASH].[dbo].[WORKERS] (ID_PERSON, BIRTHDATE, PASSPORT_SERIES, PASSPORT_NUMBER, PASSPORT_DATE, REG_ADRESS, FACT_ADRESS, ID_WORKERS_CATEGORY, IS_ON_WORK, IS_BUSY) " +
                                                            $"values ({Id}, {bdateStr}, '{PassportSeries}', '{PassportNumber}', {pdateStr}, '{RegistrationAdress}', '{FactAdress}', {WorkerCategory.GroupId}, 'true', 'false')", conn);

                    addWorker.ExecuteNonQuery();

                    er = null;
                    return true;
                }
                catch (Exception ex)
                {
                    er = ex.ToString();
                    return false;
                }
            }
        }

        public bool UpdateWorker(string connStr, WorkersCategory newCategory, string newName = "Null", string newPhone = "Null", string newEmail = "Null", DateTime? newBirthdate = null, 
                                 string newPs = "Null", string newPn = "Null", DateTime? newPdate = null, string newRa = "Null", string newFa = "Null")
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                try
                {
                    string updateString = string.Empty;

                    if (!Name.Equals(newName))
                        updateString += $" NAME = '{newName}'";

                    if (!Phone.Equals(newPhone))
                        updateString += updateString != string.Empty ? $", PHONE = '{newPhone}'" : $"PHONE = '{newPhone}'";

                    if (!Email.Equals(newEmail))
                        updateString += updateString != string.Empty ? $", EMAIL = '{newEmail}'" : $"EMAIL = '{newEmail}'";
                    
                    if (updateString != string.Empty)
                    {
                        using (SqlCommand updatePerson = new SqlCommand($"update [CARWASH].[dbo].[PERSONS] " +
                                                                        $"set {updateString} " +
                                                                        $"where ID = {Id}", conn))
                        {
                            updatePerson.ExecuteNonQuery();
                        }
                    }

                    updateString = string.Empty;

                    if (newBirthdate != null)                    
                        if (BirthDate != newBirthdate)
                            updateString += updateString != string.Empty ? $", BIRTHDATE = '{((DateTime)newBirthdate).ToString("yyyy-MM-ddThh:mm:ss")}'" : $"BIRTHDATE = '{((DateTime)newBirthdate).ToString("yyyy-MM-ddThh:mm:ss")}'";                    
                    else
                        updateString += updateString != string.Empty ? $", BIRTHDATE = NULL" : $"BIRTHDATE = NULL";

                    if (!PassportSeries.Equals(newPs))
                        updateString += updateString != string.Empty ? $", PASSPORT_SERIES = '{newPs}'" : $"PASSPORT_SERIES = '{newPs}'";

                    if (!PassportNumber.Equals(newPn))
                        updateString += updateString != string.Empty ? $", PASSPORT_NUMBER = '{newPn}'" : $"PASSPORT_NUMBER = '{newPn}'";

                    if (newBirthdate != null)                    
                        if (PassportDate != newPdate)
                            updateString += updateString != string.Empty ? $", PASSPORT_DATE = '{((DateTime)newPdate).ToString("yyyy-MM-ddThh:mm:ss")}'" : $"PASSPORT_DATE = '{((DateTime)newPdate).ToString("yyyy-MM-ddThh:mm:ss")}'";                    
                    else
                        updateString += updateString != string.Empty ? $", PASSPORT_DATE = NULL" : $"PASSPORT_DATE = NULL";

                    if (!RegistrationAdress.Equals(newRa))
                        updateString += updateString != string.Empty ? $", REG_ADRESS = '{newRa}'" : $"REG_ADRESS = '{newRa}'";

                    if (!FactAdress.Equals(newFa))
                        updateString += updateString != string.Empty ? $", FACT_ADRESS = '{newFa}'" : $"FACT_ADRESS = '{newFa}'";

                    if (WorkerCategory.GroupId != newCategory.GroupId)
                        updateString += updateString != string.Empty ? $", ID_WORKERS_GROUP = '{newCategory.GroupId}'" : $"ID_WORKERS_GROUP = '{newCategory.GroupId}'";

                    if (updateString != string.Empty)
                    {
                        using (SqlCommand updateWorker = new SqlCommand($"update [CARWASH].[dbo].[WORKERS] " +
                                                                        $"set {updateString} " +
                                                                        $"where ID_PERSON = {Id}", conn))
                        {
                            updateWorker.ExecuteNonQuery();
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

        public bool DeleteWokrer(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    SqlCommand deleteWorker = new SqlCommand($"update [CARWASH].[dbo].[PERSONS] " +
                                                                $"set VIS = 'false' " +
                                                                $"where ID = {Id}", conn);
                    deleteWorker.ExecuteNonQuery();

                    return true;
                }
                catch 
                {
                    return false;
                }
            }
        }

        public static List<Worker> GetWorkersByGroup(string connStr, int groupId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                List<Worker> workers = new List<Worker>();
                conn.Open();

                SqlCommand findWorkers = new SqlCommand($"select p.ID, p.NAME " +
                                                        $"from [CARWASH].[dbo].[PERSONS] p join [CARWASH].[dbo].[WORKERS] w " +
                                                        $"on p.ID = w.ID_PERSON " +
                                                        $"where p.IS_WORKER = 'true' " +
                                                        $"and VIS = 'true' " +
                                                        $"and w.ID_WORKERS_CATEGORY = {groupId}", conn);

                SqlDataReader dr = findWorkers.ExecuteReader();
                while (dr.Read())
                {
                    workers.Add(new Worker() { Id = dr.GetInt32(0), Name = dr.GetString(1) });
                }
                dr.Close();

                return workers;
            }
        }

        public static List<Worker> GetWorkersByGroupNotBusy(string connStr, int groupId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                List<Worker> workers = new List<Worker>();
                conn.Open();

                SqlCommand findWorkers = new SqlCommand($"select p.ID, p.NAME " +
                                                        $"from [CARWASH].[dbo].[PERSONS] p join [CARWASH].[dbo].[WORKERS] w " +
                                                        $"on p.ID = w.ID_PERSON " +
                                                        $"where p.IS_WORKER = 'true' " +
                                                        $"and VIS = 'true' " +
                                                        $"and w.IS_BUSY = 'false' " +
                                                        $"and w.ID_WORKERS_CATEGORY = {groupId}", conn);

                SqlDataReader dr = findWorkers.ExecuteReader();
                while (dr.Read())
                {
                    workers.Add(new Worker() { Id = dr.GetInt32(0), Name = dr.GetString(1) });
                }
                dr.Close();

                return workers;
            }
        }

        public static List<Worker> GetWorkersInOrder(string connStr, int orderId)
        {
            List<Worker> workers = new List<Worker>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand findWorkers = new SqlCommand($"select ID_PERSON, IS_BUSY from [CARWASH].[dbo].[CARWASH_ORDER_WORKERS] o join [CARWASH].[dbo].[WORKERS] w " +
                                                                $"on o.ID_WORKER = w.ID where o.ID_ORDER = {orderId}", conn))
                {
                    using (SqlDataReader dr = findWorkers.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            workers.Add(new Worker() 
                            { 
                                Id = dr.GetInt32(0),
                                IsBusy = dr.GetBoolean(1)
                            });
                        }
                    }
                }
            }

            return workers;
        }

        public bool SetWorkerBusyState(string connStr, bool busyState)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand setIsBusy = new SqlCommand($"update [CARWASH].[dbo].[WORKERS] " +
                                                                 $"set IS_BUSY = '{busyState}' " +
                                                                 $"where ID_PERSON = {Id}", conn))
                    {
                        setIsBusy.ExecuteNonQuery();
                    }

                    IsBusy = busyState;

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
