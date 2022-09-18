using System;
namespace Sensor
{
    //Sensor core
    public class Process
    {
        public Result? Result { get; set; }
        public bool halt { get; set; }
        private Setting setting;
        public delegate void Callback();
        public event Callback callback;

        public Process(Setting _setting)
        {
            setting = _setting;
        }
        // run one second will renew the result until "halt" set to ture
        public void Run()
        {
            halt = false;
            Result = new Result();
            while (!halt)
            {
                Thread.Sleep(setting.Duration * 1000);
                Result.RecordDateTime = DateTime.Now;
                Result.ReturnValue = setting.FinishedValue; ;
                Result.SensorID = setting.SensorID;
                Result.States = RecordingStates.stop;
                Result.Seq = setting.Seq;
                Result.Type = setting.SensorType;
                callback();
            }
        }
    }
}

