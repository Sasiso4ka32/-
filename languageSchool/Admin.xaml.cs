using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace languageSchool
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        List<Service> ServiswList = ClassBase.EM.Service.ToList();
        List<Client> ClientsList = ClassBase.EM.Client.ToList();
        public Admin()
        {
            InitializeComponent();
            DGServises.ItemsSource = ServiswList;
            newZakaz_ListPeople.ItemsSource = ClientsList;
            newZakaz_ListPeople.SelectedValuePath = "ID";
            newZakaz_ListPeople.DisplayMemberPath = "People";
        }
        int i = -1;
        int index;
        private void MediaElement_Initialized(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                i++;
                MediaElement ME = (MediaElement)sender;
                Service S = ServiswList[i];
                Uri U = new Uri(S.MainImagePath, UriKind.RelativeOrAbsolute);
                ME.Source = U;
                //   i++;
            }
        }

        private void TextBlock_Initialized(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                TextBlock TB = (TextBlock)sender;
                Service S = ServiswList[i];
                TB.Text = S.Title;
                //  i++;
            }

        }

        private void BRed_Initialized(object sender, EventArgs e)
        {
            Button BtnRed = (Button)sender;
            if (BtnRed != null)
            {
                BtnRed.Uid = Convert.ToString(i);
            }
        }

        private void BRed_Click(object sender, RoutedEventArgs e)
        {
            Button BtnRed = (Button)sender;
            int ind = Convert.ToInt32(BtnRed.Uid);
            Service S = ServiswList[ind];
            index = Convert.ToInt32(BtnRed.Uid);
            DataGrid.Visibility = Visibility.Collapsed;
            StakPanel_Redact.Visibility = Visibility.Visible;
            id_servises.Text = Convert.ToInt32(S.ID) + "";
            title_servises.Text = S.Title.ToString();
            cost_servises.Text = Convert.ToInt32(S.Cost) + "";
            time_servises.Text = Convert.ToInt32(S.DurationInSeconds) / 60 + "";            
            discount_servises.Text = Convert.ToDouble(S.Discount)*100 + "";
            path_servises.Text = S.MainImagePath.ToString();

        }
        private void Save_red_Click(object sender, RoutedEventArgs e)
        {
            int ind = index;
            Service S = ServiswList[ind];
            if (Convert.ToDouble(discount_servises.Text) <= 100 && Convert.ToInt32(S.DurationInSeconds) <= 240)
            {                       
            S.Title = title_servises.Text;
            S.Cost = Convert.ToInt32(cost_servises.Text);               
            S.DurationInSeconds = Convert.ToInt32(time_servises.Text) * 60;                          
            S.Discount = Convert.ToDouble(discount_servises.Text)/100;
            S.MainImagePath = path_servises.Text;
            MessageBox.Show("Изменения сохранены");
            ClassBase.EM.SaveChanges();
            ClassFrame.frame.Navigate(new Admin());
            }
            else
            {
                MessageBox.Show("Скидка не может быть больше 100% и время занятия не больше 400 минут");
            }
        }
        private void Image_redakt_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog OFD = new OpenFileDialog();
            OFD.ShowDialog();
            string Path = OFD.FileName;
            if(Path != "")
            {
                int c = Path.IndexOf('У');
                string Len = Path.Substring(c);
                path_servises.Text = Len.ToString();
            }           
        }

        private void StackPanel_Initialized(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                StackPanel SP = (StackPanel)sender;
                Service S = ServiswList[i];
                if (S.Discount != 0)
                {
                    SP.Background = new SolidColorBrush(Color.FromRgb(231, 250, 191));
                }
            }
        }

        private void TextBlock_priceSkidos_Initialized(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                TextBlock TB = (TextBlock)sender;
                Service S = ServiswList[i];
                TB.Text = Convert.ToInt32(S.Cost) + " ";
                if (S.Discount> 0)
                {
                    TB.TextDecorations = TextDecorations.Strikethrough;
                }
                else
                {
                    TB.Visibility = Visibility.Collapsed;
                }

                //  i++;
            }
        }
        private void TextBlock_price_Initialized(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                TextBlock TB = (TextBlock)sender;
                Service S = ServiswList[i];
                if (S.Discount>0)
                {
                    TB.Text = Convert.ToInt32(S.Cost) * (1 - S.Discount) + " рублей ";
                }
                else
                {
                    TB.Text = Convert.ToInt32(S.Cost) + " рублей ";
                }
                
                //  i++;
            }
        }
        
        private void TextBlock_time_Initialized(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                TextBlock TB = (TextBlock)sender;
                Service S = ServiswList[i];
                TB.Text = "за " + Convert.ToInt32(S.DurationInSeconds) /60 + " минут";
                //  i++;
            }
        }
        private void TextBlock_Skidos_Initialized(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {                
                TextBlock TB = (TextBlock)sender;
                Service S = ServiswList[i];
                if (S.Discount != 0)
                {
                    TB.Text = "* скидка " + Convert.ToDouble(S.Discount) * 100 + " %";
                }                    
                //  i++;
            }
        }

        private void Bdel_Initialized(object sender, EventArgs e)
        {
            Button BtnRed = (Button)sender;
            if (BtnRed != null)
            {
                BtnRed.Uid = Convert.ToString(i);
            }

        }
        private void Bdel_Click(object sender, RoutedEventArgs e)
        {
            Button BtnRed = (Button)sender;
            int ind = Convert.ToInt32(BtnRed.Uid);
            Service S = ServiswList[ind];
            ClassBase.EM.Service.Remove(S);
            MessageBox.Show("Запись удалена");
            ClassBase.EM.SaveChanges();
            ClassFrame.frame.Navigate(new Admin());

        }

        private void Bnew_Initialized(object sender, EventArgs e)
        {
            Button BtnRed = (Button)sender;
            if (BtnRed != null)
            {
                BtnRed.Uid = Convert.ToString(i);
            }
        }
        private void Bnew_Click(object sender, RoutedEventArgs e)
        {
            Button BtnRed = (Button)sender;
            int ind = Convert.ToInt32(BtnRed.Uid);
            index = Convert.ToInt32(BtnRed.Uid);
            Service S = ServiswList[ind];
            StackPanel_newZakaz.Visibility = Visibility.Visible;
            DataGrid.Visibility = Visibility.Collapsed;
            New_Zap.Visibility = Visibility.Collapsed;
            newZakaz_title.Text = "Название услуги: " + S.Title;
            newZakaz_time.Text = "Время: " + S.DurationInSeconds / 60 + " минут";
            

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.Visibility = Visibility.Visible;
            StakPanel_Redact.Visibility = Visibility.Collapsed;
        }

        private void New_Zap_Click(object sender, RoutedEventArgs e)
        {
            StackPanel_new_Zap.Visibility = Visibility.Visible;
            DataGrid.Visibility = Visibility.Collapsed;

        }

        private void Save_new_red_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(new_time.Text) <= 240 && Convert.ToDouble(new_discount.Text)<=100)
            {

           

            Service ServiceObject = new Service();
            {
                ServiceObject.Title = new_title.Text;
                ServiceObject.Cost = Convert.ToInt32(new_cost.Text);
                ServiceObject.DurationInSeconds = Convert.ToInt32(new_time.Text)*60;
                ServiceObject.Discount = Convert.ToDouble(new_discount.Text) /100;
                ServiceObject.MainImagePath = Convert.ToString(new_path.Text);
            }
            ClassBase.EM.Service.Add(ServiceObject);
            ClassBase.EM.SaveChanges();
            MessageBox.Show("Пользователь добавлен");
            ClassFrame.frame.Navigate(new Admin());
            }
            else
            {
                MessageBox.Show("Скидка не может быть больше 100% и время занятия не больше 400 минут");
            }
        }

        private void new_path_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.ShowDialog();
            string Path = OFD.FileName;
            if (Path != "")
            {
                int c = Path.IndexOf('У');
                string Len = Path.Substring(c);
                new_path.Text = Len.ToString();
            }
        }
        private void newZakaz__btn_Click(object sender, RoutedEventArgs e)
        {
            New_Zap.Visibility = Visibility.Visible;
            StackPanel_newZakaz.Visibility = Visibility.Collapsed;
            DataGrid.Visibility = Visibility.Visible;
        }

        private void addNewZakaz__hidden_Click(object sender, RoutedEventArgs e)
        {
            New_Zap.Visibility = Visibility.Collapsed;
            DataGrid.Visibility = Visibility.Visible;
            StackPanel_new_Zap.Visibility = Visibility.Visible;
        }

        private void newZakaz_ListPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = newZakaz_ListPeople.SelectedIndex + 1;
        }
        DateTime DT;
        private void newZakaz_changeSecondTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Regex r1 = new Regex("[0-1][0-9]:[0-5][0-9]");
                Regex r2 = new Regex("2[0-3]:[0-5][0-9]");
                if ((r1.IsMatch(newZakaz_changeSecondTime.Text) || r2.IsMatch(newZakaz_changeSecondTime.Text)) && newZakaz_changeSecondTime.Text.Length == 5)
                {
                    MessageBox.Show(newZakaz_changeSecondTime.Text);
                    TimeSpan TS = TimeSpan.Parse(newZakaz_changeSecondTime.Text);
                    DT = Convert.ToDateTime(newZakaz_datePicker.SelectedDate);
                    DT = DT.Add(TS);
                    if (DT > DateTime.Now)
                    {
                        MessageBox.Show(DT + "");
                    }
                    else
                    {
                        MessageBox.Show("Слишком поздно");
                        newZakaz_saveBtn.IsEnabled = false;
                    }
                }
                else
                {
                    if (newZakaz_changeSecondTime.Text.Length > 5)
                    {
                        MessageBox.Show("Время указано неверно");
                        newZakaz_saveBtn.IsEnabled = false;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }

        private void newZakaz_saveBtn_Click(object sender, RoutedEventArgs e)
        {
            int ind = index;
            Service S = ServiswList[ind];
            int client = newZakaz_ListPeople.SelectedIndex + 1;

            ClientService ClientServiceObject = new ClientService()
            {
                ClientID = client,
                ServiceID = S.ID,
                StartTime = DT,
            };


            ClassBase.EM.ClientService.Add(ClientServiceObject);
            MessageBox.Show("Запись добавлена");
            ClassBase.EM.SaveChanges();
            ClassFrame.frame.Navigate(new Admin());

        }
    }
}
