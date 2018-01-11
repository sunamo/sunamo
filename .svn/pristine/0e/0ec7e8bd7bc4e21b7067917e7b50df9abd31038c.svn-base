using System;

using System.Collections.Generic;
 
 
 public class TableRowPageBase 
 {
 	 public int ID = -1 ; 
 	 public bool IsOld = false ; 
 	 public int OverallViews = -1 ; 
 	 public bool AllowNewComments = false ; 
 	public TableRowPageBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowPageBase ( )
	{
	}
	
	public TableRowPageBase ( bool IsOld, int OverallViews, bool AllowNewComments )
	{
	this.IsOld = IsOld;
	this.OverallViews = OverallViews;
	this.AllowNewComments = AllowNewComments;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.Page; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			ID = MSTableRowParse.GetInt(o,0);
 			IsOld = MSTableRowParse.GetBool(o,1);
 			OverallViews = MSTableRowParse.GetInt(o,2);
 			AllowNewComments = MSTableRowParse.GetBool(o,3);
 		}  
 	 	 }
 
 }
 
