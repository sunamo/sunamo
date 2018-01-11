using System;

using System.Collections.Generic;
 
 
 public class TableRowUsersActivatesBase 
 {
 	 public int ID = -1 ; 
 	 public string Code = "" ; 
 	 public string Login = "" ; 
 	 public string Olseh = "" ; 
 	 public string Email = "" ; 
 	 public DateTime DeleteOn = MSStoredProceduresI.DateTimeMinVal ; 
 	 public string SecQue = "" ; 
 	 public string SecAns = "" ; 
 	 public bool Sex = false ; 
 	 public DateTime DateBorn = MSStoredProceduresI.DateTimeMinVal ; 
 	 public byte MailSettings = 0 ; 
 	 public byte IDState = 0 ; 
 	 public short IDRegion = -1 ; 
 	 public byte IDDistrict = 0 ; 
 	 public int IDCity = -1 ; 
 	public TableRowUsersActivatesBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowUsersActivatesBase ( )
	{
	}
	
	public TableRowUsersActivatesBase ( string Code, string Login, string Olseh, string Email, DateTime DeleteOn, string SecQue, string SecAns, bool Sex, DateTime DateBorn, byte MailSettings, byte IDState, short IDRegion, byte IDDistrict, int IDCity )
	{
	this.Code = Code;
	this.Login = Login;
	this.Olseh = Olseh;
	this.Email = Email;
	this.DeleteOn = DeleteOn;
	this.SecQue = SecQue;
	this.SecAns = SecAns;
	this.Sex = Sex;
	this.DateBorn = DateBorn;
	this.MailSettings = MailSettings;
	this.IDState = IDState;
	this.IDRegion = IDRegion;
	this.IDDistrict = IDDistrict;
	this.IDCity = IDCity;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.UsersActivates; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			ID = MSTableRowParse.GetInt(o,0);
 			Code = MSTableRowParse.GetString(o,1);
 			Login = MSTableRowParse.GetString(o,2);
 			Olseh = MSTableRowParse.GetString(o,3);
 			Email = MSTableRowParse.GetString(o,4);
 			DeleteOn = MSTableRowParse.GetDateTime(o,5);
 			SecQue = MSTableRowParse.GetString(o,6);
 			SecAns = MSTableRowParse.GetString(o,7);
 			Sex = MSTableRowParse.GetBool(o,8);
 			DateBorn = MSTableRowParse.GetDateTime(o,9);
 			MailSettings = MSTableRowParse.GetByte(o,10);
 			IDState = MSTableRowParse.GetByte(o,11);
 			IDRegion = MSTableRowParse.GetShort(o,12);
 			IDDistrict = MSTableRowParse.GetByte(o,13);
 			IDCity = MSTableRowParse.GetInt(o,14);
 		}  
 	 	 }
 
 }
 
