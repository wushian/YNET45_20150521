using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Runtime.InteropServices;
using YNetLib_45;

namespace DD2015_45
{
    public class StringTable
    {
        public static StringTable _instance = null;
        private Dictionary<string, Dictionary<string, string>> _stringTable = null;
        //private static bool insert = true;
        //private static bool bypass = false;
        private static bool insert = false;
        private static bool bypass = false;

        private StringTable()
        {
            _stringTable = new Dictionary<string, Dictionary<string, string>>();

            if (!bypass)
            {
                LoadStringTable();
            }
        }

        /// <summary>
        /// 查詢字串表
        /// </summary>
        public static string GetString(string langId, string messageId)
        {
            if (bypass)
                return messageId;

            langId = langId.ToUpper();

            if (_instance == null)
                _instance = new StringTable();

            // 若 GetStrings 之後 _StringTable 仍為 null，或是無資料，則毋須搜尋
            if (!insert)
            { //shan 沒有table也可以直接轉
                //if (_instance._stringTable.Count == 0)
                //  return messageId;
            }

            // 字串 ID 可以用 + 號串接，所以用 + 號作為分隔字元拆出各個項目
            string[] messageIds = messageId.Split('+');
            StringBuilder sb = new StringBuilder();

            foreach (string mId in messageIds)
            {
                string sId = mId.Trim();
                string UsId = sId.ToUpper();

                // 固定文字的判斷, 以 `` 或是 '' 或是 "" 包圍的均為固定字串
                if ((sId.StartsWith("`") && sId.EndsWith("`")) ||
                    (sId.StartsWith("'") && sId.EndsWith("'")) ||
                    (sId.StartsWith("\"") && sId.EndsWith("\"")))
                {
                    if (sId.Length >= 3)
                        sb.Append(sId.Substring(1, sId.Length - 2));
                    continue;
                }

                // 搜尋訊息表
                // 沒有這個語言
                if (langId == "ZH-CN")
                {
                    sb.Append(CharSetConverter.ToSimplified(sId));
                    continue;
                }
                else if (!_instance._stringTable.ContainsKey(langId))
                {
                    if (insert)
                    {
                        _instance.InsertStringTable(langId, sId, sId);
                        _instance._stringTable.Add(langId, new Dictionary<string, string>());
                        _instance._stringTable[langId].Add(UsId, sId);
                    }

                    sb.Append(sId);
                    continue;
                }
                // 有這個語言，搜尋內容
                else
                {
                    Dictionary<string, string> langStringTable = _instance._stringTable[langId];
                    if (langStringTable.ContainsKey(UsId))
                        sb.Append(langStringTable[UsId]);
                    else
                    {
                        // 是否新增找不到的字串表
                        if (insert)
                        {
                            _instance.InsertStringTable(langId, sId, sId);
                            langStringTable.Add(UsId, sId);
                        }

                        sb.Append(sId);
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 查詢字串表
        /// </summary>
        public static string GetString(System.Web.UI.Page page, string messageId)
        {
            string langId = page.Request.Cookies[PublicVariable.UserLanguage] != null ? page.Request.Cookies[PublicVariable.UserLanguage].Value : "";
            return GetString(langId, messageId);
        }

        /// <summary>
        /// 查詢字串表
        /// </summary>
        public static string GetString(string messageId)
        {
            return GetString(PublicVariable.UserLanguage, messageId);
        }

        /// <summary>
        /// 載入字串表
        /// </summary>
        public void LoadStringTable()
        {
            _stringTable.Clear();

            List<string> langIds = new List<string>();
            //SqlConnection conn = new SqlConnection(PublicVariable.ConnectionString);
            //SqlCommand cmd = new SqlCommand("select distinct lang_id from string_table with (nolock) order by lang_id", conn);


            DbConnection connr = DAC.NewConnection();
            connr.ConnectionString = DAC.ConnectionString;
            DbCommand cmdr = DAC.NewCommand();
            cmdr.Connection=connr;
            cmdr.CommandText = "select distinct lang_id from string_table with (nolock) order by lang_id";
            try
            {
                connr.Open();
                DbDataReader reader = cmdr.ExecuteReader();
                while (reader.Read())
                {
                    _stringTable.Add(reader.GetString(0).ToUpper(), new Dictionary<string, string>());
                    langIds.Add(reader.GetString(0));
                }
                cmdr.Cancel();
                reader.Close();

                foreach (string langId in langIds)
                {
                    switch (DAC.ConnectionType)
                    {
                       case DAC.connTypeOLEDB:
                       cmdr.CommandText = "select string_id, string_text from string_table with (nolock) where lang_Id=? ";
                       (cmdr as OleDbCommand).Parameters.AddWithValue(langId, langId);
                       break;
                    case DAC.connTypeMSSQL:
                       cmdr.CommandText = "select string_id, string_text from string_table with (nolock) where lang_Id=@langId ";
                       cmdr.Parameters.Clear();
                       (cmdr as SqlCommand).Parameters.AddWithValue(langId, langId);
                       break;
                    }
                    reader = cmdr.ExecuteReader();
                    while (reader.Read())
                    {
                        _stringTable[langId.ToUpper()].Add(reader.GetString(0).ToUpper(), reader.GetString(1));
                    }
                    cmdr.Cancel();
                    reader.Close();
                }
            }
            finally
            {
                connr.Close();
                cmdr.Dispose();
                connr.Dispose();
            }
        }

        /// <summary>
        /// 重新載入字串表
        /// </summary>
        public static void Reload()
        {
            if (_instance != null)
                _instance.LoadStringTable();
        }

        private void InsertStringTable(string langId, string stringId, string stringText)
        {
        }

        private void InsertStringTable_X(string langId, string stringId, string stringText)
        {
            DbConnection conn = DAC.NewConnection();
            conn.ConnectionString = PublicVariable.ConnectionString;
            DbCommand cmd = DAC.NewCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into string_table (lang_id, string_id, string_text) values (@lang_id, @string_id, @string_text)";

            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("lang_id", langId));
            cmd.Parameters.Add(new SqlParameter("string_id", stringId));
            cmd.Parameters.Add(new SqlParameter("string_text", stringText));
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

    }

    /// <summary> 
    /// 做為字碼轉換工具 
    /// </summary> 
    public class CharSetConverter
    {
        internal const int LOCALE_SYSTEM_DEFAULT = 0x0800;
        internal const int LCMAP_SIMPLIFIED_CHINESE = 0x02000000;
        internal const int LCMAP_TRADITIONAL_CHINESE = 0x04000000;

        /// <summary> 
        /// 使用OS的kernel.dll做為簡繁轉換工具，只要有裝OS就可以使用，不用額外引用dll，但只能做逐字轉換，無法進行詞意的轉換 
        /// <para>所以無法將電腦轉成計算機</para> 
        /// </summary> 
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int LCMapString(int Locale, int dwMapFlags, string lpSrcStr, int cchSrc, [Out] string lpDestStr, int cchDest);

        /// <summary> 
        /// 繁體轉簡體 
        /// </summary> 
        /// <param name="pSource">要轉換的繁體字：體</param> 
        /// <returns>轉換後的簡體字：体</returns> 
        public static string ToSimplified(string pSource)
        {
            String tTarget = new String(' ', pSource.Length);
            int tReturn = LCMapString(LOCALE_SYSTEM_DEFAULT, LCMAP_SIMPLIFIED_CHINESE, pSource, pSource.Length, tTarget, pSource.Length);
            return tTarget;
        }

        /// <summary> 
        /// 簡體轉繁體 
        /// </summary> 
        /// <param name="pSource">要轉換的繁體字：体</param> 
        /// <returns>轉換後的簡體字：體</returns> 
        public static string ToTraditional(string pSource)
        {
            String tTarget = new String(' ', pSource.Length);
            int tReturn = LCMapString(LOCALE_SYSTEM_DEFAULT, LCMAP_TRADITIONAL_CHINESE, pSource, pSource.Length, tTarget, pSource.Length);
            return tTarget;
        }

    }
}