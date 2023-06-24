using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;

namespace NoteLimiter.Configuration
{
	public class Config
	{
		public enum LimitType
		{
			All,
			Cardinal,
			Diagonal,
			Up,
			Down,
			Left,
			Right,
			UpLeft,
			UpRight,
			DownRight,
			DownLeft,
		}
		public static Config Instance
		{
			get;
			set;
		}

		public virtual bool Enabled
		{
			get;
			set;
		} = false;
		[UseConverter(typeof(EnumConverter<LimitType>))]
		public virtual LimitType LeftNoteLimitType
		{
			get;
			set;
		} = LimitType.All;

		[UseConverter(typeof(EnumConverter<LimitType>))]
		public virtual LimitType RightNoteLimitType
		{
			get;
			set;
		} = LimitType.All;
	}
}
