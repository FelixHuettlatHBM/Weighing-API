﻿
namespace HBM.Weighing.API.WTX.Modbus
{
    using HBM.Weighing.API;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public enum Behavior
    {

         ConnectionFail, 
         ConnectionSuccess,

         DisconnectionFail,
         DisconnectionSuccess,
         
         ReadFail,
         ReadSuccess,

         WriteFail,
         WriteSuccess,

         WriteSyncFail,
         WriteSyncSuccess,

         WriteArrayFail,
         WriteArraySuccess,

         MeasureZeroFail,
         MeasureZeroSuccess,

         TareFail,
         TareSuccess,

         AsyncWriteBackgroundworkerFail,
         AsyncWriteBackgroundworkerSuccess,

         HandshakeFail,
         HandshakeSuccess,

         CalibrationFail,
         CalibrationSuccess,

         InStandardMode,
         InFillerMode,

         LogEvent_Fail,
         LogEvent_Success,

         t_UnitValue_Fail,
         t_UnitValue_Success,
         kg_UnitValue_Success,
         kg_UnitValue_Fail,
         g_UnitValue_Success,
         g_UnitValue_Fail,
         lb_UnitValue_Success,
         lb_UnitValue_Fail,

         NetGrossValueStringComment_0D_Fail,
         NetGrossValueStringComment_0D_Success,
         NetGrossValueStringComment_1D_Fail,
         NetGrossValueStringComment_1D_Success,
         NetGrossValueStringComment_2D_Fail,
         NetGrossValueStringComment_2D_Success,
         NetGrossValueStringComment_3D_Fail,
         NetGrossValueStringComment_3D_Success,
         NetGrossValueStringComment_4D_Fail,
         NetGrossValueStringComment_4D_Success,
         NetGrossValueStringComment_5D_Fail,
         NetGrossValueStringComment_5D_Success,
         NetGrossValueStringComment_6D_Fail,
         NetGrossValueStringComment_6D_Success,

         ScaleRangeStringComment_Range1_Fail,
         ScaleRangeStringComment_Range1_Success,
         ScaleRangeStringComment_Range2_Fail,
         ScaleRangeStringComment_Range2_Success,
         ScaleRangeStringComment_Range3_Fail,
         ScaleRangeStringComment_Range3_Success,

         LimitStatusStringComment_Case0_Fail,
         LimitStatusStringComment_Case0_Success,
         LimitStatusStringComment_Case1_Fail,
         LimitStatusStringComment_Case1_Success,
         LimitStatusStringComment_Case2_Fail,
         LimitStatusStringComment_Case2_Success,
         LimitStatusStringComment_Case3_Fail,
         LimitStatusStringComment_Case3_Success,

         WeightMovingStringComment_Case0_Fail,
         WeightMovingStringComment_Case0_Success,
         WeightMovingStringComment_Case1_Fail,
         WeightMovingStringComment_Case1_Success,

         WeightTypeStringComment_Case0_Fail,
         WeightTypeStringComment_Case0_Success,
         WeightTypeStringComment_Case1_Fail,
         WeightTypeStringComment_Case1_Success,

         WriteHandshakeTestSuccess,
         WriteHandshakeTestFail,

         GrosMethodTestSuccess,
         GrosMethodTestFail, 

         TareMethodTestSuccess,
         TareMethodTestFail,

         ZeroMethodTestSuccess,
         ZeroMethodTestFail,

         AdjustingZeroMethodSuccess,
         AdjustingZeroMethodFail,

         AdjustNominalMethodTestSuccess,
         AdjustNominalMethodTestFail,

         ActivateDataMethodTestSuccess,
         ActivateDataMethodTestFail,

         ManualTaringMethodTestSuccess,
         ManualTaringMethodTestFail,

         ClearDosingResultsMethodTestSuccess,
         ClearDosingResultsMethodTestFail,

         AbortDosingMethodTestSuccess,
         AbortDosingMethodTestFail,

         StartDosingMethodTestSuccess,
         StartDosingMethodTestFail,
 
         RecordWeightMethodTestSuccess,
         RecordWeightMethodTestFail,

         ManualRedosingMethodTestSuccess,
         ManualRedosingMethodTestFail,

         WriteS32ArrayTestSuccess,
         WriteS32ArrayTestFail,
 
         WriteU16ArrayTestSuccess,
         WriteU16ArrayTestFail,

         ResetTimerTestSuccess,
        //ResetTimerTestFail,

         WriteU08ArrayTestSuccess,
         WriteU08ArrayTestFail,

         UpdateOutputTestSuccess,
         UpdateOutputTestFail,

         WriteLimitValue1ModeTestSuccess,
         WriteLimitValue1ModeTestFail,
         WriteLimitValue2ModeTestSuccess,
         WriteLimitValue2ModeTestFail,
         WriteLimitValue3ModeTestSuccess,
         WriteLimitValue3ModeTestFail,
         WriteLimitValue4ModeTestSuccess,
         WriteLimitValue4ModeTestFail,
    }

