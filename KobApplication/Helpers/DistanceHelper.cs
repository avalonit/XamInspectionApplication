using System;
using Plugin.Geolocator.Abstractions;

namespace KobApplication
{
	public class DistanceHelper
	{
		public DistanceHelper()
		{
		}
		public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
		{
			double rlat1 = Math.PI * lat1 / 180;
			double rlat2 = Math.PI * lat2 / 180;
			double theta = lon1 - lon2;
			double rtheta = Math.PI * theta / 180;
			double dist =
				Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
				Math.Cos(rlat2) * Math.Cos(rtheta);
			dist = Math.Acos(dist);
			dist = dist * 180 / Math.PI;
			dist = dist * 60 * 1.1515;

			switch (unit)
			{
				case 'K': //Kilometers -> default
					return dist * 1.609344;
				case 'N': //Nautical Miles 
					return dist * 0.8684;
				case 'M': //Miles
					return dist;
			}

			return dist;
		}

		public static double DistanceTo(Position p1, Position p2, char unit = 'K')
		{
			double lat1=p1.Latitude;
			double lon1=p1.Longitude;
			double lat2=p2.Latitude;
			double lon2=p2.Longitude;

			double rlat1 = Math.PI * lat1 / 180;
			double rlat2 = Math.PI * lat2 / 180;
			double theta = lon1 - lon2;
			double rtheta = Math.PI * theta / 180;
			double dist =
				Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
				Math.Cos(rlat2) * Math.Cos(rtheta);
			dist = Math.Acos(dist);
			dist = dist * 180 / Math.PI;
			dist = dist * 60 * 1.1515;

			switch (unit)
			{
				case 'K': //Kilometers -> default
					return dist * 1.609344;
				case 'N': //Nautical Miles 
					return dist * 0.8684;
				case 'M': //Miles
					return dist;
			}

			return dist;
		}

		public static Boolean IsNear(Position p1, Position p2, char unit = 'K')
		{
			double dist = DistanceTo(p1, p2);
			return dist <= 0.5;
		}
	}
}
