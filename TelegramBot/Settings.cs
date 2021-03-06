﻿using System.IO;
using Newtonsoft.Json;

namespace TelegramBot
{
	public class Settings
	{
		private const string SETTINGSFILE = "TelegramSettings.json";
		private static readonly object LoadLock;

		public static Settings Instance { get; private set; }

		static Settings ( )
		{
			LoadLock = new object ( );
			Instance = new Settings ( );
			Load ( );
			Save ( );
		}

		public static void Save ( )
		{
			lock ( LoadLock )
				File.WriteAllText ( SETTINGSFILE, JsonConvert.SerializeObject ( Instance, Formatting.Indented ) );
		}

		public static void Load ( )
		{
			lock ( LoadLock )
			{
				if ( File.Exists ( SETTINGSFILE ) )
					Instance = JsonConvert.DeserializeObject<Settings> ( File.ReadAllText ( SETTINGSFILE ) );
			}
		}

		#region Properties

		public string BotToken { get; set; }
		public bool WhitelistMode { get; set; }
		public string UsersFileName { get; set; }
		public string PurchaseMessageText { get; set; }

		#endregion Properties
	}
}