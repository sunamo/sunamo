using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient; 
 
 public class TableRowCzechNameDaysBase 
 {
 	 public short ID = -1 ; 
 	 public string Name = "" ; 
 	 public byte Day = 0 ; 
 	 public byte Month = 0 ; 
 	public TableRowCzechNameDaysBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowCzechNameDaysBase ( )
	{
	}
	
	public TableRowCzechNameDaysBase ( string Name, byte Day, byte Month )
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
 
