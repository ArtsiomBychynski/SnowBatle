using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnowBattle
{
    class ScanLocal
    {
        string maskClient = "255.255.255.0";
        string ipClientBuf;
        public void Start()
        {
            string endIPBrodcast = BrodcastIp(IPMyComputer(), maskClient);
            ipClientBuf = endIPBrodcast.Substring(0, endIPBrodcast.LastIndexOfAny(new char[] { '.' }) + 1);
            int iLength = Convert.ToInt32(endIPBrodcast.Substring(ipClientBuf.Length, endIPBrodcast.Length - ipClientBuf.Length)) - 1;
            for (int i = 1; i < iLength; i++)
            {
                new Thread(new ParameterizedThreadStart(pingIP)).Start(i.ToString());
            }
        }


        private void pingIP(object i)
        {
            try
            {
                string ip = ipClientBuf + (string)i;
                int time = 1200;//время отклика
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions(128, true);
                string text = "StartProgram:";
                byte[] data = Encoding.ASCII.GetBytes(text);
                PingReply reply = pingSender.Send(ip, time, data, options);
                //проверка ip
                if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                {
                    Socket mysocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    IPAddress hostIPAddress = IPAddress.Parse(ip);
                    IPEndPoint hostIPEndPoint = new IPEndPoint(hostIPAddress, 9000);
                    var host = (EndPoint)(hostIPEndPoint);
                    mysocket.SendTo(data, data.Length, SocketFlags.None, host);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка");
            }
        }

        public string IPMyComputer()
        {
            // Получение имени компьютера
            string host = Dns.GetHostName();
            // Получение ip-адреса
            IPAddress ip = Dns.GetHostByName(host).AddressList[0];
            return ip.ToString();
        }
        private string BrodcastIp(string IPAdr, string Mask)
        {
            var ip = IPAddress.Parse(IPAdr);
            var mask = IPAddress.Parse(Mask);

            byte[] ipAdrByte = ip.GetAddressBytes();
            byte[] subnetMaskByte = mask.GetAddressBytes();

            if (ipAdrByte.Length != subnetMaskByte.Length)
            {
                return Convert.ToString(-1);
            }
            byte[] brodcastAdr = new byte[ipAdrByte.Length];
            for (int i = 0; i < brodcastAdr.Length; i++)
            {
                brodcastAdr[i] = (byte)(ipAdrByte[i] | (subnetMaskByte[i] ^ 255));
            }
            return new IPAddress(brodcastAdr).ToString();
        }
    }
}
