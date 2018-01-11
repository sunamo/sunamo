using System;

using System.Collections.Generic;
 
 
 public class TableRowLoginAttemptBase 
 {
 	 public int ID = -1 ; 
 	 public string Login = "" ; 
 	 public DateTime DT = MSStoredProceduresI.DateTimeMinVal ; 
 	 public byte Count = 0 ; 
 	public TableRowLoginAttemptBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowLoginAttemptBase ( )
	{
	}
	
	public TableRowLoginAttemptBase ( string Login, DateTime DT, byte Count )
	{
	this.Login = Login;
	this.DT = DT;
	this.Count = Count;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.LoginAttempt; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			ID = MSTableRowParse.GetInt(o,0);
 			Login = MSTableRowParse.GetString(o,1);
 			DT = MSTableRowParse.GetDateTime(o,2);
 			Count = MSTableRowParse.GetByte(o,3);
 		}  
 	 	 }
 
 }
 
