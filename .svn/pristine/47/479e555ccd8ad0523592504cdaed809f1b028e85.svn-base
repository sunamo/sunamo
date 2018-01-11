using System;

using System.Collections.Generic;
 
 
 public class TableRowRegionBase 
 {
 	 public short ID = -1 ; 
 	 public string Name = "" ; 
 	public TableRowRegionBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowRegionBase ( )
	{
	}
	
	public TableRowRegionBase ( string Name )
	{
	this.Name = Name;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.Region; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			ID = MSTableRowParse.GetShort(o,0);
 			Name = MSTableRowParse.GetString(o,1);
 		}  
 	 	 }
 
 }
 
