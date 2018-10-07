using System;
using System.Collections.Generic;
using System.Text;
using sunamo;
using sunamo.Generators.Text;

public  class ClipboardHelper
{
	public static  IClipboardHelper Instance = null;

	private ClipboardHelper()
	{
	}

	public static  string GetText()
	{
		return Instance.GetText();
	}

	public static  void SetText(string s)
	{
		Instance.SetText(s);
	}

	public static void GetFirstWordOfList()
	{
		Instance.GetFirstWordOfList();
	}

	public static List<string> GetLines()
	{
		return Instance.GetLines();
	}

	public static void SetList(List<string> d)
	{
		Instance.SetList(d);
	}

	public static void SetLines(List<string> lines)
	{
		Instance.SetLines(lines);
	}

	public static void CutFiles(params string[] selected)
	{
		Instance.CutFiles(selected);
	}

	public static void SetText(TextBuilder stringBuilder)
	{
		Instance.SetText(stringBuilder);
	}

	public static void SetText(StringBuilder stringBuilder)
	{
		Instance.SetText(stringBuilder.ToString());
	}
}