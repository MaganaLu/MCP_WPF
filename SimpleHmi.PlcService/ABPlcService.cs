using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using libplctag;
using libplctag.DataTypes;


namespace SimpleHmi.PlcService
{
    public class ABPlcService : IPlcService
    {
        
        private readonly System.Timers.Timer _timer;
        private DateTime _lastScanTime;

        private volatile object _locker = new object();

        public ABPlcService()
        {
            _timer = new System.Timers.Timer();
            _timer.Interval = 100;
            _timer.Elapsed += OnTimerElapsed;
        }

        public ConnectionStates ConnectionState { get; private set; }

        public bool HighLimit { get; private set; }

        public bool PS04_04_Control_Inp_MaintEnable {  get; private set; }

        public bool LowLimit { get; private set; }

        public bool PumpState { get; private set; }

        public int TankLevel { get; private set; }

        public TimeSpan ScanTime { get; private set; }

        public int InletPumpSpeed { get; private set; }

        public int OutletPumpSpeed { get; private set; }

        public event EventHandler ValuesRefreshed;

        const int TAG_STRING_SIZE = 200;
        int TIMEOUT = 10;

        List<Tag<BoolPlcMapper, bool>> allTags = new List<Tag<BoolPlcMapper, bool>>();

        public void InitializeTags(List<string> tagList, string ipAddress)
        {
            foreach (string tagName in tagList)
            {
                var currTag = new Tag<BoolPlcMapper, bool>()
                {
                    //Name of tag on the PLC, Controller-scoped would be just "SomeDINT"
                    Name = tagName,

                    //PLC IP Address
                    Gateway = ipAddress,

                    //CIP path to PLC CPU. "1,0" will be used for most AB PLCs
                    Path = "1,0",

                    //Type of PLC
                    PlcType = PlcType.ControlLogix,

                    //Protocol
                    Protocol = Protocol.ab_eip,

                    //A global timeout value that is used for Initialize/Read/Write methods
                    //Timeout = TimeSpan.FromMilliseconds(TIMEOUT),
                };
                currTag.Initialize();
                allTags.Add(currTag);

                //Debug.WriteLine((allTags.Find(tag => tag.Name == "FMS_Global_Enabled")).Value);
                //Debug.WriteLine((allTags.Find(tag => tag.Name == "PS04_04.Control.Inp_MaintEnable")).Value);
            }




        }


        static int GetUdtId(TagInfo tag)
        {
            const ushort TYPE_UDT_ID_MASK = 0x0FFF;
            return tag.Type & TYPE_UDT_ID_MASK;
        }

        static bool TagIsUdt(TagInfo tag)
    {
        const ushort TYPE_IS_STRUCT = 0x8000;
        const ushort TYPE_IS_SYSTEM = 0x1000;

        return ((tag.Type & TYPE_IS_STRUCT) != 0) && !((tag.Type & TYPE_IS_SYSTEM) != 0);
    }

        static UdtInfo DecodeUdtInfo(Tag tag)
        {

            var template_id = tag.GetUInt16(0);
            var member_desc_size = tag.GetUInt32(2);
            var udt_instance_size = tag.GetUInt32(6);
            var num_members = tag.GetUInt16(10);
            var struct_handle = tag.GetUInt16(12);

            var udtInfo = new UdtInfo()
            {
                Fields = new UdtFieldInfo[num_members],
                NumFields = num_members,
                Handle = struct_handle,
                Id = template_id,
                Size = udt_instance_size
            };

            var offset = 14;

            for (int field_index = 0; field_index < num_members; field_index++)
            {
                ushort field_metadata = tag.GetUInt16(offset);
                offset += 2;

                ushort field_element_type = tag.GetUInt16(offset);
                offset += 2;

                ushort field_offset = tag.GetUInt16(offset);
                offset += 4;

                var field = new UdtFieldInfo()
                {
                    Offset = field_offset,
                    Metadata = field_metadata,
                    Type = field_element_type,
                };

                udtInfo.Fields[field_index] = field;
            }

            var name_str = tag.GetString(offset).Split(';')[0];
            udtInfo.Name = name_str;

            offset += tag.GetStringTotalLength(offset);

            for (int field_index = 0; field_index < num_members; field_index++)
            {
                udtInfo.Fields[field_index].Name = tag.GetString(offset);
                offset += tag.GetStringTotalLength(offset);
            }

            return udtInfo;

        }

