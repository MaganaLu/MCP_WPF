﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleHmi.PlcService
{
    public interface IPlcService
    {
        ConnectionStates ConnectionState { get; }
        bool HighLimit { get; }
        int InletPumpSpeed { get; }
        bool LowLimit { get; }
        int OutletPumpSpeed { get; }
        bool PumpState { get; }
        TimeSpan ScanTime { get; }
        int TankLevel { get; }

        event EventHandler ValuesRefreshed;

        void InitializeTags(List<string> tagList, string ipAddress);
        void Disconnect();
        Task WriteSpeedInletPump(short speed);
        Task WriteSpeedOutletPump(short speed);
        Task WriteStart();
        Task WriteStop();
    }
}