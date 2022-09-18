using System;
using Sensor;
namespace SensorController
{
    public class InitialSensors
    {
        public InitialSensors()
        {
        }
        //create the sensor list 
        public List<SensorObj> Create(int numberOfSensor)
        {
            List<SensorObj> sensors = new List<SensorObj>();

            for(int i = 1; i <= numberOfSensor;  i++)
            {
                //create sensor setting for sensor
                Setting setting = new Setting()
                {
                    Duration = 1,
                    FinishedValue = 5,
                    InitialValue = 0,
                    SensorID = i,
                    SensorType = (SenType)((i-1) % (Enum.GetNames(typeof(SenType)).Length)),
                    Seq = i
                };
                //create sensor
                SensorObj sensor = new SensorObj(setting);
                sensors.Add(sensor);
            }
            return sensors;
        }
    }
}

