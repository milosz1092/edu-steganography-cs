using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace edu_steg_lab5_routing_listener
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Title = args[1];
            
            TcpListener server = null;
            try
            {
                string address = args[0];
                Int32 port = Convert.ToInt32(args[1]);

                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                byte[] iv = new byte[16];

                string password = args[2];
                byte[] key_b64_bytes = Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(password)).Substring(0, aes.Key.Length));

                
                IPAddress localAddr = IPAddress.Parse(address);
                server = new TcpListener(localAddr, port);
                server.Start();

                Byte[] bytes = new Byte[10000000];

                byte[] decrypted_bytes;

                int space_pos = 0;
                byte[] enc_to_pass;
                string url;

                string[] next_pack = null;
                string next_server;
                int next_port;

                TcpClient client_for_request = null;
                NetworkStream stream_for_request = null;

                /* For return data */
                byte[] encrypted_data;


                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    NetworkStream stream = client.GetStream();

                    int i = 0;
                    while (i == 0)
                    {
                        i = stream.Read(bytes, 0, bytes.Length);

                        var decryptor = aes.CreateDecryptor(key_b64_bytes, iv);
                        decrypted_bytes = decryptor.TransformFinalBlock(bytes, 0, i);

                        /* Geting end position for encrypted block */
                        space_pos = 0;
                        for (int j = decrypted_bytes.Length - 1; j >= 0; j--)
                        {
                            if (decrypted_bytes[j] == 32)
                            {
                                space_pos = j;
                                break;
                            }
                        }

                        var encryptor = aes.CreateEncryptor(key_b64_bytes, iv);
                        
                        /* Only encrypted data without additional info */
                        if (space_pos == 0)
                        {
                            string decrypted_data = Encoding.UTF8.GetString(Convert.FromBase64String(Encoding.UTF8.GetString(decrypted_bytes, 0, decrypted_bytes.Length)));
                            url = decrypted_data.Substring(decrypted_data.IndexOf(':') + 1);

                            Console.WriteLine("Received encrypted data");
                            Console.WriteLine("URL: {0}", url);
                            Console.WriteLine("Downloading file...");
                            WebClient web_client = new WebClient();
                            byte[] downloaded_data = web_client.DownloadData(url);

                            encrypted_data = encryptor.TransformFinalBlock(downloaded_data, 0, downloaded_data.Length);

                            Console.WriteLine("Making response with encrypted file");
                            // Send back a response.
                            stream.Write(encrypted_data, 0, encrypted_data.Length);
                            web_client.Dispose();

                        }
                        else
                        {
                            /* Passing to next router */
                            next_pack = Encoding.UTF8.GetString(decrypted_bytes, space_pos + 1, decrypted_bytes.Count() - space_pos - 1).Split(':');
                            next_server = next_pack[1];
                            next_port = Convert.ToInt32(next_pack[2]);

                            Console.WriteLine("Received encrypted data");
                            Console.WriteLine("Passing data to next: {0}:{1}", next_pack[1], next_pack[2]);

                            enc_to_pass = new byte[space_pos];
                            System.Array.Copy(decrypted_bytes, 0, enc_to_pass, 0, space_pos);

                            client_for_request = new TcpClient(next_server, next_port);
                            stream_for_request = client_for_request.GetStream();
                            stream_for_request.Write(enc_to_pass, 0, enc_to_pass.Length);

                            /* Waiting for response from next router */
                            int x = 0;
                            while (x == 0)
                            {
                                Console.WriteLine("Received response from next router");
                                x = stream_for_request.Read(bytes, 0, bytes.Length);

                                Console.WriteLine("Encrypting data with own key");
                                encrypted_data = encryptor.TransformFinalBlock(bytes, 0, x);


                                /* Passing data down to previous router/client */
                                Console.WriteLine("Passing data down to previous router/client");
                                stream.Write(encrypted_data, 0, encrypted_data.Length);
                            }

                            stream_for_request.Close();
                            client_for_request.Close();

                        }
                    }

                    stream.Close();
                    client.Close();
                    

                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}
