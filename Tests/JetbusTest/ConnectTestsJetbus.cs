﻿// <copyright file="ConnectTestsJetbus.cs" company="Hottinger Baldwin Messtechnik GmbH">
//
// WTXGUIsimple, a demo application for HBM Weighing-API  
//
// The MIT License (MIT)
//
// Copyright (C) Hottinger Baldwin Messtechnik GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS
// BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN
// ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// </copyright>

namespace Hbm.Weighing.API.WTX.Jet
{
    using System.Collections;
    using Hbm.Weighing.API;
    using Hbm.Weighing.API.WTX;
    using Hbm.Weighing.API.Data;
    using NUnit.Framework;

    [TestFixture]
    public class ConnectTestsJetbus
    {

        private INetConnection testConnection;

        //private bool connectCallbackCalled;
        //private bool connectCompleted;

        //private int testGrossValue;

        public static IEnumerable Connect_TestCases_Jetbus
        {
            get
            {
                yield return new TestCaseData(Behavior.ConnectionSuccess).Returns(true);
                yield return new TestCaseData(Behavior.ConnectionFail).Returns(false);
            }
        }

        public static IEnumerable Disconnect_Testcases_Jetbus
        {
            get
            {
                yield return new TestCaseData(Behavior.DisconnectionSuccess).Returns(false);
                yield return new TestCaseData(Behavior.DisconnectionFail).Returns(true);
            }
        }

        [SetUp]
        public void Setup()
        {
            //testGrossValue = 0; 

            //this.connectCallbackCalled = false;
            //this.connectCompleted = true;
        }

        [Test, TestCaseSource(typeof(ConnectTestsJetbus), "Connect_TestCases_Jetbus")]
        public bool TestConnectJetbus(Behavior behaviour)
        {        
            testConnection = new TestJetbusConnection(behaviour, "wss://172.19.103.8:443/jet/canopen", "Administrator", "wtx", delegate { return true; },1000);

            WTXJet WTXJetObj = new WTXJet(testConnection, 500, Update);      

            //this.connectCallbackCalled = false;

            WTXJetObj.Connect(this.OnConnect, 100);
            
            return WTXJetObj.IsConnected;
        }

        [Test, TestCaseSource(typeof(ConnectTestsJetbus), "Disconnect_Testcases_Jetbus")]
        public bool TestDisconnectJetbus(Behavior behaviour)
        {
            testConnection = new TestJetbusConnection(behaviour, "wss://172.19.103.8:443/jet/canopen", "Administrator", "wtx", delegate { return true; });

            WTXJet WTXJetObj = new WTXJet(testConnection, 500, Update);

            //this.connectCallbackCalled = false;
            
            WTXJetObj.Connect(this.OnConnect, 100);

            WTXJetObj.Disconnect(this.OnDisconnect);

            return WTXJetObj.IsConnected;
        }

        private void Update(object sender, ProcessDataReceivedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void OnConnect(bool completed)
        {
            //this.connectCallbackCalled = true; 

            //this.connectCompleted = completed;
        }


        private void OnDisconnect(bool completed)
        {
            //this.connectCallbackCalled = false;

            //this.connectCompleted = completed;
        }

    }
}