        static TagInfo[] DecodeAllTagInfos(Tag tag)
        {
            var buffer = new List<TagInfo>();

            var tagSize = tag.GetSize();

            int offset = 0;
            while (offset < tagSize)
            {
                buffer.Add(DecodeOneTagInfo(tag, offset, out int elementSize));
                offset += elementSize;
            }

            return buffer.ToArray();
        }

        static TagInfo DecodeOneTagInfo(Tag tag, int offset, out int elementSize)
        {

            var tagInstanceId = tag.GetUInt32(offset);
            var tagType = tag.GetUInt16(offset + 4);
            var tagLength = tag.GetUInt16(offset + 6);
            var tagArrayDims = new uint[]
            {
                tag.GetUInt32(offset + 8),
                tag.GetUInt32(offset + 12),
                tag.GetUInt32(offset + 16)
            };

            var apparentTagNameLength = (int)tag.GetUInt16(offset + 20);
            var actualTagNameLength = Math.Min(apparentTagNameLength, TAG_STRING_SIZE * 2 - 1);

            var tagNameBytes = Enumerable.Range(offset + 22, actualTagNameLength)
                .Select(o => tag.GetUInt8(o))
                .Select(Convert.ToByte)
                .ToArray();

            var tagName = Encoding.ASCII.GetString(tagNameBytes);

            elementSize = 22 + actualTagNameLength;

            return new TagInfo()
            {
                Id = tagInstanceId,
                Type = tagType,
                Name = tagName,
                Length = tagLength,
                Dimensions = tagArrayDims
            };

        }


        public Task WriteSpeedInletPump(short speed)
        {
            return Task.Run(() => {
                
            });

        }

        public Task WriteSpeedOutletPump(short speed)
        {
            return Task.Run(() => {
                
            });
        }

        public void Disconnect()
        {
            Debug.WriteLine("disconenct here");
        }

        private int WriteWord(string address, short value)
        {
            return 0;
        }

        private int WriteWord(int dbNumber, int startIndex, short value)
        {
            return 0;
        }

        public async Task WriteStart()
        {
            await Task.Run(() =>
            {
                Debug.WriteLine("write STart here");
            });
        }

        public async Task WriteStop()
        {
            await Task.Run(() =>
            {
               
            });
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                _timer.Stop();
                ScanTime = DateTime.Now - _lastScanTime;
                RefreshValues();
                OnValuesRefreshed();
            }
            finally
            {
                _timer.Start();
            }
            _lastScanTime = DateTime.Now;
        }

        private void RefreshValues()
        {
            lock (_locker)
            {
                //PS04_04_Control_Inp_MaintEnable = 
            }
        }

        /// <summary>
        /// Writes a bit at the specified address. Es.: DB1.DBX10.2 writes the bit in db 1, word 10, 3rd bit
        /// </summary>
        /// <param name="address">Es.: DB1.DBX10.2 writes the bit in db 1, word 10, 3rd bit</param>
        /// <param name="value">true or false</param>
        /// <returns></returns>
        private int WriteBit(string address, bool value)
        {
            return 0;
        }

        private int WriteBit(int db, int pos, int bit, bool value)
        {
            lock (_locker)
            {
                
            }
            return 0;
        }

        private void OnValuesRefreshed()
        {
            ValuesRefreshed?.Invoke(this, new EventArgs());
        }
    }
}

