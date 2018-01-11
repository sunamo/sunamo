using System;

using System.Collections.Generic;
 
 
 public class TableRowLangsBase 
 {
 	 public byte ID = 0 ; 
 	 public string Name = "" ; 
 	public TableRowLangsBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowLangsBase ( )
	{
	}
	
	public TableRowLangsBase ( string Name )
	{
	this.Name = Name;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.Langs; 
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
 
