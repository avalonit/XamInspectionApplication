using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System.IO;
using Xamarin.Forms;
using Android.Content.Res;
using KobApp.DB;
using KobApp.Droid;

[assembly: Dependency(typeof(SQLite_Android))]
namespace KobApp.Droid
{
	public class SQLite_Android : ISQLite
	{
		public SQLite_Android()
		{
		}

		public SQLiteConnection GetConnection()
		{
			string dbName = Config.DBName;
			string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var path = Path.Combine(documentPath, dbName);
			if (!File.Exists(path))
			{
				CopyDatabase(path);
			}
			Config.DBPath = path;
			var Connection = new SQLite.SQLiteConnection(path);
			return Connection;
		}

		private bool CopyDatabase(string DatabasePath)
		{
			try
			{
				String dbSubFolderName = ""; //AV era "DB/"
				using (var br = new BinaryReader(Forms.Context.Assets.Open(dbSubFolderName + Config.DBName)))
				{
					using (var bw = new BinaryWriter(new FileStream(DatabasePath, FileMode.Create)))
					{
						byte[] buffer = new byte[2048];
						int length = 0;
						while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
						{
							bw.Write(buffer, 0, length);
						}
						bw.Close();
					}
					br.Close();
				}

				AssetManager assets = Forms.Context.Assets;

				System.Diagnostics.Debug.WriteLine("Database Copies Successfully.");
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error in copied Database : " + pException.Message);
				return false;
			}
			return true;
		}
	}
}