using HarmonyLib;
using NoteLimiter.Configuration;

namespace NoteLimiter.HarmonyPatches
{
	[HarmonyPatch(typeof(NoteJump), "Init")]
	public class NoteJumpPatch
	{
		public static NoteCutDirection GetCardinalCutDirection(NoteCutDirection currentCutDirection)
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
		public static NoteCutDirection GetDiagonalCutDirection(NoteCutDirection currentCutDirection)
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
		public static NoteCutDirection GetHorizontalDirection(NoteCutDirection currentCutDirection)
		{
			switch (currentCutDirection)
			{
				case NoteCutDirection.Any:
					return NoteCutDirection.Any;
				case NoteCutDirection.Up:
					return NoteCutDirection.Left;
				case NoteCutDirection.Down:
					return NoteCutDirection.Right;
				case NoteCutDirection.Left:
					return NoteCutDirection.Left;
				case NoteCutDirection.Right:
					return NoteCutDirection.Right;
				case NoteCutDirection.UpLeft:
					return NoteCutDirection.Left;
				case NoteCutDirection.UpRight:
					return NoteCutDirection.Right;
				case NoteCutDirection.DownLeft:
					return NoteCutDirection.Left;
				case NoteCutDirection.DownRight:
					return NoteCutDirection.Right;
				default:
					return currentCutDirection;
			}
		}
		public static NoteCutDirection GetVerticalDirection(NoteCutDirection currentCutDirection)
		{
			switch (currentCutDirection)
			{
				case NoteCutDirection.Any:
					return NoteCutDirection.Any;
				case NoteCutDirection.Up:
					return NoteCutDirection.Up;
				case NoteCutDirection.Down:
					return NoteCutDirection.Down;
				case NoteCutDirection.Left:
					return NoteCutDirection.Up;
				case NoteCutDirection.Right:
					return NoteCutDirection.Down;
				case NoteCutDirection.UpLeft:
					return NoteCutDirection.Up;
				case NoteCutDirection.UpRight:
					return NoteCutDirection.Up;
				case NoteCutDirection.DownLeft:
					return NoteCutDirection.Down;
				case NoteCutDirection.DownRight:
					return NoteCutDirection.Down;
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
					return GetCardinalCutDirection(currentCutDirection);
				case Config.LimitType.Diagonal:
					return GetDiagonalCutDirection(currentCutDirection);
				case Config.LimitType.Horizontal:
					return GetHorizontalDirection(currentCutDirection);
				case Config.LimitType.Vertical:
					return GetVerticalDirection(currentCutDirection);
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