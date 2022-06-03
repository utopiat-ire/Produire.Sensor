using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using Windows.Devices.Sensors;

namespace Produire.Sensor
{
	public class 光センサー : IProduireClass
	{
		private LightSensor _lightsensor;

		[自分で]
		public 光量情報 計測()
		{
			if (_lightsensor == null) _lightsensor = LightSensor.GetDefault();
			if (_lightsensor != null)
			{
				var reading = _lightsensor.GetCurrentReading();
				return new 光量情報(reading);
			}
			else
				return null;
		}
		public string デバイス名
		{
			get { return _lightsensor.DeviceId; }
		}
	}
	public class 光量情報 : IProduireClass
	{
		LightSensorReading reading;
		public 光量情報(LightSensorReading args)
		{
			this.reading = args;
		}
		public float 光量
		{
			get { return reading.IlluminanceInLux; }
		}
		public DateTimeOffset 測定時刻
		{
			get { return reading.Timestamp; }
		}
		public TimeSpan 測定回数
		{
			get { return reading.PerformanceCount.Value; }
		}
	}
}
