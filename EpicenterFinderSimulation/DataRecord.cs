using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicenterFinderSimulation
{
    class DataRecord
    {
        public DataRecord(Vector2D position, byte[] accelerometerData, DateTime startTime, DateTime endTime)
        {
            Position = position;
            AccelerometerData = accelerometerData;
            StartTime = startTime;
            EndTime = endTime;
            SampleRate = (int)(accelerometerData.Length / (endTime - startTime).TotalMilliseconds);
        }
        public Vector2D Position { get; set; }
        public byte[] AccelerometerData { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public int IndexAt(DateTime time) => (int)(time - StartTime).TotalMilliseconds * SampleRate;
        /// <summary>
        /// the sample rate in samples per ms
        /// </summary>
        public int SampleRate { get; }
        public byte[] Select(DateTime start, DateTime end)
        {
            byte[] ret = new byte[(int)((end - start).TotalMilliseconds * SampleRate) + 1];
            for (int i = IndexAt(start); i < ret.Length; i++)
                ret[i] = AccelerometerData[i];
            return ret;
        }
    }
}
