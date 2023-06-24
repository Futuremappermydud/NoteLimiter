using HarmonyLib;
using NoteLimiter.Configuration;

namespace NoteLimiter.HarmonyPatches
{
	[HarmonyPatch(typeof(NoteJump), "Init")]
	public class NoteJumpPatch
	{
		public static NoteCutDirection GetCardinalDirection(NoteCutDirection currentCutDirection)
		{
			switch (currentCutDirection)
			{
				case NoteCutDirection.Any:
					return NoteCutDirection.Any;
				case NoteCutDirection.UpLeft:
					return NoteCutDirection.Up;
				case NoteCutDirection.UpRight:
					return NoteCutDirection.Right;
				case NoteCutDirection.DownLeft:
					return NoteCutDirection.Left;
				case NoteCutDirection.DownRight:
					return NoteCutDirection.Down;
				case NoteCutDirection.Up:
				case NoteCutDirection.Down:
				case NoteCutDirection.Left:
				case NoteCutDirection.Right:
					return currentCutDirection;
			}

			return currentCutDirection;
		}
		public static NoteCutDirection GetDirectionalDirection(NoteCutDirection currentCutDirection)
		{
			switch (currentCutDirection)
			{
				case NoteCutDirection.Any:
					return NoteCutDirection.Any;
				case NoteCutDirection.Up:
					return NoteCutDirection.UpRight;
				case NoteCutDirection.Down:
					return NoteCutDirection.DownLeft;
				case NoteCutDirection.Left:
					return NoteCutDirection.DownRight;
				case NoteCutDirection.Right:
					return NoteCutDirection.UpLeft;
				case NoteCutDirection.UpLeft:
				case NoteCutDirection.UpRight:
				case NoteCutDirection.DownLeft:
				case NoteCutDirection.DownRight:
					return currentCutDirection;
				default:
					return currentCutDirection;
			}
		}
		public static NoteCutDirection GetNewDirection(Config.LimitType limitType, NoteCutDirection currentCutDirection)
		{
			switch (limitType)
			{
				case Config.LimitType.All:
					return currentCutDirection;
				case Config.LimitType.Cardinal:
					return GetCardinalDirection(currentCutDirection);
				case Config.LimitType.Diagonal:
					return GetDirectionalDirection(currentCutDirection);
				case Config.LimitType.Up:
					return NoteCutDirection.Up;
				case Config.LimitType.Down:
					return NoteCutDirection.Down;
				case Config.LimitType.Left:
					return NoteCutDirection.Left;
				case Config.LimitType.Right:
					return NoteCutDirection.Right;
				case Config.LimitType.UpLeft:
					return NoteCutDirection.UpLeft;
				case Config.LimitType.UpRight:
					return NoteCutDirection.UpRight;
				case Config.LimitType.DownLeft:
					return NoteCutDirection.DownLeft;
				case Config.LimitType.DownRight:
					return NoteCutDirection.DownRight;
				default:
					return currentCutDirection;
			}
		}
		public static void Prefix(NoteJump __instance, ref float endRotation)
		{
			if (!Config.Instance.Enabled) return;
			var note = __instance.GetComponent<NoteController>();
			var color = note.noteData.colorType;
			if(color == ColorType.None) return;
			var direction = note.noteData.cutDirection;
			var limitType = color == ColorType.ColorA ? Config.Instance.LeftNoteLimitType : Config.Instance.RightNoteLimitType;
			if (limitType != Config.LimitType.All)
			{
				BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("NoteLimiter");
			}
			endRotation = GetNewDirection(limitType, direction).RotationAngle();
			Plugin.Log.Info(endRotation.ToString());
			Plugin.Log.Info(limitType.ToString());
			Plugin.Log.Info(color.ToString());
		}
	}
}