using System;

using System.Collections.Generic;
 
 
 public class TableRowGoBase 
 {
 	 public int ID = -1 ; 
 	 public int IDUsers = -1 ; 
 	 public string Code = "" ; 
 	 public byte Protocol = 0 ; 
 	 public string Uri = "" ; 
 	 public bool Enabled = false ; 
 	 public short ViewCount = -1 ; 
 	 public DateTime CreatedDT = MSStoredProceduresI.DateTimeMinVal ; 
 	 public string Comment = "" ; 
 	public TableRowGoBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowGoBase ( )
	{
	}
	
	public TableRowGoBase ( int IDUsers, string Code, byte Protocol, string Uri, bool Enabled, short ViewCount, DateTime CreatedDT, string Comment )
	{
	this.IDUsers = IDUsers;
	this.Code = Code;
	this.Protocol = Protocol;
	this.Uri = Uri;
	this.Enabled = Enabled;
	this.ViewCount = ViewCount;
	this.CreatedDT = CreatedDT;
	this.Comment = Comment;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.Go; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			ID = MSTableRowParse.GetInt(o,0);
 			IDUsers = MSTableRowParse.GetInt(o,1);
 			Code = MSTableRowParse.GetString(o,2);
 			Protocol = MSTableRowParse.GetByte(o,3);
 			Uri = MSTableRowParse.GetString(o,4);
 			Enabled = MSTableRowParse.GetBool(o,5);
 			ViewCount = MSTableRowParse.GetShort(o,6);
 			CreatedDT = MSTableRowParse.GetDateTime(o,7);
 			Comment = MSTableRowParse.GetString(o,8);
 		}  
 	 	 }
 
 }
 
