using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Data;

namespace EasyTester
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{	
		public string SerNum;
		SerialPort _serpotr1 = new SerialPort("COM1");	
		string _USB_check;
		string _Plus_B;
		string _Minus_B;
		string _Phone_B;
		string _Play_B;
		string _Conn_B;
		string _start;
		string _end;
		string [] cultureName = {"de-DE"};
		DateTime start;
		DateTime end;
		TimeSpan ts;
	
		public MainForm()
		{
			InitializeComponent();
			progressBar1.Value = 100;
	        progressBar1.BackColor = Color.Red;
	       
		}
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			
		}
		void TextBox1KeyPress(object sender, KeyPressEventArgs e)
		{
		
			if (e.KeyChar == (char)Keys.Return) {
				//MessageBox.Show(textBox1.Text);
				SerNum = textBox1.Text;
				start = DateTime.Now;
				_start = start.ToString(cultureName[0]);
				
				//listBox1.Items.ToString() = textBox1.Text;
				if (!CheckSerNumb(SerNum)) {
					MessageBox.Show("Неправильний серійний номер");
					textBox1.Text = "";
					SerNum = String.Empty;
					return;
				}
			//add d0-while//
		
		CheckTester();
			
		if(USB_check())
		{
			
			Fail(1); _serpotr1.Close(); end = DateTime.Now; _end = end.ToString(cultureName[0]);
			return; 
		}
		
		if(Plus_B()){
			Fail(2); _serpotr1.Close(); end = DateTime.Now; _end = end.ToString(cultureName[0]);
			return;}
		if(Minus_B()){
			Fail(3); _serpotr1.Close(); end = DateTime.Now; _end = end.ToString(cultureName[0]);
		    return;}
		if(Phone_B()){
			Fail(4); _serpotr1.Close(); end = DateTime.Now; _end = end.ToString(cultureName[0]);
			return;}
		if(Play_B()){
			Fail(5); _serpotr1.Close(); end = DateTime.Now; _end = end.ToString(cultureName[0]);
			return;}
		if(Conn_B()){
			Fail(6); _serpotr1.Close(); end = DateTime.Now; _end = end.ToString(cultureName[0]);
			return;}
		   MessageBox.Show("PASS");
		}
	}
		
		
		
		
		
		bool CheckSerNumb(string SerNumb)
		{
			int x = SerNumb.Length;
			if((x == 10) & (SerNumb.Contains("SU") || SerNumb.Contains("AX"))){
			   	return true; }
			   	return false;
		}
