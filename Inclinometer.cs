using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using System.Drawing;
using Windows.Devices.Sensors;

namespace Produire.Sensor
{
	public class 傾斜計センサー : IProduireClass
	{
		private Inclinometer _inclinometer;

		[自分で]
		public 傾斜情報 計測()
		{
			if (_inclinometer == null) _inclinometer = Inclinometer.GetDefault();
			if (_inclinometer != null)
			{
				var reading = _inclinometer.GetCurrentReading();
				return new 傾斜情報(reading);
			}
			else
				return null;
		}
		public string デバイス名
		{
			get { return _inclinometer.DeviceId; }
		}
	}
	public class 傾斜情報 : IProduireClass
	{
		InclinometerReading reading;
		public 傾斜情報(InclinometerReading args)
		{
			this.reading = args;
		}
		public float ピッチ
		{
			get { return reading.PitchDegrees; }
		}
		public float ロール
		{
			get { return reading.RollDegrees; }
		}
		public float ヨー
		{
			get { return reading.YawDegrees; }
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
