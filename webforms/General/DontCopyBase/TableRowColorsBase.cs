using System;

using System.Collections.Generic;
 
 
 public class TableRowColorsBase 
 {
 	 public short ID = -1 ; 
 	 public string Name = "" ; 
 	 public byte R = 0 ; 
 	 public byte G = 0 ; 
 	 public byte B = 0 ; 
 	public TableRowColorsBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowColorsBase ( )
	{
	}
	
	public TableRowColorsBase ( string Name, byte R, byte G, byte B )
	{
	this.Name = Name;
	this.R = R;
	this.G = G;
	this.B = B;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.Colors; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			ID = MSTableRowParse.GetShort(o,0);
 			Name = MSTableRowParse.GetString(o,1);
 			R = MSTableRowParse.GetByte(o,2);
 			G = MSTableRowParse.GetByte(o,3);
 			B = MSTableRowParse.GetByte(o,4);
 		}  
 	 	 }
 
 }
 