    public class TestModbusTCPConnection : INetConnection, IDisposable
    {
        private Behavior behavior;

        private ushort arrayElement1;
        private ushort arrayElement2;
        private ushort arrayElement3;
        private ushort arrayElement4;

        private bool _connected;

        private ushort[] _dataWTX;

        public int command; 

        public event EventHandler BusActivityDetection;
        public event EventHandler<DataEventArgs> IncomingDataReceived;
        public event EventHandler<DataEventArgs> UpdateDataClasses;
        
        private Dictionary<string, int> _dataIntegerBuffer;

        private ICommands _commands;

        private string IP;
        private int interval;
        private int wordNumberIndex; 

        private int numPoints;

        public LogEvent _logObj;

        public TestModbusTCPConnection(Behavior behavior,string ipAddress) 
        {
            _dataWTX = new ushort[38];
            // size of 38 elements for the standard and filler application mode.            

            _commands = new ModbusCommands();

             _dataIntegerBuffer = new Dictionary<string, int>();

            this.CreateDictionary();
           
            this.behavior = behavior;

            this.numPoints = 6;

            for (int index = 0; index < _dataWTX.Length; index++)
                _dataWTX[index] = 0x00;

            _dataWTX[0] = 0x00;
            _dataWTX[1] = 0x2710;
            _dataWTX[2] = 0x00;
            _dataWTX[3] = 0x2710;
            _dataWTX[4] = 0x00;
            _dataWTX[5] = 0x00;
        }


        public void Connect()
        {
            switch(this.behavior)
            {
                case Behavior.ConnectionFail:
                    _connected = false;
                    break;

                case Behavior.ConnectionSuccess:
                    _connected = true;
                    break;

                default:
                    _connected = false;
                    break; 
            }

            Write(Convert.ToString(0), 0);
    }

        public bool IsConnected
        {
            get
            {
                return this._connected;
            }
            set
            {
                this._connected = value;
            }
        }

        public void Disconnect()
        {
            switch (this.behavior)
            {
                case Behavior.DisconnectionFail:
                    _connected = true;
                    break;

                case Behavior.DisconnectionSuccess:
                    _connected = false;
                    break;

                default:
                    _connected = true;
                    break;
            }
        }


        private void CreateDictionary()
        {
            _dataIntegerBuffer.Add(_commands.NET_VALUE, 0);
            _dataIntegerBuffer.Add(_commands.GROSS_VALUE, 0);

            _dataIntegerBuffer.Add(_commands.WEIGHING_DEVICE_1_WEIGHT_STATUS, 0);
            _dataIntegerBuffer.Add(_commands.UNIT_PREFIX_FIXED_PARAMETER, 0);

            _dataIntegerBuffer.Add(_commands.FINE_FLOW_CUT_OFF_POINT, 0);
            _dataIntegerBuffer.Add(_commands.COARSE_FLOW_CUT_OFF_POINT, 0);
            _dataIntegerBuffer.Add(_commands.DECIMALS, 0);
            _dataIntegerBuffer.Add(_commands.APPLICATION_MODE, 0);
            _dataIntegerBuffer.Add(_commands.SCALE_COMMAND_STATUS, 0);

            _dataIntegerBuffer.Add(_commands.COARSE_FLOW_MONITORING, 0);
            _dataIntegerBuffer.Add(_commands.FINE_FLOW_MONITORING, 0);
            _dataIntegerBuffer.Add(_commands.EMPTYING_MODE, 0);
            _dataIntegerBuffer.Add(_commands.MAXIMAL_DOSING_TIME, 0);

            _dataIntegerBuffer.Add(_commands.UPPER_TOLERANCE_LIMIT, 0);
            _dataIntegerBuffer.Add(_commands.LOWER_TOLERANCE_LIMIT, 0);

            //_dataIntegerBuffer.Add(_commands.DOSING_STATE, 0);
            //_dataIntegerBuffer.Add(_commands.DOSING_RESULT, 0);

            _dataIntegerBuffer.Add(_commands.DOSING_TIME, 0);
            _dataIntegerBuffer.Add(_commands.COARSE_FLOW_TIME, 0);
            _dataIntegerBuffer.Add(_commands.FINE_FLOW_TIME, 0);
            _dataIntegerBuffer.Add(_commands.RANGE_SELECTION_PARAMETER, 0);

            _dataIntegerBuffer.Add(_commands.STATUS_DIGITAL_INPUT_1, 0);
            _dataIntegerBuffer.Add(_commands.STATUS_DIGITAL_INPUT_2, 0);
            _dataIntegerBuffer.Add(_commands.STATUS_DIGITAL_INPUT_3, 0);
            _dataIntegerBuffer.Add(_commands.STATUS_DIGITAL_INPUT_4, 0);
            _dataIntegerBuffer.Add(_commands.STATUS_DIGITAL_OUTPUT_1, 0);
            _dataIntegerBuffer.Add(_commands.STATUS_DIGITAL_OUTPUT_2, 0);
            _dataIntegerBuffer.Add(_commands.STATUS_DIGITAL_OUTPUT_3, 0);
            _dataIntegerBuffer.Add(_commands.STATUS_DIGITAL_OUTPUT_4, 0);

            _dataIntegerBuffer.Add(_commands.LIMIT_VALUE, 0);

            _dataIntegerBuffer.Add(_commands.LIMIT_VALUE_MONITORING_LIV11, 0); ;
            _dataIntegerBuffer.Add(_commands.SIGNAL_SOURCE_LIV12, 0);
            _dataIntegerBuffer.Add(_commands.SWITCH_ON_LEVEL_LIV13, 0);
            _dataIntegerBuffer.Add(_commands.SWITCH_OFF_LEVEL_LIV14, 0);

            _dataIntegerBuffer.Add(_commands.LIMIT_VALUE_MONITORING_LIV21, 0);
            _dataIntegerBuffer.Add(_commands.SIGNAL_SOURCE_LIV22, 0);
            _dataIntegerBuffer.Add(_commands.SWITCH_ON_LEVEL_LIV23, 0);
            _dataIntegerBuffer.Add(_commands.SWITCH_OFF_LEVEL_LIV24, 0); ;

            _dataIntegerBuffer.Add(_commands.LIMIT_VALUE_MONITORING_LIV31, 0);
            _dataIntegerBuffer.Add(_commands.SIGNAL_SOURCE_LIV32, 0);
            _dataIntegerBuffer.Add(_commands.SWITCH_ON_LEVEL_LIV33, 0);
            _dataIntegerBuffer.Add(_commands.SWITCH_OFF_LEVEL_LIV34, 0);

            _dataIntegerBuffer.Add(_commands.LIMIT_VALUE_MONITORING_LIV41, 0);
            _dataIntegerBuffer.Add(_commands.SIGNAL_SOURCE_LIV42, 0);
            _dataIntegerBuffer.Add(_commands.SWITCH_ON_LEVEL_LIV43, 0); ;
            _dataIntegerBuffer.Add(_commands.SWITCH_OFF_LEVEL_LIV44, 0);
        }


