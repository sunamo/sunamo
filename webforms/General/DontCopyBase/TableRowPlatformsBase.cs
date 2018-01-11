using System;

using System.Collections.Generic;
 
 
 public class TableRowPlatformsBase 
 {
 	 public byte ID = 0 ; 
 	 public string Name = "" ; 
 	public TableRowPlatformsBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowPlatformsBase ( )
	{
	}
	
	public TableRowPlatformsBase ( string Name )
	{
	this.Name = Name;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.Platforms; 
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
 
