using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace Hermes.WebServices
{
    class GlobalVar
    {
		public const string URL = "http://192.168.1.100:8080/HermesWS/webresources/hermes.";
		public const string HERMES_PREFERENCES = "HERMES.PREFERENCES";
		public const string USER_ID = "HERMES.USER_ID";
		public const string REMEMBER_USER = "HERMES.REMEMBER_UER";
		public const string USER_EMAIL = "HERMES.USER_EMAIL";
		public const string USER_PASSWORD = "HERMES.USER_PASSWORD";
		public const string USER_NAMES = "HERMES.USER_NAMES";
		public const string USER_LASTNAMES = "HERMES.USER_LASTNAMES";
		public const string USER_PHONE = "HERMES.USER_PHONE";
		public const string USER_CREATED = "HERMES.USER_CREATED";
		public const string USER_UPDATED = "HERMES.USER_UPDATED";
		public const string RESERVATION_DETAILS = "HERMES.RESERVATION_DETAILS";
        //REGEX
        public static Regex PHONE_REGEX = new Regex("^([5-9][0-9]{7})$");


        public static bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}