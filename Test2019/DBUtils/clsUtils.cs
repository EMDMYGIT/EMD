using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Test2019.DBUtils
{
    public class clsUtils
    {
        #region Declarations

        private static System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
        private static System.Data.SqlClient.SqlCommand CMD = new System.Data.SqlClient.SqlCommand();

        //private static System.Data.SqlClient.SqlDataAdapter DA;


        #endregion
        // Constructor (New)
        static clsUtils()
        {

            //  System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
            //System.Data.SqlClient.SqlCommand CMD = new System.Data.SqlClient.SqlCommand();
            //System.Data.DataSet DS = new System.Data.DataSet();
            
            //conn.ConnectionString = @"Data Source=sqlimasd.database.windows.net;Initial Catalog=TestIMASD;User ID=saimasd;Password=sqlemd_2020;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            conn.ConnectionString= System.Configuration.ConfigurationManager.ConnectionStrings["SQLDEV"].ToString();
        }
        #region Contacts
        // Obtención de datos (Select , lista/por ID)
        public List<Test2019.Models.TB_Contact> RetrieveContactData(Int32 idFilter, string OrderBy)
        {
            try
            {
                conn.Open();
                // Insert code to process data.
                string sSql = @"SELECT [ID],[Contact],[Name],[Adress],[TelNo],[City],[CountryID],[CreateDate],[CreateUser] FROM [dbo].[Contacts]";
                if (idFilter != 0) sSql = sSql + " where ID=" + idFilter;
                if (OrderBy != null && OrderBy != string.Empty) sSql = sSql + @" Order by " + OrderBy;

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
        //public List<Test2019.Models.TB_Contact> RetrieveContactDataRelated(String Filter)
        //{
        //    try
        //    {
        //        conn.Open();
        //        // Insert code to process data.
        //        string sSql = @"SELECT [ContactID],[Name],[Surname],[Surname2],[Telephone],[e_mail]
        //            ,[City],[Country]  FROM[dbo].[TB_Contact]";
        //        if (Filter != string.Empty) sSql = sSql + " where 1=2 " + Filter;


        //        CMD.CommandText = sSql;
        //        CMD.Connection = conn;
        //        System.Data.SqlClient.SqlDataAdapter DA = new System.Data.SqlClient.SqlDataAdapter(CMD);
        //        System.Data.DataSet DS = new DataSet();
        //        DA.Fill(DS, "Contacts");
        //        return GetAllContacts(DS);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new System.ArgumentException("Erroron RetrieveContactData", ex);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}
        private List<Test2019.Models.TB_Contact> GetAllContacts(DataSet pDS)
        {
            try
            {
                //[ID], [Contact], [Name], [Adress], [TelNo], [City], [CountryID], [CreateDate], [CreateUser]
                List<Test2019.Models.TB_Contact> bMList = new List<Test2019.Models.TB_Contact>();

                foreach (DataRow item in pDS.Tables[0].Rows)
                {
                    Test2019.Models.TB_Contact TempModel = new Test2019.Models.TB_Contact();
                    TempModel.ID = Convert.ToInt32(item["ID"]);
                    TempModel.Name = item["Name"].ToString();
                    TempModel.Adress = item["Adress"].ToString();
                    TempModel.TelNo = item["TelNo"].ToString();
                    TempModel.City = item["City"].ToString();
                    TempModel.CountryID = int.Parse(item["CountryID"].ToString());
                    TempModel.CreateDate = DateTime.Parse(item["CreateDate"].ToString());
                    TempModel.CreateUser = item["CreateUser"].ToString();
                    

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



        //public void InsertContactsData(Test2019.Models.TB_Contact dataInsert)
        //{
        //    try
        //    {
        //        conn.Open();
        //        // Insert code to process data.
        //        string sSql = @"INSERT INTO [dbo].[TB_Contact]
        //                ([Name],[Surname],[Surname2],[Telephone],[e_mail],[City],[Country])     VALUES
        //            ('%Name%','%Surname%','%Surname2%','%Telephone%','%e_mail%,'%City%','%Country%');SELECT SCOPE_IDENTITY();";

        //        sSql = sSql.Replace("%Name%", NullToString(dataInsert.Name));
        //        sSql = sSql.Replace("%Surname%", NullToString(dataInsert.Surname));
        //        sSql = sSql.Replace("%Surname2%", NullToString(dataInsert.Surname2));
        //        sSql = sSql.Replace("%Telephone%", NullToString(dataInsert.Telephone));
        //        sSql = sSql.Replace("%e_mail%", NullToString(dataInsert.e_mail));
        //        sSql = sSql.Replace("%City%", NullToString(dataInsert.City));
        //        sSql = sSql.Replace("%Country%", NullToString(dataInsert.Country));

        //        CMD.CommandText = sSql;
        //        CMD.Connection = conn;


        //        try
        //        {
        //            Int32 returnid = 0;
        //            returnid = Convert.ToInt32(CMD.ExecuteScalar());

        //        }
        //        catch (System.Data.SqlClient.SqlException sex)
        //        {
        //            throw new System.ArgumentException("Error on Insert: " + CMD.CommandText, sex);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new System.ArgumentException("Erroron InsertContactsData ", ex);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //}

        //public void UpdateContactsData(Test2019.Models.TB_Contact dataUpdate)
        //{
        //    try
        //    {
        //        conn.Open();
        //        // Insert code to process data.
        //        string sSql = @"UPDATE [dbo].[TB_Contact]
        //                        SET [Name] = '%Name%',
        //                        [Surname] = '%Surname%',
        //                        [Surname2] = '%Surname2%',
        //                        [Telephone] = '%Telephone%',
        //                        [e_mail] = '%e_mail%',
        //                        [City]= '%City%',
        //                        [Country]= '%Country%'
        //                         WHERE [ContactID]=%ContactID%";
        //        sSql = sSql.Replace("%Name%", NullToString(dataUpdate.Name));
        //        sSql = sSql.Replace("%Surname%", NullToString(dataUpdate.Surname));
        //        sSql = sSql.Replace("%Surname2%", NullToString(dataUpdate.Surname2));
        //        sSql = sSql.Replace("%Telephone%", NullToString(dataUpdate.Telephone));
        //        sSql = sSql.Replace("%e_mail%", NullToString(dataUpdate.e_mail));
        //        sSql = sSql.Replace("%City%", NullToString(dataUpdate.City));
        //        sSql = sSql.Replace("%Country%", NullToString(dataUpdate.Country));
        //        sSql = sSql.Replace("%ContactID%", NullToString(dataUpdate.ContactID));

        //        CMD.CommandText = sSql;
        //        CMD.Connection = conn;
        //        CMD.ExecuteNonQuery();

        //        //if (dataUpdate.ContactComment != null)
        //        //{
        //        //    Contacts.Models.TB_Comments commentDta = new Contacts.Models.TB_Comments();
        //        //    commentDta.Comment = dataUpdate.ContactComment.Comment;
        //        //    commentDta.ParentId = dataUpdate.ContactID;
        //        //    commentDta.ParentType = "TB_CONTACTS";
        //        //    if (dataUpdate.ContactComment.CommentID == 0)
        //        //    { InsertCommentssData(commentDta); }
        //        //    else
        //        //    { UpdateCommentsData(commentDta); }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new System.ArgumentException("Erroron UpdateTest1Data ", ex);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //}

        //public void DeleteContacData(Int32 idDelete)
        //{
        //    try
        //    {
        //        conn.Open();
        //        // Insert code to process data.
        //        string sSql = "Delete from  [dbo].[TB_Contact]  WHERE [ID]=%ContactID%";
        //        sSql = sSql.Replace("%ContactID%", idDelete.ToString());

        //        CMD.CommandText = sSql;
        //        CMD.Connection = conn;
        //        CMD.ExecuteNonQuery();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new System.ArgumentException("Erroron DeleteContacData ", ex);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}

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