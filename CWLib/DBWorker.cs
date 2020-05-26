using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using System.Threading;
using System.Data.SqlClient;

namespace CWLib
{
    public static class DBWorker
    {
        public static bool  IsDBCreated()
        {
            bool isCreated = false;
            return isCreated;
        }

        public static void CreateDB(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand createDB = new SqlCommand(@"CREATE DATABASE CARWASH", conn);
                createDB.ExecuteNonQuery();

                Thread.Sleep(500);

                SqlCommand acOff = new SqlCommand("ALTER DATABASE [CARWASH] SET AUTO_CLOSE OFF", conn);
                acOff.ExecuteNonQuery();
            }
        }

        public static void CreateDBTables(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                ResourceManager rm = new ResourceManager("CWLib.Resources.DBTables", System.Reflection.Assembly.GetExecutingAssembly());
                ResourceSet rs = rm.GetResourceSet(System.Globalization.CultureInfo.CurrentUICulture, true, true);
                foreach (var item in rs)
                {
                    SqlCommand createTable = new SqlCommand(((System.Collections.DictionaryEntry)item).Value.ToString(), conn);
                    createTable.ExecuteNonQuery();
                }
            }
        }

        public static void AddDefaultClientGroups(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand addClientGroup = new SqlCommand($"insert into [CARWASH].[dbo].[CLIENT_GROUPS] (GROUP_NAME, DISCOUNT, VIS)" +
                                                                  $"values ('Все', 0, 'true')", conn)) 
                {
                    addClientGroup.ExecuteNonQuery();
                }
            }
        }

        public static void AddInitialWorkersInfo(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand addAdministrators = new SqlCommand("insert into [CARWASH].[dbo].[WORKERS_CATEGORIES] (GROUP_NAME, VISIBILITY) " +
                                                               "values ('Администраторы', 'true')", conn);
                addAdministrators.ExecuteNonQuery();

                SqlCommand addKass = new SqlCommand("insert into [CARWASH].[dbo].[WORKERS_CATEGORIES] (GROUP_NAME, VISIBILITY) " +
                                                               "values ('Кассиры', 'true')", conn);
                addKass.ExecuteNonQuery();

                SqlCommand addWashers = new SqlCommand("insert into [CARWASH].[dbo].[WORKERS_CATEGORIES] (GROUP_NAME, VISIBILITY) " +
                                                               "values ('Мойщики', 'true')", conn);
                addWashers.ExecuteNonQuery();

                SqlCommand addTyres = new SqlCommand("insert into [CARWASH].[dbo].[WORKERS_CATEGORIES] (GROUP_NAME, VISIBILITY) " +
                                                               "values ('Шиномонтажники', 'true')", conn);
                addTyres.ExecuteNonQuery();

                SqlCommand addDirectors = new SqlCommand("insert into [CARWASH].[dbo].[WORKERS_CATEGORIES] (GROUP_NAME, VISIBILITY) " +
                                                               "values ('Директор', 'true')", conn);
            }
        }

        public static void AddInitialServicesInfo(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand addMainServicesCat = new SqlCommand("insert into [CARWASH].[dbo].[service_categories] (NAME, IS_CARWASH, VISIBILITY) " +
                                                                 "values ('Основные услуги', 'true', 'true')", conn);
                addMainServicesCat.ExecuteNonQuery();

                SqlCommand addAdditionalServicesCat = new SqlCommand("insert into [CARWASH].[dbo].[service_categories] (NAME, IS_CARWASH, VISIBILITY) " +
                                                                 "values ('Доп. услуги', 'true', 'true')", conn);
                addAdditionalServicesCat.ExecuteNonQuery();

                SqlCommand addPolishServicesCat = new SqlCommand("insert into [CARWASH].[dbo].[service_categories] (NAME, IS_CARWASH, VISIBILITY) " +
                                                                 "values ('Полировка', 'true', 'true')", conn);
                addPolishServicesCat.ExecuteNonQuery();

                SqlCommand addChemServicesCat = new SqlCommand("insert into [CARWASH].[dbo].[service_categories] (NAME, IS_CARWASH, VISIBILITY) " +
                                                                 "values ('Хим. чистка', 'true', 'true')", conn);
                addChemServicesCat.ExecuteNonQuery();
            }
        }

        public static void AddPaymentCategories(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand addReal = new SqlCommand("insert into [CARWASH].[dbo].[PAYMENT_CATEGORIES] (NAME, VISIBILITY) " +
                                                    "values ('Наличные', 'true')", conn);
                addReal.ExecuteNonQuery();

                SqlCommand addNReal = new SqlCommand("insert into [CARWASH].[dbo].[PAYMENT_CATEGORIES] (NAME, VISIBILITY) " +
                                                    "values ('Безнал', 'true')", conn);
                addNReal.ExecuteNonQuery();

                SqlCommand addLS = new SqlCommand("insert into [CARWASH].[dbo].[PAYMENT_CATEGORIES] (NAME, VISIBILITY) " +
                                                    "values ('Лицевые счета', 'true')", conn);
            }
        }

        public static bool CheckDBCreation(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand checkDb = new SqlCommand("select count(*) from master.dbo.sysdatabases where name='CARWASH'", conn))
                {
                    conn.Open();
                    if (Convert.ToInt32(checkDb.ExecuteScalar()) > 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        public static bool CheckWorkersAndUsers(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand usersCount = new SqlCommand("select count(*) from [Carwash].[dbo].[USERS]", conn))
                {
                    conn.Open();
                    if (Convert.ToInt32(usersCount.ExecuteScalar()) > 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        private static void AddInitialUser(string name, string login, int workersCatId, string connStr, string slt)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                Protector protector = new Protector(Encoding.Default.GetBytes(slt));

                SqlCommand addPerson = new SqlCommand("insert into [CARWASH].[dbo].[PERSONS] (NAME, IS_ENTITY, IS_WORKER, VIS) " +
                                                                "OUTPUT inserted.ID " +
                                                                "values (@workerName, 'false', 'true', 'true')", conn);

                SqlParameter nameParam = new SqlParameter("@workerName", System.Data.SqlDbType.VarChar);
                nameParam.Value = name;
                addPerson.Parameters.Add(nameParam);

                int? personId = Convert.ToInt32(addPerson.ExecuteScalar());

                SqlCommand addWorker = new SqlCommand("insert into [CARWASH].[dbo].[WORKERS] (ID_PERSON, ID_WORKERS_CATEGORY, IS_ON_WORK, IS_BUSY)" +
                                                            "OUTPUT inserted.ID " +
                                                            "values (@personId, @idWorkersCat, 'true', 'false')", conn);

                SqlParameter personIdParam = new SqlParameter("@personId", System.Data.SqlDbType.Int);
                personIdParam.Value = personId;
                addWorker.Parameters.Add(personIdParam);

                SqlParameter idWCatParam = new SqlParameter("@idWorkersCat", System.Data.SqlDbType.Int);
                idWCatParam.Value = workersCatId;
                addWorker.Parameters.Add(idWCatParam);

                int? workerId = Convert.ToInt32(addWorker.ExecuteScalar());

                SqlCommand addUser = new SqlCommand("insert into [CARWASH].[dbo].[USERS] (ID_WORKER, LOGIN, PASS)" +
                                                          "values (@id, @login, @pass)", conn);

                SqlParameter workerIdp = new SqlParameter("@id", System.Data.SqlDbType.Int);
                workerIdp.Value = workerId;
                addUser.Parameters.Add(workerIdp);

                SqlParameter loginParam = new SqlParameter("@login", System.Data.SqlDbType.VarChar);
                loginParam.Value = login;
                addUser.Parameters.Add(loginParam);

                SqlParameter passParam = new SqlParameter("@pass", System.Data.SqlDbType.VarBinary);
                passParam.Value = protector.CreateHash(Encoding.Default.GetBytes("12345"), Encoding.Default.GetBytes(slt));
                addUser.Parameters.Add(passParam);

                addUser.ExecuteNonQuery();
            }
        }

        public static void AddUsersInitialInfo(string connStr, string entropy)
        {
            int? admCatId;
            int? dirCatId;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand findAdminCatId = new SqlCommand("select ID from [CARWASH].[dbo].[WORKERS_CATEGORIES] where GROUP_NAME='Администраторы'", conn);
                admCatId = Convert.ToInt32(findAdminCatId.ExecuteScalar());

                SqlCommand findDirCatId = new SqlCommand("select ID from [CARWASH].[dbo].[WORKERS_CATEGORIES] where GROUP_NAME='Директор'", conn);
                dirCatId = Convert.ToInt32(findDirCatId.ExecuteScalar());
            }

            AddInitialUser("Администратор", "Administrator", (int)admCatId, connStr, entropy);
            AddInitialUser("Директор", "Director", (int)dirCatId, connStr, entropy);
        }

        public static List<string> FindUsers(string connStr)
        {
            List<string> usersNames = new List<string>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand findUsers = new SqlCommand("select LOGIN from [CARWASH].[dbo].[USERS] ORDER BY LOGIN", conn);
                SqlDataReader dr = findUsers.ExecuteReader();
                while (dr.Read())
                    usersNames.Add(dr.GetString(0));
            }

            return usersNames;
        }

        public static bool Authentication(string connStr, string login, string pass, out int? id, string slt)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                Protector protector = new Protector(Encoding.UTF8.GetBytes(slt));

                SqlCommand enter = new SqlCommand("select ID_WORKER from [CARWASH].[dbo].[USERS] where LOGIN=@login and PASS=@pass", conn);
                
                SqlParameter authLogin = new SqlParameter("@login", System.Data.SqlDbType.VarChar);
                authLogin.Value = login;
                enter.Parameters.Add(authLogin);

                SqlParameter authPass = new SqlParameter("@pass", System.Data.SqlDbType.VarBinary);
                authPass.Value = protector.CreateHash(Encoding.Default.GetBytes(pass), Encoding.Default.GetBytes(slt));
                enter.Parameters.Add(authPass);

                if (enter.ExecuteScalar() != null)
                {
                    id = (int)enter.ExecuteScalar();
                    return true;
                }
                else
                {
                    id = -1;
                    return false;
                }                   
            }
        }

        public static void BoxesSearch(string connStr, out List<Box> boxes)
        {
            boxes = new List<Box>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand findBoxes = new SqlCommand("select * from [CARWASH].[dbo].[BOXES] where VIS='true' order by BOX_NAME", conn))
                {
                    SqlDataReader dr = findBoxes.ExecuteReader();
                    while (dr.Read())
                    {
                        Box box = new Box() { Id = dr.GetInt32(0), 
                                                Name = dr.GetString(1), 
                                                State = dr.GetValue(2) == DBNull.Value ? null : (bool?)dr.GetValue(2),
                                                TechState = dr.GetValue(3) == DBNull.Value ? null : (bool?)dr.GetValue(3), 
                                                OrderId = dr.GetValue(4) == DBNull.Value ? null : (int?)dr.GetValue(4), 
                                                OrderType = dr.GetValue(5) == DBNull.Value ? null : (byte?)dr.GetValue(5) };
                        boxes.Add(box);
                    }
                }
            }
        }

        public static List<CarCategory> CategoriesSearch(string connStr)
        {
            List<CarCategory> cats = new List<CarCategory>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand findCategories = new SqlCommand("select * " +
                                                            "from [CARWASH].[dbo].[CAR_CATEGORIES] " +
                                                            "where VIS = 'true' " +
                                                            "order by NAME", conn);
                SqlDataReader dr = findCategories.ExecuteReader();
                while (dr.Read())
                {
                    CarCategory cat = new CarCategory() { Id = dr.GetInt32(0), Name = dr.GetString(1), Classes = new List<int>() };
                    cats.Add(cat);
                }
                dr.Close();

                if (cats.Count > 0)
                {
                    for (int i = 0; i < cats.Count; i++)
                    {
                        SqlCommand findClasses = new SqlCommand($"select CLASS_ID from [CARWASH].[dbo].[CAR_CLASS_CATEGORY] " +
                                                                $"where CATEGORY_ID = {cats[i].Id}", conn);
                        SqlDataReader dr2 = findClasses.ExecuteReader();
                        while (dr2.Read())
                            cats[i].Classes.Add(dr2.GetInt32(0));

                        dr2.Close();
                    }
                }

                return cats;
            }
        }
        
        public static CarCategory FindCategoryByClassId(int classId, string connStr)
        {
            CarCategory category = new CarCategory();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand findCatName = new SqlCommand($"select CAR_CATEGORIES.ID, NAME " +
                                                        $"from [CARWASH].[dbo].[CAR_CATEGORIES], [CARWASH].[dbo].[CAR_CLASS_CATEGORY] " +
                                                        $"where CAR_CATEGORIES.ID = CAR_CLASS_CATEGORY.CATEGORY_ID and " +
                                                        $"CLASS_ID = {classId} and VIS = 'true'", conn);

                SqlDataReader dr = findCatName.ExecuteReader();
                while (dr.Read())
                {
                    category.Id = dr.GetValue(0) != DBNull.Value ? dr.GetInt32(0) : 0;
                    category.Name = dr.GetString(1);
                }
            }

            if (category.Id == 0)
                category = null;

            return category;
        }

        public static bool AddClientGroup(string name, int discount, string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                try
                {
                    SqlCommand addClientGroup = new SqlCommand($"insert into [CARWASH].[dbo].[CLIENT_GROUPS] (GROUP_NAME, DISCOUNT, VIS) " +
                                                                $"values ('{name}', {discount}, 'true')", conn);
                    addClientGroup.ExecuteNonQuery();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static List<ClientGroup> GroupsSearch(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                List<ClientGroup> groups = new List<ClientGroup>();

                SqlCommand findGroups = new SqlCommand("select * from [CARWASH].[dbo].[CLIENT_GROUPS] " +
                                                        "where VIS='true'", conn);
                SqlDataReader dr = findGroups.ExecuteReader();
                while (dr.Read())
                {
                    ClientGroup group = new ClientGroup() { Id = dr.GetInt32(0),
                                                            Name = dr.GetString(1),
                                                            Discount = dr.GetInt32(2),
                                                            Visibility = dr.GetBoolean(3) };
                    groups.Add(group);
                }
                dr.Close();

                return groups;
            }
        } 
        
        public static List<ClientNameForSearchList> ClientsSearchByName(string connStr, bool withEntities)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                List<ClientNameForSearchList> clients = new List<ClientNameForSearchList>();

                SqlCommand findClientsByName = null;
                if (withEntities)
                    findClientsByName = new SqlCommand($"select ID, NAME from [CARWASH].[dbo].[PERSONS] " +
                                                       $"where IS_WORKER = 'false' " +
                                                       $"and VIS = 'true' " +
                                                       $"order by NAME", conn);
                else
                    findClientsByName = new SqlCommand($"select ID, NAME from [CARWASH].[dbo].[PERSONS] " +
                                                       $"where IS_ENTITY='{withEntities}' " +
                                                       $"and IS_WORKER = 'false' " +
                                                       $"and VIS = 'true' " +
                                                       $"order by NAME", conn);

                SqlDataReader dr = findClientsByName.ExecuteReader();
                while (dr.Read())
                {
                    clients.Add(new ClientNameForSearchList() { Id = dr.GetInt32(0), Name = dr.GetString(1) });
                }
                dr.Close();

                return clients;
            }
        }

        public static List<ClientPlateForSearchList> ClientsSearchByPlate(string connStr, bool withEntities)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                List<ClientPlateForSearchList> clients = new List<ClientPlateForSearchList>();

                SqlCommand findClientsByPlate = null;
                if (withEntities)
                    findClientsByPlate = new SqlCommand($"select ID_PERSON, PLATE, ID_CAR_MODEL " +
                                                        $"from [CARWASH].[dbo].[PERSONS], [CARWASH].[dbo].[CLIENT_CARS] " +
                                                        $"where PERSONS.ID = CLIENT_CARS.ID_PERSON " +
                                                        $"and IS_WORKER = 'false' " +
                                                        $"and VIS = 'true' " +
                                                        $"order by PLATE", conn);
                else
                    findClientsByPlate = new SqlCommand($"select ID_PERSON, PLATE, ID_CAR_MODEL " +
                                                        $"from [CARWASH].[dbo].[PERSONS], [CARWASH].[dbo].[CLIENT_CARS] " +
                                                        $"where PERSONS.ID = CLIENT_CARS.ID_PERSON " +
                                                        $"and IS_ENTITY = 'false' " +
                                                        $"and IS_WORKER = 'false' " +
                                                        $"and VIS = 'true' " +
                                                        $"order by PLATE", conn);

                SqlDataReader dr = findClientsByPlate.ExecuteReader();
                while (dr.Read())
                {
                    clients.Add(new ClientPlateForSearchList() { Id = dr.GetInt32(0), Plate = dr.GetString(1), ModelId = dr.GetInt32(2) });
                }
                dr.Close();

                return clients;
            }
        }

        public static bool EntityClientCheck(string connStr, int personId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand isEntity = new SqlCommand($"select IS_ENTITY " +
                                                     $"from [CARWASH].[dbo].[PERSONS] " +
                                                     $"where ID = '{personId}' " +
                                                     $"and IS_WORKER = 'false'", conn);
                if ((bool)isEntity.ExecuteScalar())
                    return true;
                else
                    return false;
            }
        }

        public static List<WorkersCategory> WorkersCategorySearch(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                List<WorkersCategory> categories = new List<WorkersCategory>();

                SqlCommand findCategories = new SqlCommand("select ID, GROUP_NAME " +
                                                            "from [CARWASH].[dbo].[WORKERS_CATEGORIES] " +
                                                            "where VISIBILITY = 'true'", conn);

                SqlDataReader dr = findCategories.ExecuteReader();
                while (dr.Read())
                {
                    categories.Add(new WorkersCategory() { GroupId = dr.GetInt32(0), GroupName = dr.GetString(1) });
                }
                dr.Close();

                return categories;
            }
        }
    }
}
