﻿

using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBM.Weighing.API.WTX.Modbus
{
    [TestFixture]
    public class CommentMethodsModbusTests
    {

        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable T_UnitValueTestCases
        {
            get
            {
                yield return new TestCaseData(Behavior.t_UnitValue_Fail).Returns(0);
                yield return new TestCaseData(Behavior.t_UnitValue_Success).Returns(2);
            }
        }

        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable KG_UnitValueTestCases
        {
            get
            {
                yield return new TestCaseData(Behavior.kg_UnitValue_Fail).Returns(3);
                yield return new TestCaseData(Behavior.kg_UnitValue_Success).Returns(0);
            }
        }

        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable G_UnitValueTestCases
        {
            get
            {
                yield return new TestCaseData(Behavior.g_UnitValue_Fail).Returns(0);
                yield return new TestCaseData(Behavior.g_UnitValue_Success).Returns(1);
            }
        }

        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable LB_UnitValueTestCases
        {
            get
            {
                yield return new TestCaseData(Behavior.lb_UnitValue_Fail).Returns(0);
                yield return new TestCaseData(Behavior.lb_UnitValue_Success).Returns(3);
            }
        }

        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable NetGrossValueStringComment_0D_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.NetGrossValueStringComment_0D_Fail).ExpectedResult = 0;
                yield return new TestCaseData(Behavior.NetGrossValueStringComment_0D_Success).ExpectedResult = 1;
            }
        }

        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable NetGrossValueStringComment_1D_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.NetGrossValueStringComment_1D_Fail).ExpectedResult = 0;
                yield return new TestCaseData(Behavior.NetGrossValueStringComment_1D_Success).ExpectedResult = 1;
            }
        }

        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable NetGrossValueStringComment_2D_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.NetGrossValueStringComment_2D_Fail).ExpectedResult = 0;
                yield return new TestCaseData(Behavior.NetGrossValueStringComment_2D_Success).ExpectedResult = 1;
            }
        }

        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable NetGrossValueStringComment_3D_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.NetGrossValueStringComment_3D_Fail).ExpectedResult = 0;
                yield return new TestCaseData(Behavior.NetGrossValueStringComment_3D_Success).ExpectedResult = 1;
            }
        }

        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable NetGrossValueStringComment_4D_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.NetGrossValueStringComment_4D_Fail).ExpectedResult = 0;
                yield return new TestCaseData(Behavior.NetGrossValueStringComment_4D_Success).ExpectedResult = 1;
            }
        }

        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable NetGrossValueStringComment_5D_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.NetGrossValueStringComment_5D_Fail).ExpectedResult = 0;
                yield return new TestCaseData(Behavior.NetGrossValueStringComment_5D_Success).ExpectedResult = 1;
            }
        }

        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable NetGrossValueStringComment_6D_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.NetGrossValueStringComment_6D_Fail).ExpectedResult = 0;
                yield return new TestCaseData(Behavior.NetGrossValueStringComment_6D_Success).ExpectedResult = 1;
            }
        }

        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable ScaleRangeStringComment_Range1_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.ScaleRangeStringComment_Range1_Fail).Returns("Range 3");
                yield return new TestCaseData(Behavior.ScaleRangeStringComment_Range1_Success).Returns("Range 1");
            }
        }

        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable ScaleRangeStringComment_Range2_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.ScaleRangeStringComment_Range2_Fail).Returns("Range 1");
                yield return new TestCaseData(Behavior.ScaleRangeStringComment_Range2_Success).Returns("Range 2");
            }
        }

        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable ScaleRangeStringComment_Range3_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.ScaleRangeStringComment_Range3_Fail).Returns("Range 2");
                yield return new TestCaseData(Behavior.ScaleRangeStringComment_Range3_Success).Returns("Range 3");
            }
        }




        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable LimitStatusStringComment_Case0_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.LimitStatusStringComment_Case0_Fail).Returns("Higher than safe load limit");
                yield return new TestCaseData(Behavior.LimitStatusStringComment_Case0_Success).Returns("Weight within limits");
            }
        }
        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable LimitStatusStringComment_Case1_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.LimitStatusStringComment_Case1_Fail).Returns("Higher than maximum capacity");
                yield return new TestCaseData(Behavior.LimitStatusStringComment_Case1_Success).Returns("Lower than minimum");
            }
        }
        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable LimitStatusStringComment_Case2_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.LimitStatusStringComment_Case2_Fail).Returns("Weight within limits");
                yield return new TestCaseData(Behavior.LimitStatusStringComment_Case2_Success).Returns("Higher than maximum capacity");
            }
        }
        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable LimitStatusStringComment_Case3_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.LimitStatusStringComment_Case3_Fail).Returns("Lower than minimum");
                yield return new TestCaseData(Behavior.LimitStatusStringComment_Case3_Success).Returns("Higher than safe load limit");
            }
        }

        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable WeightMovingStringComment_Case0_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.WeightMovingStringComment_Case0_Fail).Returns("1=Weight is moving");
                yield return new TestCaseData(Behavior.WeightMovingStringComment_Case0_Success).Returns("0=Weight is not moving.");
            }
        }

        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable WeightMovingStringComment_Case1_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.WeightMovingStringComment_Case1_Fail).Returns("0=Weight is not moving.");
                yield return new TestCaseData(Behavior.WeightMovingStringComment_Case1_Success).Returns("1=Weight is moving");
            }
        }


        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable WeightTypeStringComment_Case0_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.WeightTypeStringComment_Case0_Fail).Returns("net");
                yield return new TestCaseData(Behavior.WeightTypeStringComment_Case0_Success).Returns("gross");
            }
        }


        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable WeightTypeStringComment_Case1_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.WeightTypeStringComment_Case1_Fail).Returns("gross");
                yield return new TestCaseData(Behavior.WeightTypeStringComment_Case1_Success).Returns("net");
            }
        }


        /*
        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable WeightTypeStringComment_Case0_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.WeightTypeStringComment_Case0_Fail).ExpectedResult = 0;
                yield return new TestCaseData(Behavior.WeightTypeStringComment_Case0_Success).ExpectedResult = 1;
            }
        }


        // Test case source for reading values from the WTX120 device. 
        public static IEnumerable WeightTypeStringComment_Case1_TestCase_Modbus
        {
            get
            {
                yield return new TestCaseData(Behavior.WeightTypeStringComment_Case1_Fail).ExpectedResult = 0;
                yield return new TestCaseData(Behavior.WeightTypeStringComment_Case1_Success).ExpectedResult = 1;
            }
        }
        */

        private ushort[] _data;

        /*
        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "T_UnitValueTestCases")]
        public async Task<int> testUnit_t(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");

            WtxModbus _wtxObj = new WtxModbus(testConnection, 200,update);
           
            _wtxObj.Connect(this.OnConnect, 100);

            await Task.Run(async () =>
            {
                ushort[] result = await testConnection.ReadAsync();
                _wtxObj.OnData(result);
            });


            return _wtxObj.ProcessData.Unit;
        }

        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "KG_UnitValueTestCases")]
        public async Task<int> testUnit_kg(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");

            WtxModbus _wtxObj = new WtxModbus(testConnection, 200,update);

            _wtxObj.Connect(this.OnConnect, 100);

            await Task.Run(async () =>
            {
                ushort[] result = await testConnection.ReadAsync();
                _wtxObj.OnData(result);
            });

            return _wtxObj.ProcessData.Unit;
        }

        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "G_UnitValueTestCases")]
        public async Task<int> testUnit_g(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");

            WtxModbus _wtxObj = new WtxModbus(testConnection, 200,update);

            _wtxObj.Connect(this.OnConnect, 100);

            await Task.Run(async () =>
            {
                ushort[] result = await testConnection.ReadAsync();
                _wtxObj.OnData(result);
            });

            return _wtxObj.ProcessData.Unit;
        }

        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "LB_UnitValueTestCases")]
        public async Task<int> testUnit_lb(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WtxModbus _wtxObj = new WtxModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

            await Task.Run(async () =>
            {
                ushort[] result = await testConnection.ReadAsync();
                _wtxObj.OnData(result);
            });

            return _wtxObj.ProcessData.Unit;
        }
        */
        
        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "NetGrossValueStringComment_0D_TestCase_Modbus")]
        public void testModbus_NetGrossValueStringComment_0D(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WTXModbus _wtxObj = new WTXModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

           //_wtxObj.WriteSync(0, 0x00);    ???

            string strValue = _wtxObj.CurrentWeight;
            double dValue = _wtxObj.ProcessData.GrossValue / Math.Pow(10, _wtxObj.ProcessData.Decimals);

            Assert.AreEqual(dValue.ToString(), strValue);
        }
        

        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "NetGrossValueStringComment_1D_TestCase_Modbus")]
        public void testModbus_NetGrossValueStringComment_1D(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WTXModbus _wtxObj = new WTXModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

            //_wtxObj.WriteSync(0, 0x00);    ???
            
            double dValue = _wtxObj.ProcessData.GrossValue / Math.Pow(10, 1 /*_wtxObj.Decimals*/);

            Assert.AreEqual(dValue.ToString("0.0"), _wtxObj.CurrentWeight);
        }
        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "NetGrossValueStringComment_2D_TestCase_Modbus")]
        public void testModbus_NetGrossValueStringComment_2D(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WTXModbus _wtxObj = new WTXModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

            //_wtxObj.WriteSync(0, 0x00);    ???

            string strValue = _wtxObj.CurrentWeight;
            double dValue = _wtxObj.ProcessData.GrossValue / Math.Pow(10, 2 /*_wtxObj.Decimals*/);

            Assert.AreEqual(dValue.ToString("0.00"), strValue);
        }
        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "NetGrossValueStringComment_3D_TestCase_Modbus")]
        public void testModbus_NetGrossValueStringComment_3D(Behavior behavior)
        {
            INetConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WTXModbus _wtxObj = new WTXModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

            //_wtxObj.WriteSync(0, 0x00);    ???

            string strValue = _wtxObj.CurrentWeight;
            double dValue = _wtxObj.ProcessData.GrossValue / Math.Pow(10, 3 /*_wtxObj.Decimals*/);

            Assert.AreEqual(dValue.ToString("0.000"), strValue);
        }
        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "NetGrossValueStringComment_4D_TestCase_Modbus")]
        public void testModbus_NetGrossValueStringComment_4D(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WTXModbus _wtxObj = new WTXModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

            //_wtxObj.WriteSync(0, 0x00);    ???

            double dValue = _wtxObj.ProcessData.GrossValue / Math.Pow(10, 4 /*_wtxObj.Decimals*/);

            Assert.AreEqual(dValue.ToString("0.0000"), _wtxObj.CurrentWeight);
        }
        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "NetGrossValueStringComment_5D_TestCase_Modbus")]
        public void testModbus_NetGrossValueStringComment_5D(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WTXModbus _wtxObj = new WTXModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

            //_wtxObj.WriteSync(0, 0x00);    ???

            string strValue = _wtxObj.CurrentWeight;
            double dValue = _wtxObj.ProcessData.GrossValue / Math.Pow(10, 5 /*_wtxObj.Decimals*/);

            Assert.AreEqual(dValue.ToString("0.00000"), strValue);
        }
        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "NetGrossValueStringComment_6D_TestCase_Modbus")]
        public void testModbus_NetGrossValueStringComment_6D(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WTXModbus _wtxObj = new WTXModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

            //_wtxObj.WriteSync(0, 0x00);    ???

            string strValue = _wtxObj.CurrentWeight;
            double dValue = _wtxObj.ProcessData.GrossValue / Math.Pow(10, 6 /*_wtxObj.Decimals*/);

            Assert.AreEqual(dValue.ToString("0.000000"), strValue);
        }
        /*
        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "ScaleRangeStringComment_Range1_TestCase_Modbus")]
        //public async Task<string> testModbus_ScaleRangeStringComment_Range1(Behavior behavior)
        public async Task<string> testModbus_ScaleRangeStringComment_Range1(Behavior behavior)
        {            
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WtxModbus _wtxObj = new WtxModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

            await Task.Run(async () =>
            {
                 ushort[] result = await testConnection.ReadAsync();
                 _wtxObj.OnData(result);
            });

            return _wtxObj.ScaleRangeStringComment();         
        }

        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "ScaleRangeStringComment_Range2_TestCase_Modbus")]
        public async Task<string> testModbus_ScaleRangeStringComment_Range2(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WtxModbus _wtxObj = new WtxModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

            await Task.Run(async () =>
            {
                ushort[] result = await testConnection.ReadAsync();
                _wtxObj.OnData(result);
            });

            return _wtxObj.ScaleRangeStringComment();
        }

        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "ScaleRangeStringComment_Range3_TestCase_Modbus")]
        public async Task<string> testModbus_ScaleRangeStringComment_Range3(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WtxModbus _wtxObj = new WtxModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

            await Task.Run(async () =>
            {
                ushort[] result = await testConnection.ReadAsync();
                _wtxObj.OnData(result);
            });

            return _wtxObj.ScaleRangeStringComment();
        }
       
        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "LimitStatusStringComment_Case0_TestCase_Modbus")]
        public async Task<string> testModbus_LimitStatusStringComment_Case0(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WtxModbus _wtxObj = new WtxModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

            await Task.Run(async () =>
            {
                ushort[] result = await testConnection.ReadAsync();
                _wtxObj.OnData(result);
            });

            return _wtxObj.LimitStatusStringComment();
        }

        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "LimitStatusStringComment_Case1_TestCase_Modbus")]
        public async Task<string> testModbus_LimitStatusStringComment_Case1(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WtxModbus _wtxObj = new WtxModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

            await Task.Run(async () =>
            {
                ushort[] result = await testConnection.ReadAsync();
                _wtxObj.OnData(result);
            });

            return _wtxObj.LimitStatusStringComment();
        }

        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "LimitStatusStringComment_Case2_TestCase_Modbus")]
        public async Task<string> testModbus_LimitStatusStringComment_Case2(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WtxModbus _wtxObj = new WtxModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

            await Task.Run(async () =>
            {
                ushort[] result = await testConnection.ReadAsync();
                _wtxObj.OnData(result);
            });

            return _wtxObj.LimitStatusStringComment();
        }

        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "LimitStatusStringComment_Case3_TestCase_Modbus")]
        public async Task<string> testModbus_LimitStatusStringComment_Case3(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WtxModbus _wtxObj = new WtxModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

            await Task.Run(async () =>
            {
                ushort[] result = await testConnection.ReadAsync();
                _wtxObj.OnData(result);
            });

            return _wtxObj.LimitStatusStringComment();
        }   
        
        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "WeightMovingStringComment_Case0_TestCase_Modbus")]
        public async Task<string> testModbus_WeightMovingStringComment_Case0(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WtxModbus _wtxObj = new WtxModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

            await Task.Run(async () =>
            {
                ushort[] result = await testConnection.ReadAsync();
                _wtxObj.OnData(result);
            });

            return _wtxObj.WeightMovingStringComment();
        }
        
        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "WeightMovingStringComment_Case1_TestCase_Modbus")]
        public async Task<string> testModbus_WeightMovingStringComment_Case1(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WtxModbus _wtxObj = new WtxModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

            await Task.Run(async () =>
            {
                ushort[] result = await testConnection.ReadAsync();
                _wtxObj.OnData(result);
            });

            return _wtxObj.WeightMovingStringComment();
        }
        
        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "WeightTypeStringComment_Case0_TestCase_Modbus")]
        public async Task<string> testModbus_WeightTypeStringComment_Case0(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WtxModbus _wtxObj = new WtxModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

            await Task.Run(async () =>
            {
                ushort[] result = await testConnection.ReadAsync();
                _wtxObj.OnData(result);
            });

            return _wtxObj.WeightTypeStringComment();
        }
        

        [Test, TestCaseSource(typeof(CommentMethodsModbusTests), "WeightTypeStringComment_Case1_TestCase_Modbus")]
        public async Task<string> testModbus_WeightTypeStringComment_Case1(Behavior behavior)
        {
            TestModbusTCPConnection testConnection = new TestModbusTCPConnection(behavior, "172.19.103.8");
            WtxModbus _wtxObj = new WtxModbus(testConnection, 200,update);
            _wtxObj.Connect(this.OnConnect, 100);

            await Task.Run(async () =>
            {
                ushort[] result = await testConnection.ReadAsync();
                _wtxObj.OnData(result);
                //_wtxObj.ProcessData.UpdateProcessData(this, new DataEventArgs(testConnection.AllData));
            });

            return _wtxObj.WeightTypeStringComment();
        }
        */
        private void update(object sender, ProcessDataReceivedEventArgs e)
        {
        }
        private void OnConnect(bool obj)
        {
        }
        
    }
}
