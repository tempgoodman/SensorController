// See https://aka.ms/new-console-template for more information
using System;
using Sensor;

namespace SensorController
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //create sensor list
            InitialSensors initialSensors = new InitialSensors();
            List<SensorObj> sensors = initialSensors.Create(20);

            //for exercise 2, change the last 5 sensor initial value to 3
            for ( int i= sensors.Count; i >= sensors.Count - 5; i--)
            {
                sensors[i - 1].Setting.InitialValue = 3;
            }

            //check if no args, return msg
            if (args.Length == 0)
            {
                Console.WriteLine("Missing control pattern");
                return;
            }
            switch (args[0])
            {
                //sequence control pattern
                case "sequence":
                    SequenceProcessor sequenceProcessor = new SequenceProcessor();
                    sequenceProcessor.start(sensors);
                    break;
                //type control pattern
                case "type":
                    TypeProcessor typeProcessor = new TypeProcessor();
                    typeProcessor.start(sensors);
                    break;
                //not valid control pattern
                default:
                    Console.WriteLine("Control pattern not found");
                    break;
            }
            return;
        }
    }
}
