using System;

using System.Collections.Generic;
 
 
 public class TableRowCommentsBase 
 {
 	 public int ID = -1 ; 
 	 public int IDPages = -1 ; 
 	 public int IDUsers = -1 ; 
 	 public DateTime DT = MSStoredProceduresI.DateTimeMinVal ; 
 	 public bool IsDeletedByAdmin = false ; 
 	 public bool IsDeletedByUser = false ; 
 	 public short CountThumbsUp = -1 ; 
 	 public short CountThumbsDown = -1 ; 
 	 public short CountMarkedSpam = -1 ; 
 	 public bool PhotoGallery = false ; 
 	 public DateTime Edited = MSStoredProceduresI.DateTimeMinVal ; 
 	 public byte IDSeries = 0 ; 
 	 public string Content = "" ; 
 	 public int IDIP = -1 ; 
 	public TableRowCommentsBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowCommentsBase ( )
	{
	}
	
	public TableRowCommentsBase ( int IDPages, int IDUsers, DateTime DT, bool IsDeletedByAdmin, bool IsDeletedByUser, short CountThumbsUp, short CountThumbsDown, short CountMarkedSpam, bool PhotoGallery, DateTime Edited, byte IDSeries, string Content, int IDIP )
	{
	this.IDPages = IDPages;
	this.IDUsers = IDUsers;
	this.DT = DT;
	this.IsDeletedByAdmin = IsDeletedByAdmin;
	this.IsDeletedByUser = IsDeletedByUser;
	this.CountThumbsUp = CountThumbsUp;
	this.CountThumbsDown = CountThumbsDown;
	this.CountMarkedSpam = CountMarkedSpam;
	this.PhotoGallery = PhotoGallery;
	this.Edited = Edited;
	this.IDSeries = IDSeries;
	this.Content = Content;
	this.IDIP = IDIP;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.Comments; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			ID = MSTableRowParse.GetInt(o,0);
 			IDPages = MSTableRowParse.GetInt(o,1);
 			IDUsers = MSTableRowParse.GetInt(o,2);
 			DT = MSTableRowParse.GetDateTime(o,3);
 			IsDeletedByAdmin = MSTableRowParse.GetBool(o,4);
 			IsDeletedByUser = MSTableRowParse.GetBool(o,5);
 			CountThumbsUp = MSTableRowParse.GetShort(o,6);
 			CountThumbsDown = MSTableRowParse.GetShort(o,7);
 			CountMarkedSpam = MSTableRowParse.GetShort(o,8);
 			PhotoGallery = MSTableRowParse.GetBool(o,9);
 			Edited = MSTableRowParse.GetDateTime(o,10);
 			IDSeries = MSTableRowParse.GetByte(o,11);
 			Content = MSTableRowParse.GetString(o,12);
 			IDIP = MSTableRowParse.GetInt(o,13);
 		}  
 	 	 }
 
 }
 
