using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.JT808.Messages
{
    [MessageType(ID = 0x0200)]
    public class ClientPostion
    {
        public ClientPostion()
        {
            Status = new ClientPostionStatus();
            WarningMark = new ClientPostionWarningMark();
        }

        [UIntBitHandler(typeof(ClientPostionWarningMark))]
        public ClientPostionWarningMark WarningMark { get; set; }

        [UIntBitHandler(typeof(ClientPostionStatus))]
        public ClientPostionStatus Status { get; set; }

        [UIntHandler]
        public uint Longitude { get; set; }

        [UIntHandler]
        public uint Latitude { get; set; }

        [UInt16Handler]
        public ushort Height { get; set; }

        [UInt16Handler]
        public ushort Speed { get; set; }

        [UInt16Handler]
        public ushort Direction { get; set; }

        [TimeBCD]
        public DateTime Time { get; set; }


    }


 

    public class ClientPostionWarningMark : IBitCustomType
    {
        /// <summary>
        /// 紧急报瞥触动报警开关后触发
        /// </summary>
        public bool TouchAlarmSwitch { get; set; }

        /// <summary>
        /// 超速报警
        /// </summary>
        public bool SpeedLimit { get; set; }

        /// <summary>
        /// 疲劳驾驶
        /// </summary>
        public bool Fatigue { get; set; }

        /// <summary>
        /// 预警
        /// </summary>
        public bool Alert { get; set; }

        /// <summary>
        /// GNSS模块发生故障
        /// </summary>
        public bool GNSSModule { get; set; }

        /// <summary>
        /// GNSS天线未接或被剪断
        /// </summary>
        public bool GNSSCutAntenna { get; set; }

        /// <summary>
        /// GNSS天线短路
        /// </summary>
        public bool GNSSShortCircuit { get; set; }

        /// <summary>
        /// 终端主电源欠压
        /// </summary>
        public bool MainPowerVoltage { get; set; }

        /// <summary>
        /// 终端主电源掉电
        /// </summary>
        public bool MainPowerOff { get; set; }

        /// <summary>
        /// 终端LCD或显示器故障
        /// </summary>
        public bool DisplayTheFault { get; set; }

        /// <summary>
        /// TTS模块故障
        /// </summary>
        public bool TTSModuleFailure { get; set; }

        /// <summary>
        /// 摄像头故障
        /// </summary>
        public bool CameraMalfunction { get; set; }

        public bool Keep12 { get; set; }

        public bool Keep13 { get; set; }

        public bool Keep14 { get; set; }

        public bool Keep15 { get; set; }

        public bool Keep16 { get; set; }

        public bool Keep17 { get; set; }

        /// <summary>
        /// 驾驶超时
        /// </summary>
        public bool DrivingTimeoutOfDay { get; set; }

        /// <summary>
        /// 超时停车
        /// </summary>
        public bool StopTimeout { get; set; }

        /// <summary>
        /// 进出区域
        /// </summary>
        public bool InOutArea { get; set; }

        /// <summary>
        /// 进出路线
        /// </summary>
        public bool InOutLine { get; set; }

        /// <summary>
        /// 路段行驶时间
        /// </summary>
        public bool BritainsTime { get; set; }

        /// <summary>
        /// 路线偏离报警
        /// </summary>
        public bool LaneDeparture { get; set; }

        /// <summary>
        /// VSS故障
        /// </summary>
        public bool VSSFault { get; set; }

        /// <summary>
        /// 油量异常
        /// </summary>
        public bool OilFault { get; set; }

        /// <summary>
        /// 被盗
        /// </summary>
        public bool Stolen { get; set; }

        /// <summary>
        /// 非法点火
        /// </summary>
        public bool IllegalIgnition { get; set; }

        /// <summary>
        /// 车辆非法位移
        /// </summary>
        public bool IllegalDisplacement { get; set; }

        public bool Keep29 { get; set; }

        public bool Keep30 { get; set; }

        public bool Keep31 { get; set; }

        public void Load(object value)
        {
            uint data = (uint)value;
            TouchAlarmSwitch = Core.GetBitValue(data, 0);
            SpeedLimit = Core.GetBitValue(data, 1);
            Fatigue = Core.GetBitValue(data, 2);
            Alert = Core.GetBitValue(data, 3);
            GNSSModule = Core.GetBitValue(data, 4);
            GNSSCutAntenna = Core.GetBitValue(data, 5);
            GNSSShortCircuit = Core.GetBitValue(data, 6);
            MainPowerVoltage = Core.GetBitValue(data, 7);
            MainPowerOff = Core.GetBitValue(data, 8);
            DisplayTheFault = Core.GetBitValue(data, 9);
            TTSModuleFailure = Core.GetBitValue(data, 10);
            CameraMalfunction = Core.GetBitValue(data, 11);
            Keep12 = Core.GetBitValue(data, 12);
            Keep13 = Core.GetBitValue(data, 13);
            Keep14 = Core.GetBitValue(data, 14);
            Keep15 = Core.GetBitValue(data, 15);
            Keep16 = Core.GetBitValue(data, 16);
            Keep17 = Core.GetBitValue(data, 17);
            DrivingTimeoutOfDay = Core.GetBitValue(data, 18);
            StopTimeout = Core.GetBitValue(data, 19);
            InOutArea = Core.GetBitValue(data, 20);
            InOutLine = Core.GetBitValue(data, 21);
            BritainsTime = Core.GetBitValue(data, 22);
            LaneDeparture = Core.GetBitValue(data, 23);
            VSSFault = Core.GetBitValue(data, 24);
            OilFault = Core.GetBitValue(data, 25);
            Stolen = Core.GetBitValue(data, 26);
            IllegalIgnition = Core.GetBitValue(data, 27);
            IllegalDisplacement = Core.GetBitValue(data, 28);
            Keep29 = Core.GetBitValue(data, 29);
            Keep30 = Core.GetBitValue(data, 30);
            Keep31 = Core.GetBitValue(data, 31);

        }

        public object Save()
        {
            return Core.GetUIntBitValue(TouchAlarmSwitch, SpeedLimit, Fatigue, Alert, GNSSModule, GNSSCutAntenna, GNSSShortCircuit,
                MainPowerVoltage, MainPowerOff, DisplayTheFault, TTSModuleFailure, CameraMalfunction, Keep12,
                Keep13, Keep14, Keep15, Keep16, Keep17, DrivingTimeoutOfDay, StopTimeout, InOutArea, InOutLine,
                BritainsTime, LaneDeparture, VSSFault, OilFault, Stolen, IllegalIgnition, IllegalDisplacement,
                Keep29, Keep30, Keep31);
        }
    }

    public class ClientPostionStatus : IBitCustomType
    {
        public bool ACC { get; set; }

        public bool Location { get; set; }

        public bool Latitude { get; set; }

        public bool Longitude { get; set; }

        public bool Operate { get; set; }

        public bool Encryption { get; set; }

        public bool Keep6 { get; set; }

        public bool Keep7 { get; set; }

        public bool Keep8 { get; set; }

        public bool Keep9 { get; set; }

        public bool OilRoad { get; set; }

        public bool ElectricityRoad { get; set; }

        public bool DoorLock { get; set; }

        public bool Keep13 { get; set; }

        public bool Keep14 { get; set; }

        public bool Keep15 { get; set; }

        public bool Keep16 { get; set; }

        public bool Keep17 { get; set; }

        public bool Keep18 { get; set; }

        public bool Keep19 { get; set; }

        public bool Keep20 { get; set; }

        public bool Keep21 { get; set; }

        public bool Keep22 { get; set; }

        public bool Keep23 { get; set; }

        public bool Keep24 { get; set; }

        public bool Keep25 { get; set; }

        public bool Keep26 { get; set; }

        public bool Keep27 { get; set; }

        public bool Keep28 { get; set; }

        public bool Keep29 { get; set; }

        public bool Keep30 { get; set; }

        public bool Keep31 { get; set; }

        public void Load(object value)
        {
            uint data = (uint)value;
            ACC = Core.GetBitValue(data, 0);
            Location = Core.GetBitValue(data, 1);
            Latitude = Core.GetBitValue(data, 2);
            Longitude = Core.GetBitValue(data, 3);
            Operate = Core.GetBitValue(data, 4);
            Encryption = Core.GetBitValue(data, 5);
            Keep6 = Core.GetBitValue(data, 6);
            Keep7 = Core.GetBitValue(data, 7);
            Keep8 = Core.GetBitValue(data, 8);
            Keep9 = Core.GetBitValue(data, 9);
            OilRoad = Core.GetBitValue(data, 10);
            ElectricityRoad = Core.GetBitValue(data, 11);
            DoorLock = Core.GetBitValue(data, 12);
            Keep13 = Core.GetBitValue(data, 13);
            Keep14 = Core.GetBitValue(data, 14);
            Keep15 = Core.GetBitValue(data, 15);
            Keep16 = Core.GetBitValue(data, 16);
            Keep17 = Core.GetBitValue(data, 17);
            Keep18 = Core.GetBitValue(data, 18);
            Keep19 = Core.GetBitValue(data, 19);
            Keep20 = Core.GetBitValue(data, 20);
            Keep21 = Core.GetBitValue(data, 21);
            Keep22 = Core.GetBitValue(data, 22);
            Keep23 = Core.GetBitValue(data, 23);
            Keep24 = Core.GetBitValue(data, 24);
            Keep25 = Core.GetBitValue(data, 25);
            Keep26 = Core.GetBitValue(data, 26);
            Keep27 = Core.GetBitValue(data, 27);
            Keep28 = Core.GetBitValue(data, 28);
            Keep29 = Core.GetBitValue(data, 29);
            Keep30 = Core.GetBitValue(data, 30);
            Keep31 = Core.GetBitValue(data, 31);

        }

        public object Save()
        {
            return Core.GetUIntBitValue(ACC, Location, Latitude, Longitude, Operate, Encryption, Keep6, Keep7, Keep8, Keep9,
                OilRoad, ElectricityRoad, DoorLock, Keep13, Keep14, Keep15, Keep16, Keep17, Keep18, Keep19, Keep20, Keep21, Keep22,
                Keep23, Keep24, Keep25, Keep26, Keep27, Keep28, Keep29, Keep30, Keep31);
        }
    }
}
