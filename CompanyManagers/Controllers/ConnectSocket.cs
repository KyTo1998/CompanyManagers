using System;
using System.Collections.Generic;
using System.Net;
using SocketIOClient;
using SocketIOClient.Windows7;

namespace CompanyManagers.Controllers
{
    public delegate void checkEro(string ex);
    public class ConnectSocket
    {
        public SocketIO WIO { get; set; }
        public ConnectSocket()
        {
            try
            {
                var uri = new Uri("https://socket.timviec365.vn/");
                WIO = new SocketIO(uri, new SocketIOOptions
                {
                    AutoUpgrade = true,
                    Reconnection = true,
                    EIO = EngineIO.V4,
                    Query = new Dictionary<string, string>
                    {
                        {"token", "V3" }
                    },
                    ReconnectionDelay = 200,
                    ReconnectionDelayMax = 1500,
                    ReconnectionAttempts = 10,
                });
                string WIOString = WIO.ToString();
                WIO.ConnectAsync();
                //WIO.ClientWebSocketProvider = () => new SystemNetWebSocketsClientWebSocketync();
                WIO.OnError += WIO_OnError;
                WIO.OnReconnected += WIO_OnReconnected;
                WIO.OnConnected += WIO_OnConnected;
            }
            catch (Exception ex)
            {
                //CheckError(ex.Message);
                using (WebClient webclient = new WebClient())
                {
                    /*webclient.QueryString.Add("mesage", ex.Message);
                    webclient.QueryString.Add("messageId", ex.Message);
                    webclient.UploadValuesAsync(new Uri(*//*Properties.Resources.URL_NodeJs*//*UrlApiNew.URL_NodeJs_New + "logs/LogsSocketExceptionWpf"), "POST", webclient.QueryString);*/
                }
            }
        }

        private void WIO_OnConnected(object sender, EventArgs e)
        {
            try
            {
                if (!WIO.Connected)
                    WIO.ConnectAsync();
            }
            catch
            {

            }
        }

        private void WIO_OnReconnected(object sender, int e)
        {
            try
            {
                if (!WIO.Connected)
                    WIO.ConnectAsync();
            }
            catch
            {

            }
        }

        private void WIO_OnError(object sender, string e)
        {
            try
            {
                if (!WIO.Connected)
                    WIO.ConnectAsync();
            }
            catch
            {

            }
        }
    }
}
