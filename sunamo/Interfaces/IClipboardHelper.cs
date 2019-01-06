using System;
using System.Collections.Generic;
using System.Text;
using sunamo.Generators.Text;

public interface IClipboardHelper
    {
		string GetText();
		void SetText(string s);
    void SetText2(string s);
    void SetText3(string s);
    void GetFirstWordOfList();
	List<string> GetLines();
	void SetList(List<string> d);
	void SetLines(List<string> lines);
	void CutFiles(params string[] selected);
	void SetText(TextBuilder stringBuilder);
	void SetText(StringBuilder stringBuilder);
    bool ContainsText();
    }

