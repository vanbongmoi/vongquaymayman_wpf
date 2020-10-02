using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
namespace taken6
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        XmlDocument ticketdoc = new XmlDocument();
        XmlDocument mydoc = new XmlDocument();
        XmlDocument configdoc = new XmlDocument();
        XmlNode comNode;
        XmlNode comName;
        XmlNode mycreditnode;
        XmlNode ticketnode;
        XmlNode ticketgroup1;
        XmlNode ticketgroup2;
        XmlNode ticketgroup3;
        XmlNode ticketgroup4;
        XmlNode ticketgroup5;
        XmlNode mynode;
        XmlNode mybonus;
        XmlNode mythoigian;
        XmlNode numbonus;
        void Loadcauhinh()
        {
            configdoc.Load("config.xml");
            mynode = configdoc.DocumentElement.FirstChild;
            mythoigian = mynode.FirstChild;
            numbonus = mythoigian.NextSibling.NextSibling;
            mycreditnode = numbonus.NextSibling;

            txtcoin.Text = mycreditnode.InnerText;
            txtnumofbonus.Text = numbonus.InnerText;
            txtplaytime.Text = mythoigian.InnerText;

        }
        int groupticket1, groupticket2, groupticket3, groupticket4, groupticket5, bonus = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
         
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Saveinfo();
            MessageBox.Show("Save.");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Loadticket();
            LoadPort();
            Loadcauhinh();
        }
        void LoadPort()
        {
            mydoc.Load("com.xml");
            comNode = mydoc.DocumentElement.FirstChild;
            comName = comNode.FirstChild;
            txtcom.Text = comName.InnerText;
        }
        void Loadticket()
        {
            ticketdoc.Load("settings.xml");
            ticketnode = ticketdoc.DocumentElement.FirstChild;
            ticketgroup1 = ticketnode.FirstChild;
            ticketgroup2 = ticketgroup1.NextSibling;
            ticketgroup3 = ticketgroup2.NextSibling;
            ticketgroup4 = ticketgroup3.NextSibling;
            ticketgroup5 = ticketgroup4.NextSibling;
            mybonus = ticketgroup5.NextSibling;
            bonus = int.Parse(mybonus.InnerText);
            groupticket1 = int.Parse(ticketgroup1.InnerText);
            groupticket2 = int.Parse(ticketgroup2.InnerText);
            groupticket3 = int.Parse(ticketgroup3.InnerText);
            groupticket4 = int.Parse(ticketgroup4.InnerText);
            groupticket5 = int.Parse(ticketgroup5.InnerText);
            txtg1.Text = groupticket1.ToString();
            txtg2.Text = groupticket2.ToString();
            txtg3.Text = groupticket3.ToString();
            txtg4.Text = groupticket4.ToString();
            txtg5.Text = groupticket5.ToString();
            txtbonus.Text = bonus.ToString();
        }
        void Saveinfo()
        {
            ticketgroup1.InnerText = txtg1.Text.Trim();
            ticketgroup2.InnerText = txtg2.Text.Trim();
            ticketgroup3.InnerText = txtg3.Text.Trim();
            ticketgroup4.InnerText = txtg4.Text.Trim();
            ticketgroup5.InnerText = txtg5.Text.Trim();
            mybonus.InnerText = txtbonus.Text.Trim();
            ticketdoc.Save("settings.xml");
            comName.InnerText = txtcom.Text.Trim();
            mydoc.Save("com.xml");

            mycreditnode.InnerText = txtcoin.Text.Trim();
            numbonus.InnerText = txtnumofbonus.Text.Trim();
            mythoigian.InnerText = txtplaytime.Text.Trim();
            configdoc.Save("config.xml");
        }
    }
}
