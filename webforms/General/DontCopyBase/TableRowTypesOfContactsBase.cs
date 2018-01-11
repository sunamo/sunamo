using System;

using System.Collections.Generic;
 
 
 public class TableRowTypesOfContactsBase 
 {
 	 public byte ID = 0 ; 
 	 public string Name = "" ; 
 	public TableRowTypesOfContactsBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowTypesOfContactsBase ( )
	{
	}
	
	public TableRowTypesOfContactsBase ( string Name )
	{
	this.Name = Name;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.TypesOfContacts; 
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
 
