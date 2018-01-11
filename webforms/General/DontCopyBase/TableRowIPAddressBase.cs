using System;

using System.Collections.Generic;
 
 
 public class TableRowIPAddressBase 
 {
 	 public int ID = -1 ; 
 	 public byte IP1 = 0 ; 
 	 public byte IP2 = 0 ; 
 	 public byte IP3 = 0 ; 
 	 public byte IP4 = 0 ; 
 	 public bool IsBlocked = false ; 
 	public TableRowIPAddressBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowIPAddressBase ( )
	{
	}
	
	public TableRowIPAddressBase ( byte IP1, byte IP2, byte IP3, byte IP4, bool IsBlocked )
	{
	this.IP1 = IP1;
	this.IP2 = IP2;
	this.IP3 = IP3;
	this.IP4 = IP4;
	this.IsBlocked = IsBlocked;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.IPAddress; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			ID = MSTableRowParse.GetInt(o,0);
 			IP1 = MSTableRowParse.GetByte(o,1);
 			IP2 = MSTableRowParse.GetByte(o,2);
 			IP3 = MSTableRowParse.GetByte(o,3);
 			IP4 = MSTableRowParse.GetByte(o,4);
 			IsBlocked = MSTableRowParse.GetBool(o,5);
 		}  
 	 	 }
 
 }
 
