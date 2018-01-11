using System;

using System.Collections.Generic;
 
 
 public class TableRowComments : TableRowCommentsBase 
 {
 		public TableRowComments ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowComments ( ) : base()
		{
		}
		
		public TableRowComments ( int IDPages, int IDUsers, DateTime DT, bool IsDeletedByAdmin, bool IsDeletedByUser, short CountThumbsUp, short CountThumbsDown, short CountMarkedSpam, bool PhotoGallery, DateTime Edited, byte IDSeries, string Content, int IDIP ) : base(IDPages,IDUsers,DT,IsDeletedByAdmin,IsDeletedByUser,CountThumbsUp,CountThumbsDown,CountMarkedSpam,PhotoGallery,Edited,IDSeries,Content,IDIP)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "ID", ID);
		ParseRow(o);
  
 	 }
 
 	 public int InsertToTable() 
 	 {
 		ID = (int)MSStoredProceduresI.ci.Insert(TableName, typeof(int),"ID",IDPages,IDUsers,DT,IsDeletedByAdmin,IsDeletedByUser,CountThumbsUp,CountThumbsDown,CountMarkedSpam,PhotoGallery,Edited,IDSeries,Content,IDIP);
		return ID; 
 	 }
 
 	 public int InsertToTable2() 
 	 {
 		ID=(int)MSStoredProceduresI.ci.Insert2(TableName,"ID",typeof(int),IDPages,IDUsers,DT,IsDeletedByAdmin,IsDeletedByUser,CountThumbsUp,CountThumbsDown,CountMarkedSpam,PhotoGallery,Edited,IDSeries,Content,IDIP);		return ID; 
 	 }
 
 	 public void InsertToTable3(int ID) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, ID,IDPages,IDUsers,DT,IsDeletedByAdmin,IsDeletedByUser,CountThumbsUp,CountThumbsDown,CountMarkedSpam,PhotoGallery,Edited,IDSeries,Content,IDIP); 
 	 }
 
 }
 
