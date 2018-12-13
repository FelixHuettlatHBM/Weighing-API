﻿// <copyright file="DataEvent.cs" company="Hottinger Baldwin Messtechnik GmbH">
//
// HBM.Weighing.API, a library to communicate with HBM weighing technology devices  
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
using HBM.Weighing.API.WTX;
using System;

namespace HBM.Weighing.API
{
    public class DeviceDataReceivedEventArgs : EventArgs 
    {
        private IDeviceData _processData; 

        public DeviceDataReceivedEventArgs(IDeviceData _Idata)
        {
            _processData = _Idata;
        }
       
        public IDeviceData ProcessData
        {
            get
            {
                return _processData;
            }
            set
            {
                _processData = value;
            }
        }

    }
}



// Before: 
/*
public class DeviceDataReceivedEventArgs : EventArgs
{
    private ushort[] _ushortArgs;
    private string[] _strArgs;
    

    public DeviceDataReceivedEventArgs(ushort[] _ushortArrayParam, string[] _strArrayParam)
    {
        _ushortArgs = _ushortArrayParam;
        _strArgs = _strArrayParam;
    }

    public ushort[] ushortArgs
    {
        get
        {
            return _ushortArgs;
        }
        set
        {
            _ushortArgs = value;
        }
    }

    public string[] strArgs
    {
        get
        {
            return _strArgs;
        }
        set
        {
            _strArgs = value;
        }
    }
}
*/