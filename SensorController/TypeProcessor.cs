using System;
using Sensor;
using System.Threading;
using System.Threading.Tasks;

namespace SensorController
{
    //Type control pattern
    public class TypeProcessor
    {
        public TypeProcessor()
        {
        }
        public int start(List<SensorObj> sensors)
        {
            //run x-axis sensor only
            List<SensorObj> sensorsFilted = sensors.Where(c => c.Setting.SensorType == SenType.xAxis).ToList();

            //create task array for x-axis sensor
            Task[] tasks = new Task[sensorsFilted.Count];
            int i = 0;
            foreach (SensorObj sensor in sensorsFilted)
            {
                ProcessSensor processSensor = new ProcessSensor(sensor);
                //assign sensor to task and run
                tasks[i] = Task.Run(() => processSensor.Run());
                i++;
            }
            try
            {
                //wait for all task finished
                Task.WaitAll(tasks);
            }
            catch (AggregateException ae)
            {
                Console.WriteLine("One or more exceptions occurred: ");
                foreach (var ex in ae.Flatten().InnerExceptions)
                    Console.WriteLine("   {0}", ex.Message);
            }
            return 0;
        }
    }
}

