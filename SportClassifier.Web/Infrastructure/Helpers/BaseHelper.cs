
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using System.Web;


namespace SportClassifier.Web.Infrastructure.Helpers
{
    public class BaseHelper
    {

        public static string GetCaptionString(string key)
        {
            ResourceManager resource = new ResourceManager("Agromo.Web.App_Resourses.English", Assembly.GetExecutingAssembly());

            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("bg-BG");


            string result = "#" + key + "#";

            if (resource.GetString(key) != null)
            {
                result = resource.GetString(key);
            }

            return result;
        }

        public static string ScrubHtml(string html)
        {
            var step1 = Regex.Replace(html, @"<[^>]+>|&nbsp;", "").Trim();
            var step2 = Regex.Replace(step1, @"\s{2,}", " ");
            var step3 = Regex.Replace(step2, @"\r\n?|\n", " ");
            var step4 = Regex.Replace(step3, @"[\d-]", " ");
            return step4;
        }

        public static void WriteInFile(string fileName, string text)
        {
            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                writetext.WriteLine(text);
            }
        }

        public static string MakeToastPlusScriptTag(SportClassifier.Web.Infrastructure.Enums.Enumerations.ToastrType toastrType, string message)
        {
            return string.Format("<script language='javascript' type='text/javascript'>toastr.{0}('{1}');</script>", toastrType.ToString(), message);

            //return string.Format("toastr.{0}('{1}');", toastrType.ToString(), message);
        }

        public static string MakeToast(SportClassifier.Web.Infrastructure.Enums.Enumerations.ToastrType toastrType, string message)
        {

            return string.Format("toastr.{0}('{1}');", toastrType.ToString(), message);
        }

        public static string GetCapchaResponse(string response)
        {
            const string secret = "6LdKWAoTAAAAAACaaalKSnlUyufO7cyg3NiVOVry";

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            return reply;
        }

        //public static string CheckCapchaErrorMessage(CaptchaResponse captchaResponse)
        //{
        //    string errorMessage = "Please mark the check box";
        //    if (captchaResponse.ErrorCodes.Count >= 0)
        //    {
        //        var error = captchaResponse.ErrorCodes[0].ToLower();
        //        switch (error)
        //        {
        //            case ("missing-input-secret"):
        //                errorMessage = "The secret parameter is missing.";
        //                break;
        //            case ("invalid-input-secret"):
        //                errorMessage = "The secret parameter is invalid or malformed.";
        //                break;

        //            case ("missing-input-response"):
        //                errorMessage = "The response parameter is missing.";
        //                break;
        //            case ("invalid-input-response"):
        //                errorMessage = "The response parameter is invalid or malformed.";
        //                break;

        //            default:
        //                errorMessage = "Error occured. Please try again";
        //                break;
        //        }
        //    }

        //    return errorMessage;
        //}


    }
}