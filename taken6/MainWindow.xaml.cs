using System;
using System.Collections.Generic;
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
using System.IO.Ports;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Threading;
using System.Windows.Navigation;

namespace taken6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            // Insert code required on object creation below this point.
        }
        int coin = 0;
        int thoigian = 0;     
	    int thoigianchoi = 0;
        int ticket = 0;
        int bonus = 0;
		 int bonustam = 0;      
		int countplaying=0;
		int numofbonus=0;	
		int mycountplay=0;	
		int countxoay=0;
        int groupticket1, groupticket2, groupticket3, groupticket4, groupticket5 = 0;
        Storyboard Storyboardmohopqua;
		Storyboard Storyboardblinkdemo;
		Storyboard Storyboardnhay;
		Storyboard Storyboardplaying;
		 Storyboard Storyboadmusicdemo;
		 Storyboard Storyboardbanten;
		 Storyboard Storyboardtam;
		 Storyboard Storyboardmegabonus;
		 Storyboard Storyboardthemxu;	
		 Storyboard Storyboardgun;		
		 Storyboard Storyboardtradiem;
		Storyboard Storyboardsoundtradiem;
        Storyboard Storyboardcoin;
        Storyboard Storyboardtapthe;    
        Storyboard Storyboardbtn;
        XmlDocument ticketdoc = new XmlDocument();
        XmlDocument mydoc = new XmlDocument();
        XmlDocument COMdoc = new XmlDocument();
        XmlNode ticketnode;
        XmlNode ticketgroup1;
        XmlNode ticketgroup2;
        XmlNode ticketgroup3;
        XmlNode ticketgroup4;
        XmlNode ticketgroup5;       
        XmlNode mynode;
        XmlNode mythoigian;
        XmlNode myxu;       
        XmlNode mybonus;		
		 XmlNode diembonus;
		 XmlNode playtime;
		 XmlNode countplay;
        XmlNode comNode;
        XmlNode comName;
		int credit=1;
        int randombonus = 0;
        string getdata = "";
        SerialPort sp1;
        void sendata(string dt)
        {
            try
            {
                if (!sp1.IsOpen)
                {
                    return;
                }
                else
                {
                    sp1.WriteLine(dt);
                }
            }
            catch
            {
                return;
            }
        }
        bool allowdemo = false;
        bool allowbtnstart = false;		
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
            bonustam = bonus;    
            groupticket1 = int.Parse(ticketgroup1.InnerText);
            groupticket2 = int.Parse(ticketgroup2.InnerText);
            groupticket3 = int.Parse(ticketgroup3.InnerText);
            groupticket4 = int.Parse(ticketgroup4.InnerText);
            groupticket5 = int.Parse(ticketgroup5.InnerText);
            tblgroup11.Text = groupticket1.ToString();
            tblgroup12.Text = groupticket1.ToString();
            tblgroup13.Text = groupticket1.ToString();
            tblgroup14.Text = groupticket1.ToString();

            tblgroup21.Text = groupticket3.ToString();
            tblgroup22.Text = groupticket3.ToString();
            tblgroup23.Text = groupticket3.ToString();

            tblgroup31.Text = groupticket2.ToString();
            tblgroup32.Text = groupticket2.ToString();

            tblgroup4.Text = groupticket4.ToString();
            tblgroup5.Text = groupticket5.ToString();
        }
        void loadcauhinh()
        {
            mydoc.Load("config.xml");
            mynode = mydoc.DocumentElement.FirstChild; 
			mythoigian = mynode.FirstChild;
            myxu = mythoigian.NextSibling; 
			diembonus= myxu.NextSibling;
			playtime = diembonus.NextSibling;
			countplay = playtime.NextSibling;
			credit = int.Parse(playtime.InnerText);
			coin=int.Parse(myxu.InnerText);					
			numofbonus =int.Parse(diembonus.InnerText);
            Random rd = new Random();
            randombonus = rd.Next((numofbonus / 2), numofbonus);           
            thoigian = int.Parse(mythoigian.InnerText);
			mycountplay = int.Parse(countplay.InnerText);
            lblcoin.Content = coin.ToString();
            thoigianchoi = thoigian;
            lblcountdown.Content = thoigianchoi.ToString();
        }		
		 System.Windows.Threading.DispatcherTimer Time_phidao_demo; 	
		System.Windows.Threading.DispatcherTimer Time_xoay;   
        System.Windows.Threading.DispatcherTimer kiemtracoinTimer;
        System.Windows.Threading.DispatcherTimer demnguocTimer;
        System.Windows.Threading.DispatcherTimer countstopTimer;
        System.Windows.Threading.DispatcherTimer getdatafromarduinoTimer;
        System.Windows.Threading.DispatcherTimer tradiemTimer;
		System.Windows.Threading.DispatcherTimer megabonusTimer;
		 System.Windows.Threading.DispatcherTimer Time_hopqua;
	 void hamMegabonusTimer()
        {
            megabonusTimer = new System.Windows.Threading.DispatcherTimer();
            megabonusTimer.Tick += megabonus_tick;
            megabonusTimer.Interval = new TimeSpan(0, 0, 1);
           // megabonusTimer.Start();
        }
		int timedstop_bonus=10;
		int countmegabonus=0;
		 void megabonus_tick(object sender, EventArgs e)
        {
			countmegabonus++;
			if(countmegabonus>=timedstop_bonus)
			{
				countmegabonus=0;
				megabonusTimer.Stop();
                tradiemTimer.Start();
                //hamtradiemTimer();		
                counting.Visibility = System.Windows.Visibility.Hidden;
			}
		}
		void demoautoplay()
		{
			Storyboardblinkdemo.Begin();		
			bonusbong.Visibility = System.Windows.Visibility.Visible;
			tbldemo.Visibility = System.Windows.Visibility.Visible;
			hinhtron.Visibility = System.Windows.Visibility.Visible;
			phinhan.Visibility = System.Windows.Visibility.Visible;
			Dao.Visibility = System.Windows.Visibility.Visible;
            //timer_phidao_demo();
            Time_phidao_demo.Start();
        }	
		void huydemoautoplay()
		{
            if(Time_xoay.IsEnabled)
            {
                Time_xoay.Stop();
            }           
			if(Time_phidao_demo.IsEnabled)
            {              
                Time_phidao_demo.Stop();				
				Storyboardbanten.Stop();			
			}
			canvashopqua.Visibility = System.Windows.Visibility.Hidden;
			bonusbong.Visibility = System.Windows.Visibility.Hidden;
			phinhan.Visibility = System.Windows.Visibility.Hidden;
			Dao.Visibility = System.Windows.Visibility.Hidden;
			Storyboardblinkdemo.Stop();
			tbldemo.Visibility = System.Windows.Visibility.Hidden;
			hinhtron.Visibility = System.Windows.Visibility.Hidden;          
        }
        void hamtradiemTimer()
        {
            tradiemTimer = new System.Windows.Threading.DispatcherTimer();
            tradiemTimer.Tick += tradiem_tick;
            tradiemTimer.Interval = new TimeSpan(0, 0, 1);
            //tradiemTimer.Start();
        }
        int counttradiem = 8;       
        void tradiem_tick(object sender, EventArgs e)
        {
            counttradiem--;
            if (counttradiem == 5)
            {
				Storyboardtam.Stop();
				Storyboardbanten.Stop();
				phinhan.Visibility = System.Windows.Visibility.Hidden;
				tam.Visibility = System.Windows.Visibility.Hidden;
				hinhtron.Visibility = System.Windows.Visibility.Hidden;
				Dao.Visibility = System.Windows.Visibility.Hidden;
				timecanvas.Visibility = System.Windows.Visibility.Hidden;			   		
				countplaying++;	
				lblcountdown.Content = "";			
              	odiem.Visibility= System.Windows.Visibility.Visible;
				Storyboardtradiem.Begin();
				Storyboardsoundtradiem.Begin();
				if(showgif)
				{
					showgif=false;
					anhienthihopqua();
				}
            }
			if (counttradiem == 0)
            {					
				tradiemTimer.Stop();	
				counttradiem = 8;
				Storyboardtradiem.Stop();
				odiem.Visibility= System.Windows.Visibility.Hidden;						
                Storyboardcoin.Begin();             
                kiemtracoinTimer.Start();               	
				Storyboardsoundtradiem.Stop();
				Storyboardtradiem.Stop();	
				Storyboardmegabonus.Stop();				
				lblbonus.Content = bonustam;
				Storyboadmusicdemo.Begin();
				huongdangame.Visibility = System.Windows.Visibility.Visible;
				ticketcanvas.Visibility = System.Windows.Visibility.Hidden;
                diem = 0;		
				lblscore.Content = "";
                allowdemo = true;
            }
        }	
        void chayhuongdantapthe()
        {
            tapthe.Visibility = System.Windows.Visibility.Visible;
            Storyboardtapthe.Begin();
        }
        void huyhuongdantapthe()
        {
            Storyboardtapthe.Stop();
            tapthe.Visibility = System.Windows.Visibility.Hidden;
        }   
		bool machine=false;
        void stopplaying(object sender, EventArgs e)
        {          
			thoigianchoi--;	
            lblcountdown.Content = thoigianchoi.ToString();
			if(thoigianchoi<=0)
			{
				machine=true;				
				Storyboardgun.Begin();    
			 	phidao();	
				thoigianchoi=thoigian;
			}
        }
        void endgame()
        {				
			bonustam ++;
            allowbtnstart = false;  
			Time_xoay.Stop();
			Storyboardplaying.Stop();
            Storyboardbtn.Stop();   
			countxoay=0;
			tam.Visibility = System.Windows.Visibility.Visible;		
            myhand.Visibility = System.Windows.Visibility.Hidden;          
            countstopTimer.Stop();
            thoigianchoi = thoigian;               
            lblbonus.Content = bonustam;
			if(!showgif)
			{
            	lblscore.Content = diem;
            }			
			sendata(diem.ToString());
            capnhatthongtin_xu(coin); 
			if(countplaying>= randombonus)
			{	
				if(!machine)
				{                  
                    lblscore.Content = "";					
					counting.Visibility = System.Windows.Visibility.Visible;
					countplaying=0;							
					timedstop_bonus=110;
                    megabonusTimer.Start();
                   // hamMegabonusTimer();
					bonustam = bonus;
                    Random rd = new Random();
                    randombonus = rd.Next((numofbonus / 2), numofbonus);
                }
				else
				{
                    tradiemTimer.Start();
                  //  hamtradiemTimer();	
				}
			} 			
			else if(showgif)
			{
				tblhopqua.Text = diem.ToString();
				counting.Visibility = System.Windows.Visibility.Visible;
                Time_hopqua.Start();
              //  timer_hopqua();
			}
            else if (diem > 80)
            {
                timedstop_bonus = 25;
                counting.Visibility = System.Windows.Visibility.Visible;
                megabonusTimer.Start();
              //  hamMegabonusTimer();
            }
            else if(diem>60)
			{		
				timedstop_bonus=20;
				counting.Visibility = System.Windows.Visibility.Visible;
                megabonusTimer.Start();
               // hamMegabonusTimer();				
			}
            else if (diem > 40)
            {
                timedstop_bonus = 15;
                counting.Visibility = System.Windows.Visibility.Visible;
                megabonusTimer.Start();
               // hamMegabonusTimer();
            }
            else
			{
                tradiemTimer.Start();
              //  hamtradiemTimer();				
			}
        }		
		int counttamdemo=0;
		void doihopqua()
		{
			counttamdemo++;			
			if(counttamdemo>=5)
			{
				Time_hopqua.Stop();
				counttamdemo=0;				
				hienthihopqua();
				timedstop_bonus=15;
                megabonusTimer.Start();
              //  hamMegabonusTimer();
			}			
		}
		void timer_hopqua()
        {
            Time_hopqua = new System.Windows.Threading.DispatcherTimer();
            Time_hopqua.Tick += Time_hopqua_Tick;
            Time_hopqua.Interval = new TimeSpan(0, 0, 1);
            //Time_hopqua.Start();
        }
		private void Time_hopqua_Tick(object sender, EventArgs e)
        {
			doihopqua();
		}
		void hienthihopqua()
		{
			canvashopqua.Visibility = System.Windows.Visibility.Visible;
			Storyboardmohopqua.Begin();
		}
		void anhienthihopqua()
		{
			canvashopqua.Visibility = System.Windows.Visibility.Hidden;
			Storyboardmohopqua.Stop();
		}
        void stopplayingtimer()
        {
            countstopTimer = new System.Windows.Threading.DispatcherTimer();
            countstopTimer.Tick += stopplaying;
            countstopTimer.Interval = new TimeSpan(0, 0, 1);
           // countstopTimer.Start();
        }
        int countdown = 3;
        void demnguoc(object sender, EventArgs e)
        {  		
            Storyboardcoin.Stop();		
			huongdangame.Visibility = System.Windows.Visibility.Hidden;
            lblready.Visibility = System.Windows.Visibility.Visible;
            lblready1.Visibility = System.Windows.Visibility.Visible;
            kiemtracoinTimer.Stop();		         
			countdown--;
            lblready.Content = countdown.ToString();					
            if (countdown == 0)
            {  
				demnguocTimer.Stop();
			     countdown = 3;			
				Storyboardnhay.Begin();
				Storyboardplaying.Begin();   
                lblcoin.Content = coin.ToString();
                countstopTimer.Start();
                // stopplayingtimer();
                lblready.Visibility = System.Windows.Visibility.Hidden;
                lblready1.Visibility = System.Windows.Visibility.Hidden;
                myhand.Visibility = System.Windows.Visibility.Visible;  
				timecanvas.Visibility = System.Windows.Visibility.Visible;
			   ticketcanvas.Visibility = System.Windows.Visibility.Visible;
				 hinhtron.Visibility = System.Windows.Visibility.Visible;
				phinhan.Visibility = System.Windows.Visibility.Visible;
			   Dao.Visibility = System.Windows.Visibility.Visible;
                Storyboardbtn.Begin();
                //timer_xoay();
                Time_xoay.Start();
                allowbtnstart =true;
       		 }		
			}
	     void batdaudemnguoc()
        {
            demnguocTimer = new System.Windows.Threading.DispatcherTimer();
            demnguocTimer.Tick += demnguoc;
            demnguocTimer.Interval = new TimeSpan(0, 0, 1);
           // demnguocTimer.Start();
        }
        void kiemtracoin()
        {
            kiemtracoinTimer = new System.Windows.Threading.DispatcherTimer();
            kiemtracoinTimer.Tick += ckeckcoin;
            kiemtracoinTimer.Interval = new TimeSpan(0, 0, 1);
            kiemtracoinTimer.Start();
        }
        int countchaydemo = 0;
        void ckeckcoin(object sender, EventArgs e)
        {
            countchaydemo++;
            if (countchaydemo >= 51)
            {
                countchaydemo = 0;
            }
            if (allowdemo == true)
            {
                if (countchaydemo == 1)
                {                  
                    chayhuongdantapthe();
                    demoautoplay();
                }
                if (countchaydemo==3)
				{
					huyhuongdantapthe();									
				}
				if(countchaydemo==50)
				{				
					huydemoautoplay();
				}	
            }
            if (coin > 0)
            {               
                allowdemo = false;
				huydemoautoplay();				
                coin--;				
			    huyhuongdantapthe();
                demnguocTimer.Start();
                //batdaudemnguoc();
				countchaydemo = 0;
				Storyboadmusicdemo.Stop();	
			}		
			moPort();
        }          
        void taoPort()
        {
            COMdoc.Load("com.xml");
            comNode = COMdoc.DocumentElement.FirstChild;
            comName = comNode.FirstChild;
            sp1 = new SerialPort();
            sp1.BaudRate = 9600;
            sp1.StopBits = StopBits.One;
            sp1.DataReceived += new SerialDataReceivedEventHandler(sp1_DataReceived);
            sp1.PortName = comName.InnerText;
        }
        void moPort()
        {
            try
            {
                if (!sp1.IsOpen)
                {
                    sp1.Open();  
					tblerr.Text = "";
                }				
            }
            catch
            { 
				tblerr.Text = "COM?";
                return;
            }
        }
        bool sxu = false;
        bool sstart = false;
		bool showgif=false;
        private void sp1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            getdata = sp1.ReadExisting();
            getdata = getdata.Trim();
            comunicatetoarduino(getdata);
        }
        void capnhatthongtin_xu(int xu)
        {
            // myxu.InnerText = xu.ToString();
            // mydoc.Save("config.xml");
        }
		 void luuluotchoi(int lc)
        {
            // countplay.InnerText = lc.ToString();
           	// mydoc.Save("config.xml");
        }
        void khoitaogetdatafromarduinoTimer()
        {
            getdatafromarduinoTimer = new System.Windows.Threading.DispatcherTimer();
            getdatafromarduinoTimer.Tick += checkdatafromarduino;
            getdatafromarduinoTimer.Interval = new TimeSpan(0, 0, 1/2);
            getdatafromarduinoTimer.Start();
        }	
		void phidao()
		{	
				Time_xoay.Stop();
				Storyboardnhay.Stop();
		    	Storyboardbanten.Begin();			
				if(countplaying>= randombonus)
				{					
					if(!machine)
					{
						countxoay=350;
						RotateTransform xoay = new RotateTransform(countxoay);
						this.hinhgame.RenderTransform = xoay;
                    bonusbong.Visibility = System.Windows.Visibility.Visible;
                    Storyboardmegabonus.Begin();
                }					
				}
				else 
				{
					if(countxoay>346 && countxoay<361)
					{
						countxoay=10;
						RotateTransform xoay = new RotateTransform(countxoay);
						this.hinhgame.RenderTransform = xoay;	
					}
				}	
				Storyboardtam.Begin();						
				diem = tinhdiem(countxoay);					
				endgame();
		}	
		int diem=0;
		void timer_phidao_demo()
        {
            Time_phidao_demo = new System.Windows.Threading.DispatcherTimer();
            Time_phidao_demo.Tick += Time_phidao_demo_Tick;
            Time_phidao_demo.Interval = new TimeSpan(0, 0, 1);
            //Time_phidao_demo.Start();
        }
		int countphidaodemo=0;	
		 private void Time_phidao_demo_Tick(object sender, EventArgs e)
        {           
            if (allowdemo)
            {   
                countphidaodemo++;             			
			    if(countphidaodemo==1)
			    {
                    // timer_xoay();
                    Time_xoay.Start();
                }
			    if(countphidaodemo==5)
			    {	
				    Storyboardgun.Begin();  			
				    Storyboardbanten.Begin();	
				    Time_xoay.Stop();			
				    RotateTransform xoay = new RotateTransform(80);
				    this.hinhgame.RenderTransform = xoay;	
				    Storyboardgun.Stop();
				    Random rd = new	 Random();
				    int t = rd.Next(60,150);
				    tblhopqua.Text=(t.ToString());
			    }
			    if(countphidaodemo==8)
			    {
				    Storyboardbanten.Stop();
                    if (allowdemo)
                        {
                            hienthihopqua();
                        }
                    }
			    if(countphidaodemo==11)
			    {
                    //  timer_xoay();
                    Time_xoay.Start();
                    countphidaodemo =0;                
                    anhienthihopqua();               
				    Time_phidao_demo.Stop();	
			    }
            }
            else
            {
                countphidaodemo = 0;
            }
        }			
		void timer_xoay()
        {
            Time_xoay = new System.Windows.Threading.DispatcherTimer();
            Time_xoay.Tick += Time_xoay_Tick;
            Time_xoay.Interval = new TimeSpan(0, 0, 1/2);
            //Time_xoay.Start();
        }
        int countcheckport = 0;
		 private void Time_xoay_Tick(object sender, EventArgs e)
        {
			countxoay+=1;			
			if(countxoay>360)
			{
				countxoay=0;
			}
			RotateTransform xoay = new RotateTransform(countxoay);
            this.hinhgame.RenderTransform = xoay;	
		}
		int  tinhdiem(int num)
		{
			ticket=0;
			if(num>-1 && num<43 || num >115 && num <160 || num>204 &&  num <251 || num >300 && num< 347 )
			{
				ticket=groupticket1;
			}
			else if(num>89 && num<117 || num> 178 && num< 206 )
			{
				ticket= groupticket2;
			}
			else if(num>42 && num<63 || num> 159 && num< 179 || num> 250 && num< 269)
			{
				ticket= groupticket3;
			}
			else if(num>62 && num<90)
			{
				Random rd=new Random();
				int dr = rd.Next(groupticket4, groupticket5);
				ticket=dr;
				showgif=true;
			}
			else if(num>286 && num<302)
			{
				ticket= groupticket5;				
			}
			else if(num>268 && num<287)
			{
				ticket= groupticket4;
			}
			else if(num>346 && num<360)
			{
				ticket=bonustam;
			}
			else
			{
				ticket= groupticket1;
			}
			return ticket;
		}
        void checkdatafromarduino(object sender, EventArgs e)
        {
            countcheckport += 1;
            if(countcheckport>=500)
            {
                countcheckport = 0;
                moPort();
            }           
            if (sxu == true)
            {
                my_insert_coin();
                sxu = false;
            }
            if (sstart == true)
            {
                if (allowbtnstart == true)
                {	
					machine=false;
					allowbtnstart =false;		
					Storyboardgun.Begin(); 
					phidao();
                }      
				 sstart = false;       
			}
        }
        void comunicatetoarduino(string t)
        {
            if (t == "x")
            {
                sxu = true;			
            }
            if (t == "s")
            {
                sstart = true;
            }
        }
		int sumplay=0;
        void my_insert_coin()
        {
			Storyboardthemxu.Begin();
            coin += credit;
			mycountplay +=1;
			sumplay+=1;
			tblcountplay.Text=sumplay.ToString();
            lblcoin.Content = coin.ToString();
            capnhatthongtin_xu(coin);
			luuluotchoi(mycountplay);
            //////////////////////////////////////////////////////////////////
        }
        private void winload(object sender, System.Windows.RoutedEventArgs e)
        {
            taoPort();
            moPort();	
			counting.Visibility = System.Windows.Visibility.Hidden;
			canvashopqua.Visibility = System.Windows.Visibility.Hidden;
			tbldemo.Visibility = System.Windows.Visibility.Hidden;
			bonusbong.Visibility = System.Windows.Visibility.Hidden;
			huongdangame.Visibility = System.Windows.Visibility.Visible;
			phinhan.Visibility = System.Windows.Visibility.Hidden;
			tam.Visibility = System.Windows.Visibility.Hidden;
  			timecanvas.Visibility = System.Windows.Visibility.Hidden;
			ticketcanvas.Visibility = System.Windows.Visibility.Hidden;
			hinhtron.Visibility = System.Windows.Visibility.Hidden;
			Dao.Visibility = System.Windows.Visibility.Hidden;
            lblready.Visibility = System.Windows.Visibility.Hidden;
            lblready1.Visibility = System.Windows.Visibility.Hidden;
            tapthe.Visibility = System.Windows.Visibility.Hidden;  
            myhand.Visibility = System.Windows.Visibility.Hidden;			
	     	odiem.Visibility= System.Windows.Visibility.Hidden;                 
            timer_phidao_demo();
            batdaudemnguoc();
            timer_xoay();
            stopplayingtimer();
            hamtradiemTimer();
            hamMegabonusTimer();
            timer_hopqua();
            loadcauhinh();
            Loadticket();
            kiemtracoin();
            khoitaogetdatafromarduinoTimer();	
			Storyboardmohopqua = this.FindResource("Storyboardmohopqua") as Storyboard;
			Storyboardblinkdemo = this.FindResource("Storyboardblinkdemo") as Storyboard;
			Storyboardnhay = this.FindResource("Storyboardnhay") as Storyboard;
			Storyboadmusicdemo = this.FindResource("Storyboardmusicdemo") as Storyboard;
			Storyboardplaying = this.FindResource("Storyboardplaying") as Storyboard;
			Storyboardbanten = this.FindResource("Storyboardbanten") as Storyboard;
			Storyboardmegabonus = this.FindResource("Storyboardmegabonus") as Storyboard;
			Storyboardtam = this.FindResource("Storyboardtam") as Storyboard;
	      	Storyboardgun = this.FindResource("Storyboardgun") as Storyboard;
			Storyboardthemxu = this.FindResource("Storyboardthemxu") as Storyboard;
			Storyboardtradiem = this.FindResource("Storyboardtradiem") as Storyboard;
			Storyboardsoundtradiem = this.FindResource("Storyboardsoundtradiem") as Storyboard;
            Storyboardbtn = this.FindResource("Storyboardbtn") as Storyboard;
            Storyboardcoin = this.FindResource("Storyboardcoin") as Storyboard;
            Storyboardtapthe = this.FindResource("Storyboardtapthe") as Storyboard;
            Storyboardcoin.Begin();          
			lblbonus.Content=bonus;
			Storyboadmusicdemo.Begin();
            allowdemo = true;
            Thread.Sleep(5000);
			System.Diagnostics.Process.Start(@"enter.vbs");
        }  
        private void nhanphim(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.M)
            {
                this.Topmost = false;
                Window1 w1 = new Window1();
                w1.ShowDialog();
            }
            if (e.Key == Key.X)
            {
                my_insert_coin();
            }			
            if (e.Key == Key.Enter)
            {
                if (allowbtnstart == true)
                {
					machine=false;
					allowbtnstart =false;
					Storyboardgun.Begin();  
					phidao();
                }
            }  
        }
    }
}