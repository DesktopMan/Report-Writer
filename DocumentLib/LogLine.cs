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

			switch (level)
			{
				case Level.INFO: sb.Append("I"); break;
				case Level.WARN: sb.Append("W"); break;
				case Level.ERR: sb.Append("E"); break;
				case Level.DBG: sb.Append("D"); break;
			}

			sb.Append(": " + text);

			return sb.ToString();
		}

		public Level level;
	}
}
