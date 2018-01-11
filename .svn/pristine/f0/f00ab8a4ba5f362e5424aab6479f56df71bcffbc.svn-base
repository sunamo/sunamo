using System;

using System.Collections.Generic;
 
 
 public class TableRowStateBase 
 {
 	 public byte ID = 0 ; 
 	 public string Name = "" ; 
 	public TableRowStateBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowStateBase ( )
	{
	}
	
	public TableRowStateBase ( string Name )
	{
	this.Name = Name;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.State; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			ID = MSTableRowParse.GetByte(o,0);
 			Name = MSTableRowParse.GetString(o,1);
 		}  
 	 	 }
 
 }
 
