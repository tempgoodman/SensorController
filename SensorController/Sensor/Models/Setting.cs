using System;
namespace Sensor
{ 
    //sensor setting, every sensor have individual setting
    public class Setting
    {
        private int finishedValue;
        public SenType SensorType { get; set; }
        public int Duration { get; set; }
        public int InitialValue { get; set; }
        public int FinishedValue
        {
            get { return finishedValue - InitialValue; } // default return value offset the initial value 
            set { finishedValue = value; }
        }

        public int Seq { get; set; }
        public int SensorID { get; set; }
    }
}

