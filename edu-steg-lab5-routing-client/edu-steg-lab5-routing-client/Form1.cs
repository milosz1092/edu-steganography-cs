using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace edu_steg_lab5_routing_client
{
    public partial class Form1 : Form
    {
        private List<Process> _listenersProcesses = new List<Process>();
        private List<String> _currentSelectedListeners = new List<String>();
        private IDictionary<string, string> _listenersPasswords = new Dictionary<string, string>();
        

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            /* Opening local listeners instances onload */
            foreach (string line in File.ReadLines(@"servers.ini"))
            {
                string[] pack = line.Split(' ');
                string[] endpoint = pack[0].Split(':');

                string server = endpoint[0];
                string port = endpoint[1];
                string password = pack[1];


                string param = server + ' ' + port + ' ' + password;
                string filepath = "../../../../edu-steg-lab5-routing-listener/edu-steg-lab5-routing-listener/bin/Debug/edu-steg-lab5-routing-listener.exe".Replace(@"/", @"\"); ;

                var proc = System.Diagnostics.Process.Start(filepath, param);

                listBoxServers.Items.Add(pack[0]);

                _listenersProcesses.Add(proc);
                _listenersPasswords[pack[0]] = password;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string enc_key;
            string raw_msg;

            byte[] enc_msg_bytes = null;
            byte[] msg_bytes = null;

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            byte[] iv = new byte[16];
            byte[] key_b64_bytes;

            /* Preparing onion structured data */
            for (int i=_currentSelectedListeners.Count-1; i>=0; i--)
            {
                enc_key = _listenersPasswords[_currentSelectedListeners[i]];
                key_b64_bytes = Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(enc_key)).Substring(0, aes.Key.Length));

                if (i != _currentSelectedListeners.Count - 1)
                {
                    raw_msg = " NEXT:" + _currentSelectedListeners[i + 1];
                    byte[] raw_msg_bytes = Encoding.UTF8.GetBytes(raw_msg);
                    byte[] tmp_msg = new byte[msg_bytes.Count() + raw_msg_bytes.Count()];
                    System.Array.Copy(msg_bytes, 0, tmp_msg, 0, msg_bytes.Count());
                    System.Array.Copy(raw_msg_bytes, 0, tmp_msg, msg_bytes.Length, raw_msg_bytes.Length);

                    msg_bytes = tmp_msg;
                }
                else
                {
                    raw_msg = "URL:" + textBoxMessage.Text;
                    msg_bytes = Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(raw_msg)));
                }


                var encryptor = aes.CreateEncryptor(key_b64_bytes, iv);
                enc_msg_bytes = encryptor.TransformFinalBlock(msg_bytes, 0, msg_bytes.Length);
                msg_bytes = enc_msg_bytes;
            }


            /* Sending onion message to first router */
            string[] endpoint_data = _currentSelectedListeners[0].Split(':');
            string server = endpoint_data[0];
            int port = Convert.ToInt32(endpoint_data[1]);

            /* Buffer for response with file */
            byte[] buffor_bytes = new byte[10000000];

            TcpClient client = new TcpClient(server, port);
            NetworkStream stream = client.GetStream();
            stream.Write(enc_msg_bytes, 0, enc_msg_bytes.Length);

            /* Waiting for file in response */
            int x = 0;
            while (x == 0)
            {
                x = stream.Read(buffor_bytes, 0, buffor_bytes.Length);

                byte[] onion_bytes = new byte[x];
                System.Array.Copy(buffor_bytes, onion_bytes, x);

                byte[] decrypted_bytes = null;

                /* Fetching data from onion structure */
                int iterator = 0;
                while (iterator < _currentSelectedListeners.Count)
                {
                    enc_key = _listenersPasswords[_currentSelectedListeners[iterator]];
                    key_b64_bytes = Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(enc_key)).Substring(0, aes.Key.Length));
                    var decryptor = aes.CreateDecryptor(key_b64_bytes, iv);

                    decrypted_bytes = decryptor.TransformFinalBlock(onion_bytes, 0, onion_bytes.Length);
                    onion_bytes = decrypted_bytes;

                    iterator++;
                }

                /* Setting filename */
                int filename_start_pos = textBoxMessage.Text.LastIndexOf('/')+1;
                string filename = textBoxMessage.Text.Substring(filename_start_pos, textBoxMessage.Text.Length - filename_start_pos);
                int query_start_pos;
                if ((query_start_pos = filename.IndexOf('?')) > -1)
                {
                    filename = filename.Substring(0, query_start_pos);
                }

                /* Saving bytes to file on disc */
                var fs = new FileStream("download\\" + filename, FileMode.Create);
                fs.Write(decrypted_bytes, 0, decrypted_bytes.Length);
                fs.Close();

                textBoxResponse.AppendText("File '" + filename + "' saved on disc." + Environment.NewLine);
            }

            stream.Close();

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            /* Shutdown listeners on form closed */
            foreach (Process proc in this._listenersProcesses)
            {
                proc.CloseMainWindow();
                proc.Close();
            }
        }

        private void listBoxServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* Prepare routers path */

            foreach (string listBoxItem in listBoxServers.SelectedItems)
            {
                if (!_currentSelectedListeners.Contains(listBoxItem))
                {
                    _currentSelectedListeners.Add(listBoxItem);
                }
            }

            foreach (string currentPathItem in _currentSelectedListeners)
            {
                if (!listBoxServers.SelectedItems.Contains(currentPathItem))
                {
                    _currentSelectedListeners.RemoveAll(element => element == currentPathItem);
                    break;
                }
            }
        }
    }
}