        private void UpdateDictionary()
        {
            // Process data : 

            _dataIntegerBuffer[_commands.NET_VALUE] = _dataWTX[1] + (_dataWTX[0] << 16);
            _dataIntegerBuffer[_commands.GROSS_VALUE] = _dataWTX[3] + (_dataWTX[2] << 16);
            _dataIntegerBuffer[_commands.WEIGHING_DEVICE_1_WEIGHT_STATUS] = _dataWTX[4];
            _dataIntegerBuffer[_commands.SCALE_COMMAND_STATUS] = _dataWTX[5];                       // status -> Measured value status
            _dataIntegerBuffer[_commands.STATUS_DIGITAL_INPUT_1] = _dataWTX[6];
            _dataIntegerBuffer[_commands.STATUS_DIGITAL_OUTPUT_1] = _dataWTX[7];
            _dataIntegerBuffer[_commands.LIMIT_VALUE] = _dataWTX[8];
            _dataIntegerBuffer[_commands.FINE_FLOW_CUT_OFF_POINT] = _dataWTX[20];
            _dataIntegerBuffer[_commands.COARSE_FLOW_CUT_OFF_POINT] = _dataWTX[22];

            _dataIntegerBuffer[_commands.APPLICATION_MODE] = _dataWTX[5] & 0x1;                      // application mode 
            _dataIntegerBuffer[_commands.DECIMALS] = (_dataWTX[5] & 0x70) >> 4;                      // decimals
            _dataIntegerBuffer[_commands.UNIT_PREFIX_FIXED_PARAMETER] = (_dataWTX[5] & 0x180) >> 7;  // unit

            _dataIntegerBuffer[_commands.COARSE_FLOW_MONITORING] = _dataWTX[8] & 0x1;         //_coarseFlow
            _dataIntegerBuffer[_commands.FINE_FLOW_MONITORING] = ((_dataWTX[8] & 0x2) >> 1);  // _fineFlow

            _dataIntegerBuffer[_commands.EMPTYING_MODE] = ((_dataWTX[8] & 0x10) >> 4);
            _dataIntegerBuffer[_commands.MAXIMAL_DOSING_TIME] = ((_dataWTX[8] & 0x100) >> 8);
            _dataIntegerBuffer[_commands.UPPER_TOLERANCE_LIMIT] = ((_dataWTX[8] & 0x400) >> 10);
            _dataIntegerBuffer[_commands.LOWER_TOLERANCE_LIMIT] = ((_dataWTX[8] & 0x800) >> 11);
            _dataIntegerBuffer[_commands.STATUS_DIGITAL_INPUT_1] = ((_dataWTX[8] & 0x4000) >> 14);

            _dataIntegerBuffer[_commands.DOSING_RESULT] = _dataWTX[12];
            _dataIntegerBuffer[_commands.MEAN_VALUE_DOSING_RESULTS] = _dataWTX[14];
            _dataIntegerBuffer[_commands.STANDARD_DEVIATION] = _dataWTX[16];
            _dataIntegerBuffer[_commands.DOSING_TIME] = _dataWTX[24];                 // _currentDosingTime = _dataWTX[24];

            _dataIntegerBuffer[_commands.COARSE_FLOW_TIME] = _dataWTX[25];            // _currentCoarseFlowTime
            _dataIntegerBuffer[_commands.FINE_FLOW_TIME] = _dataWTX[26];              // _currentFineFlowTime
            _dataIntegerBuffer[_commands.RANGE_SELECTION_PARAMETER] = _dataWTX[27];   // _parameterSetProduct

            _dataIntegerBuffer[_commands.LIMIT_VALUE] = _dataWTX[8];

            // Standard data: Missing ID's
            /*
                _limitStatus1 = (_dataWTX[8] & 0x1); ;
                _limitStatus2 = ((_dataWTX[8] & 0x2) >> 1);
                _limitStatus3 = ((_dataWTX[8] & 0x4) >> 2);
                _limitStatus4 = ((_dataWTX[8] & 0x8) >> 3);
                _weightMemDay = (_dataWTX[9]);
                _weightMemMonth = (_dataWTX[10]);
                _weightMemYear = (_dataWTX[11]);
                _weightMemSeqNumber = (_dataWTX[12]);
                _weightMemGross = (_dataWTX[13]);
                _weightMemNet = (_dataWTX[14]);
           */

            // Filler data: Missing ID's
            /*
                _ready = ((_dataWTX[8] & 0x4) >> 2);
                _reDosing = ((_dataWTX[8] & 0x8) >> 3)
                _emptying = ((_dataWTX[8] & 0x10) >> 4);
                _flowError = ((_dataWTX[8] & 0x20) >> 5);
                _alarm = ((_dataWTX[8] & 0x40) >> 6);
                _adcOverUnderload = ((_dataWTX[8] & 0x80) >> 7);           
                _legalForTradeOperation = ((_dataWTX[8] & 0x200) >> 9);
                _statusInput1 = ((_dataWTX[8] & 0x4000) >> 14);
                _generalScaleError = ((_dataWTX[8] & 0x8000) >> 15);
                _fillingProcessStatus = _dataWTX[9];
                _numberDosingResults = _dataWTX[11];          
                _totalWeight = _dataWTX[18];
            */

        }

