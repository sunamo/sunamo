using System;

using System.Collections.Generic;
 
 
 public class TableRowPageNameBase 
 {
 	 public int ID = -1 ; 
 	 public string Name = "" ; 
 	public TableRowPageNameBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowPageNameBase ( )
	{
	}
	
	public TableRowPageNameBase ( string Name )
	{
	this.Name = Name;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.PageName; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			ID = MSTableRowParse.GetInt(o,0);
 			Name = MSTableRowParse.GetString(o,1);
 		}  
 	 	 }
 
 }
 
