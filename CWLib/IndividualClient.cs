using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CWLib.Interfaces;

namespace CWLib
{
    /* Клиенты физ.лица */
    public class IndividualClient : ClientBase, IClient
    {
        public int? ClientId { get; set; }
        public DateTime? BirthDate { get; set; }

        public bool AddIndividualClient(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                try
                {
                    SqlCommand addPerson = new SqlCommand($"insert into [CARWASH].[dbo].[PERSONS] (NAME, PHONE, EMAIL, COMMENT, IS_ENTITY, IS_WORKER, VIS) " +
                                                                  $"output inserted.ID " +
                                                                  $"values ('{Name}', '{Phone}', '{Email}', '{Comment}', 'false', 'false', 'true')", conn);

                    Id = (int)addPerson.ExecuteScalar();

                    string bdateStr = BirthDate != null ? $"'" + ((DateTime)BirthDate).ToString("yyyy-MM-ddThh:mm:ss") + "'" : "NULL";

                    SqlCommand addPersonAsClient = new SqlCommand($"insert into [CARWASH].[dbo].[INDIVIDUAL_CLIENTS] (ID_PERSON, ID_GROUP, BIRTHDATE, ADD_DATE) " +
                                                                  $"output inserted.ID " +
                                                                  $"values ({Id}, {Group.Id}, {bdateStr}, '{DateAdd.ToString("yyyy-MM-ddThh:mm:ss")}')", conn);

                    ClientId = (int?)addPersonAsClient.ExecuteScalar();

                    if (Cars != null && Cars.Count > 0)
                    {
                        for (int i = 0; i < Cars.Count; i++)
                        {
                            SqlCommand addClientCar = new SqlCommand($"insert into [CARWASH].[dbo].[CLIENT_CARS] (ID_PERSON, ID_CAR_MODEL, PLATE, CARWASH_NUM, TYRESERVICE_NUM) " +
                                                                     $"values ({Id}, {Cars[i].Car.Id}, '{Cars[i].LicencePlate}', 0, 0)", conn);
                            addClientCar.ExecuteNonQuery();
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

        public override int GetOrderClientsCar(string connStr, string licPlate, int idModel)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand findClientCarId = new SqlCommand($"select ID from [CARWASH].[dbo].[CLIENT_CARS] " +
                                                                    $"where PLATE = '{licPlate}' and ID_CAR_MODEL = {idModel}", conn))
                {
                    return (int)findClientCarId.ExecuteScalar();
                }
            }
        }

        public bool UpdateIndividualClient(string connStr, ClientGroup newGroup, List<ClientCar> newCars, string newName = "NULL", string newPhone = "NULL", string newEmail = "NULL", DateTime? newBirthdate = null)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string updateString = string.Empty;

                try
                {
                    /* persons */

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
                            updatePerson.ExecuteNonQuery();
                    }

                    /* individual client */

                    updateString = string.Empty;

                    if (Group.Id != newGroup.Id && newGroup != null)
                        updateString += $" ID_GROUP = '{newGroup.Id}'";

                    if (newBirthdate != null)
                        if (BirthDate != newBirthdate)
                            updateString += updateString != string.Empty ? $", BIRTHDATE = '{((DateTime)newBirthdate).ToString("yyyy-MM-ddThh:mm:ss")}'" : $"BIRTHDATE = '{((DateTime)newBirthdate).ToString("yyyy-MM-ddThh:mm:ss")}'";
                        else
                            updateString += updateString != string.Empty ? $", BIRTHDATE = NULL" : $"BIRTHDATE = NULL";

                    if (updateString != string.Empty)
                    {
                        using (SqlCommand updateClient = new SqlCommand($"update [CARWASH].[dbo].[INDIVIDUAL_CLIENTS] " +
                                                                        $"set {updateString} " +
                                                                        $"where ID_PERSON = {Id}", conn))
                            updateClient.ExecuteNonQuery();
                    }

                    /* cars */

                    List<int> oldCarsIds = Cars.Select(x => (int)x.Car.Id).ToList();
                    List<int> newCarsIds = newCars.Select(x => (int)x.Car.Id).ToList();

                    if (!oldCarsIds.SequenceEqual(newCarsIds))                    
                        ClientCar.UpdateClientsCars(connStr, (int)Id, newCars);                    

                    return true;
                }

                catch
                {
                    return false;
                }
            }
        }

        public bool DeleteClient(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                try
                {
                    SqlCommand deleteClient = new SqlCommand($"update [CARWASH].[dbo].[PERSONS] " +
                                                                        $"set [VIS] = 'false' " +
                                                                        $"where [ID] = {Id}", conn);
                    deleteClient.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public void GetClientInfoById(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand findClientInfo = new SqlCommand($"select PERSONS.NAME, PERSONS.PHONE, PERSONS.EMAIL, PERSONS.COMMENT, INDIVIDUAL_CLIENTS.ID_GROUP, CLIENT_GROUPS.GROUP_NAME, " +
                                                            $"INDIVIDUAL_CLIENTS.BIRTHDATE, INDIVIDUAL_CLIENTS.ADD_DATE " +
                                                            $"from [CARWASH].[dbo].[PERSONS],[CARWASH].[dbo].[INDIVIDUAL_CLIENTS],[CARWASH].[dbo].[CLIENT_GROUPS] " +
                                                            $"where persons.ID = INDIVIDUAL_CLIENTS.ID_PERSON " +
                                                            $"and INDIVIDUAL_CLIENTS.ID_GROUP = CLIENT_GROUPS.ID " +
                                                            $"and PERSONS.ID = {this.Id}", conn);

                SqlDataReader dr = findClientInfo.ExecuteReader();
                while (dr.Read())
                {
                    Name = dr.GetString(0);
                    Phone = dr.GetValue(1) != DBNull.Value ? dr.GetString(1) : null;
                    Email = dr.GetValue(2) != DBNull.Value ? dr.GetString(2) : null;
                    Comment = dr.GetValue(3) != DBNull.Value ? dr.GetString(3) : null;
                    Group = new ClientGroup() { Id = dr.GetInt32(4), Name = dr.GetString(5) };
                    BirthDate = dr.GetValue(6) != DBNull.Value ? dr.GetDateTime(6) : DateTime.MinValue;
                    DateAdd = dr.GetValue(7) != DBNull.Value ? dr.GetDateTime(7) : DateTime.MinValue;
                    Cars = new List<ClientCar>();
                }
                dr.Close();

                SqlCommand findCars = new SqlCommand($"select * from [CARWASH].[dbo].[CLIENT_CARS] " +
                                                        $"where ID_PERSON = {Id}", conn);
                
                SqlDataReader dr2 = findCars.ExecuteReader();
                while (dr2.Read())
                {
                    Cars.Add(new ClientCar() { Car = new CarModel() { Id = dr2.GetInt32(2) }, LicencePlate = dr2.GetString(3),  CarwashNumber = dr2.GetInt32(4), TyreserviceNumber = dr2.GetInt32(5) });
                }
                dr2.Close();

                for (int i=0; i<Cars.Count; i++)
                {
                    Cars[i].Car = XMLWorker.SearchModelById((int)Cars[i].Car.Id);
                }
            }
        }
    }
}
