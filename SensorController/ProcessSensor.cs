using System;
using System.Collections.Generic;
using Sensor;
namespace SensorController
{
    //Handle sensor from start to stop
    public class ProcessSensor
    {
        public SensorObj sensor;
        public bool finished { get; set; }
        public ProcessSensor(SensorObj _sensor)
        {
            sensor = _sensor;
            //set the callback to sensor
            sensor.callback += SensorCallBack;
            finished = false;
        }
        public void Run()
        {
            //Start sensor and receive the sensor result
            Result result = sensor.start();
            Console.WriteLine("{0} Sensor {1} {2} {3} {4}", result.RecordDateTime, result.Seq.ToString(), SensorTypeTranslate(result.Type), result.States, result.ReturnValue);
        }

        //callback function for senor alert the controller finsihed the data recording
        public void SensorCallBack()
        {
            // stop the sensor and receive the sensor last result
            Result result = sensor.Stop();
            Console.WriteLine("{0} Sensor {1} {2} {3} {4}", result.RecordDateTime, result.Seq.ToString(), SensorTypeTranslate(result.Type), result.States, result.ReturnValue);
            //update the finished flag 
            finished = true;
        }
        // translate the Sensor type enum to readable string
        public string SensorTypeTranslate(SenType senType)
        {
            switch(senType)
            {
                case SenType.xAxis:
                    return "x-axis";
                case SenType.yAxis:
                    return "y-axis";
                case SenType.zAxis:
                    return "z-axis";
                default:
                    return "";
            }
        }
    }
}

