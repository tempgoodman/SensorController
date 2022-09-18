using System;
using System.Threading;
namespace Sensor
{
    public class SensorObj
    {
        public Setting Setting { get; set; }
        private Process? process;
        public delegate void Callback();
        public event Callback callback;

        public SensorObj(Setting sensorSetting)
        {
            Setting = sensorSetting;
        }

        //Stop the sensor core and return the last result
        public Result Stop()
        {
            if (process?.Result != null)
            {
                process.halt = true;
                Result result = process.Result;
                result.States = RecordingStates.stop;
                return result;
            }
            else
                throw new Exception("Not Start Yet");
        }

        //callback for the sensor core to alert the sensor finished the data recording
        public void ProcessCallback()
        {
            callback();
        }

        //Start the sensor core and return the start result
        public Result start()
        {
            Result returnResult = new Result()
            {
                SensorID = Setting.SensorID,
                States = RecordingStates.start,
                Seq = Setting.Seq,
                RecordDateTime = DateTime.Now,
                ReturnValue = Setting.InitialValue,
                Type = Setting.SensorType
            };
            process = new Process(Setting);
            //set callback function to sensor core
            process.callback += ProcessCallback;
            Thread thread = new Thread(process.Run); // start the sensor core in thread
            thread.Start();
            return returnResult;
        }
    }
}

