using System;

using System.Collections.Generic;
 
 
 public class TableRowGoVisitorsBase 
 {
 	 public long ID = -1 ; 
 	 public DateTime DT = MSStoredProceduresI.DateTimeMinVal ; 
 	 public int IDGo = -1 ; 
 	 public byte IDBrowser = 0 ; 
 	 public long IDHostName = -1 ; 
 	 public byte IDPlatform = 0 ; 
 	 public byte IDLang = 0 ; 
 	 public byte BrowserVersion = 0 ; 
 	public TableRowGoVisitorsBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowGoVisitorsBase ( )
	{
	}
	
	public TableRowGoVisitorsBase ( DateTime DT, int IDGo, byte IDBrowser, long IDHostName, byte IDPlatform, byte IDLang, byte BrowserVersion )
	{
	this.DT = DT;
	this.IDGo = IDGo;
	this.IDBrowser = IDBrowser;
	this.IDHostName = IDHostName;
	this.IDPlatform = IDPlatform;
	this.IDLang = IDLang;
	this.BrowserVersion = BrowserVersion;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.GoVisitors; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			ID = MSTableRowParse.GetLong(o,0);
 			DT = MSTableRowParse.GetDateTime(o,1);
 			IDGo = MSTableRowParse.GetInt(o,2);
 			IDBrowser = MSTableRowParse.GetByte(o,3);
 			IDHostName = MSTableRowParse.GetLong(o,4);
 			IDPlatform = MSTableRowParse.GetByte(o,5);
 			IDLang = MSTableRowParse.GetByte(o,6);
 			BrowserVersion = MSTableRowParse.GetByte(o,7);
 		}  
 	 	 }
 
 }
 
