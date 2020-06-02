using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CWLib.Interfaces;

namespace CWLib
{
    /* Клиенты юр.лица */
    public class EntityClient : ClientBase, IClient
    {
        public int? ClientId { get; set; }
        public string FullName { get; set; }
        public string CityPhone { get; set; }
        public string FactAdress { get; set; }
        public string LegalAdress { get; set; }
        public string Inn { get; set; }
        public string Ogrn { get; set; }
        public string PaymentAccount { get; set; } //расчетный счет
        public string CorrAccount { get; set; } //корр. счет
        public string Bik { get; set; }
        public string ChiefAccountant { get; set; } //главный бухгалтер
        public string GeneralManager { get; set; } //генеральный директор
        public string ContactPerson { get; set; } //контактное лицо
        public string ContactPersonPhone { get; set; } //телефон контактного лица        

        public bool AddEntityClient(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                try
                {
                    SqlCommand addPerson = new SqlCommand($"insert into [CARWASH].[dbo].[PERSONS] (NAME, PHONE, EMAIL, COMMENT, IS_ENTITY, IS_WORKER, VIS) " +
                                                                              $"output inserted.ID " +
                                                                              $"values ('{Name}', '{Phone}', NULL, NULL, 'true', 'false', 'true')", conn);

                    Id = (int)addPerson.ExecuteScalar();

                    SqlCommand addPersonAsClient = new SqlCommand($"insert into [CARWASH].[dbo].[ENTITY_CLIENTS] (ID_PERSON, FULL_NAME, CITYPHONE, FACT_ADRESS, LEGAL_ADRESS, INN, KPP, OGRN, PAYMENT_ACCOUNT, CORR_ACCOUNT, BIK, CHIEF_ACCOUNTANT, GENERAL_MANAGER, CONTACT_PERSON, CONTACT_PERSON_PHONE, ADD_DATE) " +
                                                                      $"output inserted.ID " +
                                                                      $"values ({Id}, '{Name}', '{Phone}', '{FactAdress}', '{LegalAdress}', '{Inn}', null, '{Ogrn}', '{PaymentAccount}', '{CorrAccount}', '{Bik}', '{ChiefAccountant}', '{GeneralManager}', '{ContactPerson}', '{ContactPersonPhone}', @date)", conn);
                    
                    SqlParameter date = new SqlParameter(@"date", System.Data.SqlDbType.DateTime);
                    date.Value = DateAdd;
                    addPersonAsClient.Parameters.Add(date);

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

                using (SqlCommand findClientCarId = new SqlCommand($"select ID from [CARWASH].[dbo].[CLIENT_CAR] " +
                                                                    $"where PLATE = '{licPlate}' and ID_CAR_MODEL = {idModel}", conn))
                {
                    return (int)findClientCarId.ExecuteScalar();
                }
            }
        }

        public bool UpdateEntityClient(string connStr, List<ClientCar> newCars, string newName = "NULL", string newPhone = "NULL", 
                                        string newfactAdress = "NULL", string newLegalAdress = "NULL", string newInn = "NULL", string newOgrn = "NULL", string newPaymentAcc = "NULL", string newBik = "NULL", 
                                        string newAccountant = "NULL", string newGeneralManager = "NULL", string newContPerson = "NULL", string newCPPhone = "NULL")
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

                    if (updateString != string.Empty)
                    {
                        using (SqlCommand updatePerson = new SqlCommand($"update [CARWASH].[dbo].[PERSONS] " +
                                                                    $"set {updateString} " +
                                                                    $"where ID = {Id}", conn))
                            updatePerson.ExecuteNonQuery();
                    }

                    /* entity client */

                    updateString = string.Empty;

                    if (!Name.Equals(newName))
                        updateString += $" FULL_NAME = '{newName}'";

                    if (!Phone.Equals(newPhone))
                        updateString += updateString != string.Empty ? $", CITYPHONE = '{newPhone}'" : $"CITYPHONE = '{newPhone}'";

                    if (!FactAdress.Equals(newfactAdress))
                        updateString += updateString != string.Empty ? $", FACT_ADRESS = '{newfactAdress}'" : $"FACT_ADRESS = '{newfactAdress}'";

                    if (!LegalAdress.Equals(newLegalAdress))
                        updateString += updateString != string.Empty ? $", LEGAL_ADRESS = '{newLegalAdress}'" : $"LEGAL_ADRESS = '{newLegalAdress}'";

                    if (!Inn.Equals(newInn))
                        updateString += updateString != string.Empty ? $", INN = '{newInn}'" : $"INN = '{newInn}'";

                    if (!Ogrn.Equals(newOgrn))
                        updateString += updateString != string.Empty ? $", OGRN = '{newOgrn}'" : $"OGRN = '{newOgrn}'";

