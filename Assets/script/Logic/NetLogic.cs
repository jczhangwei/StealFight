using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace stealFight {

    public class NetLogic : BaseLogic {
        private static NetLogic instance;

        public static NetLogic Instance {
            get { return instance ?? (instance = new NetLogic()); }
        }

        string msg = "";
        ManualResetEvent allDone;

        public override void Init() {

        }

        public void Connect() {
            IPHostEntry lipa = Dns.GetHostEntry("127.0.0.1");
            IPEndPoint lep = new IPEndPoint(lipa.AddressList[0], 9000);

            Socket s = new Socket(lep.Address.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);
            try {
                Console.WriteLine("Establishing Connection");
                s.BeginConnect(lep, new AsyncCallback((IAsyncResult result) => { Debug.Log(result); }), s);
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }
    }

}