//		void Button1Click(object sender, EventArgs e)
//		{
//			MainForm form = new MainForm();
//			//form.TextBox1KeyPress += 
//	    }
		void CheckTester()
		{
			//byte [] buff;
			
			listBox1.Items.Add("Checking connection to the tester");
			try{
				_serpotr1.BaudRate = 9600;
				_serpotr1.Parity = Parity.None;
				_serpotr1.StopBits = StopBits.One;
				_serpotr1.Handshake = Handshake.None;
				_serpotr1.Open();
				_serpotr1.RtsEnable = true;
				_serpotr1.DtrEnable = true;
				//_serpotr1.Write("1");
				//_serpotr1.BytesToWrite(1);
				
				
			   }catch(IOException ex)
			{
				MessageBox.Show(ex.ToString());
			}
//			_serpotr1.Write(new byte[] {1}, 0,1 );
//				Thread.Sleep(1000);
//			 arduino = _serpotr1.ReadLine();
//			 if(arduino.Contains("OK"))
//				
//				listBox1.Items.Add("Connection is OK");
//				
//			else _serpotr1.Close();
//					
//			return true;
		
		}
		bool ReadComPort()
		{   listBox1.Items.Add ("Checking connection");
			string arduino= string.Empty;
			 double e = 0.0;
			 DateTime u = DateTime.Now;
			do
			{
				DateTime u2 = DateTime.Now;
				ts =u2-u; e = ts.TotalSeconds;
			_serpotr1.Write("1");
			Thread.Sleep(2000);
			arduino = _serpotr1.ReadLine();
			
			}while((e >= 15.0) || _serpotr1.ReadLine() == "OK");
		
			if(arduino.Contains("OK"))
				return false;
			return true;
		}
		
		void Timer1Tick(object sender, EventArgs e)
		{
	
		}
		bool USB_check()
		{   listBox1.Items.Add ("Testing USB");
			 double r = 0.0;
			 DateTime u = DateTime.Now;
			do{
				DateTime u2 = DateTime.Now;
				ts =u2-u; r = ts.TotalSeconds;
			if(_serpotr1.ReadLine() == "USB"){
					_USB_check = "PASS";  listBox1.Items.Add (" USB - OK");   return false;}
				}while( (r >= 15.0) || _serpotr1.ReadLine() == "USB" || _serpotr1.ReadLine() == "FAIL");
			  listBox1.Items.Add ("USB - Fail");
				return true;
	    }
		
	//Method Fail//
	   string Fail(int x)
	   {
	   	switch(x)
	   	{
				case 1:
					 _USB_check = "FAIL";
					 break;
				case 2:
					 _Plus_B = "FAIL";
					 break;
				case 3:
					 _Minus_B = "FAIL";
					break;
				case 4: 
					 _Phone_B = "FAIL";
					 break;
				case 5:
					 _Play_B = "FAIL";
					 break;
				case 6: 
					 _Conn_B = "FAIL";
					 break;
		}
			return "PASS";
		}
	   // Buttons/// 
	
	    bool Plus_B()
	    {      listBox1.Items.Add ("Testing Plus_B");   
	    	double r1 =0.0f;
	        DateTime p1 = DateTime.Now;
	    	do{
	        	DateTime p2 = DateTime.Now;
				ts =p2-p1; r1 = ts.TotalSeconds;
	   	  if(_serpotr1.ReadLine() == "Plus"){
	    			_Plus_B = "PASS"; listBox1.Items.Add ("Plus_B - OK"); return false;}
	    	}while(r1 >= 15 || _serpotr1.ReadLine() == "Plus" || _serpotr1.ReadLine() == "FAIL");
	   	     listBox1.Items.Add ("Plus_B - Fail"); return true;
	    }
		bool Minus_B()
	    {   listBox1.Items.Add ("Testing Minus_B");
			double r2 = 0.0f;
			 DateTime m1 = DateTime.Now;
			do{
			 	DateTime m2 = DateTime.Now;
				ts =m2-m1; r2 = ts.TotalSeconds;
		 	if(_serpotr1.ReadLine() == "Minus"){
	   		_Minus_B = "PASS";listBox1.Items.Add ("Minus_B - OK"); return false;}
			}while(r2 >= 15 || _serpotr1.ReadLine() == "Minus" || _serpotr1.ReadLine() == "FAIL");
			 listBox1.Items.Add ("Minus_B-Fail");
	   	    return true;
	    }
		bool Phone_B()
	    {    listBox1.Items.Add ("Testing Phone_B");
			 double r3 = 0.0f;
			 DateTime p1 = DateTime.Now;
			do{
			 	DateTime p2 = DateTime.Now;
				ts =p2-p1; r3 = ts.TotalSeconds;
		  	if(_serpotr1.ReadLine() == "Phone"){
	   		_Phone_B = "PASS";  listBox1.Items.Add ("Phone_B - OK"); return false;}
			}while(r3 >= 15 || _serpotr1.ReadLine() == "Phone" || _serpotr1.ReadLine() == "FAIL");
			  listBox1.Items.Add ("Phone_B-Fail");
	   	    return true;
	    }
		bool Play_B()
	    {    listBox1.Items.Add ("Testing Play_B");
			 double r4 = 0.0f;
			 DateTime p2 = DateTime.Now;
			 do{
			 	DateTime p3 = DateTime.Now;
				ts =p3-p2; r4 = ts.TotalSeconds;
		 	if(_serpotr1.ReadLine() == "Play"){
	   		_Play_B = "PASS";  listBox1.Items.Add ("Play_B - OK"); return false;}
			 }while(r4 >= 15 || _serpotr1.ReadLine() == "Play" || _serpotr1.ReadLine() == "FAIL");
			  listBox1.Items.Add ("Play_B-Fail");
	   	    return true;
	    }
		bool Conn_B()
	    {    listBox1.Items.Add ("Testing Conn_B");
			double r5 = 0.0f;
			 DateTime c1 = DateTime.Now;
			 do{
			 	DateTime c2 = DateTime.Now;
				ts =c2-c1; r5 = ts.TotalSeconds;
		  	if(_serpotr1.ReadLine() == "Conn"){
	   		_Conn_B = "PASS";  listBox1.Items.Add ("Conn_B - OK"); return false;}
			 }while(r5 >= 15 || _serpotr1.ReadLine() == "Conn" || _serpotr1.ReadLine() == "FAIL" );
			  listBox1.Items.Add ("Conn_B-Fail");
	   	    return true;
	    }
		 
     }
}