        public int Read(object index)
        {
            switch (this.behavior)
            {
                case Behavior.WriteHandshakeTestSuccess:

                    if (_dataWTX[4] == 0x0000)
                        _dataWTX[4] = 0x4000;
                    else
                        if (_dataWTX[4] == 0x4000)
                        _dataWTX[4] = 0x0000;
                    break;

                case Behavior.InFillerMode:

                    //data word for a application mode being in filler mode: Bit .0-1 = 1 || 2 (2 is the given value for filler mode according to the manual, but actually it is 1.)
                    _dataWTX[5] = 0x1;
                    break;

                case Behavior.InStandardMode:

                    //data word for a application mode being in standard mode, not in filler mode: Bit .0-1 = 0
                    _dataWTX[5] = 0x00;

                    break;

                case Behavior.CalibrationFail:

                    //Handshake bit:

                    if (_dataWTX[5] >> 14 == 0)
                        _dataWTX[5] = 0x4000;
                    else if (_dataWTX[5] >> 14 == 1)
                        _dataWTX[5] = 0x0000;

                    break;

                case Behavior.CalibrationSuccess:

                    //Handshake bit:

                    if (_dataWTX[5] >> 14 == 0)
                        _dataWTX[5] = 0x4000;
                    else if (_dataWTX[5] >> 14 == 1)
                        _dataWTX[5] = 0x0000;

                    break;

                case Behavior.MeasureZeroFail:

                    // Net value in hexadecimal: 
                    _dataWTX[0] = 0x00;
                    _dataWTX[1] = 0x2710;

                    // Gross value in hexadecimal:
                    _dataWTX[2] = 0x00;
                    _dataWTX[3] = 0x2710;

                    //Handshake bit:
                    if (_dataWTX[5] >> 14 == 0)
                        _dataWTX[5] = 0x4000;

                    else if (_dataWTX[5] >> 14 == 1)
                        _dataWTX[5] = 0x0000;

                    break;

                case Behavior.MeasureZeroSuccess:

                    // Net value in hexadecimal: 
                    _dataWTX[0] = 0x00;
                    _dataWTX[1] = 0x00;

                    // Gross value in hexadecimal:
                    _dataWTX[2] = 0x00;
                    _dataWTX[3] = 0x00;

                    //Handshake bit:
                    if (_dataWTX[5] >> 14 == 0)
                        _dataWTX[5] = 0x4000;
                    else if (_dataWTX[5] >> 14 == 1)
                        _dataWTX[5] = 0x0000;

                    break;

                case Behavior.NetGrossValueStringComment_0D_Success:
                    _dataWTX[5] = 0x0000;
                    break;

                case Behavior.NetGrossValueStringComment_1D_Success:
                    _dataWTX[5] = 0x10;
                    break;

                case Behavior.NetGrossValueStringComment_2D_Success:
                    _dataWTX[5] = 0x20;
                    break;

                case Behavior.NetGrossValueStringComment_3D_Success:
                    _dataWTX[5] = 0x30;
                    break;

                case Behavior.NetGrossValueStringComment_4D_Success:
                    _dataWTX[5] = 0x40;
                    break;

                case Behavior.NetGrossValueStringComment_5D_Success:
                    _dataWTX[5] = 0x50;
                    break;

                case Behavior.NetGrossValueStringComment_6D_Success:
                    _dataWTX[5] = 0x60;
                    break;
                    
                case Behavior.WriteSyncSuccess:

                    this.command = 0x100;

                    //Handshake bit:
                    if (_dataWTX[5] >> 14 == 0)
                        _dataWTX[5] = 0x4000;
                    else if (_dataWTX[5] >> 14 == 1)
                        _dataWTX[5] = 0x0000;

                    break;

                case Behavior.WriteSyncFail:

                    this.command = 0;

                    //Handshake bit:
                    if (_dataWTX[5] >> 14 == 0)
                        _dataWTX[5] = 0x4000;
                    else if (_dataWTX[5] >> 14 == 1)
                        _dataWTX[5] = 0x0000;

                    break;
                    
                case Behavior.WriteFail:

                    this.command = 0;

                    //Handshake bit:
                    if (_dataWTX[5] >> 14 == 0)
                        _dataWTX[5] = 0x4000;
                    else if (_dataWTX[5] >> 14 == 1)
                        _dataWTX[5] = 0x0000;

                    break;

                default:
                    /*
                    for (int index = 0; index < _dataWTX.Length; index++)
                    {
                        _dataWTX[index] = 0;
                    }
                    _logObj = new LogEvent("Read failed : Registers have not been read");
                    BusActivityDetection?.Invoke(this, _logObj);
                    */
                    break;
            }

            _dataIntegerBuffer["0"] = 1;

            this.UpdateDictionary();
            // Updata data in data classes : 
            this.UpdateDataClasses?.Invoke(this, new DataEventArgs(this._dataIntegerBuffer));

            return _dataWTX[Convert.ToInt16(index)];
        }

