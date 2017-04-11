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
using AxMSTSCLib;
//using System.Runtime.InteropServices;

using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;




namespace TCP_to_RDP_Converter
{
    public partial class Form1 : Form
    {
        public static RDPSession currentSession = null;

        private  KeyboardHookListener m_KeyboardHookManager;
        private  MouseHookListener m_MouseHookManager;


        private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {
            label1.Text = e.KeyData.ToString() + " Pressed";
            e.SuppressKeyPress = true;
        }

        private void HookManager_KeyUp(object sender, KeyEventArgs e)
        {
            label1.Text = e.KeyData.ToString() + " Released";
            e.SuppressKeyPress = true;
        }

        private void HookManager_MouseUp(object sender, MouseEventArgs e)
        {
            label1.Text = e.Button.ToString() + " Released";
            //   e.Handled = true;


        }

        private void HookManager_MouseDown(object sender, MouseEventExtArgs e)
        {
            label1.Text = e.Button.ToString() + " Pressed";
            e.Handled = true;
        }

        
        public static void createSession()
        {
            currentSession = new RDPSession();
        }

        public static void Connect(RDPSession session)
        {
            session.OnAttendeeConnected += Incoming;
            session.Open();
        }

        public static void Disconnect(RDPSession session)
        {
            try
            {
                session.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error Connecting", "Error connecting to remote desktop "  + " Error:  " + Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public static string getConnectionString(RDPSession session, String authString, 
            string group, string password, int clientLimit)
        {
            IRDPSRAPIInvitation invitation =
                session.Invitations.CreateInvitation
                (authString, group, password, clientLimit);
                        return invitation.ConnectionString;
        }

        private static void Incoming(object Guest)
        {
            IRDPSRAPIAttendee MyGuest = (IRDPSRAPIAttendee)Guest;
            MyGuest.ControlLevel = CTRL_LEVEL.CTRL_LEVEL_INTERACTIVE;
        }

        /// <summary>
        /// Handle the form items
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createSession();
            Connect(currentSession);
            textConnectionString.Text = getConnectionString(currentSession,
                "Test","Group","",16);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Disconnect(currentSession);
        }

        private void button3_Click(object sender, EventArgs e)
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


        


    }
}
