using System;

using System.Collections.Generic;
 
 
 public class TableRowUsersBase 
 {
 	 public int ID = -1 ; 
 	 public string SessionID = "" ; 
 	 public string Login = "" ; 
 	 public string Email = "" ; 
 	 public DateTime LastSeen = MSStoredProceduresI.DateTimeMinVal ; 
 	 public string SecQue = "" ; 
 	 public string SecAns = "" ; 
 	 public bool Sex = false ; 
 	 public DateTime DateBorn = MSStoredProceduresI.DateTimeMinVal ; 
 	 public byte MailSettings = 0 ; 
 	 public byte IDState = 0 ; 
 	 public short IDRegion = -1 ; 
 	 public byte IDDistrict = 0 ; 
 	 public int IDCity = -1 ; 
 	 public byte Status = 0 ; 
 	 public bool BlockNewComment = false ; 
 	 public string FacebookNick = "" ; 
 	 public string TwitterNick = "" ; 
 	 public string GoogleNick = "" ; 
 	 public short WidthUserPage = -1 ; 
 	 public string HtmlAbout = "" ; 
 	public TableRowUsersBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowUsersBase ( )
	{
	}
	
	public TableRowUsersBase ( string SessionID, string Login, string Email, DateTime LastSeen, string SecQue, string SecAns, bool Sex, DateTime DateBorn, byte MailSettings, byte IDState, short IDRegion, byte IDDistrict, int IDCity, byte Status, bool BlockNewComment, string FacebookNick, string TwitterNick, string GoogleNick, short WidthUserPage, string HtmlAbout )
	{
	this.SessionID = SessionID;
	this.Login = Login;
	this.Email = Email;
	this.LastSeen = LastSeen;
	this.SecQue = SecQue;
	this.SecAns = SecAns;
	this.Sex = Sex;
	this.DateBorn = DateBorn;
	this.MailSettings = MailSettings;
	this.IDState = IDState;
	this.IDRegion = IDRegion;
	this.IDDistrict = IDDistrict;
	this.IDCity = IDCity;
	this.Status = Status;
	this.BlockNewComment = BlockNewComment;
	this.FacebookNick = FacebookNick;
	this.TwitterNick = TwitterNick;
	this.GoogleNick = GoogleNick;
	this.WidthUserPage = WidthUserPage;
	this.HtmlAbout = HtmlAbout;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.Users; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			ID = MSTableRowParse.GetInt(o,0);
 			SessionID = MSTableRowParse.GetString(o,1);
 			Login = MSTableRowParse.GetString(o,2);
 			Email = MSTableRowParse.GetString(o,3);
 			LastSeen = MSTableRowParse.GetDateTime(o,4);
 			SecQue = MSTableRowParse.GetString(o,5);
 			SecAns = MSTableRowParse.GetString(o,6);
 			Sex = MSTableRowParse.GetBool(o,7);
 			DateBorn = MSTableRowParse.GetDateTime(o,8);
 			MailSettings = MSTableRowParse.GetByte(o,9);
 			IDState = MSTableRowParse.GetByte(o,10);
 			IDRegion = MSTableRowParse.GetShort(o,11);
 			IDDistrict = MSTableRowParse.GetByte(o,12);
 			IDCity = MSTableRowParse.GetInt(o,13);
 			Status = MSTableRowParse.GetByte(o,14);
 			BlockNewComment = MSTableRowParse.GetBool(o,15);
 			FacebookNick = MSTableRowParse.GetString(o,16);
 			TwitterNick = MSTableRowParse.GetString(o,17);
 			GoogleNick = MSTableRowParse.GetString(o,18);
 			WidthUserPage = MSTableRowParse.GetShort(o,19);
 			HtmlAbout = MSTableRowParse.GetString(o,20);
 		}  
 	 	 }
 
 }
 
