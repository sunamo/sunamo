using System;

using System.Collections.Generic;
 
 
 public class TableRowLocationBase 
 {
 	 public short ID = -1 ; 
 	 public byte IDState = 0 ; 
 	 public short IDRegion = -1 ; 
 	 public byte SerieDistrict = 0 ; 
 	 public string District = "" ; 
 	public TableRowLocationBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowLocationBase ( )
	{
	}
	
	public TableRowLocationBase ( byte IDState, short IDRegion, byte SerieDistrict, string District )
	{
	this.IDState = IDState;
	this.IDRegion = IDRegion;
	this.SerieDistrict = SerieDistrict;
	this.District = District;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.Location; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			ID = MSTableRowParse.GetShort(o,0);
 			IDState = MSTableRowParse.GetByte(o,1);
 			IDRegion = MSTableRowParse.GetShort(o,2);
 			SerieDistrict = MSTableRowParse.GetByte(o,3);
 			District = MSTableRowParse.GetString(o,4);
 		}  
 	 	 }
 
 }
 
