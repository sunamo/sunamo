using System;

using System.Collections.Generic;
 
 
 public class TableRowCitiesBase 
 {
 	 public int ID = -1 ; 
 	 public string Name = "" ; 
 	public TableRowCitiesBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowCitiesBase ( )
	{
	}
	
	public TableRowCitiesBase ( string Name )
	{
	this.Name = Name;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.Cities; 
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
 
