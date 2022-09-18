using System;
using Sensor;
namespace SensorController
{
    //Sequence control pattern
    public class SequenceProcessor
    {
        public SequenceProcessor()
        {
        }
        public int start(List<SensorObj> sensors)
        {
            //run sensor one by one
            foreach (SensorObj sensor in sensors)
            {
                ProcessSensor processSensor = new ProcessSensor(sensor);
                processSensor.Run();

                //sleep unit the sensor is finished
                while (!processSensor.finished)
                {
                    Thread.Sleep(100);
                }
            }
            return 0;
        }
    }
}

