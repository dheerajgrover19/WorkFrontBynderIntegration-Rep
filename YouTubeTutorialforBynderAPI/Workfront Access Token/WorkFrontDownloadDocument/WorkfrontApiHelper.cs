using Newtonsoft.Json;
using System;
using iText;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Net.Mime;
using DocumentFormat.OpenXml.Bibliography;
using System.Collections.Generic;

namespace Workfront_Access_Token.Common
{
    public class WorkfrontApiHelper
    {
        public static string apiKey = "adxebcvn6i84k7rprcl1kq016c51vfqs";

        public static WorkfrontToken WorkfrontAuthentication()
        {
            WorkfrontToken token = new WorkfrontToken();
            System.Net.WebRequest request = System.Net.WebRequest.Create("https://thermofishermsd.preview.workfront.com/login?username=aditya.singh3@thermofisher.com&password=Password1");
            try
            {
                request.Method = "POST";
                request.ContentType = "application/json";
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                var hea = response.Headers;
                var header = response.Headers.GetValues("Set-Cookie");
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        string responseFromServer = reader.ReadToEnd();
                        token = JsonConvert.DeserializeObject<WorkfrontToken>(responseFromServer);
                        if (header.Length > 0)
                        {
                            AuthenticationCookie authenticationCookie = new AuthenticationCookie();
                            foreach (var arr in header)
                            {
                                string arrdata = arr;
                                if (arrdata.ToLower().Contains("wf-node="))
                                {
                                    authenticationCookie.wf_node = arrdata.Split(';')[0];
                                }
                                else if (arrdata.ToLower().Contains("xsrf-token="))
                                {
                                    authenticationCookie.xsrf_token = arrdata.Split(';')[0];
                                }
                                else if (arrdata.ToLower().Contains("attask="))
                                {
                                    authenticationCookie.attask = arrdata.Split(';')[0];
                                }
                                else if (arrdata.ToLower().Contains("sessionexpiration="))
                                {
                                    authenticationCookie.sessionExpiration = arrdata.Split(';')[0];
                                }
                                else if (arrdata.ToLower().Contains("webcache="))
                                {
                                    authenticationCookie.webcache = arrdata.Split(';')[0];
                                }
                            }
                            authenticationCookie.Cookie = authenticationCookie.xsrf_token + ";" + authenticationCookie.attask + ";" + authenticationCookie.sessionExpiration + ";" + authenticationCookie.webcache + ";" + authenticationCookie.wf_node;
                            token.AuthenticationCookie = authenticationCookie;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return token;
        }

        public static string DownloadDocument(string Cookie, string documentID,string downloadPath)
        {
            string path = downloadPath;
            string filename = "";
            string ResponseString = "no";
            System.Net.WebRequest request = System.Net.WebRequest.Create("https://thermofishermsd.preview.workfront.com/internal/document/download?ID=" + documentID + "");
            try
            {
                request.Method = "Get";
                request.Headers.Add("Cookie", Cookie);
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                string Content_Disposition = response.Headers["Content-Disposition"];
                ContentDisposition contentDisposition = new ContentDisposition(Content_Disposition);
                string fl = contentDisposition.FileName.ToString();
                filename = Uri.UnescapeDataString(fl);
                path = path + filename;

                WebClient webClient = new WebClient();
                {
                    webClient.DownloadFile(response.ResponseUri.AbsoluteUri, path);
                    ResponseString = filename;
                }
            }
            catch (Exception ex)
            {

            }

            return ResponseString;
        }

        public static TotalProjectsWithDocuments TotalProjectsDocumentsData(string apiKey)
        {
            TotalProjectsWithDocuments totalProjectsWithDocuments = new TotalProjectsWithDocuments();
            string ResponseString = "";
            System.Net.WebRequest request = System.Net.WebRequest.Create("https://thermofishermsd.preview.workfront.com/attask/api/v14.0/proj/search?fields=ID,name,documents&$$FIRST=5500&$$LIMIT=500");
            try
            {
                request.Method = "GET";
                request.Headers.Add("apiKey", apiKey);

                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        ResponseString = reader.ReadToEnd();
                        totalProjectsWithDocuments = JsonConvert.DeserializeObject<TotalProjectsWithDocuments>(ResponseString);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return totalProjectsWithDocuments;
        }

        public static DocumentFolderLocation DocumentFolderLocation(string apiKey, string documentID)
        {
            DocumentFolderLocation documentFolderLocation = new DocumentFolderLocation();
            string ResponseString = "";
            System.Net.WebRequest request = System.Net.WebRequest.Create("https://thermofishermsd.preview.workfront.com/attask/api/v14.0/docu/" + documentID + "?fields=folders");
            try
            {
                request.Method = "GET";
                request.Headers.Add("apiKey", apiKey);

                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        ResponseString = reader.ReadToEnd();
                        documentFolderLocation = JsonConvert.DeserializeObject<DocumentFolderLocation>(ResponseString);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return documentFolderLocation;
        }

        public static CustomFormsAndDocumentsOutput DocumentsWithCustomFormsLinked(string apiKey)
        {
            CustomFormsAndDocumentsOutput customFormsAndDocumentsOutput = new CustomFormsAndDocumentsOutput();
            string ResponseString = "";
            string fields = "DE:MSD Metadata Complete - Ready for DAM import," +
                "DE:MSD Event Title," +
                "DE:MSD Metadata Asset Type," +
                "DE:MSD Metadata Asset Sub-Type | Photography," +
                "DE:MSD Metadata Asset Sub-Type | Illustrations/Diagrams," +
                "DE:MSD Metadata Asset Sub-Type | Application Images," +
                "DE:MSD Metadata Asset Sub-Type | Branding," +
                "DE:MSD Metadata Asset Sub-Type | Documents," +
                "DE:MSD Metadata Asset Sub-Type | Advertisements," +
                "DE:MSD Metadata Asset Sub-Type | Audio," +
                "DE:MSD Metadata Asset Sub-Type | Presentations," +
                "DE:MSD Metadata Asset Sub-Type | Sales Enablement," +
                "DE:MSD Metadata Asset Sub-Type | Tradeshow/Event Assets," +
                "DE:MSD Metadata Asset Sub-Type | Videos," +
                "DE:MSD Metadata Channel," +
                "DE:MSD Metadata Brand," +
                "DE:MSD Metadata Business Unit," +
                "DE:MSD Metadata Sub Business Unit | Spec," +
                "DE:MSD Metadata Sub Business Unit | LS," +
                "DE:MSD Metadata Technology," +
                "DE:MSD Metadata Product Name | Core UV," +
                "DE:MSD Metadata Product Name | FTIR," +
                "DE:MSD Metadata Product Name | NIR," +
                "DE:MSD Metadata Product Name - Raman," +
                "DE:MSD Metadata Product Name | NanoDrop," +
                "DE:MSD Metadata Product Name | MC-Lab," +
                "DE:MSD Metadata Product Name | MC-Process," +
                "DE:MSD Metadata Product Name | NMR," +
                "DE:MSD Metadata Product Name | MC-Pharma," +
                "DE:MSD Metadata Product Name | OES," +
                "DE:MSD Metadata Product Name | XRD," +
                "DE:MSD Metadata Product Name | XRF," +
                "DE:MSD Metadata Product Name | EDS," +
                "DE:MSD Metadata Product Name | WDS," +
                "DE:MSD Metadata Product Name | EBSD," +
                "DE:MSD Metadata Product Name | Electrical Fault Analysis EFA," +
                "DE:MSD Metadata Product Name | Large DualBeam LDB," +
                "DE:MSD Metadata Product Name | Small DualBeam SDB," +
                "DE:MSD Metadata Product Name | Scanning Electron Microscope SEM," +
                "DE:MSD Metadata Product Name | Transmission Electron Microscopy TEM," +
                "DE:MSD Metadata Product Name | Surface Analysis XPS," +
                "DE:MSD Metadata Product Name | microCT XRS," +
                "DE:MSD Metadata Product Name | Software SWA," +
                "DE:MSD Metadata Market," +
                "DE:MSD Metadata Application," +
                "DE:MSD Metadata Sample/Material Type," +
                "DE:MSD Metadata Language," +
                "DE:MSD Metadata Year Created/Updated," +
                "DE:MSD Metadata Usage Rights," +
                "DE:MSD Metadata Complete - Ready for DAM import," +
                "DE:MSD Metadata Product Status," +
                "DE:MSD Metadata WF Tactic Reference Number," +
                "DE:MSD Metadata Part Number,";
            System.Net.WebRequest request = System.Net.WebRequest.Create("https://thermofishermsd.preview.workfront.com/attask/api/v14.0/docu/search?fields=" + fields + "category:*&category:name=MSD Document Metadata&DE:MSD Metadata Complete - Ready for DAM import=Yes");
            try
            {
                request.Method = "GET";
                request.Headers.Add("apiKey", apiKey);

                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        ResponseString = reader.ReadToEnd();
                        customFormsAndDocumentsOutput = JsonConvert.DeserializeObject<CustomFormsAndDocumentsOutput>(ResponseString);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return customFormsAndDocumentsOutput;
        }
    }
}