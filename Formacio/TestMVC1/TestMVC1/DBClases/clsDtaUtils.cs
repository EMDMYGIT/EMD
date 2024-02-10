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

        // Obtención de datos (Select , lista/por ID)
        public List<TestMVC1.Models.Test1Model> RetrieveTest1Data(Int32 idFilter)
        {
            try
            {
                conn.Open();
                // Insert code to process data.
                string sSql = "SELECT Id, Code, Description FROM Test1";
                if (idFilter!=0)  sSql=sSql + " where Id=" + idFilter;

                
                CMD.CommandText = sSql;
                CMD.Connection = conn;
                System.Data.SqlClient.SqlDataAdapter DA = new System.Data.SqlClient.SqlDataAdapter(CMD);
                System.Data.DataSet DS = new DataSet();
                DA.Fill(DS, "Test1");   
                return GetAllTest1(DS);
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Erroron RetrieveTest1Data", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        private List<TestMVC1.Models.Test1Model> GetAllTest1(DataSet pDS)
        {
            try
            {
              List<TestMVC1.Models.Test1Model> bMList = new List<TestMVC1.Models.Test1Model>();
            TestMVC1.Models.Test1Model Test1Model = new TestMVC1.Models.Test1Model();

            foreach (DataRow item in pDS.Tables[0].Rows)
             {
                TestMVC1.Models.Test1Model Test1Modelx = new TestMVC1.Models.Test1Model();
                Test1Modelx.id = Convert.ToInt32(item["id"]);
                Test1Modelx.code= item["code"].ToString();
                Test1Modelx.description = item["description"].ToString();
                bMList.Add(Test1Modelx);
                Test1Model = null;
            }
            return bMList;
            }
            catch (Exception ex)
            {

                throw new System.ArgumentException("Erroron GetAllTest1", ex);
            }
        }


        public void InsertTest1Data(TestMVC1.Models.Test1Model dataTest1)
        {
            try
            {
                conn.Open();
                // Insert code to process data.
                string sSql = "Insert into Test1 (Code, Description)  values ('%CODE%' , '%DESC%') ";
                sSql = sSql.Replace("%CODE%", dataTest1.code.ToString().Trim());
                sSql = sSql.Replace("%DESC%", dataTest1.description.ToString().Trim());

                CMD.CommandText = sSql;
                CMD.Connection = conn;
                try
                { 
                CMD.ExecuteNonQuery();
                }
                catch(System.Data.SqlClient.SqlException sex)
                {
                    throw new System.ArgumentException("Error on Insert: " + CMD.CommandText, sex);
                }
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Erroron InsertTest1Data ", ex);
            }
            finally
            {
                conn.Close();
            }

        }

        public void UpdateTest1Data(TestMVC1.Models.Test1Model dataTest1)
        {
            try
            {
                conn.Open();
                // Insert code to process data.
                string sSql = "Update Test1 set Code='%CODE%', Description=' %DESC%' where Id=%ID%";
                sSql= sSql.Replace("%CODE%", dataTest1.code.ToString().Trim());
                sSql= sSql.Replace("%DESC%", dataTest1.description.ToString().Trim());
                sSql= sSql.Replace("%ID%", dataTest1.id.ToString().Trim());

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

        public void DeleteTest1Data(TestMVC1.Models.Test1Model dataTest1)
        {
            try
            {
                conn.Open();
                // Insert code to process data.
                string sSql = "Delete from  Test1  where Id=%ID%";
                sSql = sSql.Replace("%ID%", dataTest1.id.ToString().Trim());

                CMD.CommandText = sSql;
                CMD.Connection = conn;
                CMD.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Erroron DeleteTest1Data ", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}