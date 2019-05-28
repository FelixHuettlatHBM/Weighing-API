﻿// <copyright file="DataStandard.cs" company="Hottinger Baldwin Messtechnik GmbH">
//
// Hbm.Weighing.API, a library to communicate with HBM weighing technology devices  
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

using Hbm.Weighing.API.WTX.Jet;
using Hbm.Weighing.API.WTX.Modbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hbm.Weighing.API.Data
{
    /// <summary>
    /// Implementation of the interface IDataStandard for the standard mode.
    /// The class DataStandard contains the data input word and data output words for the filler mode
    /// of WTX device 120 and 110.
    /// </summary>
    public class DataStandardJet : IDataStandard
    {

        #region ================= privates - standard mode =================

        // Input words :

        private int _input1;
        private int _input2;
        private int _input3;
        private int _input4;

        private int _output1;
        private int _output2;
        private int _output3;
        private int _output4;

        // Output words: 

        private int _limitSwitch1Source;
        private int _limitSwitch1Mode;
        private int _limitSwitch1ActivationLevelLowerBandLimit;
        private int _limitSwitch1HysteresisBandHeight;

        private int _limitSwitch2Source;
        private int _limitSwitch2Mode;
        private int _limitSwitch2ActivationLevelLowerBandLimit;
        private int _limitSwitch2HysteresisBandHeight;

        private int _limitSwitch3Source;
        private int _limitSwitch3Mode;
        private int _limitSwitch3ActivationLevelLowerBandLimit;
        private int _limitSwitch3HysteresisBandHeight;

        private int _limitSwitch4Source;
        private int _limitSwitch4Mode;
        private int _limitSwitch4ActivationLevelLowerBandLimit;
        private int _limitSwitch4HysteresisBandHeight;

        private int _manualTareValue;
        private int _limitValue1Input;
        private int _limitValue1Mode;
        private int _limitValue1ActivationLevelLowerBandLimit;
        private int _limitValue1HysteresisBandHeight;
        private int _limitValue2Source;
        private int _limitValue2Mode;
        private int _limitValue2ActivationLevelLowerBandLimit;
        private int _limitValue2HysteresisBandHeight;

        private int _limitValue3Source;
        private int _limitValue3Mode;
        private int _limitValue3ActivationLevelLowerBandLimit;
        private int _limitValue3HysteresisBandHeight;
        private int _limitValue4Source;
        private int _limitValue4Mode;
        private int _limitValue4ActivationLevelLowerBandLimit;
        private int _limitValue4HysteresisBandHeight;
        private int _calibrationWeight;
        private int _zeroLoad;
        private int _nominalLoad;

        private INetConnection _connection;
        #endregion

        #region =============== constructors & destructors =================
        /// <summary>
        /// Constructor of class DataStandardJet : Initalizes values and connects 
        /// the eventhandler from Connection to the interal update method
        /// </summary>
        public DataStandardJet(INetConnection Connection)
        {
            _connection = Connection;

            _connection.UpdateData += UpdateStandardData;
            Console.WriteLine("DataStandardJet");

            _input1 = 0;
            _input2=0;
            _input3=0;
            _input4=0;

            _output1=0;
            _output2=0;
            _output3=0;
            _output4=0;

            LimitStatus1 = 0;
            LimitStatus2 = 0;
            LimitStatus3 = 0;
            LimitStatus4 = 0;

            WeightMemDay=0;
            WeightMemMonth=0;
            WeightMemYear=0;
            WeightMemSeqNumber=0;
            WeightMemGross=0;
            WeightMemNet=0;

            WeightStorage=0;

            _limitSwitch1Source=0;
            _limitSwitch1Mode=0;
            _limitSwitch1ActivationLevelLowerBandLimit=0;
            _limitSwitch1HysteresisBandHeight=0;

            _limitSwitch2Source=0;
            _limitSwitch2Mode=0;
            _limitSwitch2ActivationLevelLowerBandLimit=0;
            _limitSwitch2HysteresisBandHeight=0;

            _limitSwitch3Source=0;
            _limitSwitch3Mode=0;
            _limitSwitch3ActivationLevelLowerBandLimit=0;
            _limitSwitch3HysteresisBandHeight=0;

            _limitSwitch4Source=0;
            _limitSwitch4Mode=0;
            _limitSwitch4ActivationLevelLowerBandLimit=0;
            _limitSwitch4HysteresisBandHeight=0;

            _manualTareValue = 0;
            _limitValue1Input = 0;
            _limitValue1Mode = 0;
            _limitValue1ActivationLevelLowerBandLimit = 0;
            _limitValue1HysteresisBandHeight = 0;
            _limitValue2Source = 0;
            _limitValue2Mode = 0;
            _limitValue2ActivationLevelLowerBandLimit = 0;
            _limitValue2HysteresisBandHeight = 0;

            _limitValue3Source = 0;
            _limitValue3Mode = 0;
            _limitValue3ActivationLevelLowerBandLimit = 0;
            _limitValue3HysteresisBandHeight = 0;
            _limitValue4Source = 0;
            _limitValue4Mode = 0;
            _limitValue4ActivationLevelLowerBandLimit = 0;
            _limitValue4HysteresisBandHeight = 0;
            _calibrationWeight = 0;
            _zeroLoad = 0;
            _nominalLoad = 0;
    }

        #endregion

        #region =============== Update method - standard mode ==============

        /// <summary>
        /// Updates & converts the values from buffer (Dictionary<string,string>) 
        /// </summary>
        /// <param name="sender">Connection class</param>
        /// <param name="e">EventArgs, Event argument</param>
        public void UpdateStandardData(object sender, EventArgs e)
        {
            try
            {
                _input1 = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Status_digital_input_1));
                _input2 = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Status_digital_input_2));
                _input3 = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Status_digital_input_3));
                _input4 = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Status_digital_input_4));

                _output1 = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Status_digital_output_1));
                _output2 = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Status_digital_output_2));
                _output3 = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Status_digital_output_3));
                _output4 = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Status_digital_output_4));

                LimitStatus1 = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Limit_value_status1));
                LimitStatus2 = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Limit_value_status2));
                LimitStatus3 = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Limit_value_status3));
                LimitStatus4 = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Limit_value_status4));

                WeightStorage = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Storage_weight_mode));

                if (Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Application_mode)) == 0 || Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Application_mode)) == 1)  // If application mode is in standard mode
                {
                    _limitSwitch1Source = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Limit_value_monitoring_liv11));
                    _limitSwitch1Mode = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Signal_source_liv12));
                    _limitSwitch1ActivationLevelLowerBandLimit = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Switch_on_level_liv13));
                    _limitSwitch1HysteresisBandHeight = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Switch_off_level_liv14));

                    _limitSwitch2Source = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Limit_value_monitoring_liv21));
                    _limitSwitch2Mode = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Signal_source_liv22));
                    _limitSwitch2ActivationLevelLowerBandLimit = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Switch_on_level_liv23));
                    _limitSwitch2HysteresisBandHeight = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Switch_off_level_liv24));

                    _limitSwitch3Source = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Limit_value_monitoring_liv31));
                    _limitSwitch3Mode = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Signal_source_liv32));
                    _limitSwitch3ActivationLevelLowerBandLimit = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Switch_on_level_liv33));
                    _limitSwitch3HysteresisBandHeight = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Switch_off_level_liv34));

                    _limitSwitch4Source = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Limit_value_monitoring_liv41));
                    _limitSwitch4Mode = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Signal_source_liv42));
                    _limitSwitch4ActivationLevelLowerBandLimit = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Switch_on_level_liv43));
                    _limitSwitch4HysteresisBandHeight = Convert.ToInt32(_connection.ReadFromBuffer(JetBusCommands.Switch_off_level_liv44));
                }
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("KeyNotFoundException in class DataStandardJet, update method");
                //_connection.CommunicationLog.Invoke(this, new LogEvent((new KeyNotFoundException()).Message));
            }
        }
        #endregion

        #region ============== Get-properties - standard mode ==============

        public int Input1
        {
            get{ return _input1; }
            set
            {
                _connection.Write(JetBusCommands.Status_digital_input_1, value);
                _input1 = value;
            }
        }
        public int Input2
        {
            get{ return _input2; }
            set
            {
                _connection.Write(JetBusCommands.Status_digital_input_2, value);
                _input2 = value;
            }
        }
        public int Input3
        {
            get{ return _input3; }
            set
            {
                _connection.Write(JetBusCommands.Status_digital_input_3, value);
                _input3 = value;
            }
        }
        public int Input4
        {
            get{ return _input4; }
            set
            {
                _connection.Write(JetBusCommands.Status_digital_input_4, value);
                _input4 = value;
            }
        }
        public int Output1
        {
            get{ return _output1; }
            set
            {
                _connection.Write(JetBusCommands.Status_digital_output_1, value);
                _output1 = value;
            }
        }
        public int Output2
        {
            get{ return _output2; }
            set
            {
                _connection.Write(JetBusCommands.Status_digital_output_2, value);
                _output2 = value;
            }
        }
        public int Output3
        {
            get{ return _output3; }
            set
            {
                _connection.Write(JetBusCommands.Status_digital_output_3, value);
                _output3 = value;
            }
        }
        public int Output4
        {
            get{ return _output4; }
            set
            {
                _connection.Write(JetBusCommands.Status_digital_output_4, value);
                _output4 = value;
            }
        }
        public int LimitStatus1 { get; private set; }
        public int LimitStatus2 { get; private set; }
        public int LimitStatus3 { get; private set; }
        public int LimitStatus4 { get; private set; }
        public int WeightMemDay { get; private set; }
        public int WeightMemMonth { get; private set; }
        public int WeightMemYear { get; private set; }
        public int WeightMemSeqNumber { get; private set; }
        public int WeightMemGross { get; private set; }
        public int WeightMemNet { get; private set; }
        public int WeightStorage { get; set; }
        #endregion

        #region ============ Get-/Set-properties - standard mode ===========

        public int LimitSwitch1Source // Type : unsigned integer 8 Bit
        {
            get { return _limitSwitch1Source; }
            set
            {
                  _connection.Write(JetBusCommands.TareValue, value);
                  _manualTareValue = value;
            }
        }
        public int LimitSwitch1Mode // Type : unsigned integer 8 Bit
        {
            get { return _limitSwitch1Mode; }
            set
            {
                _connection.Write(JetBusCommands.Signal_source_liv12, value);
                _limitValue1Input = value;
            }
        }
        public int LimitSwitch1Level // Type : signed integer 32 Bit
        {
            get { return _limitSwitch1ActivationLevelLowerBandLimit; }
            set
            {
                _connection.Write(JetBusCommands.Limit_value_monitoring_liv11, value);
                _limitValue1Mode = value;
            }
        }
        public int LimitSwitch1Hysteresis // Type : signed integer 32 Bit
        {
            get { return _limitSwitch1HysteresisBandHeight; }
            set
            {
                _connection.Write(JetBusCommands.Switch_off_level_liv14, value);
                _limitSwitch1HysteresisBandHeight = value;
            }
        }
        public int LimitSwitch1LowerBandValue // Type : signed integer 32 Bit
        {
            get { return _limitSwitch1ActivationLevelLowerBandLimit; }
            set
            {
                this._connection.Write(JetBusCommands.Switch_on_level_liv13, value);
                _limitValue1ActivationLevelLowerBandLimit = value;
            }
        }
        public int LimitSwitch1BandHeight // Type : signed integer 32 Bit
        {
            get { return _limitSwitch1HysteresisBandHeight; }
            set
            {
                _connection.Write(JetBusCommands.Switch_off_level_liv14, value);
                _limitValue1HysteresisBandHeight = value;
            }
        }
        public int LimitSwitch2Source // Type : unsigned integer 8 Bit
        {
            get { return _limitSwitch2Source; }
            set
            {
                _connection.Write(JetBusCommands.Signal_source_liv22, value);
                _limitValue2Source = value;
            }
        }
        public int LimitSwitch2Mode // Type : unsigned integer 8 Bit
        {
            get { return _limitSwitch2Mode; }
            set
            {
                _connection.Write(JetBusCommands.Limit_value_monitoring_liv21, value);
                _limitValue2Mode = value;
            }
        }
        public int LimitSwitch2Level // Type : signed integer 32 Bit
        {
            get { return _limitSwitch2ActivationLevelLowerBandLimit; }
            set
            {
                _connection.Write(JetBusCommands.Switch_on_level_liv23, value);
                _limitValue2ActivationLevelLowerBandLimit = value;
            }
        }
        public int LimitSwitch2Hysteresis // Type : signed integer 32 Bit
        {
            get { return _limitSwitch2HysteresisBandHeight; }
            set
            {
                _connection.Write(JetBusCommands.Switch_off_level_liv24, value);
                _limitValue2HysteresisBandHeight = value;
            }
        }
        public int LimitSwitch2LowerBandValue // Type : signed integer 32 Bit
        {
            get { return _limitSwitch1ActivationLevelLowerBandLimit; }
            set
            {
                this._connection.Write(JetBusCommands.Switch_on_level_liv13, value);
                _limitSwitch1ActivationLevelLowerBandLimit = value;
            }
        }
        public int LimitSwitch2BandHeight // Type : signed integer 32 Bit
        {
            get { return _limitSwitch1HysteresisBandHeight; }
            set
            {
                _connection.Write(JetBusCommands.Switch_off_level_liv14, value);
                _limitSwitch1HysteresisBandHeight = value;
            }
        }
        public int LimitSwitch3Source // Type : unsigned integer 8 Bit
        {
            get { return _limitSwitch3Source; }
            set
            {
                _connection.Write(JetBusCommands.Signal_source_liv32, value);
                _limitValue3Source = value;
            }
        }

        public int LimitSwitch3Mode // Type : unsigned integer 8 Bit
        {
            get { return _limitSwitch3Mode; }
            set
            {
                _connection.Write(JetBusCommands.Limit_value_monitoring_liv31, value);
                _limitValue3Mode = value;
            }
        }
        public int LimitSwitch3ActivationLevelLowerBandLimit // Type : signed integer 32 Bit
        {
            get { return _limitSwitch3ActivationLevelLowerBandLimit; }
            set
            {
                _connection.Write(JetBusCommands.Switch_on_level_liv33, value);
                _limitValue3ActivationLevelLowerBandLimit = value;
            }
        }
        public int LimitSwitch3Hysteresis // Type : signed integer 32 Bit
        {
            get { return _limitSwitch3HysteresisBandHeight; }
            set
            {
                _connection.Write(JetBusCommands.Switch_off_level_liv34, value);
                _limitValue3HysteresisBandHeight = value;
            }
        }
        public int LimitSwitch3LowerBandValue // Type : signed integer 32 Bit
        {
            get { return _limitSwitch1ActivationLevelLowerBandLimit; }
            set
            {
                _connection.Write(JetBusCommands.Signal_source_liv42, value);
                _limitValue4Source = value;
            }
        }
        public int LimitSwitch3BandHeight // Type : signed integer 32 Bit
        {
            get { return _limitSwitch1HysteresisBandHeight; }
            set
            {
                _connection.Write(JetBusCommands.Limit_value_monitoring_liv41, value);
                _limitValue4Mode = value;
            }
        }
        public int LimitSwitch4Source // Type : unsigned integer 8 Bit
        {
            get { return _limitSwitch4Source; }
            set
            {
                _connection.Write(JetBusCommands.Switch_on_level_liv43, value);
                _limitValue4ActivationLevelLowerBandLimit = value;
            }
        }
        public int LimitSwitch4Mode // Type : unsigned integer 8 Bit
        {
            get { return _limitSwitch4Mode; }
            set
            {
                _connection.Write(JetBusCommands.Switch_off_level_liv44, value);
                _limitValue4HysteresisBandHeight = value;
            }
        }
        public int LimitSwitch4Level // Type : signed integer 32 Bit
        {
            get { return _limitSwitch4ActivationLevelLowerBandLimit; }
            set
            {
                _connection.Write(JetBusCommands.CalibrationWeight, value);
                _calibrationWeight = value;
            }
        }
        public int LimitSwitch4Hysteresis // Type : signed integer 32 Bit
        {
            get { return _limitSwitch4HysteresisBandHeight; }
            set
            {
                _connection.Write(JetBusCommands.LDWZeroValue, value);
                _zeroLoad = value;
            }
        }
        public int LimitSwitch4LowerBandValue // Type : signed integer 32 Bit
        {
            get { return _limitSwitch1ActivationLevelLowerBandLimit; }
            set
            {
                _connection.Write(JetBusCommands.LWTNominalValue, value);
                _nominalLoad = value;
            }
        }
        public int LimitSwitch4BandHeight // Type : signed integer 32 Bit
        {
            get { return _limitSwitch1HysteresisBandHeight; }
            set
            {
                _connection.Write(JetBusCommands.Switch_off_level_liv14, value);
                _limitSwitch1HysteresisBandHeight = value;
            }
        }
        #endregion

    }
}