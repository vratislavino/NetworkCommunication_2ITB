using System.Net.Sockets;

namespace Client
{
    public partial class Form1 : Form
    {
        TcpClient client;
        StreamWriter writer;

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            client = new TcpClient();
            await client.ConnectAsync("127.0.0.1", 8888);
            listBox1.Items.Add("Pøipojeno k serveru");
            StartReading();
        }

        private async void StartReading()
        {
            using(var stream = client.GetStream())
            using (var reader = new StreamReader(stream))
            {
                while(true)
                {
                    try { 
                        string message = await reader.ReadLineAsync();
                        if (message == null) break;

                        Invoke((MethodInvoker)delegate
                        {
                            listBox1.Items.Add("Received: " + message);
                        });
                    }
                    catch (Exception e)
                    {
                        listBox1.Items.Add(e.Message);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
