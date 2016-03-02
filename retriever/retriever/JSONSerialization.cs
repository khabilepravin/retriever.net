using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Retriever.Net
{
    internal static class JSONSerialization
    {
        internal static string SerializeToJSON(this SqlDataReader reader)
        {
            var r = SerializeSingle(reader);
            string serializedJsonString = JsonConvert.SerializeObject(r);
            reader.Close();

            return serializedJsonString;
        }

        private static IEnumerable<Dictionary<string, object>> Serialize(this SqlDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
            { cols.Add(reader.GetName(i)); }

            while (reader.Read())
            {
                results.Add(SerializeRow(cols, reader));
            }

            reader.Close();
            return results;
        }

        private static Dictionary<string, object> SerializeRow(IEnumerable<string> cols,
                                                       SqlDataReader reader)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
            {
                if (reader[col] != DBNull.Value)
                {
                    result.Add(col, reader[col]);
                }
            }

            return result;
        }

        private static Dictionary<string, object> SerializeSingle(this SqlDataReader reader)
        {
            var result = new Dictionary<string, object>();
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
            { cols.Add(reader.GetName(i)); }

            if (reader.Read())
            {
                result = SerializeRow(cols, reader);
            }

            return result;
        }

        internal static dynamic SerializeToList<T>(this SqlDataReader reader)
        {
            var r = Serialize(reader);
            string serilizedInString = JsonConvert.SerializeObject(r);
            return JsonConvert.DeserializeObject<T>(serilizedInString);
        }

        internal static dynamic SerializeToObject<T>(this SqlDataReader reader)
        {
            var r = SerializeSingle(reader);
            string serilizedInString = JsonConvert.SerializeObject(r);
            return JsonConvert.DeserializeObject<T>(serilizedInString);
        }

        internal static SqlParameter[] DeserializeJsonIntoSqlParameters(this string jsonString)
        {
            List<SqlParameter> sqlParams = null;
            Dictionary<string, dynamic> parametersDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);

            if (parametersDictionary != null)
            {
                foreach (KeyValuePair<string, dynamic> keyVal in parametersDictionary)
                {
                    SqlParameter param = new SqlParameter()
                    {
                        ParameterName = string.Format("@{0}", keyVal.Key),
                        Value = keyVal.Value
                    };

                    if (sqlParams == null) { sqlParams = new List<SqlParameter>(); }
                    sqlParams.Add(param);
                }
            }

            return sqlParams.ToArray();
        }

        internal static T GetValueOrDefault<T>(this IDataRecord row, string fieldName)
        {
            int ordinal = row.GetOrdinal(fieldName);
            return row.GetValueOrDefault<T>(ordinal);
        }

        internal static T GetValueOrDefault<T>(this IDataRecord row, int ordinal)
        {
            return (T)(row.IsDBNull(ordinal) ? default(T) : row.GetValue(ordinal));
        }

        internal static bool GetValueOrBoolean(this IDataRecord row, string fieldName)
        {
            int ordinal = row.GetOrdinal(fieldName);
            return row.IsDBNull(ordinal) ? false : Convert.ToBoolean(row.GetValue(ordinal));
        }

        internal static object GetDefault(this Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }

        internal static T GetDefault<T>()
        {
            var t = typeof(T);
            return (T)GetDefault(t);
        }

        internal static object IsDefault<T>(T other)
        {
            T defaultValue = GetDefault<T>();
            if (other == null)
            {
                return (object)DBNull.Value;
            }
            else if (other is bool) // since default value for bool is false which is also a valid value.
            {
                return other;
            }
            else if (other.Equals(defaultValue))
            {
                return (object)DBNull.Value;
            }
            else
            {
                return other;
            }
        }
    }
}
