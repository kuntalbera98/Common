using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Globalization;
using System.Reflection;
using System.Data;
using System.Text.RegularExpressions;

namespace Common
{
    [Serializable]
    public class Common
    {
        public string? DisplayIndianCurrency(float amount)
        {
            return DisplayIndianCurrency(Convert.ToInt32(amount));
        }
        public string? DisplayIndianCurrency(decimal amount)
        {
            return DisplayIndianCurrency(Convert.ToInt32(amount));
        }
        public string? DisplayIndianCurrency(int amount)
        {
            try
            {
                string value = amount.ToString();
                decimal parsed = decimal.Parse(value, CultureInfo.InvariantCulture);
                CultureInfo hindi = new("hi-IN");
                value = string.Format(hindi, "{0:c}", parsed);
                return value.Split(new Char[] { '.' }, StringSplitOptions.None)[0];
            }
            catch (Exception)
            {

                return null;
            }
        }
        public string ConvertDate(string Date)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(Date.Split(new Char[] { 'T' }, StringSplitOptions.None)[0]);
                dt = new DateTime(dt.Year, dt.Month, dt.Day);
                //Date = dt.ToString("ddd','MMM' 'dd' 'yyyy");
                Date = dt.ToString("MMM' 'dd");
                return Date;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public string? ConvertCustomDateFormat(DateTime dateTime,string format)
        {
            try
            {
                return dateTime.ToString(format);
            }
            catch (Exception)
            {

                return null;
            }
        }
        public string ConvertDuration(int Duration)
        {
            try
            {
                double hou, min;
                string duration = "0h 0m";

                hou = (Duration - Duration % 60) / 60;
                min = (Duration - hou * 60);

                duration = hou.ToString() + "h " + min.ToString() + "m";
                return duration;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public string CovertMinuteToHours(int minutes)
        {
            string? hours = null;
            try
            {
                TimeSpan result = TimeSpan.FromMinutes(minutes);
                hours = result.Hours.ToString() + "h "+ result.Minutes.ToString()+"m";
            }
            catch (Exception)
            {

                throw;
            }
            return hours;
        }
        public long NEWGUID()
        {
            //// ex: 8a847645-8cac-422c-962a-fdf3aa220065            
            //Guid g = Guid.NewGuid();
            //long value = BitConverter.ToInt64(g.ToByteArray());
            ////var ho= (6666666666666666666 - 6000000000000000000 - 1)* value + 6000000000000000000;
            // value = Convert.ToInt64("6"+ value.ToString().Substring(0, 18));
            //return value;
            long m_strRow = 0;
            try
            {
                Thread.Sleep(10);
                m_strRow = long.Parse(DateTime.Now.AddYears(-1).Ticks + "1");
            }
            catch
            {
                m_strRow = 0000000000000000000;
            }
            finally
            {
            }
            return m_strRow;
        }

        public string GetCurrentDateTime(bool blnWithTime = false)
        {
            string strDay;
            string strMonth;
            string strYear;
            string strTime;
            string strDate = "";
            DateTime dateTime = GetISTCurrentDateTime();

            strDay = dateTime.Day.ToString("00");
            strMonth = dateTime.Month.ToString("00");
            strYear = dateTime.Year.ToString("0000");

            strDate = strYear + "-" + strMonth + "-" + strDay;

            strTime = dateTime.ToString("HH:mm:ss");
            if (blnWithTime)
            {
                strDate = strDate + " " + strTime;
            }

            return strDate;
        }
        public DateTime GetISTCurrentDateTime()
        {
            try
            {
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                return dateTime;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }
        
        public string? NewNum()
        {
            try
            {
                return "LC/" + DateTime.Now.ToString("yyyMMddhhmmssff");
            }
            catch (Exception)
            {

                return null;
            }

        }
        public string? DateDiff(DateTime startDate, DateTime endDate)
        {
            try
            {
                double diff2 = (endDate.Date - startDate.Date).TotalDays;
                string? dayDiff = null;
                if (diff2 > 0)
                {
                    dayDiff = "+" + diff2.ToString() + " Day";
                }
                else
                {
                    dayDiff = null;
                }
                return dayDiff;
            }
            catch (Exception)
            {

                return null;
            }

        }
        public string? DateDiffToolTrip(DateTime startDate, DateTime endDate)
        {
            try
            {
                double diff2 = (endDate.Date - startDate.Date).TotalDays;
                string? dayDiff = null;
                if (diff2 > 0)
                {
                    dayDiff = endDate.ToString("ddd-MMM-dd");
                }
                else
                {
                    dayDiff = null;
                }
                return dayDiff;
            }
            catch (Exception)
            {

                return null;
            }

        }
        public string? GetDateYyyyMmDd(string p_date)
        {
            string? m_str_date = null;

            try
            {
                DateTime dt = Convert.ToDateTime(p_date);
                m_str_date = dt.ToString("yyyy-MM-ddTHH:mm:ss");

            }
            catch
            {
                m_str_date = null;
            }
            finally
            {
            }

            return m_str_date;
        }
        public string getDBDateFormat(string objDate, bool blnTime = false)
        {
            try
            {
                DateTime sDate;
                string[] formats = { "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy",
                                "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy", "MM/dd/yyyy","dd-MM-yyyy HH:mm:ss","dd-MM-yyyyTHH:mm:ss","MM/dd/yyyy HH:mm:ss",
                                    "yyyy-MM-dd HH:mm:ss","yyyy-MM-ddTHH:mm:ss"};
                DateTime.TryParseExact(objDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out sDate);
                if (blnTime == true)
                {
                    return sDate.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    return sDate.ToString("yyyy-MM-dd");
                }
            }
            catch (Exception)
            {
                return "0000000";
            }

        }
        public string? ConvertDOBFormat(string dt)
        {
            try
            {
                if (dt != null)
                {
                    DateTime dtFrm = Convert.ToDateTime(dt);
                    return dtFrm.ToString("dd-MM-yyyy");
                }
                else
                    return null;
            }
            catch (Exception)
            {

                return null;
            }
        }        
        public static string GetLocalIPAddress()
        {
            try
            {
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public string? GenerateRandomOTP(int iOTPLength)
        {
            try
            {
                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
                string sOTP = String.Empty;

                string sTempChars = String.Empty;

                Random rand = new Random();

                for (int i = 0; i < iOTPLength; i++)
                {
                    int p = rand.Next(0, saAllowedCharacters.Length);

                    sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                    sOTP += sTempChars;

                }

                return sOTP;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public string? RandomPassword(int passwordLength)
        {
            try
            {
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789~!@#$%^&*()-+=?><";
                return new string(Enumerable.Repeat(chars, passwordLength).Select(s => s[random.Next(s.Length)]).ToArray());
            }
            catch (Exception)
            {

                return null;
            }

        }
        public string? TextFileRead(string FullPath)
        {
            try
            {
                string? text = null;
                if(!string.IsNullOrEmpty(FullPath))
                {
                    text = File.ReadAllText(FullPath);
                }
                return text;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string checkDBNull(string strtext)
        {
            try
            {
                if (strtext != null)
                {
                    return strtext;
                }
                else
                    return "";
            }
            catch (Exception)
            {

                return "";
            }
        }
        public object? checkDBNull(object value,bool returnNotNull=false)
        {
            try
            {
                if(value == null)
                {
                    if((bool)returnNotNull)
                    {
                        return "";
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                    return value;
            }
            catch (Exception)
            {
                if ((bool)returnNotNull)
                {
                    return "";
                }
                else
                    return null;
            }
        }
        public object checkNumeric(object value)
        {
            try
            {
                if (value != null)
                {
                    return value;
                } 
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public object checkNumeric(object value, bool convertToInt = false)
        {
            try
            {
                if (value != null)
                {
                    if ((bool)convertToInt)
                    {
                        return Convert.ToInt32(value);
                    }
                    else
                    {
                        return value;
                    }
                }
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public decimal CheckDecimal(object value)
        {
            try
            {
                if (value != null)
                    return Convert.ToDecimal(value);
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public long CheckInt64(object value)
        {
            try
            {
                if (value != null)
                {
                    return Convert.ToInt64(value);
                }
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public decimal RoundDownToNearest(decimal value,int nearestValue= 50)
        {
            try
            {
                decimal rnd = Math.Floor(value / nearestValue) * nearestValue;
                return rnd;

            }
            catch (Exception)
            {
                return 0;
            }
        }
        public decimal StringToDecimal(string value)
        {
            try
            {
                if(value != null)
                {
                    value = Regex.Replace(value, "[^0-9.]", "");
                    return Convert.ToDecimal(value);
                }
                else
                {
                    return new decimal();
                }
            }
            catch (Exception)
            {
                return new decimal();
            }
        }        
    }
    [Serializable]
    public static class LinqHelper
    {
        public static IEnumerable<TSource> l_DistinctBy<TSource, TKey>
        (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        public static List<t> GetRandomElements<t>(List<t> list, int elementsCount)
        {
            return list.OrderBy(x => Guid.NewGuid()).Take(elementsCount).ToList();
        }
        public static object? GetProperty<T>(this T entity, string propertyName) where T : class, new()
        {
            try
            {
                Type type = entity.GetType();
                PropertyInfo? propertyInfo = type.GetProperty(propertyName);
                object? value = propertyInfo?.GetValue(entity);
                return value;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public static void DetachEntity<T>(this DbContext dbContext, T entity, string propertyName) where T : class, new()
        //{
        //    try
        //    {
        //        var dbEntity = dbContext.Find<T>(entity.GetProperty(propertyName));
        //        if (dbEntity != null)
        //            dbContext.Entry(dbEntity).State = EntityState.Detached;
        //        dbContext.Entry(entity).State = EntityState.Modified;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public static DataTable CreateDataTable<T>(List<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            dataTable.TableName = typeof(T).FullName;
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object?[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        public static DataTable CreateDataTable<T>(T model)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            dataTable.TableName = typeof(T).FullName;
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }
            object?[] values = new object[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                values[i] = properties[i].GetValue(model);
            }

            dataTable.Rows.Add(values);

            return dataTable;
        }
        public static String ToCamelCase(this string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    string? strCamelCaseValue = null;
                    var list = new List<string>();
                    var arr = value.Split(" ");
                    foreach (var str in arr)
                    {
                        list.Add(char.ToUpper(str[0]) + str.Substring(1));
                    }
                    strCamelCaseValue = string.Join(' ', list);
                    return strCamelCaseValue;
                }
                else
                {
                    return value;
                }
            }
            catch (Exception)
            {
                return value;
            }
        }
        public static String ToSentenceCase(this string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // start by converting entire string to lower case
                    var lowerCase = value.ToLower();
                    // matches the first sentence of a string, as well as subsequent sentences
                    var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);
                    // MatchEvaluator delegate defines replacement of setence starts to uppercase
                    var result = r.Replace(lowerCase, s => s.Value.ToUpper());
                    return result;
                }
                else
                {
                    return value;
                }
            }
            catch (Exception)
            {
                return value;
            }

        }
        public static String? GetFirstName(this string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    return value.Split(' ').FirstOrDefault();
                }
                else
                {
                    return value;
                }
            }
            catch (Exception)
            {
                return value;
            }
        }
    }
}
