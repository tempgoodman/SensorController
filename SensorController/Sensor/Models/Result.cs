using System;
namespace Sensor
{
    //Sensor record data class
    public class Result
    {
        public DateTime RecordDateTime { get; set; }
        public SenType Type{ get; set; }
        public int ReturnValue { get; set; }
        public int Seq { get; set; }
        public int SensorID { get; set; }
        public RecordingStates States { get; set; }
    }
}