                    if (!PaymentAccount.Equals(newPaymentAcc))
                        updateString += updateString != string.Empty ? $", PAYMENT_ACCOUNT = '{newPaymentAcc}'" : $"PAYMENT_ACCOUNT = '{newPaymentAcc}'";

                    if (!Bik.Equals(newBik))
                        updateString += updateString != string.Empty ? $", BIK = '{newBik}'" : $"BIK = '{newBik}'";

                    if (!ChiefAccountant.Equals(newAccountant))
                        updateString += updateString != string.Empty ? $", CHIEF_ACCOUNTANT = '{newAccountant}'" : $"CHIEF_ACCOUNTANT = '{newAccountant}'";

                    if (!GeneralManager.Equals(newGeneralManager))
                        updateString += updateString != string.Empty ? $", GENERAL_MANAGER = '{newGeneralManager}'" : $"GENERAL_MANAGER = '{newGeneralManager}'";

                    if (!ContactPerson.Equals(newContPerson))
                        updateString += updateString != string.Empty ? $", CONTACT_PERSON = '{newContPerson}'" : $"CONTACT_PERSON = '{newContPerson}'";

                    if (!ContactPersonPhone.Equals(newCPPhone))
                        updateString += updateString != string.Empty ? $", CONTACT_PERSON_PHONE = '{newCPPhone}'" : $"CONTACT_PERSON_PHONE = '{newCPPhone}'";

                    if (updateString != string.Empty)
                    {
                        using (SqlCommand updateClient = new SqlCommand($"update [CARWASH].[dbo].[ENTITY_CLIENTS] " +
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

        public void GetClientInfoById(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand findClientInfo = new SqlCommand($"select p.NAME, p.PHONE, p.EMAIL, c.LEGAL_ADRESS, " +
                                                            $"c.FACT_ADRESS, c.INN, c.OGRN, c.PAYMENT_ACCOUNT, c.CORR_ACCOUNT, c.BIK, " +
                                                            $"c.GENERAL_MANAGER, c.CHIEF_ACCOUNTANT, c.CONTACT_PERSON, c.CONTACT_PERSON_PHONE, c.ADD_DATE " +
                                                            $"from [CARWASH].[dbo].[PERSONS] p join [CARWASH].[dbo].[ENTITY_CLIENTS] c " +
                                                            $"on p.ID = c.ID_PERSON " +
                                                            $"where p.ID = {Id}", conn);

                SqlDataReader dr = findClientInfo.ExecuteReader();
                while (dr.Read())
                {
                    Name = dr.GetString(0);
                    Phone = dr.GetValue(1) != DBNull.Value ? dr.GetString(1) : null;
                    Email = dr.GetValue(2) != DBNull.Value ? dr.GetString(2) : null;
                    LegalAdress = dr.GetValue(3) != DBNull.Value ? dr.GetString(3) : null;
                    FactAdress = dr.GetValue(4) != DBNull.Value ? dr.GetString(4) : null;
                    Inn = dr.GetValue(5) != DBNull.Value ? dr.GetString(5) : null;
                    Ogrn = dr.GetValue(6) != DBNull.Value ? dr.GetString(6) : null;
                    PaymentAccount = dr.GetValue(7) != DBNull.Value ? dr.GetString(7) : null;
                    CorrAccount = dr.GetValue(8) != DBNull.Value ? dr.GetString(8) : null;
                    Bik = dr.GetValue(9) != DBNull.Value ? dr.GetString(9) : null;
                    GeneralManager = dr.GetValue(10) != DBNull.Value ? dr.GetString(10) : null;
                    ChiefAccountant = dr.GetValue(11) != DBNull.Value ? dr.GetString(11) : null;
                    ContactPerson = dr.GetValue(12) != DBNull.Value ? dr.GetString(12) : null;
                    ContactPersonPhone = dr.GetValue(12) != DBNull.Value ? dr.GetString(13) : null;
                    DateAdd = dr.GetValue(14) != DBNull.Value ? dr.GetDateTime(14) : DateTime.MinValue;
                    Cars = new List<ClientCar>();
                }
                dr.Close();

                SqlCommand findCars = new SqlCommand($"select * from [CARWASH].[dbo].[CLIENT_CARS] " +
                                                        $"where ID_PERSON = {Id}", conn);

                SqlDataReader dr2 = findCars.ExecuteReader();
                while (dr2.Read())
                {
                    Cars.Add(new ClientCar() { Car = new CarModel() { Id = dr2.GetInt32(2) }, LicencePlate = dr2.GetString(3), CarwashNumber = dr2.GetInt32(4), TyreserviceNumber = dr2.GetInt32(5) });
                }
                dr2.Close();

                for (int i = 0; i < Cars.Count; i++)
                {
                    Cars[i].Car = XMLWorker.SearchModelById((int)Cars[i].Car.Id);
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
    }
}
