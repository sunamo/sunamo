using System;

using System.Collections.Generic;
 
 
 public class TableRowUsersReactivatesBase 
 {
 	 public int IDUsers = -1 ; 
 	 public string Code = "" ; 
 	 public string Email = "" ; 
 	 public DateTime DateChanged = MSStoredProceduresI.DateTimeMinVal ; 
 	 public byte ChangedTimes = 0 ; 
 	public TableRowUsersReactivatesBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowUsersReactivatesBase ( )
	{
	}
	
	public TableRowUsersReactivatesBase ( string Code, string Email, DateTime DateChanged, byte ChangedTimes )
	{
	this.Code = Code;
	this.Email = Email;
	this.DateChanged = DateChanged;
	this.ChangedTimes = ChangedTimes;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.UsersReactivates; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			IDUsers = MSTableRowParse.GetInt(o,0);
 			Code = MSTableRowParse.GetString(o,1);
 			Email = MSTableRowParse.GetString(o,2);
 			DateChanged = MSTableRowParse.GetDateTime(o,3);
 			ChangedTimes = MSTableRowParse.GetByte(o,4);
 		}  
 	 	 }
 
 }
 
