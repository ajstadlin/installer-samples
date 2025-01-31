﻿using GdPicture14.WEB;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;

namespace docuviewareBlazorDemo
{
    public static class Globals
    {
        private static readonly string m_rootDirectory = Directory.GetCurrentDirectory();
        public static readonly int SESSION_TIMEOUT = 20; //Set to 20 minutes. use -1 to handle DocuVieware session timeout through asp.net session mechanism.
        public const bool STICKY_SESSION = true; //Set false to use DocuVieware on Servers Farm with non sticky sessions.
        public const DocuViewareSessionStateMode DOCUVIEWARE_SESSION_STATE_MODE = DocuViewareSessionStateMode.InProc; //Set DocuViewareSessionStateMode.File if STICKY_SESSION is False.


        public static string GetCacheDirectory()
        {
            return Path.Combine(m_rootDirectory, @"cache");
        }


        public static string GetDocumentsDirectory()
        {
            return Path.Combine(m_rootDirectory , @"wwwroot\documents");
        }


        public static string BuildDocuViewareControlSessionID(HttpContext HttpContext, string clientID)
        {
            if (HttpContext.Session.GetString("DocuViewareInit") == null)
            {
                HttpContext.Session.SetString("DocuViewareInit", "true");
            }

            return HttpContext.Session.Id + clientID;
        }


        public static DocuViewareLocale GetDocuViewareLocale(HttpRequest request)
        {
            if (request != null)
            {
                IList<StringWithQualityHeaderValue> acceptLanguage = request.GetTypedHeaders().AcceptLanguage;

                if (acceptLanguage != null)
                {
                    foreach (StringWithQualityHeaderValue language in acceptLanguage)
                    {
                        object docuviewareLocale;
                        if (Enum.TryParse(typeof(DocuViewareLocale), language.Value.Value, true, out docuviewareLocale))
                        {
                            return (DocuViewareLocale)docuviewareLocale;
                        }
                    }
                }
            }

            return DocuViewareLocale.En;
        }              
    }
}