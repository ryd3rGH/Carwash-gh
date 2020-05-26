using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace CWLib
{
    /* Автомобиль клиента */
    public class ClientCar : ICloneable
    {
        public CarModel Car { get; set; }
        public int? PersonId { get; set; }
        public string LicencePlate { get; set; }
        public int CarwashNumber { get; set; } //кол-во моек данного авто
        public int TyreserviceNumber { get; set; } //кол-во раз, когда данный авто был на шиномонтаже
    
        public static List<ClientCar> GetCars(string connStr)
        {
            List<ClientCar> cars = new List<ClientCar>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand findCars = new SqlCommand("select c.ID_PERSON, c.ID_CAR_MODEL, c.PLATE, c.CARWASH_NUM, c.TYRESERVICE_NUM "+
                                                    "from [CARWASH].[dbo].[PERSONS] p join [CARWASH].[dbo].[CLIENT_CARS] c "+
                                                    "on p.ID = c.ID_PERSON "+
                                                    "where p.VIS = 'true'", conn);

                using (SqlDataReader dr = findCars.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cars.Add(new ClientCar() 
                        { 
                            Car = new CarModel() { Id = dr.GetInt32(1) },
                            PersonId = dr.GetInt32(0),
                            LicencePlate = dr.GetString(2),
                            CarwashNumber = dr.GetInt32(3),
                            TyreserviceNumber = dr.GetInt32(4)
                        });
                    }
                }

                return cars;
            }
        }

        public static ClientCar GetCarByIdClientCar(string connStr, int clientCarId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                int idModel = 0;
                ClientCar car = new ClientCar();

                using (SqlCommand findCar = new SqlCommand($"select * from [CARWASH].[dbo].[CLIENT_CARS] " +
                                                            $"where ID = {clientCarId}", conn))
                {
                    using (SqlDataReader dr = findCar.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            car.PersonId = dr.GetInt32(1);
                            idModel = dr.GetInt32(2);
                            car.LicencePlate = dr.GetString(3);
                            car.CarwashNumber = dr.GetInt32(4);
                            car.TyreserviceNumber = dr.GetInt32(5);
                        }
                    }
                }

                car.Car = XMLWorker.SearchModelById(idModel);

                return car;
            }
        }

        public static bool AddCars(string connStr, int personId, List<ClientCar> cars)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                try
                {
                    if (cars.Count > 0)
                    {
                        for (int i = 0; i < cars.Count; i++)
                        {
                            SqlCommand addClientCar = new SqlCommand($"insert into [CARWASH].[dbo].[CLIENT_CARS] (ID_PERSON, ID_CAR_MODEL, PLATE, CARWASH_NUM, TYRESERVICE_NUM) " +
                                                                     $"values ({personId}, {cars[i].Car.Id}, '{cars[i].LicencePlate}', 0, 0)", conn);
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

        public static bool UpdateClientsCars(string connStr, int personId, List<ClientCar> newCars)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                try
                {
                    using (SqlCommand delOldCars = new SqlCommand($"delete from [CARWASH].[dbo].[CLIENT_CARS] " +
                                                                            $"where ID_PERSON = {personId}", conn))
                    {
                        delOldCars.ExecuteNonQuery();
                    }

                    AddCars(connStr, personId, newCars);

                    return true;
                }
                catch 
                {
                    return false;
                }
            }
        }

        public object Clone()
        {
            return new ClientCar() { Car = this.Car, CarwashNumber = this.CarwashNumber, LicencePlate = this.LicencePlate, PersonId = this.PersonId, TyreserviceNumber = this.TyreserviceNumber };
        }

        public override string ToString()
        {
            return LicencePlate;
        }
    }
}
