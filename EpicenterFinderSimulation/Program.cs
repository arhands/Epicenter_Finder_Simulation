using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicenterFinderSimulation
{
    class DataRecord
    {
        public DataRecord(Vector2D position, byte[] accelerometerData,DateTime startTime, DateTime endTime)
        {
            Position = position;
            AccelerometerData = accelerometerData;
            StartTime = startTime;
            EndTime = endTime;
            SampleRate = (int)(accelerometerData.Length/(endTime - startTime).TotalMilliseconds);
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
    class Line
    {
        public double X0 { get; set; }
        public double Y0 { get; set; }
        public double M { get; set; }
        public double Y(double x) => M * (x - X0);
        public static Vector2D FindIntersection(Line a, Line b)
        {
            Vector2D pos = new Vector2D((a.M - b.M) / (a.M * a.X0 - b.M * b.X0),0);
            pos.Y = a.Y(pos.X);
            return pos;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            
        }
        /// <summary>
        /// finds a line which the epicenter shall lie upon.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        Line FindEpicenterPosition(DataRecord a, DataRecord b)
        {
            throw new NotImplementedException("I find your lack of code disturbing");
        }
        /// <summary>
        /// finds the time delay between one and the other.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        double FindTimeDifferential(DataRecord a, DataRecord b)
        {
            DateTime t_1 = a.StartTime;
            if (t_1 < b.StartTime)
                t_1 = b.StartTime;

            DateTime t_2 = a.EndTime;
            if (t_2 < b.EndTime)
                t_2 = b.EndTime;

            //assuming sample rate is constant.
            int samples = (int)(a.SampleRate * (t_2 - t_1).TotalMilliseconds);
            int firstSample_a = (int)(t_1 - a.StartTime).TotalMilliseconds*a.SampleRate;
            int firstSample_b = (int)(t_1 - b.StartTime).TotalMilliseconds*b.SampleRate;
            //creating quotient series
            int recordDifferential = FindRecordDifferential(a.Select(t_1, t_2), b.Select(t_1, t_2));
            return recordDifferential / a.SampleRate;
        }
        /// <summary>
        /// a and b must have the same length. Finds the time difference between the first and second records.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        int FindRecordDifferential(byte[] a, byte[] b)
        {
            double recordDifferential = 0;
            double recordDifferential_last = 2;
            int count = 0;
            while(Math.Abs(recordDifferential_last - recordDifferential) >= 0.5 && count < 1000)
            {
                double f_last = a[(int)Math.Round(recordDifferential)] / b[0];
                double err = 0,derr = 0;
                for(int i = 1; i < a.Length; i++)
                {
                    double f = a[(int)Math.Round(recordDifferential) + i] / b[i];
                    //
                    double df = f - f_last;
                    recordDifferential = recordDifferential - f / df;
                    //
                    f_last = f;
                }
                count++;
            }
            return (int)Math.Round(recordDifferential);
        }
    }
}
