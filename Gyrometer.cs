using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using Windows.Devices.Sensors;

namespace Produire.Sensor
{
	public class ジャイロセンサー : IProduireClass
	{
		private Gyrometer _gyrometer;

		[自分で]
		public ジャイロ情報 計測()
		{
			if (_gyrometer == null) _gyrometer = Gyrometer.GetDefault();
			if (_gyrometer != null)
			{
				var reading = _gyrometer.GetCurrentReading();
				return new ジャイロ情報(reading);
			}
			else
				return null;
		}
		public string デバイス名
		{
			get { return _gyrometer.DeviceId; }
		}
	}
	public class ジャイロ情報 : IProduireClass
	{
		GyrometerReading reading;
		public ジャイロ情報(GyrometerReading args)
		{
			this.reading = args;
		}
		public double X
		{
			get { return reading.AngularVelocityX; }
		}
		public double Y
		{
			get { return reading.AngularVelocityY; }
		}
		public double Z
		{
			get { return reading.AngularVelocityZ; }
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