        public int getCommand
        {
            get { return this.command; }
        }

        public void Write(string index, int data)
        {
            switch (this.behavior)
            {
                case Behavior.UpdateOutputTestSuccess:
                    this.command = 0x800;
                    break;

                case Behavior.UpdateOutputTestFail:
                    this.command = 0x00;
                    break;

                case Behavior.WriteU08ArrayTestSuccess:
                    this.wordNumberIndex = (ushort)Convert.ToUInt16(index);
                    this.arrayElement1 = (ushort)data;
                    break;

                case Behavior.WriteU08ArrayTestFail:
                    this.wordNumberIndex = 0;
                    this.arrayElement1 = 0;
                    break;
                case Behavior.WriteU16ArrayTestSuccess:
                    this.wordNumberIndex = (ushort)Convert.ToUInt16(index);
                    this.arrayElement1 = (ushort)data;
                    break;
                case Behavior.WriteU16ArrayTestFail:
                    this.wordNumberIndex = 0;
                    this.arrayElement1 = 0;
                    break;
                case Behavior.GrosMethodTestSuccess:
                    this.command = data;
                    break;
                case Behavior.GrosMethodTestFail:
                    this.command = 0;
                    break;
                case Behavior.TareMethodTestSuccess:
                    this.command = data;
                    break;
                case Behavior.TareMethodTestFail:
                    this.command = 0;
                    break;

                case Behavior.AdjustingZeroMethodSuccess:
                    this.command = data;
                    break;
                case Behavior.AdjustingZeroMethodFail:
                    this.command = 0;
                    break;
                case Behavior.AdjustNominalMethodTestSuccess:
                    this.command = data;
                    break;
                case Behavior.AdjustNominalMethodTestFail:
                    this.command = 0;
                    break;
                case Behavior.ActivateDataMethodTestSuccess:
                    this.command = data;
                    break;
                case Behavior.ActivateDataMethodTestFail:
                    this.command = 0;
                    break;
                case Behavior.ManualTaringMethodTestSuccess:
                    this.command = data;
                    break;
                case Behavior.ManualTaringMethodTestFail:
                    this.command = 0;
                    break;
                case Behavior.ClearDosingResultsMethodTestSuccess:
                    this.command = data;
                    break;
                case Behavior.ClearDosingResultsMethodTestFail:
                    this.command = 0;
                    break;

                case Behavior.StartDosingMethodTestSuccess:
                    command = data;
                    break;
                case Behavior.StartDosingMethodTestFail:
                    this.command = 0;
                    break;
                case Behavior.RecordWeightMethodTestSuccess:
                    this.command = data;
                    break;
                case Behavior.RecordWeightMethodTestFail:
                    this.command = 0;
                    break;
                case Behavior.ManualRedosingMethodTestSuccess:
                    this.command = data;
                    break;
                case Behavior.ManualRedosingMethodTestFail:
                    this.command = 0;
                    break;
                case Behavior.WriteHandshakeTestSuccess:

                    if (_dataWTX[4] == 0x0000)
                    {
                        this.command = data;
                        _dataWTX[4] = 0x4000;
                    }
                    else
                    if (_dataWTX[4] == 0x4000)
                    {
                        this.command = 0x0;
                        _dataWTX[4] = 0x0000;
                    }

                    break;

                case Behavior.WriteHandshakeTestFail:
                    _dataWTX[4] = 0x0000;
                    break;

                case Behavior.InFillerMode:
                    //data word for a application mode being in filler mode: Bit .0-1 = 1 || 2 (2 is the given value for filler mode according to the manual, but actually it is 1.)
                    _dataWTX[5] = 0x1;
                    break;

                case Behavior.InStandardMode:
                    //data word for a application mode being in standard mode, not in filler mode: Bit .0-1 = 0
                    _dataWTX[5] = 0x00;
                    break;

                case Behavior.CalibrationFail:
                    this.command = 0;
                    break;

                case Behavior.CalibrationSuccess:
                    this.command = data;
                    break;

                case Behavior.WriteSyncSuccess:

                    this.command = data;

                    //Handshake bit:
                    if (_dataWTX[5] >> 14 == 0)
                        _dataWTX[5] = 0x4000;
                    else if (_dataWTX[5] >> 14 == 1)
                        _dataWTX[5] = 0x0000;

                    break;

                case Behavior.WriteSyncFail:

                    this.command = 0x100;

                    //Handshake bit:
                    if (_dataWTX[5] >> 14 == 0)
                        _dataWTX[5] = 0x4000;
                    else if (_dataWTX[5] >> 14 == 1)
                        _dataWTX[5] = 0x0000;

                    break;
                    
                case Behavior.WriteFail:
                    this.command = 0x2;
                    break;

                case Behavior.WriteSuccess:
                    command = 0;
                    break;

                case Behavior.HandshakeSuccess:
                    //Handshake bit:
                    if (_dataWTX[5] >> 14 == 0)
                        _dataWTX[5] = 0x4000;
                    else if (_dataWTX[5] >> 14 == 1)
                        _dataWTX[5] = 0x0000;
                    break;

                case Behavior.HandshakeFail:
                    //Handshake bit:
                    if (_dataWTX[5] >> 14 == 0)
                        _dataWTX[5] = 0x4000;
                    else if (_dataWTX[5] >> 14 == 1)
                        _dataWTX[5] = 0x0000;
                    break;

                case Behavior.WriteLimitValue1ModeTestSuccess:
                    this.command = 4; 
                    break;
                case Behavior.WriteLimitValue1ModeTestFail:
                    this.command = 0;
                    break;
                case Behavior.WriteLimitValue2ModeTestSuccess:
                    this.command = 11;
                    break;
                case Behavior.WriteLimitValue2ModeTestFail:
                    this.command = 0;
                    break;
                case Behavior.WriteLimitValue3ModeTestSuccess:
                    this.command = 17;
                    break;
                case Behavior.WriteLimitValue3ModeTestFail:
                    this.command = 0;
                    break;
                case Behavior.WriteLimitValue4ModeTestSuccess:
                    this.command = 23;
                    break;
                case Behavior.WriteLimitValue4ModeTestFail:
                    this.command = 0;
                    break;
            }

        }

