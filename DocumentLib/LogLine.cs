using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentLib
{
	public class LogLine : Content
	{
		public enum Level { INFO, DBG, WARN, ERR };

		public LogLine(Level level, string match, string text, int position = -1) : base("", position, match, text)
		{
			this.level = level;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(GetLevel());
			sb.Append(": " + text);

			return sb.ToString();
		}

		public string GetLevel()
		{
			switch (level)
			{
				case Level.INFO: return "I";
				case Level.WARN: return "W";
				case Level.ERR: return "E";
				case Level.DBG: return "D";
			}

			return "";
		}

		public Level level;
	}
}
