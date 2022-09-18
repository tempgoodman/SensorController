using System;
using Sensor;
using SensorController;
namespace SensorController.Test;

public class InitialSensorTest
{
    private InitialSensors _initialSensors { get; set; }
    private Setting _setting { get; set; }

    [SetUp]
    public void Setup()
    {
        _initialSensors = new InitialSensors();
        _setting = new Setting()
        {
            Duration = 1,
            FinishedValue = 5,
            InitialValue = 0,
        };
    }

    [TestCase(10)]
    [TestCase(20)]
    public void CreateSensorListLengthTest(int count)
    {
        List< SensorObj> sensors = _initialSensors.Create(count);
        int actual = sensors.Count;
        Assert.That(count, Is.EqualTo(actual));
        Assert.Pass();
    }

    [TestCase(3)]
    [TestCase(5)]
    [TestCase(7)]
    public void CreateSensorListContentTest(int count)
    {
        List<SensorObj> sensors = _initialSensors.Create(count);
        Setting setting;
        int i = 0;        
        foreach (SensorObj s in sensors)
        {
            setting = s.Setting;
            Assert.That(setting.SensorID, Is.EqualTo(i + 1));
            Assert.That(setting.Seq, Is.EqualTo(i + 1));
            Assert.That(setting.SensorType, Is.EqualTo((SenType)(i % (Enum.GetNames(typeof(SenType)).Length))));
            Assert.That(i + 1, Is.EqualTo(setting.Seq));
            Assert.That(setting.Duration, Is.EqualTo(_setting.Duration));
            Assert.That(setting.FinishedValue, Is.EqualTo(_setting.FinishedValue));
            Assert.That(setting.InitialValue, Is.EqualTo(_setting.InitialValue));
            i++;
        }
        Assert.Pass();
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void CreateSensorListZeroNegativeParameterTest(int count)
    {
        List<SensorObj> sensors = _initialSensors.Create(count);
        Assert.IsEmpty(sensors);
        Assert.Pass();
    }
}
