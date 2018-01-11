using System;

using System.Collections.Generic;
 
 
 public class TableRowCommentSubscribeBase 
 {
 	 public int IDPage = -1 ; 
 	 public int IDUser = -1 ; 
 	 public byte Serie = 0 ; 
 	public TableRowCommentSubscribeBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowCommentSubscribeBase ( )
	{
	}
	
	public TableRowCommentSubscribeBase ( int IDUser, byte Serie )
	{
	this.IDUser = IDUser;
	this.Serie = Serie;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.CommentSubscribe; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			IDPage = MSTableRowParse.GetInt(o,0);
 			IDUser = MSTableRowParse.GetInt(o,1);
 			Serie = MSTableRowParse.GetByte(o,2);
 		}  
 	 	 }
 
 }
 
