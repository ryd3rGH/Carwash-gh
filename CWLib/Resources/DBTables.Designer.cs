﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CWLib.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class DBTables {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DBTables() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CWLib.Resources.DBTables", typeof(DBTables).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE BOXES (ID INT IDENTITY(1,1) PRIMARY KEY, BOX_NAME VARCHAR (50) NOT NULL, STATE BIT NOT NULL, TECH_STATE BIT NOT NULL, ID_ORDER INT NULL, ORDER_TYPE INT NULL, VIS BIT NOT NULL).
        /// </summary>
        internal static string BoxesTable {
            get {
                return ResourceManager.GetString("BoxesTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE CAR_CATEGORIES (ID INT IDENTITY(1,1) PRIMARY KEY, NAME VARCHAR (50) NOT NULL, VIS BIT NOT NULL).
        /// </summary>
        internal static string CarCategoriesTable {
            get {
                return ResourceManager.GetString("CarCategoriesTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE CAR_CLASS_CATEGORY (ID INT IDENTITY(1,1) PRIMARY KEY, CATEGORY_ID INT NOT NULL, CLASS_ID INT NOT NULL).
        /// </summary>
        internal static string CarClassCategories {
            get {
                return ResourceManager.GetString("CarClassCategories", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE CARWASH_ORDER_SERVICES (ID INT IDENTITY(1,1) PRIMARY KEY, ID_ORDER INT NOT NULL, ID_SERVICE INT NOT NULL, CUSTOM_CATEGORIES BIT NOT NULL, ID_CLASS INT NULL, ID_CATEGORY INT NULL, COUNT INT NOT NULL, PRICE DECIMAL NOT NULL, VIS BIT NOT NULL).
        /// </summary>
        internal static string CarwashOrderServicesTable {
            get {
                return ResourceManager.GetString("CarwashOrderServicesTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE CARWASH_ORDERS (ID INT IDENTITY(1,1) PRIMARY KEY, ID_BOX INT NOT NULL, ID_PERSON INT NOT NULL, ID_PAYMENT_TYPE INT NOT NULL, ID_CLIENTS_CAR INT NOT NULL, START_TIME DATETIME NOT NULL, EST_END_TIME DATETIME NOT NULL, END_TIME DATETIME NULL, SUM DECIMAL NOT NULL, COMMENT VARCHAR(MAX) NULL, VIS BIT NOT NULL, IS_DELETED BIT NOT NULL, ID_ADMIN INT NOT NULL).
        /// </summary>
        internal static string CarwashOrdersTable {
            get {
                return ResourceManager.GetString("CarwashOrdersTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE CARWASH_ORDER_WORKERS (ID INT IDENTITY(1,1) PRIMARY KEY, ID_ORDER INT NOT NULL, ID_WORKER INT NOT NULL, VIS BIT NOT NULL).
        /// </summary>
        internal static string CarwashOrderWorkersTable {
            get {
                return ResourceManager.GetString("CarwashOrderWorkersTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE CSERVICE_CATEGORY_PRICE (ID INT IDENTITY(1,1) PRIMARY KEY, ID_SERVICE INT NOT NULL, ID_CLASS INT NOT NULL, PRICE DECIMAL NOT NULL).
        /// </summary>
        internal static string CarwashServicesPricesCategoriesTable {
            get {
                return ResourceManager.GetString("CarwashServicesPricesCategoriesTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE CSERVICE_CLASS_PRICE (ID INT IDENTITY(1,1) PRIMARY KEY, ID_SERVICE INT NOT NULL, ID_CLASS INT NOT NULL, PRICE DECIMAL NOT NULL).
        /// </summary>
        internal static string CarwashServicesPricesClassesTable {
            get {
                return ResourceManager.GetString("CarwashServicesPricesClassesTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE CARWASH_SERVICES (ID INT IDENTITY(1,1) PRIMARY KEY, NAME VARCHAR (50) NOT NULL, ID_TYPE INT NOT NULL, DESCR VARCHAR(MAX) NULL, DURATION INT NOT NULL, VIS BIT NOT NULL).
        /// </summary>
        internal static string CarwashServicesTable {
            get {
                return ResourceManager.GetString("CarwashServicesTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE CASHBOX_OPERATIONS (ID INT IDENTITY(1,1) PRIMARY KEY, ID_ORDER INT NULL, SUM DECIMAL NOT NULL, OPERATION_DATETIME DATETIME NOT NULL, ID_MONEY_TYPE INT NOT NULL, DESCR VARCHAR (MAX) NOT NULL, IS_DELETED BIT NOT NULL, IS_CONSUMPTION BIT NOT NULL).
        /// </summary>
        internal static string CashboxOperationsTable {
            get {
                return ResourceManager.GetString("CashboxOperationsTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE CLIENT_CARS (ID INT IDENTITY(1,1) PRIMARY KEY, ID_PERSON INT NOT NULL, ID_CAR_MODEL INT NOT NULL, PLATE VARCHAR(50) NOT NULL, CARWASH_NUM INT NOT NULL, TYRESERVICE_NUM INT NOT NULL).
        /// </summary>
        internal static string ClientCarsTable {
            get {
                return ResourceManager.GetString("ClientCarsTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE CLIENT_GROUPS (ID INT IDENTITY(1,1) PRIMARY KEY, GROUP_NAME VARCHAR (50) NOT NULL, DISCOUNT INT, VIS BIT NOT NULL).
        /// </summary>
        internal static string ClientGroupsTable {
            get {
                return ResourceManager.GetString("ClientGroupsTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE ENTITY_CLIENTS (ID INT IDENTITY(1,1) PRIMARY KEY, ID_PERSON INT NOT NULL, FULL_NAME VARCHAR (100), CITYPHONE VARCHAR (15), FACT_ADRESS VARCHAR (200), LEGAL_ADRESS VARCHAR (200), INN VARCHAR (15), KPP VARCHAR (15), OGRN VARCHAR (15), PAYMENT_ACCOUNT VARCHAR (20), CORR_ACCOUNT VARCHAR (20), BIK VARCHAR (50), CHIEF_ACCOUNTANT VARCHAR (100), GENERAL_MANAGER VARCHAR (100), CONTACT_PERSON VARCHAR (100), CONTACT_PERSON_PHONE VARCHAR (15), ADD_DATE DATETIME NOT NULL).
        /// </summary>
        internal static string EntityClientsTable {
            get {
                return ResourceManager.GetString("EntityClientsTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE INDIVIDUAL_CLIENTS (ID INT IDENTITY(1,1) PRIMARY KEY, ID_PERSON INT NOT NULL, ID_GROUP INT NOT NULL, BIRTHDATE DATETIME NULL, ADD_DATE DATETIME NOT NULL).
        /// </summary>
        internal static string IndividualClientsTable {
            get {
                return ResourceManager.GetString("IndividualClientsTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE PAYMENT_CATEGORIES (ID INT IDENTITY(1,1) PRIMARY KEY, NAME VARCHAR (50), VISIBILITY BIT NOT NULL).
        /// </summary>
        internal static string PaymentCategoriesTable {
            get {
                return ResourceManager.GetString("PaymentCategoriesTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE PERSONS (ID INT IDENTITY(1,1) PRIMARY KEY, NAME VARCHAR (200), PHONE VARCHAR (15) NULL, EMAIL VARCHAR (50) NULL, COMMENT VARCHAR (250) NULL, IS_ENTITY BIT NOT NULL, IS_WORKER BIT NOT NULL, VIS BIT NOT NULL).
        /// </summary>
        internal static string PersonsTable {
            get {
                return ResourceManager.GetString("PersonsTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE USERS (ID INT IDENTITY(1,1) PRIMARY KEY, ID_WORKER INT, LOGIN VARCHAR (50), PASS VARBINARY (MAX)).
        /// </summary>
        internal static string ProgrammUsersTable {
            get {
                return ResourceManager.GetString("ProgrammUsersTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE SERVICE_CATEGORIES (ID INT IDENTITY(1,1) PRIMARY KEY, NAME VARCHAR (50), IS_CARWASH BIT NOT NULL, VISIBILITY BIT NOT NULL).
        /// </summary>
        internal static string ServiceCategoriesTable {
            get {
                return ResourceManager.GetString("ServiceCategoriesTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE WORKERS_CATEGORIES (ID INT IDENTITY(1,1) PRIMARY KEY, GROUP_NAME VARCHAR (50), VISIBILITY BIT NOT NULL).
        /// </summary>
        internal static string WorkersCategoriesTable {
            get {
                return ResourceManager.GetString("WorkersCategoriesTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [CARWASH] CREATE TABLE WORKERS (ID INT IDENTITY(1,1) PRIMARY KEY, ID_PERSON INT NOT NULL, BIRTHDATE DATETIME NULL, PASSPORT_SERIES VARCHAR (10) NULL, PASSPORT_NUMBER VARCHAR (10) NULL, PASSPORT_DATE DATETIME NULL, REG_ADRESS VARCHAR (200) NULL, FACT_ADRESS VARCHAR (200) NULL, ID_WORKERS_CATEGORY INT NOT NULL, IS_ON_WORK BIT, IS_BUSY BIT NOT NULL).
        /// </summary>
        internal static string WorkersTable {
            get {
                return ResourceManager.GetString("WorkersTable", resourceCulture);
            }
        }
    }
}
