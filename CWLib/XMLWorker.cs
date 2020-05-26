using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CWLib
{
    public static class XMLWorker
    {
        public static List<CarClass> ReadClasses()
        {
            List<CarClass> classes = new List<CarClass>();

            XmlDocument classesFile = new XmlDocument();
            classesFile.LoadXml(Resources.XMLResources.CarClasses);

            foreach (XmlNode node in classesFile)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    classes.Add(new CarClass()
                    {
                        Id = Convert.ToInt32(node.ChildNodes[i].Attributes["ClassId"].Value),
                        Name = node.ChildNodes[i].Attributes["ClassName"].Value
                    });
                }
            }

            return classes;
        }

        public static CarClass ClassSearch(int id)
        {
            XmlDocument classesFile = new XmlDocument();
            classesFile.LoadXml(Resources.XMLResources.CarClasses);

            CarClass cls = null;

            foreach (XmlNode node in classesFile)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    if (Convert.ToInt32(node.ChildNodes[i].Attributes["ClassId"].Value) == id)
                        cls = new CarClass() 
                        { 
                            Id = Convert.ToInt32(node.ChildNodes[i].Attributes["ClassId"].Value),
                            Name = node.ChildNodes[i].Attributes["ClassName"].Value
                        };
                }
            }

            return cls;
        }

        public static List<CarBrand> ReadBrands()
        {
            List<CarBrand> brands = new List<CarBrand>();

            XmlDocument brandsFile = new XmlDocument();
            brandsFile.LoadXml(Resources.XMLResources.CarBrands);

            foreach (XmlNode node in brandsFile)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    brands.Add(new CarBrand()
                    {
                        Id = Convert.ToInt32(node.ChildNodes[i].Attributes["BrandId"].Value),
                        Name = node.ChildNodes[i].Attributes["Name"].Value,
                        IsFast = false,
                        Models = new List<CarModel>()
                    }
                        );
                }
            }

            brands = brands.OrderBy(x=>x.Name).ToList();

            return brands;
        }

        public static List<CarModel> ReadAndSearchModels(CarBrand brand)
        {
            List<CarModel> models = new List<CarModel>();

            XmlDocument modelsFile = new XmlDocument();
            modelsFile.LoadXml(Resources.XMLResources.CarModels);

            foreach (XmlNode node in modelsFile)
            {
                for (int i=0; i<node.ChildNodes.Count; i++)
                {
                    if (Convert.ToInt32(node.ChildNodes[i].Attributes["BrandId"].Value) == brand.Id)
                    {
                        models.Add(new CarModel()
                        {
                            Id = Convert.ToInt32(node.ChildNodes[i].Attributes["Id"].Value),
                            Name = node.ChildNodes[i].Attributes["Name"].Value,
                            Class = new CarClass() { Id = Convert.ToInt32(node.ChildNodes[i].Attributes["ClassId"].Value),
                                                     Name = node.ChildNodes[i].Attributes["ClassName"].Value },
                            Brand = brand,
                            Description = node.ChildNodes[i].Attributes["Descr"].Value
                        });
                    }
                }
            }

            models = models.OrderBy(x => x.Name).ToList();

            return models;
        }

        public static CarModel SearchModelById(int modelId)
        {
            XmlDocument modelsFile = new XmlDocument();
            modelsFile.LoadXml(Resources.XMLResources.CarModels);

            CarModel car = null;

            foreach (XmlNode node in modelsFile)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    if (Convert.ToInt32(node.ChildNodes[i].Attributes["Id"].Value) == modelId)
                    {
                        car = new CarModel()
                        {
                            Id = modelId,
                            Name = node.ChildNodes[i].Attributes["Name"].Value,
                            Class = new CarClass()
                            {
                                Id = Convert.ToInt32(node.ChildNodes[i].Attributes["ClassId"].Value),
                                Name = node.ChildNodes[i].Attributes["ClassName"].Value
                            },
                            Brand = new CarBrand() { Id = Convert.ToInt32(node.ChildNodes[i].Attributes["BrandId"].Value) },
                            Description = node.ChildNodes[i].Attributes["Descr"].Value
                        };
                    }
                }
            }

            XmlDocument brandsFile = new XmlDocument();
            brandsFile.LoadXml(Resources.XMLResources.CarBrands);

            foreach(XmlNode node in brandsFile)
            {
                for (int i=0; i<node.ChildNodes.Count; i++)
                {
                    if (Convert.ToInt32(node.ChildNodes[i].Attributes["BrandId"].Value) == car.Brand.Id)
                    {
                        car.Brand.Name = node.ChildNodes[i].Attributes["Name"].Value;
                    }
                }
            }

            return car;
        }

        public static bool AddBrand(string brandName)
        {
            try
            {
                XmlDocument brandsFile = new XmlDocument();
                brandsFile.LoadXml(Resources.XMLResources.CarBrands);

                XmlNode root = brandsFile.SelectSingleNode("CarBrands");
                XmlNode newBrand = brandsFile.CreateNode(XmlNodeType.Element, "Carmake", null);
                XmlAttribute id = brandsFile.CreateAttribute("BrandId");
                id.Value = "11111";

                XmlAttribute name = brandsFile.CreateAttribute("Name");
                name.Value = brandName;

                newBrand.Attributes.Append(id);
                newBrand.Attributes.Append(name);

                root.AppendChild(newBrand);
                brandsFile.Save(Resources.XMLResources.CarBrands);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
