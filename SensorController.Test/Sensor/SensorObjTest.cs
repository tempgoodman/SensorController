using System;
using Sensor;

namespace SensorController.Test
{
    public class SensorObjTest
    {
        private Setting _setting { get; set; }
        private SensorObj? _sensorObj { get; set; }
        private int callBackFlag { get; set; }
        [SetUp]
        public void Setup()
        {
            _setting = new Setting()
            {
                Duration = 1,
                FinishedValue = 5,
                InitialValue = 0,
                SensorID = 1,
                SensorType = SenType.xAxis,
                Seq = 1
            };
                     
        }

        [TestCase(0)]
        [TestCase(3)]
        public void SensorObjStartTest(int initialValue)
        {
            callBackFlag = 0;
            _sensorObj = new SensorObj(_setting);
            _sensorObj.Setting.InitialValue = initialValue;
            _sensorObj.callback += SensorCallBack;
            Result actual = _sensorObj.start();
            Assert.That(actual.SensorID, Is.EqualTo(_setting.SensorID));
            Assert.That(actual.States, Is.EqualTo(RecordingStates.start));
            Assert.That(actual.Seq, Is.EqualTo(_setting.Seq));
            Assert.That(actual.ReturnValue, Is.EqualTo(initialValue));
            Assert.That(actual.Type, Is.EqualTo(_setting.SensorType));
            Thread.Sleep(2000);
            Assert.That(callBackFlag, Is.EqualTo(1));
            Assert.That(actual.RecordDateTime, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(5.0)));
            Assert.Pass();

        }

        [TestCase(0)]
        [TestCase(3)]
        public void SensorObjStopTest(int initialValue)
        {
            _sensorObj = new SensorObj(_setting);
            _sensorObj.Setting.InitialValue = initialValue;
            _sensorObj.Setting.FinishedValue = 5;
            _sensorObj.callback += SensorCallBack;
            Result startResult = _sensorObj.start();
            Thread.Sleep(2000);
            Assert.IsNotNull(startResult);
            Result actual = _sensorObj.Stop();

            Assert.That(callBackFlag, Is.EqualTo(1));
            Assert.That(actual.SensorID, Is.EqualTo(_setting.SensorID));
            Assert.That(actual.States, Is.EqualTo(RecordingStates.stop));
            Assert.That(actual.Seq, Is.EqualTo(_setting.Seq));
            Assert.That(actual.ReturnValue, Is.EqualTo(5 - initialValue));
            Assert.That(actual.Type, Is.EqualTo(_setting.SensorType));
            Assert.That(actual.RecordDateTime, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(5.0)));
            Assert.Pass();
        }

        public void SensorCallBack()
        {
            callBackFlag = 1;
        }
    }
}

