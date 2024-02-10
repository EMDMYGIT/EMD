using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;



namespace DBClases
{
    
    public class clsDtaUtils
    {
        #region Declarations

        private static  System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
        private static System.Data.SqlClient.SqlCommand CMD = new System.Data.SqlClient.SqlCommand();
        
        //private static System.Data.SqlClient.SqlDataAdapter DA;


        #endregion
        // Constructor (New)
        static clsDtaUtils()
        {

          //  System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
            //System.Data.SqlClient.SqlCommand CMD = new System.Data.SqlClient.SqlCommand();
            //System.Data.DataSet DS = new System.Data.DataSet();
            conn.ConnectionString = @"Data Source=IMASDEM\IMASD;Initial Catalog=TEST;Integrated Security=True";

        }
        #region Contacts
        // Obtención de datos (Select , lista/por ID)
        public List<Contacts.Models.TB_Contact> RetrieveContactData(Int32 idFilter,string OrderBy)
        {
            try
            {
                conn.Open();
                // Insert code to process data.
                string sSql = @"SELECT [ContactID],[Name],[Surname],[Surname2],[Telephone],[e_mail]
                    ,[City],[Country]  FROM[dbo].[TB_Contact]";
                if (idFilter!=0)  sSql=sSql + " where ContactId=" + idFilter ;
                if (OrderBy !=null && OrderBy != string.Empty) sSql = sSql + @" Order by " + OrderBy;
                
                CMD.CommandText = sSql;
                CMD.Connection = conn;
                System.Data.SqlClient.SqlDataAdapter DA = new System.Data.SqlClient.SqlDataAdapter(CMD);
                System.Data.DataSet DS = new DataSet();
                DA.Fill(DS, "Contacts");   
                return GetAllContacts(DS);
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Erroron RetrieveContactData", ex);
            }
            finally
            {
                conn.Close();
            }
        }
        public List<Contacts.Models.TB_Contact> RetrieveContactDataRelated(String Filter)
        {
            try
            {
                conn.Open();
                // Insert code to process data.
                string sSql = @"SELECT [ContactID],[Name],[Surname],[Surname2],[Telephone],[e_mail]
                    ,[City],[Country]  FROM[dbo].[TB_Contact]";
                if (Filter != string.Empty) sSql = sSql + " where 1=2 " + Filter;


                CMD.CommandText = sSql;
                CMD.Connection = conn;
                System.Data.SqlClient.SqlDataAdapter DA = new System.Data.SqlClient.SqlDataAdapter(CMD);
                System.Data.DataSet DS = new DataSet();
                DA.Fill(DS, "Contacts");
                return GetAllContacts(DS);
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Erroron RetrieveContactData", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        private List<Contacts.Models.TB_Contact> GetAllContacts(DataSet pDS)
        {
            try
            {
              List<Contacts.Models.TB_Contact> bMList = new List<Contacts.Models.TB_Contact>();

            foreach (DataRow item in pDS.Tables[0].Rows)
             {
                Contacts.Models.TB_Contact TempModel = new Contacts.Models.TB_Contact();
                    TempModel.ContactID = Convert.ToInt32(item["ContactID"]);
                    TempModel.Name= item["Name"].ToString();
                    TempModel.Surname = item["Surname"].ToString();
                    TempModel.Surname2 = item["Surname2"].ToString();
                    TempModel.Telephone = item["Telephone"].ToString();
                    TempModel.e_mail = item["e_mail"].ToString();
                    TempModel.City = item["City"].ToString();
                    TempModel.Country = item["Country"].ToString();
                    TempModel.ContactComment = RetrieveContactComment(TempModel.ContactID);

                    bMList.Add(TempModel);
                    TempModel = null;
            }
            return bMList;
            }
            catch (Exception ex)
            {

                throw new System.ArgumentException("Erroron GetAllContacts", ex);
            }
        }

        public void InsertContactsData(Contacts.Models.TB_Contact dataInsert)
        {
            try
            {
                conn.Open();
                // Insert code to process data.
                string sSql = @"INSERT INTO [dbo].[TB_Contact]
                        ([Name],[Surname],[Surname2],[Telephone],[e_mail],[City],[Country])     VALUES
                    ('%Name%','%Surname%','%Surname2%','%Telephone%','%e_mail%,%City%,%Country%');SELECT SCOPE_IDENTITY();";
                    
                sSql = sSql.Replace("%Name%", NullToString(dataInsert.Name));
                sSql = sSql.Replace("%Surname%", NullToString(dataInsert.Surname));
                sSql = sSql.Replace("%Surname2%", NullToString(dataInsert.Surname2));
                sSql = sSql.Replace("%Telephone%", NullToString(dataInsert.Telephone));
                sSql = sSql.Replace("%e_mail%", NullToString(dataInsert.e_mail));
                sSql = sSql.Replace("%City%", NullToString(dataInsert.City));
                sSql = sSql.Replace("%Country%", NullToString(dataInsert.Country));

                CMD.CommandText = sSql;
                CMD.Connection = conn;
               

                try
                {
                    Int32 returnid = 0;
                    returnid= Convert.ToInt32(CMD.ExecuteScalar());
                  
                }
                catch(System.Data.SqlClient.SqlException sex)
                {
                    throw new System.ArgumentException("Error on Insert: " + CMD.CommandText, sex);
                }
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Erroron InsertContactsData ", ex);
            }
            finally
            {
                conn.Close();
            }

        }

        public void UpdateContactsData(Contacts.Models.TB_Contact dataUpdate)
        {
            try
            {
                conn.Open();
                // Insert code to process data.
                string sSql = @"UPDATE [dbo].[TB_Contact]
                                SET [Name] = '%Name%',
                                [Surname] = '%Surname%',
                                [Surname2] = '%Surname2%',
                                [Telephone] = '%Telephone%',
                                [e_mail] = '%e_mail%',
                                [City]= '%e_mail%',
                                [Country]= '%Country%'
                                 WHERE [ContactID]=%ContactID%";
                sSql= sSql.Replace("%Name%", NullToString(dataUpdate.Name));
                sSql= sSql.Replace("%Surname%", NullToString(dataUpdate.Surname));
                sSql = sSql.Replace("%Surname2%", NullToString(dataUpdate.Surname2));
                sSql = sSql.Replace("%Telephone%", NullToString(dataUpdate.Telephone));
                sSql = sSql.Replace("%e_mail%", NullToString(dataUpdate.e_mail));
                sSql = sSql.Replace("%City%", NullToString(dataUpdate.City));
                sSql = sSql.Replace("%Country%", NullToString(dataUpdate.Country));
                sSql = sSql.Replace("%ContactID%", NullToString(dataUpdate.ContactID));

                CMD.CommandText = sSql;
                CMD.Connection = conn;
                CMD.ExecuteNonQuery();

                if (dataUpdate.ContactComment != null)
                {
                    Contacts.Models.TB_Comments commentDta = new Contacts.Models.TB_Comments();
                    commentDta.Comment = dataUpdate.ContactComment.Comment;
                    commentDta.ParentId = dataUpdate.ContactID;
                    commentDta.ParentType = "TB_CONTACTS";
                    if (dataUpdate.ContactComment.CommentID == 0)
                    { InsertCommentssData(commentDta); }
                    else
                    { UpdateCommentsData(commentDta); }
                }
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Erroron UpdateTest1Data ", ex);
            }
            finally
            {
                conn.Close();
            }

        }

        public void DeleteContacData(Int32 idDelete )
        {
            try
            {
                conn.Open();
                // Insert code to process data.
                string sSql = "Delete from  [dbo].[TB_Contact]  WHERE [ContactID]=%ContactID%";
                sSql = sSql.Replace("%ContactID%", idDelete.ToString());

                CMD.CommandText = sSql;
                CMD.Connection = conn;
                CMD.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Erroron DeleteContacData ", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    
        #endregion


        #region Comments
    // Obtención de datos (Select , lista/por ID)
    public List<Contacts.Models.TB_Comments> RetrieveCommentData(Int32 idFilter)
    {
        try
        {
            conn.Open();
            // Insert code to process data.
            string sSql = @"SELECT[CommentID],[Comment],[ParentType],[ParentId] FROM[dbo].[TB_Comments]";
            if (idFilter != 0) sSql = sSql + " where CommentsID=" + idFilter;


            CMD.CommandText = sSql;
            CMD.Connection = conn;
            System.Data.SqlClient.SqlDataAdapter DA = new System.Data.SqlClient.SqlDataAdapter(CMD);
            System.Data.DataSet DS = new DataSet();
            DA.Fill(DS, "Comments");
            return GetAllComments(DS);
        }
        catch (Exception ex)
        {
            throw new System.ArgumentException("Erroron RetrieveCommentData", ex);
        }
        finally
        {
            conn.Close();
        }
    }
        public Contacts.Models.TB_Comments RetrieveContactComment(Int32 idFilter)
        {
            bool bisopen = true;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    bisopen = false;
                }
                // Insert code to process data.
                string sSql = @"SELECT[CommentID],[Comment],[ParentType],[ParentId] FROM[dbo].[TB_Comments]";
                if (idFilter != 0) sSql = sSql + " where ParentType ='TB_CONTACTS' and ParentId=" + idFilter;


                CMD.CommandText = sSql;
                CMD.Connection = conn;
                System.Data.SqlClient.SqlDataAdapter DA = new System.Data.SqlClient.SqlDataAdapter(CMD);
                System.Data.DataSet DS = new DataSet();
                DA.Fill(DS, "Comments");
                if (DS.Tables[0].Rows.Count == 0) return null;
                else return GetAllComments(DS)[0];
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Erroron RetrieveCommentData", ex);
            }
            finally
            {
                if (bisopen==false)  conn.Close();
            }
        }

        private List<Contacts.Models.TB_Comments> GetAllComments(DataSet pDS)
    {
        try
        {
            List<Contacts.Models.TB_Comments> bMList = new List<Contacts.Models.TB_Comments>();

            foreach (DataRow item in pDS.Tables[0].Rows)
            {
                Contacts.Models.TB_Comments TempModel = new Contacts.Models.TB_Comments();
                TempModel.CommentID = Convert.ToInt32(item["CommentID"]);
                TempModel.Comment = item["Comment"].ToString();
                TempModel.ParentType = item["ParentType"].ToString();
                TempModel.ParentId = Convert.ToInt32(item["ParentId"]);

                bMList.Add(TempModel);
                TempModel = null;
            }
            return bMList;
        }
        catch (Exception ex)
        {

            throw new System.ArgumentException("Erroron GetAllContacts", ex);
        }
    }


    public void InsertCommentssData(Contacts.Models.TB_Comments dataInsert)
    {
            bool bconopen = true;
        try
        {
                if (conn.State == ConnectionState.Closed)
                { conn.Open();
                    bconopen = false;
                }
            // Insert code to process data.
            string sSql = @"INSERT INTO [dbo].[TB_Comments]
                        ([Comment],[ParentType],[ParentId])
                        VALUES
                        ('%Comment%','%ParentType%',%ParentId%)";

            sSql = sSql.Replace("%Comment%", NullToString(dataInsert.Comment));
            sSql = sSql.Replace("%ParentType%", NullToString(dataInsert.ParentType));
            sSql = sSql.Replace("%ParentId%", NullToString(dataInsert.ParentId));

            CMD.CommandText = sSql;
            CMD.Connection = conn;
            try
            {
                CMD.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException sex)
            {
                throw new System.ArgumentException("Error on Insert: " + CMD.CommandText, sex);
            }
        }
        catch (Exception ex)
        {
            throw new System.ArgumentException("Erroron InsertCommentssData ", ex);
        }
        finally
        {
                if (bconopen==false)   conn.Close();
        }

    }

    public void UpdateCommentsData(Contacts.Models.TB_Comments dataUpdate)
    {
        try
        {
            conn.Open();
            // Insert code to process data.
            string sSql = @"UPDATE [dbo].[TB_Comments]
                           SET [Comment] = '%Comment%',[ParentType] = '%ParentType%',[ParentId] = %ParentId
                           WHERE [CommentID]=%CommentsID%";
            sSql = sSql.Replace("%Comment%", dataUpdate.Comment.ToString().Trim());
            sSql = sSql.Replace("%ParentType%", dataUpdate.ParentType.ToString().Trim());
            sSql = sSql.Replace("%ParentId%", dataUpdate.ParentId.ToString().Trim());
            sSql = sSql.Replace("%CommentsID%", dataUpdate.CommentID.ToString().Trim());

            CMD.CommandText = sSql;
            CMD.Connection = conn;
            CMD.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new System.ArgumentException("Erroron UpdateTest1Data ", ex);
        }
        finally
        {
            conn.Close();
        }

    }

    public void DeleteCommentsData(Contacts.Models.TB_Comments dataDelete)
    {
        try
        {
            conn.Open();
            // Insert code to process data.
            string sSql = @"DELETE FROM[dbo].[TB_Comments] WHERE[CommentID]=%CommentsID%";
            sSql = sSql.Replace("%CommentsID%", dataDelete.CommentID.ToString().Trim());

            CMD.CommandText = sSql;
            CMD.Connection = conn;
            CMD.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new System.ArgumentException("Erroron DeleteCommentsData ", ex);
        }
        finally
        {
            conn.Close();
        }
    }

        #endregion

        #region UtilsMethods
        static string NullToString(object Value)
        {

            // Value.ToString() allows for Value being DBNull, but will also convert int, double, etc.
            return Value == null ? "" : Value.ToString().Trim();
        }
        #endregion
        }
}

