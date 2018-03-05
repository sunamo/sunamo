using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public delegate void VoidBool(bool b);
public delegate void VoidBoolNullable(bool? b);
public delegate String StringString(string s);
public delegate void VoidString(string s);
public delegate void VoidTypeOfMessageStringParamsObject(TypeOfMessage tom, string s, params object[] args);
public delegate void VoidStringTU<T, U>(string s, T t, U u);
public delegate void SetStatusDelegate(TypeOfMessage t, string message);

public delegate void VoidObjectBool(object o, bool b);

public delegate byte[] ByteArrayByteArrayByteArrayHandler(byte[] b1, byte[] b2);
public delegate DateTime DateTimeDoubleDateTimeHandler(double d, DateTime dt);
public delegate DateTime DateTimeIntDateTimeHandler(int nt, DateTime dt);
public delegate void EmptyHandler();
public delegate void StatusBroadcasterHandler(string del);
public delegate string StringByteArrayByteArrayHandler(byte[] b1, byte[] b2);
public delegate string StringStringHandler(string s);
public delegate String StringStringByteArrayHandler(String s, byte[] b);
public delegate void VoidT<T>(T t);
public delegate void VoidT3<T, U, Z>(T t, U u, Z z);
public delegate void VoidVoid();
public delegate void VoidListT<T>(List<T> c);
public delegate void VoidUri(Uri uri);

public delegate void VoidAction(sunamo.Action action);
public delegate void VoidDouble(Double c);
public delegate void VoidObject(object o);
public delegate void VoidStringParamsObjects(string s, params object[] o);
public delegate void VoidObjectParamsObjects(object s, params object[] o);
public delegate bool BoolString(string s);
public delegate byte[] SifrujSymetricky(byte[] plainTextBytes, string passPhrase, byte[] saltValueBytes, byte[] initVectorBytes);
public delegate string EditHtmlWidthHandler(ref HtmlNode hm, string s);
public delegate String StringVoid();
public delegate void VoidUIElement(VoidUIElement uie);
public delegate void VoidIntDouble(int nt, double d);

public delegate Task TaskVoid();
public delegate Task TaskObject(object o);
//public delegate string EditHtmlWidthHandler(ref HtmlNode hm, string s);
