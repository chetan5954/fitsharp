// Modified or written by Object Mentor, Inc. for inclusion with FitNesse.
// Copyright (c) 2002 Cunningham & Cunningham, Inc.
// Released under the terms of the GNU General Public License version 2 or later.
using System;
using System.Text;
using fitSharp.Fit.Model;

namespace fitnesse.fitserver
{
	public class Protocol
	{
		public static string FormatInteger(int encodeInteger)
		{
			string numberPartOfString = "" + encodeInteger;
			return new String('0', 10 - numberPartOfString.Length) + numberPartOfString;
		}

		public static string FormatDocument(string document)
		{
			return FormatInteger(Encoding.UTF8.GetBytes(document).Length) + document;
		}

		public static String FormatCounts(TestCounts status)
		{
			var builder = new StringBuilder();
			builder.Append(FormatInteger(0));
			builder.Append(FormatInteger(status.GetCount(CellAttributes.RightStatus)));
			builder.Append(FormatInteger(status.GetCount(CellAttributes.WrongStatus)));
			builder.Append(FormatInteger(status.GetCount(CellAttributes.IgnoreStatus)));
			builder.Append(FormatInteger(status.GetCount(CellAttributes.ExceptionStatus)));
			return builder.ToString();
		}
	}
}
