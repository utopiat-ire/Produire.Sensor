using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using Windows.Devices.Sensors;
using System.Threading.Tasks;

namespace Produire.Sensor
{
	public class 方位センサー : IProduireClass
	{
		private Compass _compass;

		/// <summary>
		/// センサーから計測してその結果を返します。
		/// </summary>
		[自分で]
		public 方位情報 計測()
		{
			if (_compass == null) _compass = Compass.GetDefault();
			if (_compass != null)
			{
				var reading = _compass.GetCurrentReading();
				return new 方位情報(reading);
			}
			else
				return null;
		}
		/// <summary>
		/// 測定に使用するセンサーをデバイスIDで選択します。
		/// </summary>
		[自分から]
		public bool 選択(string ID)
		{
			Task<Compass> t1 = null;
			Task.Run(() =>
			{
				t1 = Compass.FromIdAsync(ID).AsTask<Compass>();
			}).Wait();
			_compass = t1.Result;
			return _compass != null;
		}
		public string デバイス名
		{
			get { return _compass.DeviceId; }
		}
	}
	public class 方位情報 : ProduireEventArgs
	{
		CompassReading reading;
		public 方位情報(CompassReading args)
		{
			this.reading = args;
		}
		public double 北角度
		{
			get { return reading.HeadingTrueNorth.Value; }
		}
		public double 方位
		{
			get { return reading.HeadingMagneticNorth; }
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
