using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace Common
{
    [Serializable]
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            ///Set Session Without timeout
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static void SetObject(this ISession session, string key, object value, TimeSpan expireafter)
        {
            ///Set Session With timeout
            session.SetString(key, JsonConvert.SerializeObject(value));
            session.SetObject(key + "ExpDate", DateTime.Now.Add(expireafter));
        }
        public static T? GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            if (session.IsSessionExpire(key))
            {
                return default(T);
            }
            else
            {
                return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
            }

        }
        public static object? Get(this ISession session, string key, bool CheckExpire)
        {
            try
            {
                var value = session.GetString(key);

                ///For Checking session timout
                if (CheckExpire)
                {
                    if (session.IsSessionExpire(key))
                    {
                        return null;
                    }
                    else
                    {
                        return value == null ? null : value;
                    }
                }
                else
                {
                    return value == null ? null : value;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static void RemoveExpiredSessions(this ISession session)
        {
            ///Remove all timout sessions
            try
            {
                foreach (var key in session.Keys)
                {
                    session.IsSessionExpire(key);
                }
            }
            catch (Exception)
            {
            }
        }
        public static bool IsSessionExpire(this ISession session, string key)
        {
            ///Check Session timout or not
            try
            {
                DateTime expdate = new DateTime();
                if (session.Get(key + "ExpDate") != null)
                {
                    expdate = session.GetObject<DateTime>(key + "ExpDate");
                }
                if (expdate == new DateTime())
                {
                    return false;
                }
                else
                {
                    if (expdate < DateTime.Now)
                    {
                        session.Remove(key);
                        session.Remove(key + "ExpDate");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        //public static byte[] GetBarCode(this string Content)
        //{
        //    try
        //    {
        //        Byte[] byteArray;
        //        if (!string.IsNullOrEmpty(Content))
        //        {

        //            var writer =  ZXing.BarcodeWriter()
        //            {
        //                Format = BarcodeFormat.PDF_417,
        //                Options = new EncodingOptions
        //                {
        //                    Height = 3,
        //                    Width = 100,
        //                    PureBarcode = true,
        //                    Margin = 10,
        //                },
        //                Renderer = new BitmapRenderer()
        //                {
        //                    TextFont = new Font("Arial", 9, FontStyle.Regular),
        //                }
        //            };

        //            var pixelData = writer.Write(Content);

        //            using (var ms = new MemoryStream())
        //            {
        //                // save to stream as PNG
        //                pixelData.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //                byteArray = ms.ToArray();
        //            }

        //            return byteArray;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        return null;
        //    }
        //}
        
    }
}
