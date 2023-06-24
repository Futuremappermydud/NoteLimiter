using System;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using NoteLimiter.Configuration;


namespace NoteLimiter.UI
{
	internal class GameplaySetupView
	{
		[UIValue("enabled")]
		public bool Enabled
		{
			get => Config.Instance.Enabled;
			set => Config.Instance.Enabled = value;
		}

		[UIValue("available-limits")]
		private readonly List<object> _limitTypes = Enum.GetNames(typeof(Config.LimitType)).Select(x=>(object)x).ToList();

		[UIValue("left-limit")]
		public string LeftLimitType
		{
			get => Config.Instance.LeftNoteLimitType.ToString();
			set => Config.Instance.LeftNoteLimitType = (Config.LimitType)Enum.Parse(typeof(Config.LimitType), value);
		}

		[UIValue("right-limit")]
		public string RightLimitType
		{
			get => Config.Instance.RightNoteLimitType.ToString();
			set => Config.Instance.RightNoteLimitType = (Config.LimitType)Enum.Parse(typeof(Config.LimitType), value);
		}
	}
}
