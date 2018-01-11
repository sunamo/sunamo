using System.Drawing;
using System.Windows.Forms;
public delegate void setTextInTextBoxForms(TextBoxForms txt, string text);
public delegate void appendLineInTextBoxForms(TextBoxForms txt, string text);
public delegate void appendLineInTextBox(TextBox txt, string text);
public delegate string getTextInTextBoxForms(TextBoxForms txt);
public delegate void ClearAllNodesTreeView(TreeView tv);
public delegate void VoidStringIcon(string s, Icon icon);
