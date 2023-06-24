using HarmonyLib;
using IPA;
using IPA.Config.Stores;
using NoteLimiter.Configuration;
using NoteLimiter.UI;
using IPALogger = IPA.Logging.Logger;

namespace NoteLimiter
{
	[Plugin(RuntimeOptions.SingleStartInit)]
	public class Plugin
	{
		internal static Plugin Instance { get; private set; }
		internal static Harmony Harmony = new Harmony("com.FutureMapper.NoteLimiter");
		internal static IPALogger Log { get; private set; }

		[Init]
		public Plugin(IPALogger logger, IPA.Config.Config conf)
		{
			Instance = this;
			Log = logger;

			Config.Instance = conf.Generated<Config>(true);
		}

		[OnStart]
		public void OnStart()
		{
			Harmony.PatchAll();

			BeatSaberMarkupLanguage.GameplaySetup.GameplaySetup.instance.AddTab("NoteLimiter", "NoteLimiter.UI.GameplaySetupView", new GameplaySetupView());
		}

		[OnExit]
		public void OnExit()
		{
			Harmony.UnpatchSelf();;
		}
	}
}