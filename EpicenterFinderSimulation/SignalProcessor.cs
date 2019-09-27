using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicenterFinderSimulation
{
    class SignalProcessor
    {
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
            int firstSample_a = (int)(t_1 - a.StartTime).TotalMilliseconds * a.SampleRate;
            int firstSample_b = (int)(t_1 - b.StartTime).TotalMilliseconds * b.SampleRate;
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
            while (Math.Abs(recordDifferential_last - recordDifferential) >= 0.5 && count < 1000)
            {
                double f_last = a[(int)Math.Round(recordDifferential)] / b[0];
                double err = 0, derr = 0;
                for (int i = 1; i < a.Length; i++)
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
