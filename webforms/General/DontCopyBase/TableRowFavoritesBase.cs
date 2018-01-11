using System;

using System.Collections.Generic;
 
 
 public class TableRowFavoritesBase 
 {
 	 public int IDPages = -1 ; 
 	 public int IDUsers = -1 ; 
 	public TableRowFavoritesBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowFavoritesBase ( )
	{
	}
	
	public TableRowFavoritesBase ( int IDUsers )
	{
	this.IDUsers = IDUsers;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.Favorites; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			IDPages = MSTableRowParse.GetInt(o,0);
 			IDUsers = MSTableRowParse.GetInt(o,1);
 		}  
 	 	 }
 
 }
 
