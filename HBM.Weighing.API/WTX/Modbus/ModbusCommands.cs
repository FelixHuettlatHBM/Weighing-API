﻿// <copyright file="ModbusCommands.cs" company="Hottinger Baldwin Messtechnik GmbH">
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hbm.Weighing.API.WTX.Modbus
{
    /// <summary>
    /// Class for using commands, respectively indexes/paths, to read/write the 
    /// registers of the WTX device via Modbus to get the data.
    /// ID's/Commands for subscribing values of the WTX device. 
    /// The ID's are commited as a parameter for the read and/or write method call.  
    /// This class inherits from interface ICommands. 
    /// </summary>
    public static class ModbusCommands
    {
        static ModbusCommands()
        {
            // region : ID Commands : Memory - day, month, year, seqNumber, gross, net
            // For standard mode: 
            WeightMemDayStandard   = new ModbusCommand(DataType.S16, 9, IOType.Input, ApplicationMode.Standard, 0, 0);
            WeightMemMonthStandard = new ModbusCommand(DataType.S16, 10, IOType.Input, ApplicationMode.Standard, 0, 0);
            WeightMemYearStandard = new ModbusCommand(DataType.S16, 11, IOType.Input, ApplicationMode.Standard, 0, 0);
            WeightMemSeqNumberStandard = new ModbusCommand(DataType.S16, 12, IOType.Input, ApplicationMode.Standard, 0, 0);
            WeightMemGrossStandard = new ModbusCommand(DataType.S16, 13, IOType.Input, ApplicationMode.Standard, 0, 0);
            WeightMemNetStandard = new ModbusCommand(DataType.S16, 14, IOType.Input, ApplicationMode.Standard, 0, 0);

            // For filler mode: 
            WeightMemDayFiller   = new ModbusCommand(DataType.S16, 32, IOType.Output, ApplicationMode.Filler, 0, 0);
            WeightMemMonthFiller = new ModbusCommand(DataType.S16, 33, IOType.Output, ApplicationMode.Filler, 0, 0);
            WeightMemYearFiller = new ModbusCommand(DataType.S16, 34, IOType.Output, ApplicationMode.Filler, 0, 0);
            WeightMemSeqNumberFiller = new ModbusCommand(DataType.S16, 35, IOType.Output, ApplicationMode.Filler, 0, 0);
            WeightMemGrossFiller = new ModbusCommand(DataType.S16, 36, IOType.Output, ApplicationMode.Filler, 0, 0);
            WeightMemNetFiller = new ModbusCommand(DataType.S16, 37, IOType.Output, ApplicationMode.Filler, 0, 0);

            // region ID Commands : Maintenance - Calibration

            CWTScaleCalibrationWeight = new ModbusCommand(DataType.S32, 46, IOType.Output, ApplicationMode.Standard, 0, 0);
            LDWZeroSignal   = new ModbusCommand(DataType.S32, 48, IOType.Output, ApplicationMode.Standard, 0, 0);
            LWTNominalSignal = new ModbusCommand(DataType.S32, 50, IOType.Output, ApplicationMode.Standard, 0, 0);

            // region ID commands for process data
            Net   = new ModbusCommand(DataType.S32, 0, IOType.Input, ApplicationMode.Standard, 32, 0);
            Gross = new ModbusCommand(DataType.S32, 2, IOType.Input, ApplicationMode.Standard, 32, 0);
            //Zero  = new ModbusCommand(DataType.S32, "",  IOType.Input, ApplicationMode.Standard, 32, 0);
            
            GeneralWeightError = new ModbusCommand(DataType.BIT, 4, IOType.Input, ApplicationMode.Standard, 0, 1);
            ScaleAlarmTriggered = new ModbusCommand(DataType.BIT, 4, IOType.Input, ApplicationMode.Standard, 1, 1);
            Limit_status = new ModbusCommand(DataType.BIT, 4, IOType.Input, ApplicationMode.Standard, 2, 2);                   // data word = 4 ; length = 2 ; offset = 2;
            WeightMoving = new ModbusCommand(DataType.BIT, 4, IOType.Input, ApplicationMode.Standard, 4, 1);
            ScaleSealIsOpen = new ModbusCommand(DataType.BIT, 4, IOType.Input, ApplicationMode.Standard, 5, 1);
            ManualTare = new ModbusCommand(DataType.BIT, 4, IOType.Input, ApplicationMode.Standard, 6, 1);
            WeightType = new ModbusCommand(DataType.BIT, 4, IOType.Input, ApplicationMode.Standard, 7, 1);
            ScaleRange = new ModbusCommand(DataType.BIT, 4, IOType.Input, ApplicationMode.Standard, 8, 2);
            ZeroRequired = new ModbusCommand(DataType.BIT, 4, IOType.Input, ApplicationMode.Standard, 10, 1);
            WeightinCenterOfZero = new ModbusCommand(DataType.BIT, 4, IOType.Input, ApplicationMode.Standard, 11, 1);
            WeightinZeroRange = new ModbusCommand(DataType.BIT, 4, IOType.Input, ApplicationMode.Standard, 12, 1);

            Application_mode = new ModbusCommand(DataType.BIT, 5, IOType.Input, ApplicationMode.Standard, 0, 2);       // data word = 5 ; length = 2 ; offset = 0;
            Decimals = new ModbusCommand(DataType.BIT, 5, IOType.Input, ApplicationMode.Standard, 4, 3);               // data word = 5 ; length = 3 ; offset = 4;
            Unit = new ModbusCommand(DataType.BIT, 5, IOType.Input, ApplicationMode.Standard, 7, 2);                   // data word = 5 ; length = 2 ; offset = 7;
            Handshake = new ModbusCommand(DataType.BIT, 5, IOType.Input, ApplicationMode.Standard, 14, 1);             // data word = 5 : length = 1 ; offset = 14;
            Status = new ModbusCommand(DataType.BIT, 5, IOType.Input, ApplicationMode.Standard, 15, 1);                // data word = 5 ; length = 1 ; offset = 15;

            // region ID commands for standard mode
            Status_digital_input_1 = new ModbusCommand(DataType.BIT, 6, IOType.Input, ApplicationMode.Standard, 1, 1);    // IS1
            Status_digital_input_2 = new ModbusCommand(DataType.BIT, 6, IOType.Input, ApplicationMode.Standard, 2, 1);    // IS2
            Status_digital_input_3 = new ModbusCommand(DataType.BIT, 6, IOType.Input, ApplicationMode.Standard, 3, 1);    // IS3
            Status_digital_input_4 = new ModbusCommand(DataType.BIT, 6, IOType.Input, ApplicationMode.Standard, 4, 1);    // IS4

            Status_digital_output_1 = new ModbusCommand(DataType.BIT, 7, IOType.Input, ApplicationMode.Standard, 1, 1);   // OS1
            Status_digital_output_2 = new ModbusCommand(DataType.BIT, 7, IOType.Input, ApplicationMode.Standard, 2, 1);   // OS2
            Status_digital_output_3 = new ModbusCommand(DataType.BIT, 7, IOType.Input, ApplicationMode.Standard, 3, 1);   // OS3
            Status_digital_output_4 = new ModbusCommand(DataType.BIT, 7, IOType.Input, ApplicationMode.Standard, 4, 1);   // OS4

            Limit_value = new ModbusCommand(DataType.U08, 8, IOType.Input, ApplicationMode.Standard, 0, 0);   // LVS , standard
            ManualTareValue = new ModbusCommand(DataType.U08, 2, IOType.Input, ApplicationMode.Standard, 0, 0);  // manual tare value

            LimitValue1Input = new ModbusCommand(DataType.U08, 4, IOType.Output, ApplicationMode.Standard, 0, 0); 
            LimitValue1Mode = new ModbusCommand(DataType.U08, 5, IOType.Output, ApplicationMode.Standard, 0, 0);
            LimitValue1ActivationLevelLowerBandLimit = new ModbusCommand(DataType.S32, 6, IOType.Output, ApplicationMode.Standard, 0, 0);       
            LimitValue1HysteresisBandHeight = new ModbusCommand(DataType.S32, 8, IOType.Output, ApplicationMode.Standard, 0, 0);       

            LimitValue2Source = new ModbusCommand(DataType.U08, 10, IOType.Output, ApplicationMode.Standard, 0, 0);
            LimitValue2Mode = new ModbusCommand(DataType.U08, 11, IOType.Output, ApplicationMode.Standard, 0, 0);
            LimitValue2ActivationLevelLowerBandLimit = new ModbusCommand(DataType.S32, 12, IOType.Output, ApplicationMode.Standard, 0, 0);
            LimitValue2HysteresisBandHeight = new ModbusCommand(DataType.S32, 14, IOType.Output, ApplicationMode.Standard, 0, 0);

            LimitValue3Source = new ModbusCommand(DataType.U08, 16, IOType.Output, ApplicationMode.Standard, 0, 0);
            LimitValue3Mode = new ModbusCommand(DataType.U08, 17, IOType.Output, ApplicationMode.Standard, 0, 0);
            LimitValue3ActivationLevelLowerBandLimit = new ModbusCommand(DataType.S32, 18, IOType.Output, ApplicationMode.Standard, 0, 0);
            LimitValue3HysteresisBandHeight = new ModbusCommand(DataType.S32, 20, IOType.Output, ApplicationMode.Standard, 0, 0);

            LimitValue4Source = new ModbusCommand(DataType.U08, 22, IOType.Output, ApplicationMode.Standard, 0, 0);
            LimitValue4Mode = new ModbusCommand(DataType.U08, 23, IOType.Output, ApplicationMode.Standard, 0, 0);
            LimitValue4ActivationLevelLowerBandLimit = new ModbusCommand(DataType.S32, 24, IOType.Output, ApplicationMode.Standard, 0, 0);
            LimitValue4HysteresisBandHeight = new ModbusCommand(DataType.S32, 26, IOType.Output, ApplicationMode.Standard, 0, 0);

            // region ID commands for filler data

            CoarseFlow = new ModbusCommand(DataType.BIT, 8, IOType.Input, ApplicationMode.Filler, 0, 1);               // data input word 8, bit .0, application mode=filler
            FineFlow = new ModbusCommand(DataType.BIT, 8, IOType.Input, ApplicationMode.Filler, 1, 1);                 // data input word 8, bit .1, application mode=filler
            Ready = new ModbusCommand(DataType.BIT, 8, IOType.Input, ApplicationMode.Filler, 2, 1);                    // data input word 8, bit .2, application mode=filler
            ReDosing = new ModbusCommand(DataType.BIT, 8,  IOType.Input, ApplicationMode.Filler, 3, 1);                // data input word 8, bit .3, application mode=filler; RDS = Nachdosieren
            Emptying = new ModbusCommand(DataType.BIT, 8,  IOType.Input, ApplicationMode.Filler, 4, 1);                // data input word 8, bit .4, application mode=filler
            FlowError = new ModbusCommand(DataType.BIT, 8, IOType.Input, ApplicationMode.Filler, 5, 1);                // data input word 8, bit .5, application mode=filler
            Alarm = new ModbusCommand(DataType.BIT, 8, IOType.Input, ApplicationMode.Filler, 6, 1);                    // data input word 8, bit .6, application mode=filler
            AdcOverUnderload = new ModbusCommand(DataType.BIT, 8, IOType.Input, ApplicationMode.Filler, 7, 1);         // data input word 8, bit .7, application mode=filler
            MaximalDosingTimeInput = new ModbusCommand(DataType.BIT, 8, IOType.Input, ApplicationMode.Filler, 8, 1);   // data input word 8, bit .8, application mode=filler
            LegalForTradeOperation = new ModbusCommand(DataType.BIT, 8, IOType.Input, ApplicationMode.Filler, 9, 1);   // data input word 8, bit .9, application mode=filler
            ToleranceErrorPlus = new ModbusCommand(DataType.BIT, 8, IOType.Input, ApplicationMode.Filler, 10, 1);      // data input word 8, bit .10, application mode=filler
            ToleranceErrorMinus = new ModbusCommand(DataType.BIT, 8, IOType.Input, ApplicationMode.Filler, 11, 1);     // data input word 8, bit .11, application mode=filler
            StatusInput1 = new ModbusCommand(DataType.BIT, 8, IOType.Input, ApplicationMode.Filler, 14, 1);            // data input word 8, bit .14, application mode=filler
            GeneralScaleError = new ModbusCommand(DataType.BIT, 8, IOType.Input, ApplicationMode.Filler, 15, 1);       // data input word 8, bit .15, application mode=filler

            TotalWeight = new ModbusCommand(DataType.S32, 18, IOType.Input, ApplicationMode.Filler, 0, 0);             // data input word 18, application mode=filler
            Dosing_time = new ModbusCommand(DataType.U16, 24, IOType.Input, ApplicationMode.Filler, 0, 0);             // DST = Dosieristzeit
            Coarse_flow_time = new ModbusCommand(DataType.U16, 25, IOType.Input, ApplicationMode.Filler, 0, 0);        // CFT = Grobstromzeit
            CurrentFineFlowTime = new ModbusCommand(DataType.U16, 26, IOType.Input, ApplicationMode.Filler, 0, 0);     // data input word 26, application mode=filler; FFT = Feinstromzeit
            ParameterSetProduct = new ModbusCommand(DataType.U08, 27, IOType.Input, ApplicationMode.Filler, 0, 0);     // data input word 27, application mode=filler

            TargetFillingWeight = new ModbusCommand(DataType.S32, 10, IOType.Output, ApplicationMode.Filler, 0, 0);        // data output word 10, application mode=filler
            Residual_flow_time = new ModbusCommand(DataType.U16, 9, IOType.Output, ApplicationMode.Filler, 0, 0);          // RFT = Nachstromzeit
            Coarse_flow_cut_off_point = new ModbusCommand(DataType.S32, 12, IOType.Output, ApplicationMode.Filler, 0, 0);  // CFD = Grobstromabschaltpunkt
            Fine_flow_cut_off_point = new ModbusCommand(DataType.S32, 14, IOType.Output, ApplicationMode.Filler, 0, 0);    // FFD = Feinstromabschaltpunkt

            Minimum_fine_flow = new ModbusCommand(DataType.S32, 16, IOType.Output, ApplicationMode.Filler, 0, 0);          // FFM = Minimaler Feinstromanteil
            Optimization = new ModbusCommand(DataType.U08, 18, IOType.Output, ApplicationMode.Filler, 0, 0);               // OSN = Optimierung
            Maximal_dosing_time = new ModbusCommand(DataType.U16, 19, IOType.Output, ApplicationMode.Filler, 0, 0);        // MDT = Maximale Dosierzeit
            Run_start_dosing = new ModbusCommand(DataType.U16, 20, IOType.Output, ApplicationMode.Filler, 0, 0);           // RUN = Start Dosieren

            Lockout_time_coarse_flow = new ModbusCommand(DataType.U16, 21, IOType.Output, ApplicationMode.Filler, 0, 0);   // LTC = Sperrzeit Grobstrom
            Lockout_time_fine_flow = new ModbusCommand(DataType.U16, 22, IOType.Output, ApplicationMode.Filler, 0, 0);     // LTF = Sperrzeit Feinstrom
            Tare_mode = new ModbusCommand(DataType.U08, 23, IOType.Output, ApplicationMode.Filler, 0, 0);                  // TMD = Tariermodus
            Upper_tolerance_limit = new ModbusCommand(DataType.S32, 24, IOType.Output, ApplicationMode.Filler, 0, 0);      // UTL = Obere Toleranz

            Lower_tolerance_limit = new ModbusCommand(DataType.S32, 26, IOType.Output, ApplicationMode.Filler, 0, 0);      // LTL = Untere Toleranz
            Minimum_start_weight = new ModbusCommand(DataType.S32, 28, IOType.Output, ApplicationMode.Filler, 0, 0);       // MSW = Minimum Startgewicht
            Empty_weight = new ModbusCommand(DataType.S32, 30, IOType.Output, ApplicationMode.Filler, 0, 0);
            Tare_delay = new ModbusCommand(DataType.U16, 32, IOType.Output, ApplicationMode.Filler, 0, 0);                  // TAD = Tarierverzögerung

            Coarse_flow_monitoring_time = new ModbusCommand(DataType.U16, 33, IOType.Output, ApplicationMode.Filler, 0, 0); // CBT = Überwachungszeit Grobstrom
            Coarse_flow_monitoring = new ModbusCommand(DataType.U32, 34, IOType.Output, ApplicationMode.Filler, 0, 0);      // CBK = Füllstromüberwachung Grobstrom
            Fine_flow_monitoring = new ModbusCommand(DataType.U32, 36, IOType.Output, ApplicationMode.Filler, 0, 0);        // FBK = Füllstromüberwachung Feinstrom
            Fine_flow_monitoring_time = new ModbusCommand(DataType.U16, 38, IOType.Output, ApplicationMode.Filler, 0, 0);   // FBT = Überwachungszeit Feinstrom

            Delay_time_after_fine_flow = new ModbusCommand(DataType.U08, 39, IOType.Output, ApplicationMode.Filler, 0, 0);
            Activation_time_after_fine_flow = new ModbusCommand(DataType.U08, 40, IOType.Output, ApplicationMode.Filler, 0, 0);

            Systematic_difference = new ModbusCommand(DataType.U32, 41, IOType.Output, ApplicationMode.Filler, 0, 0);       // SYD = Systematische Differenz
            DownwardsDosing = new ModbusCommand(DataType.U08, 42, IOType.Output, ApplicationMode.Filler, 0, 0);             // data output word 42, application mode=filler
            Valve_control = new ModbusCommand(DataType.U08, 43, IOType.Output, ApplicationMode.Filler, 0, 0);               // VCT = Ventilsteuerung
            Emptying_mode = new ModbusCommand(DataType.U08, 44, IOType.Output, ApplicationMode.Filler, 0, 0);               // EMD = Entleermodus

            Control_word_ResetHandshake = new ModbusCommand(DataType.U16, 0, IOType.Output, ApplicationMode.Standard, 0, 0);
            Control_word_Taring   = new ModbusCommand(DataType.BIT, 0, IOType.Output, ApplicationMode.Standard, 0, 1);
            Control_word_GrossNet = new ModbusCommand(DataType.BIT, 0, IOType.Output, ApplicationMode.Standard, 1, 1);
            Control_word_ClearDosingResults = new ModbusCommand(DataType.BIT, 0, IOType.Output, ApplicationMode.Standard, 2, 1);
            Control_word_AbortDosing    = new ModbusCommand(DataType.BIT, 0, IOType.Output, ApplicationMode.Standard, 3, 1);
            Control_word_StartDosing    = new ModbusCommand(DataType.BIT, 0, IOType.Output, ApplicationMode.Standard, 4, 1);
            Control_word_Zeroing        = new ModbusCommand(DataType.BIT, 0, IOType.Output, ApplicationMode.Standard, 6, 1);
            Control_word_AdjustZero     = new ModbusCommand(DataType.BIT, 0, IOType.Output, ApplicationMode.Standard, 7, 1);
            Control_word_AdjustNominal  = new ModbusCommand(DataType.BIT, 0, IOType.Output, ApplicationMode.Standard, 8, 1);
            Control_word_ActivateData   = new ModbusCommand(DataType.BIT, 0, IOType.Output, ApplicationMode.Standard, 11, 1);
            Control_word_RecordWeight   = new ModbusCommand(DataType.BIT, 0, IOType.Output, ApplicationMode.Standard, 14, 1);
            Control_word_ManualReDosing = new ModbusCommand(DataType.BIT, 0, IOType.Output, ApplicationMode.Standard, 15, 1);
            
        }

        // Output word : Control word:
        public static ModbusCommand Control_word_ResetHandshake { get; private set; }
        public static ModbusCommand Control_word_Taring { get; private set; }
        public static ModbusCommand Control_word_GrossNet { get; private set; }
        public static ModbusCommand Control_word_ClearDosingResults { get; private set; }
        public static ModbusCommand Control_word_AbortDosing { get; private set; }
        public static ModbusCommand Control_word_StartDosing { get; private set; }
        public static ModbusCommand Control_word_Zeroing { get; private set; }
        public static ModbusCommand Control_word_AdjustZero { get; private set; }
        public static ModbusCommand Control_word_AdjustNominal { get; private set; }
        public static ModbusCommand Control_word_ActivateData { get; private set; }
        public static ModbusCommand Control_word_RecordWeight { get; private set; }
        public static ModbusCommand Control_word_ManualReDosing { get; private set; }

        // region ID Commands : Memory - day, month, year, seqNumber, gross, net

        // For standard mode: 
        public static  ModbusCommand WeightMemDayStandard { get; private set; }
        public static  ModbusCommand WeightMemMonthStandard { get; private set; }
        public static  ModbusCommand WeightMemYearStandard { get; private set; }
        public static  ModbusCommand WeightMemSeqNumberStandard { get; private set; }
        public static  ModbusCommand WeightMemGrossStandard { get; private set; }
        public static  ModbusCommand WeightMemNetStandard { get; private set; }

        // For filler mode: 
        public static  ModbusCommand WeightMemDayFiller { get; private set; }
        public static  ModbusCommand WeightMemMonthFiller { get; private set; }
        public static  ModbusCommand WeightMemYearFiller { get; private set; }
        public static  ModbusCommand WeightMemSeqNumberFiller { get; private set; }
        public static  ModbusCommand WeightMemGrossFiller { get; private set; }
        public static  ModbusCommand WeightMemNetFiller { get; private set; }

        // region ID Commands : Maintenance - Calibration

        public static  ModbusCommand LDWZeroSignal { get; private set; }        
        public static  ModbusCommand LWTNominalSignal { get; private set; }      
        public static  ModbusCommand CWTScaleCalibrationWeight { get; private set; }   

        // region ID commands for process data
        public static  ModbusCommand Net { get; private set; }
        public static  ModbusCommand Gross { get; private set; }
        public static  ModbusCommand Zero { get; private set; }
        public static  ModbusCommand ManualTareValue { get; private set; }  

        public static  ModbusCommand GeneralWeightError { get; private set; }
        public static  ModbusCommand ScaleAlarmTriggered { get; private set; }
        public static  ModbusCommand WeightMoving { get; private set; }
        public static  ModbusCommand ScaleSealIsOpen { get; private set; }
        public static  ModbusCommand ManualTare { get; private set; }
        public static  ModbusCommand WeightType { get; private set; }
        public static  ModbusCommand ScaleRange { get; private set; }
        public static  ModbusCommand ZeroRequired { get; private set; }
        public static  ModbusCommand WeightinCenterOfZero { get; private set; }
        public static  ModbusCommand WeightinZeroRange { get; private set; }
        public static  ModbusCommand Decimals { get; private set; }
        public static  ModbusCommand Handshake { get; private set; }
        public static  ModbusCommand Limit_status { get; private set; }             
        public static  ModbusCommand Unit { get; private set; }   
        public static  ModbusCommand Application_mode { get; private set; }            
        public static  ModbusCommand Status { get; private set; }    

        // region ID commands for standard mode
        public static  ModbusCommand Status_digital_input_1 { get; private set; }    // IS1
        public static  ModbusCommand Status_digital_input_2 { get; private set; }    // IS2
        public static  ModbusCommand Status_digital_input_3 { get; private set; }    // IS3
        public static  ModbusCommand Status_digital_input_4 { get; private set; }    // IS4

        public static  ModbusCommand Status_digital_output_1 { get; private set; }   // OS1
        public static  ModbusCommand Status_digital_output_2 { get; private set; }   // OS2
        public static  ModbusCommand Status_digital_output_3 { get; private set; }   // OS3
        public static  ModbusCommand Status_digital_output_4 { get; private set; }   // OS4

        public static  ModbusCommand Limit_value { get; private set; }   // LVS


        public static  ModbusCommand LimitValue1Input { get; private set; } // = Grenzwertüberwachung 
        public static  ModbusCommand LimitValue1Mode { get; private set; }
        public static  ModbusCommand LimitValue1ActivationLevelLowerBandLimit { get; private set; }        // = Einschaltpegel
        public static  ModbusCommand LimitValue1HysteresisBandHeight { get; private set; }       // = Ausschaltpegel

        public static  ModbusCommand LimitValue2Source { get; private set; }
        public static  ModbusCommand LimitValue2Mode { get; private set; }
        public static  ModbusCommand LimitValue2ActivationLevelLowerBandLimit { get; private set; }
        public static  ModbusCommand LimitValue2HysteresisBandHeight { get; private set; }

        public static  ModbusCommand LimitValue3Source { get; private set; }
        public static  ModbusCommand LimitValue3Mode { get; private set; }
        public static  ModbusCommand LimitValue3ActivationLevelLowerBandLimit { get; private set; }
        public static  ModbusCommand LimitValue3HysteresisBandHeight { get; private set; }

        public static  ModbusCommand LimitValue4Source { get; private set; }
        public static  ModbusCommand LimitValue4Mode { get; private set; }
        public static  ModbusCommand LimitValue4ActivationLevelLowerBandLimit { get; private set; }
        public static  ModbusCommand LimitValue4HysteresisBandHeight { get; private set; }

        // region ID commands for filler data

        public static  ModbusCommand CoarseFlow { get; private set; }                // data input word 8, bit .0, application mode=filler
        public static  ModbusCommand FineFlow { get; private set; }                 // data input word 8, bit .1, application mode=filler
        public static  ModbusCommand Ready { get; private set; }                    // data input word 8, bit .2, application mode=filler
        public static  ModbusCommand ReDosing { get; private set; }                 // data input word 8, bit .3, application mode=filler; RDS = Nachdosieren
        public static  ModbusCommand Emptying { get; private set; }                 // data input word 8, bit .4, application mode=filler
        public static  ModbusCommand FlowError { get; private set; }                // data input word 8, bit .5, application mode=filler
        public static  ModbusCommand Alarm { get; private set; }                    // data input word 8, bit .6, application mode=filler
        public static  ModbusCommand AdcOverUnderload { get; private set; }         // data input word 8, bit .7, application mode=filler
        public static  ModbusCommand MaximalDosingTimeInput { get; private set; }   // data input word 8, bit .8, application mode=filler
        public static  ModbusCommand LegalForTradeOperation { get; private set; }   // data input word 8, bit .9, application mode=filler
        public static  ModbusCommand ToleranceErrorPlus { get; private set; }       // data input word 8, bit .10, application mode=filler
        public static  ModbusCommand ToleranceErrorMinus { get; private set; }      // data input word 8, bit .11, application mode=filler
        public static  ModbusCommand StatusInput1 { get; private set; }             // data input word 8, bit .14, application mode=filler
        public static  ModbusCommand GeneralScaleError { get; private set; }        // data input word 8, bit .15, application mode=filler

        public static  ModbusCommand TotalWeight { get; private set; }             // data input word 18, application mode=filler
        public static  ModbusCommand Dosing_time { get; private set; }             // DST = Dosieristzeit
        public static  ModbusCommand Coarse_flow_time { get; private set; }        // CFT = Grobstromzeit
        public static  ModbusCommand CurrentFineFlowTime { get; private set; }     // data input word 26, application mode=filler; FFT = Feinstromzeit
        public static  ModbusCommand ParameterSetProduct { get; private set; }     // data input word 27, application mode=filler
        public static  ModbusCommand TargetFillingWeight { get; private set; }     // data output word 10, application mode=filler

        public static  ModbusCommand Residual_flow_time { get; private set; }          // RFT = Nachstromzeit
       // public static  ModbusCommand Reference_value_dosing { get; private set; }      // FWT = Sollwert dosieren = Target filling weight
        public static  ModbusCommand Coarse_flow_cut_off_point { get; private set; }   // CFD = Grobstromabschaltpunkt
        public static  ModbusCommand Fine_flow_cut_off_point { get; private set; }     // FFD = Feinstromabschaltpunkt

        public static  ModbusCommand Minimum_fine_flow { get; private set; }           // FFM = Minimaler Feinstromanteil
        public static  ModbusCommand Optimization { get; private set; }                // OSN = Optimierung
        public static  ModbusCommand Maximal_dosing_time { get; private set; }         // MDT = Maximale Dosierzeit
        public static  ModbusCommand Run_start_dosing { get; private set; }            // RUN = Start Dosieren

        public static  ModbusCommand Lockout_time_coarse_flow { get; private set; }    // LTC = Sperrzeit Grobstrom
        public static  ModbusCommand Lockout_time_fine_flow { get; private set; }      // LTF = Sperrzeit Feinstrom
        public static  ModbusCommand Tare_mode { get; private set; }                   // TMD = Tariermodus
        public static  ModbusCommand Upper_tolerance_limit { get; private set; }       // UTL = Obere Toleranz

        public static  ModbusCommand Lower_tolerance_limit { get; private set; }   // LTL = Untere Toleranz
        public static  ModbusCommand Minimum_start_weight { get; private set; }   // MSW = Minimum Startgewicht
        public static  ModbusCommand Empty_weight { get; private set; }
        public static  ModbusCommand Tare_delay { get; private set; }   // TAD = Tarierverzögerung

        public static  ModbusCommand Coarse_flow_monitoring_time { get; private set; }  // CBT = Überwachungszeit Grobstrom
        public static  ModbusCommand Coarse_flow_monitoring { get; private set; }       // CBK = Füllstromüberwachung Grobstrom
        public static  ModbusCommand Fine_flow_monitoring { get; private set; }         // FBK = Füllstromüberwachung Feinstrom
        public static  ModbusCommand Fine_flow_monitoring_time { get; private set; }    // FBT = Überwachungszeit Feinstrom

        public static  ModbusCommand Delay_time_after_fine_flow { get; private set; }
        public static  ModbusCommand Activation_time_after_fine_flow { get; private set; }

        public static  ModbusCommand Systematic_difference { get; private set; }       // SYD = Systematische Differenz
        public static  ModbusCommand DownwardsDosing { get; private set; }             // data output word 42, application mode=filler
        public static  ModbusCommand Valve_control { get; private set; }               // VCT = Ventilsteuerung
        public static  ModbusCommand Emptying_mode { get; private set; }               // EMD = Entleermodus
    }
}
