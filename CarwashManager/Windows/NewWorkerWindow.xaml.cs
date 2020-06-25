using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CarwashManager.Classes;
using CWLib;

namespace CarwashManager.Windows
{
    /// <summary>
    /// Interaction logic for NewWorkerWindow.xaml
    /// </summary>
    public partial class NewWorkerWindow : Window, IWindow
    {
        public Worker WorkerToUpdate { get; set; }
        private string ConnStr { get; set; }


        public void SetWindowBackGround()
        {
            var bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFromString(System.Configuration.ConfigurationManager.AppSettings["WindowBackColor"].ToString());
        }

        public void SetWindowTitle()
        {
            ResourceManager rm = new ResourceManager("CarwashManager.Resources.TextStrings.WinNames", Assembly.GetExecutingAssembly());
            if (WorkerToUpdate == null)
                this.Title = rm.GetString("NewWorkerWindowName");
            else
                this.Title = rm.GetString("UpdateWorkerWindowName");
        }

        private void HideUIElements(bool isHidden)
        {
            if (isHidden)
            {
                loginLbl.Visibility = Visibility.Hidden;
                loginTxt.Visibility = Visibility.Hidden;
                passLbl.Visibility = Visibility.Hidden;
                passTxt.Visibility = Visibility.Hidden;
            }
            else
            {
                loginLbl.Visibility = Visibility.Visible;
                loginTxt.Visibility = Visibility.Visible;
                passLbl.Visibility = Visibility.Visible;
                passTxt.Visibility = Visibility.Visible;
            }
        }

        private void RefreshGroupList()
        {
            groupList.ItemsSource = null;
            groupList.Items.Clear();

            groupList.ItemsSource = DBWorker.WorkersCategorySearch(System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString());
        }

        public NewWorkerWindow()
        {            
            InitializeComponent();

            SetWindowBackGround();            

            RefreshGroupList();
            ConnStr = System.Configuration.ConfigurationManager.AppSettings["ConnString"].ToString();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nameTxt.Text != string.Empty && !String.IsNullOrWhiteSpace(nameTxt.Text))
            {
                if (phoneTxt.Text != string.Empty && !String.IsNullOrWhiteSpace(phoneTxt.Text))
                {
                    if (groupList.SelectedIndex != -1)
                    {
                        if (WorkerToUpdate == null)
                        {
                            Worker Worker = new Worker()
                            {
                                Name = nameTxt.Text,
                                Phone = phoneTxt.Text,
                                Email = emailTxt.Text != string.Empty ? emailTxt.Text : null,
                                BirthDate = (DateTime?)bDateTxt.SelectedDate != null ? (DateTime?)bDateTxt.SelectedDate : null,
                                PassportSeries = psTxt.Text != string.Empty ? psTxt.Text : null,
                                PassportNumber = pnTxt.Text != string.Empty ? pnTxt.Text : null,
                                PassportDate = (DateTime?)pDateTxt.SelectedDate != null ? (DateTime?)pDateTxt.SelectedDate : null,
                                RegistrationAdress = raTxt.Text != string.Empty ? raTxt.Text : null,
                                FactAdress = faTxt.Text != string.Empty ? faTxt.Text : null,
                                WorkerCategory = (WorkersCategory)groupList.SelectedItem,
                                IsOnWork = true,
                                User = null,
                                Comment = null,
                                IsBusy = false
                            };

                            if (Worker.WorkerCategory.GroupName == "Администраторы")                            
                                if (loginTxt.Text != string.Empty && String.IsNullOrWhiteSpace(loginTxt.Text) != true)                                
                                    if (passTxt.Password != string.Empty && String.IsNullOrWhiteSpace(passTxt.Password) != true)                                    
                                        Worker.SetLoginPass(loginTxt.Text, passTxt.Password);                                    
                                    else
                                        Worker.SetLoginPass(loginTxt.Text, "12345");                                
                                else
                                    Worker.SetLoginPass("New Admin", "12345");                                                  

                            string err = string.Empty;
                            if (Worker.AddWorker(ConnStr, out err))
                            {
                                MessageBox.Show("Новый работник успешно добавлен");
                                this.Close();
                            }
                            else
                                MessageBox.Show($"При добавлении работника вознилка ошибка\n{err}"); 
                        }
                        
                        else
                        {
                            if (WorkerToUpdate.UpdateWorker(ConnStr, (WorkersCategory)groupList.SelectedItem, 
                                                            nameTxt.Text, phoneTxt.Text, emailTxt.Text, 
                                                            bDateTxt.SelectedDate != null ? bDateTxt.SelectedDate : null, psTxt.Text, pnTxt.Text, 
                                                            pDateTxt.SelectedDate != null ? pDateTxt.SelectedDate : null, raTxt.Text, faTxt.Text))
                            {
                                MessageBox.Show("Информация о работнике успешно обновлена");
                                this.Close();
                            }
                            else
                                MessageBox.Show($"При обновлении информации о работнике вознилка ошибка");
                        }
                    }
                    else
                        MessageBox.Show("Не выбрана группа работников");
                }
                else
                    MessageBox.Show("Не указан телефон");
            }
            else
                MessageBox.Show("Не указано имя");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetWindowTitle();
            HideUIElements(true);

            if (WorkerToUpdate != null)
            {
                addBtn.Style = this.FindResource("SaveButton") as Style;
                nameTxt.Text = WorkerToUpdate.Name;
                phoneTxt.Text = WorkerToUpdate.Phone;
                emailTxt.Text = WorkerToUpdate.Email;
                bDateTxt.SelectedDate = WorkerToUpdate.BirthDate;
                psTxt.Text = WorkerToUpdate.PassportSeries;
                pnTxt.Text = WorkerToUpdate.PassportNumber;
                pDateTxt.SelectedDate = WorkerToUpdate.PassportDate;
                raTxt.Text = WorkerToUpdate.RegistrationAdress;
                faTxt.Text = WorkerToUpdate.FactAdress;

                for (int  i = 0;  i < groupList.Items.Count;  i++)
                {
                    if (((WorkersCategory)groupList.Items[i]).GroupId == WorkerToUpdate.WorkerCategory.GroupId)
                        groupList.SelectedItem = groupList.Items[i];
                }               
            }
            else            
                addBtn.Style = this.FindResource("AddWorkerButton") as Style;            
        }

        private void groupList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (groupList.Items.Count > 0)
            {
                if (((WorkersCategory)groupList.SelectedItem).GroupName == "Администраторы")                
                    HideUIElements(false);                
                else
                    HideUIElements(true);
            }            
        }

        private void phoneTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void psTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void pnTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
