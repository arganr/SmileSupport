using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Threading;
using System.Text;
using System.Security.Cryptography;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers
{
    public class SqlDBSessionSQL : SqlDBSession
    {
        protected override bool GetSession(string aComputerName, string aUserName, int privateKey)
        {
            bool flag = false;
            SqlConnection connection = new SqlConnection(this.myLocalConnectionString);
            try
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT SessionKey, NIK FROM Session WHERE ComputerName=@ComputerName AND UserName=@UserName AND PrivateKey=@PrivateKey AND TimeEnd IS NULL ORDER BY SessionKey DESC", connection);
                sqlCommand.Parameters.Add("@ComputerName", SqlDbType.NVarChar, 100).Value = (object)aComputerName;
                sqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 100).Value = (object)aUserName;
                sqlCommand.Parameters.Add("@PrivateKey", SqlDbType.Int).Value = (object)privateKey;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    sqlDataReader.Read();
                    this.mySessionKey = sqlDataReader.GetInt32(0);
                    this.myLoginUserID = sqlDataReader.GetString(1);
                    if (this.myAfterLoginEvent != null)
                        this.myAfterLoginEvent();
                    flag = true;
                }
                sqlDataReader.Close();
            }
            catch (SqlException ex)
            {
                DataError.HandleSqlException(ex);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return flag;
        }
        protected override bool CloseSession()
        {
            if (this.mySessionKey < 0)
            {
                return false;
            }
            else
            {
                SqlConnection connection = new SqlConnection(this.myLocalConnectionString);
                try
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("UPDATE Session SET TimeEnd=GetDate() WHERE SessionKey=@SessionKey", connection);
                    sqlCommand.Parameters.AddWithValue("@SessionKey", (object)this.mySessionKey);
                    int num = sqlCommand.ExecuteNonQuery();
                    this.mySessionKey = -1;
                    this.myLoginUserID = "";
                    return num > 0;
                }
                catch (SqlException ex)
                {
                    DataError.HandleSqlException(ex);
                    return false;
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
        protected override void CreateSessionRecord(string aComputerName, string aUserName, int privateKey, string aUserID)
        {
            SqlConnection connection = new SqlConnection(this.myLocalConnectionString);
            try
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Session (NIK, ComputerName, UserName, PrivateKey,TimeStart) VALUES (@NIK, @ComputerName, @UserName, @PrivateKey, @TimeStart)", connection);
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@NIK", SqlDbType.NVarChar, 100);
                sqlParameter1.Value = (object)aUserID;
                sqlParameter1.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@ComputerName", SqlDbType.NVarChar, 100);
                sqlParameter2.Value = (object)aComputerName;
                sqlParameter2.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 100);
                sqlParameter3.Value = (object)aUserName;
                sqlParameter3.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@PrivateKey", SqlDbType.Int);
                sqlParameter4.Value = (object)privateKey;
                sqlParameter4.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@TimeStart", SqlDbType.DateTime);
                sqlParameter5.Value = (object)DateTime.Now;
                sqlParameter5.Direction = ParameterDirection.Input;
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                DataError.HandleSqlException(ex);
                return;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            this.GetSession(aComputerName, aUserName, privateKey);
        }
        public static bool VerifyPassword(string hashPassword, string password)
        {
            if (hashPassword.Length < 24)
            {
                return false;
            }
            else
            {
                string str = hashPassword.Substring(0, 24);
                byte[] hash = new SHA512Managed().ComputeHash(new ASCIIEncoding().GetBytes(str + password));
                return hashPassword == str + System.Convert.ToBase64String(hash);
            }
        }
        public override bool Authenticate(string aUserID, string aPassword)
        {
            if (this.myAllowRootLogin && string.Compare(aUserID, "__ROOT", true) == 0 && string.Compare(aPassword, DateTime.Today.ToString("yyyyMMdd"), true) == 0)
            {
                return true;
            }
            else
            {
                bool flag = false;
                SqlConnection connection = new SqlConnection(this.myConnectionString);
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("[sp_master_user_login]", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter spID = new SqlParameter("@p_user_id", aUserID);
                        spID.SqlDbType = SqlDbType.NVarChar;
                        cmd.Parameters.Add(spID);
                        SqlParameter spPass = new SqlParameter("@p_user_password", aPassword);
                        spPass.SqlDbType = SqlDbType.NVarChar;
                        cmd.Parameters.Add(spPass);
                        SqlDataReader sqlDataReader = cmd.ExecuteReader();
                        if (sqlDataReader.HasRows)
                        {
                            flag = true;
                        }
                        sqlDataReader.Close();
                    }
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    DataError.HandleSqlException(ex);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
                return flag;
            }
        }
    }
}