using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebSocket4Net;
using SocketIOClient;

namespace socketIO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Make it invisible
            // this.WindowState = FormWindowState.Minimized;
            //this.ShowInTaskbar = false;
            Execute();
        }

        Client socket;
        public void Execute()
        {
            updateData("Starting Client");

            socket = new Client("http://23.23.209.78:8124/"); // url to nodejs 

            socket.Connect();
             updateData("socket started");

          
            //Used for sending on Socket
            socket.On("client_data", (fn) =>
            {
                updateData("on client_data");
               // socket.Emit("partInfo", "TEST");
            });


            //Used for receiving Socket
            socket.On("message", (data) =>
            {
                updateData("Received" + data.Json.Args[0] );
                MessageBox.Show(data.Json.Args[0]);
                //data.RawMessage  data.MessageText  data.Json.ToJsonString() data.Json.Args[0]

            });

            
        }

        public void updateData(String msg)
        {
            richTextBox1.AppendText(msg +'\n');
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String msg = message.Text;
            socket.Emit("client_data",msg);
            updateData("Sendding on Btton Click");
        }

    }
}
