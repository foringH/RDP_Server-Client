using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RDPCOMAPILib;
using AxRDPCOMAPILib;

using System.Threading;

using System.Security.Permissions;
using Microsoft.Win32;
using System.Diagnostics;

using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;



namespace Simple_RDP_Client
{
    public partial class Form1 : Form
    {
        public static int flag = 1;
        
        /*
        /// Return Type: BOOL->int
        ///fBlockIt: BOOL->int
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "BlockInput")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        
        public static extern bool BlockInput([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)] bool fBlockIt);

        public static void BlockInput(TimeSpan span)
        {
            try
            {
                Form1.BlockInput(true);
                Thread.Sleep(span);
            }
            finally
            {
                Form1.BlockInput(false);
            }
        }
        */
      //  private readonly KeyboardHookListener m_KeyboardHookManager;
     //   private readonly MouseHookListener m_MouseHookManager;

        private KeyboardHookListener m_KeyboardHookManager;
        private MouseHookListener m_MouseHookManager;

           
        

        public Form1()
        {
            InitializeComponent();

        }

        private void initializeHooks()
        {
            m_KeyboardHookManager = new KeyboardHookListener(new GlobalHooker());
            m_KeyboardHookManager.Enabled = true;
            m_KeyboardHookManager.KeyDown += HookManager_KeyDown;
            m_KeyboardHookManager.KeyUp += HookManager_KeyUp;



            m_MouseHookManager = new MouseHookListener(new GlobalHooker());
            m_MouseHookManager.Enabled = true;
            m_MouseHookManager.MouseDownExt += HookManager_MouseDown;
            m_MouseHookManager.MouseUp += HookManager_MouseUp;

        
        }

        private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {
            label1.Text = e.KeyData.ToString() + " Pressed";

            if (flag == 1)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void HookManager_KeyUp(object sender, KeyEventArgs e)
        {
            label1.Text = e.KeyData.ToString() + " Released";
            if (flag == 1)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void HookManager_MouseUp(object sender, MouseEventArgs e)
        {
            label1.Text = e.Button.ToString() + " Released";
         //   e.Handled = true;
             
            
        }

        private void HookManager_MouseDown(object sender, MouseEventExtArgs e)
        {
            label1.Text = e.Button.ToString() + " Pressed";
            //e.Handled = true;

            if(flag == 1){
                e.Handled = true;
            }

        }

        public static void Connect(string invitation, AxRDPViewer display, string userName, string password)
        {
            display.Connect(invitation, userName, password);
        }

        public static void disconnect(AxRDPViewer display)
        {
            display.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TimeSpan interval = new TimeSpan(0, 0, 50);
            //BlockInput(interval);
            
            try
            {
                Connect(textConnectionString.Text, this.axRDPViewer, "", "");
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to connect to the Server");
            }
        }

        private void textConnectionString_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void disable()
        {

            flag = 1;

            initializeHooks();
        
        }

        private void enable()
        {
            flag = 0;

            //initializeHooks();
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            //Disable();
            disable();
           
            //Thread.Sleep(10000);

            //enable();
            //enable();
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            //Enable()
           // enable();


        }

        private void axRDPViewer_Enter(object sender, EventArgs e)
        {


        }

       
    }
}
