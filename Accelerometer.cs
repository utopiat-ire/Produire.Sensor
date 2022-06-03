using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using Windows.Devices.Sensors;

namespace Produire.Sensor
{
	public class 加速度センサー : IProduireClass
	{
		private Accelerometer _accelerometer;

		[自分で]
		public 加速度情報 計測()
		{
			if (_accelerometer == null) _accelerometer = Accelerometer.GetDefault();
			if (_accelerometer != null)
			{
				var reading = _accelerometer.GetCurrentReading();
				return new 加速度情報(reading);
			}
			else
				return null;
		}
		public string デバイス名
		{
			get { return _accelerometer.DeviceId; }
		}
	}
	public class 加速度情報 : IProduireClass
	{
		AccelerometerReading reading;
		public 加速度情報(AccelerometerReading args)
		{
			this.reading = args;
		}
		public double X
		{
			get { return reading.AccelerationX; }
		}
		public double Y
		{
			get { return reading.AccelerationY; }
		}
		public double Z
		{
			get { return reading.AccelerationZ; }
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
