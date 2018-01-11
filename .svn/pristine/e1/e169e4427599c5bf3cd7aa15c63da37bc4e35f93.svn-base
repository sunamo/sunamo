using System;

using System.Collections.Generic;
 
 
 public class TableRowNameDaysBase 
 {
 	 public short ID = -1 ; 
 	 public string Name = "" ; 
 	 public byte Day = 0 ; 
 	 public byte Month = 0 ; 
 	public TableRowNameDaysBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowNameDaysBase ( )
	{
	}
	
	public TableRowNameDaysBase ( string Name, byte Day, byte Month )
	{
	this.Name = Name;
	this.Day = Day;
	this.Month = Month;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.CzechNameDays; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			ID = MSTableRowParse.GetShort(o,0);
 			Name = MSTableRowParse.GetString(o,1);
 			Day = MSTableRowParse.GetByte(o,2);
 			Month = MSTableRowParse.GetByte(o,3);
 		}  
 	 	 }
 
 }
 
