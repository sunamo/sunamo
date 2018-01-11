using System;

using System.Collections.Generic;
 
 
 public class TableRowPageArgumentBase 
 {
 	 public int ID = -1 ; 
 	 public string Arg = "" ; 
 	public TableRowPageArgumentBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowPageArgumentBase ( )
	{
	}
	
	public TableRowPageArgumentBase ( string Arg )
	{
	this.Arg = Arg;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.PageArgument; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			ID = MSTableRowParse.GetInt(o,0);
 			Arg = MSTableRowParse.GetString(o,1);
 		}  
 	 	 }
 
 }
 