        public void WriteArray(ushort index, ushort[] data)
        {

            switch (this.behavior)
            {
                case Behavior.UpdateOutputTestSuccess:
                        this.arrayElement1 = data[0];
                        this.arrayElement2 = data[1];
                    break;

                case Behavior.UpdateOutputTestFail:
                    this.arrayElement1 = 0;
                    this.arrayElement2 = 0;
                    break;

                case Behavior.WriteS32ArrayTestSuccess:
                        this.wordNumberIndex = index;
                        this.arrayElement1 = data[0];
                        this.arrayElement2 = data[1];
                    break;

                case Behavior.WriteS32ArrayTestFail:
                        this.wordNumberIndex = 0;
                        this.arrayElement1 = 0;
                        this.arrayElement2 = 0;
                    break;

                case Behavior.CalibrationFail:
                        this.arrayElement1 = 0;
                        this.arrayElement2 = 0;
                   break;

                case Behavior.CalibrationSuccess:

                    if ((int)index == 48 || (int) index== 46)       // According to the index 48 (=wordnumber) the preload is written. 
                    {
                        this.arrayElement1 = data[0];
                        this.arrayElement2 = data[1];
                    }
                    else
                    if ((int)index == 50)       // According to the index 50 (=wordnumber) the nominal load is written. 
                    {
                        this.arrayElement3 = data[0];
                        this.arrayElement4 = data[1];
                    }
                        break;

                case Behavior.WriteArrayFail:
                    this.arrayElement1 = 0;
                    this.arrayElement2 = 0;

                    break;

                case Behavior.WriteArraySuccess:
                    this.arrayElement1 = data[0];
                    this.arrayElement2 = data[1];

                    break;

                case Behavior.MeasureZeroSuccess:

                    _dataWTX[0] = 0;
                    _dataWTX[0] = 0; 
                    this.arrayElement1 = data[0];
                    this.arrayElement2 = data[1];

                    break;

                case Behavior.MeasureZeroFail:
                    
                    _dataWTX[0] = 555;
                    this.arrayElement1 = 0;
                    this.arrayElement2 = 0;

                    break;
                default:
                    break; 
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<ushort[]> ReadAsync()
        {
            ushort[] value = new ushort[1];
            await Task.Run(async () =>
            {
            switch (behavior)
            {
                case Behavior.ReadFail:

                    // If there is a connection fail, all data attributes get 0 as value.

                    for (int i = 0; i < _dataWTX.Length; i++)
                    {
                        _dataWTX[i] = 0x0000;
                    }

                    _logObj = new LogEvent("Read failed : Registers have not been read");

                    BusActivityDetection?.Invoke(this, _logObj);

                    break;

                case Behavior.ReadSuccess:


                    // The most important data attributes from the WTX120 device: 
                    
                    _dataWTX[0] = 0x0000;
                    _dataWTX[1] = 0x4040;
                    _dataWTX[2] = 0x0000;
                    _dataWTX[3] = 0x4040;
                    _dataWTX[4] = 0x0000;
                    _dataWTX[5] = 0x0000;

                    _logObj = new LogEvent("Read successful: Registers have been read");
                    BusActivityDetection?.Invoke(this, _logObj);
                    break;

                // Simulate for testing 'Unit': 

                case Behavior.t_UnitValue_Success:
                    _dataWTX[5] = 0x100;
                    break;
                case Behavior.t_UnitValue_Fail:
                    _dataWTX[5] = 0x0000;
                    break;

                case Behavior.kg_UnitValue_Success:
                    _dataWTX[5] = 0x0000;
                    break;

                case Behavior.kg_UnitValue_Fail:
                    _dataWTX[5] = 0xFFFF;
                    break;

                case Behavior.g_UnitValue_Success:
                    _dataWTX[5] = 0x80;
                    break;

                case Behavior.g_UnitValue_Fail:
                    _dataWTX[5] = 0x0000;
                    break;

                case Behavior.lb_UnitValue_Success:
                    _dataWTX[5] = 0x180;
                    break;


                case Behavior.lb_UnitValue_Fail:
                    _dataWTX[5] = 0x0000;
                    break;


                // Simulate for testing 'Limit status': 

                case Behavior.LimitStatusStringComment_Case0_Fail:
                    _dataWTX[4] = 0xC;
                    break;
                case Behavior.LimitStatusStringComment_Case1_Fail:
                    _dataWTX[4] = 0x8;
                    break;
                case Behavior.LimitStatusStringComment_Case2_Fail:
                    _dataWTX[4] = 0x0000;
                    break;
                case Behavior.LimitStatusStringComment_Case3_Fail:
                    _dataWTX[4] = 0x4;
                    break;

                case Behavior.LimitStatusStringComment_Case0_Success:
                    _dataWTX[4] = 0x0000;
                    break;
                case Behavior.LimitStatusStringComment_Case1_Success:
                    _dataWTX[4] = 0x4;
                    break;
                case Behavior.LimitStatusStringComment_Case2_Success:
                    _dataWTX[4] = 0x8;
                    break;
                case Behavior.LimitStatusStringComment_Case3_Success:
                    _dataWTX[4] = 0xC;
                    break;

                // Simulate for testing 'Weight moving': 
                case Behavior.WeightMovingStringComment_Case0_Fail:
                    _dataWTX[4] = 0x0010;
                    break;
                case Behavior.WeightMovingStringComment_Case1_Fail:
                    _dataWTX[4] = 0x0000;
                    break;
                case Behavior.WeightMovingStringComment_Case0_Success:
                    _dataWTX[4] = 0x0000;
                    break;
                case Behavior.WeightMovingStringComment_Case1_Success:
                    _dataWTX[4] = 0x0010;
                    break;

                // Simulate for testing 'Weight type': 
                case Behavior.WeightTypeStringComment_Case0_Fail:
                    _dataWTX[4] = 0x0080;
                    break;
                case Behavior.WeightTypeStringComment_Case1_Fail:
                    _dataWTX[4] = 0x0000;
                    break;

                case Behavior.WeightTypeStringComment_Case0_Success:
                    _dataWTX[4] = 0x0000;
                    break;
                case Behavior.WeightTypeStringComment_Case1_Success:
                    _dataWTX[4] = 0x0080;
                    break;
                // Simulate for testing 'Scale range': 

                case Behavior.ScaleRangeStringComment_Range1_Fail:
                    _dataWTX[4] = 0x200;
                    break;

                case Behavior.ScaleRangeStringComment_Range2_Fail:
                    _dataWTX[4] = 0x0000;
                    break;

                case Behavior.ScaleRangeStringComment_Range3_Fail:
                    _dataWTX[4] = 0x100;
                    break;

                case Behavior.ScaleRangeStringComment_Range1_Success:
                    _dataWTX[4] = 0x0000;
                    break;

                case Behavior.ScaleRangeStringComment_Range2_Success:
                    _dataWTX[4] = 0x100;
                    break;

                case Behavior.ScaleRangeStringComment_Range3_Success:
                    _dataWTX[4] = 0x200;
                    break;

                case Behavior.LogEvent_Fail:

                    _logObj = new LogEvent("Read failed : Registers have not been read");
                    BusActivityDetection?.Invoke(this, _logObj);
                    break;

                case Behavior.LogEvent_Success:

                    _logObj = new LogEvent("Read successful: Registers have been read");
                    BusActivityDetection?.Invoke(this, _logObj);
                    break;
            }
            if (_dataWTX[5] == 0x0000)
            {
                _dataWTX[5] = 0x4000;
            }
            else
            if (_dataWTX[5] == 0x4000)
            {
                _dataWTX[5] = 0x0000;
            }
            
                this.UpdateDictionary();
                // Update data in data classes : 
                this.UpdateDataClasses?.Invoke(this, new DataEventArgs(this._dataIntegerBuffer));

                return _dataWTX;

             });

            this.UpdateDictionary();
            // Update data in data classes : 
            this.UpdateDataClasses?.Invoke(this, new DataEventArgs(this._dataIntegerBuffer));

            return _dataWTX;
        }

        public async Task<int> WriteAsync(ushort index, ushort commandParam)
        {
            this.command = commandParam;

            switch (behavior)
            {
                case Behavior.ZeroMethodTestSuccess:
                    this.command = commandParam;
                    break;
                case Behavior.ZeroMethodTestFail:
                    this.command = 0x00;
                    break;

                case Behavior.AbortDosingMethodTestSuccess:
                    this.command = commandParam;
                    break;
                case Behavior.AbortDosingMethodTestFail:
                    this.command = 0;
                    break;
            }

            // Change the handshake bit : bit .14 from 0 to 1.
            if (_dataWTX[5] == 0x0000)
                _dataWTX[5] = 0x4000;
            else
                if (_dataWTX[5] == 0x4000)
                _dataWTX[5] = 0x0000;


            this.command = commandParam;

            return this.command;
        }


        public Dictionary<string, int> AllData
        {
            get
            {
                return this._dataIntegerBuffer;
            }
        }

        public int getWordNumber
        {
            get
            {
                return this.wordNumberIndex;
            }
        }

        public ushort getArrElement1
        {
            get
            {
                return this.arrayElement1;
            }
        }

        public ushort getArrElement2
        {
            get
            {
                return this.arrayElement2;
            }
        }

        public ushort getArrElement3
        {
            get
            {
                return this.arrayElement3;
            }
        }

        public ushort getArrElement4
        {
            get
            {
                return this.arrayElement4;
            }
        }

        public int NumofPoints
        {
            get
            {
                return this.numPoints;
            }
            set
            {
                this.numPoints = value; 
            }
        }
        
        public string IpAddress
        {
            get
            {
                return this.IP;
            }
            set
            {
                this.IP = value; 
            }
        }

        public int SendingInterval
        {
            get
            {
                return this.interval;
            }
            set
            {
                this.interval = value;
            }
        }

        public ushort[] getData {

            get
            {
                return this._dataWTX;
            }
            set
            {
                this._dataWTX = value; 
            }

        }

        public string ConnectionType
        {
            get { return "Modbus"; }
        }

        public ICommands IDCommands
        {
            get
            {
                return this._commands;
            }
        }
        //public Dictionary<string, JToken> getDataBuffer => throw new NotImplementedException();
    }
}
