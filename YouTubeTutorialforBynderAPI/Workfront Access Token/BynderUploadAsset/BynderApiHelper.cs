using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Workfront_Access_Token.BynderUploadAsset
{
    public class BynderApiHelper
    {
        
        public static string ApplicationName = "VisualStudioOAuthApp";

        public static string ClientId = "dacfd354-4cac-4351-b855-16ebc4050fbe";

        public static string ClientSecret = "f2aad51c-cb07-4ebe-9850-f710a264b13e";

        public static string RedirectUri = "https://localhost:44318/Home/OauthCallback";

        public static string TokenUri = "https://damconsultantsdemo.getbynder.com/v6/authentication/oauth2/token";

        public static string OauthUri = "https://damconsultantsdemo.getbynder.com/v6/authentication/oauth2/auth?";

        public static string Scopes = "offline asset:write";


        public static BynderTokenCC GetTokenByClientCredentials()
        {
            BynderTokenCC token = new BynderTokenCC();

            System.Net.WebRequest request = System.Net.WebRequest.Create("https://damconsultantsdemo.getbynder.com/v6/authentication/oauth2/token/");
            try
            {
                request.Method = "POST";
                var postData = "grant_type=client_credentials";
                postData += "&client_id=" + ClientId;
                postData += "&client_secret=" + ClientSecret;
                postData += "&scope=offline asset:write";

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                System.Net.WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                token = JsonConvert.DeserializeObject<BynderTokenCC>(responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {

            }

            return token;

        }

        public static string AWSUploadEndpoint()
        {
            string ResponseString = "";
            System.Net.WebRequest request = System.Net.WebRequest.Create("https://damconsultantsdemo.getbynder.com/api/upload/endpoint/");
            try
            {
                request.Method = "Get";
                request.ContentType = "application/json";
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        ResponseString = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return ResponseString;
        }

        public static BynderInitializeUpload InitialiseUpload(string access_token)
        {
            BynderInitializeUpload bynderInitializeUpload = new BynderInitializeUpload();
            System.Net.WebRequest request = System.Net.WebRequest.Create("https://damconsultantsdemo.getbynder.com/api/upload/init/");
            try
            {
                request.Method = "POST";
                request.Headers.Add("Bearer", access_token);
                var postData = "filename=testimage.jpg";

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                //request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                System.Net.WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                bynderInitializeUpload = JsonConvert.DeserializeObject<BynderInitializeUpload>(responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {

            }

            return bynderInitializeUpload;

        }

        public static string UploadingChunk1(string Content_Type, string Policy, string X_Amz_Signature, string ACL, string key, string success_action_status, string X_Amz_algorithm, string X_Amz_credential, string X_Amz_date, string name, int chunk, int chunks, string Filename, string file)
        {

            string UploadedChunkKey = "";
            try
            {
                using (var client = new HttpClient())
                {
                    using (var multipartFormDataContent = new MultipartFormDataContent())
                    {
                        var values = new[]
                        {
                            new KeyValuePair<string, string>("Content-Type", Content_Type),
                            new KeyValuePair<string, string>("Policy", Policy),
                            new KeyValuePair<string, string>("X-Amz-Signature", X_Amz_Signature),
                            new KeyValuePair<string, string>("acl", ACL),
                            new KeyValuePair<string, string>("key", key),
                            new KeyValuePair<string, string>("success_action_status", success_action_status),
                            new KeyValuePair<string, string>("X-Amz-algorithm", X_Amz_algorithm),
                            new KeyValuePair<string, string>("X-Amz-credential", X_Amz_credential),
                            new KeyValuePair<string, string>("X-Amz-date", X_Amz_date),
                            new KeyValuePair<string, string>("name", name),
                            new KeyValuePair<string, string>("chunk","1"),
                            new KeyValuePair<string, string>("chunks", "1"),
                            new KeyValuePair<string, string>("Filename", Filename)
                        };

                        foreach (var keyValuePair in values)
                        {
                            multipartFormDataContent.Add(new StringContent(keyValuePair.Value),
                                String.Format("\"{0}\"", keyValuePair.Key));
                        }

                        multipartFormDataContent.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(file)),
                            '"' + "File" + '"',
                            '"' + "test.txt" + '"');

                        var requestUri = "https://bynder-public-ap-northeast-1.s3.amazonaws.com";
                        var result = client.PostAsync(requestUri, multipartFormDataContent).Result.Headers.Location.LocalPath.ToString();
                        UploadedChunkKey = result.Trim('/');
                    }
                }
            }
            catch (Exception ex)
            {

            }



            return UploadedChunkKey;

        }


        public static string RegisterUploadedChunk(string access_token, string uploadid, string targetid, string UploadedChunkKey)
        {
            string status = "";
            System.Net.WebRequest request = System.Net.WebRequest.Create("https://damconsultantsdemo.getbynder.com/api/v4/upload/" + uploadid);
            try
            {
                request.Method = "POST";
                request.Headers.Add("Authorization", "Bearer " + access_token);
                var postData = "chunkNumber=1";
                postData += "&targetid=" + targetid;
                postData += "&Filename=" + UploadedChunkKey;


                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                System.Net.WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                UploadedChunkRegistered uploadedChunkRegistered = JsonConvert.DeserializeObject<UploadedChunkRegistered>(responseFromServer);
                status = uploadedChunkRegistered.status;
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {

            }

            return status;

        }

        public static FinaliseUploadedFile FinaliseCompletelyUploadedFile(string access_token, string uploadid, string targetid, string s3_filename, string original_filename)
        {
            FinaliseUploadedFile finaliseUploadedFile = new FinaliseUploadedFile();
            System.Net.WebRequest request = System.Net.WebRequest.Create("https://damconsultantsdemo.getbynder.com/api/v4/upload/" + uploadid);
            try
            {
                request.Method = "POST";
                request.Headers.Add("Authorization", "Bearer " + access_token);
                var postData = "targetid=" + targetid;
                postData += "&s3_filename=" + s3_filename;
                postData += "&chunks=1";
                postData += "&original_filename=" + original_filename;

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                System.Net.WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                finaliseUploadedFile = JsonConvert.DeserializeObject<FinaliseUploadedFile>(responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {

            }

            return finaliseUploadedFile;

        }

        public static pollProcessingStateOfFinalisedFiles PollProcessingState(string access_token, string import_id)
        {
            pollProcessingStateOfFinalisedFiles pollProcessingStateOfFinalisedFiles = new pollProcessingStateOfFinalisedFiles();
            string ResponseString = "";
            System.Net.WebRequest request = System.Net.WebRequest.Create("https://damconsultantsdemo.getbynder.com/api/v4/upload/poll/?items=" + import_id);
            try
            {
                request.Method = "Get";
                request.Headers.Add("Authorization", "Bearer " + access_token);
                //request.ContentType = "application/json";
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        ResponseString = reader.ReadToEnd();
                        pollProcessingStateOfFinalisedFiles = JsonConvert.DeserializeObject<pollProcessingStateOfFinalisedFiles>(ResponseString);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return pollProcessingStateOfFinalisedFiles;
        }

        public static savingCompletedAsset SavingACompletedAsset(string access_token, string importId, string name)
        {
            savingCompletedAsset savingCompletedAsset = new savingCompletedAsset();
            System.Net.WebRequest request = System.Net.WebRequest.Create("https://damconsultantsdemo.getbynder.com/api/v4/media/save/" + importId);
            try
            {
                request.Method = "POST";
                request.Headers.Add("Authorization", "Bearer " + access_token);
                var postData = "brandId=1C6527E7-8871-498B-84584F65771C3014";
                postData += "&name=" + name;
                postData += "&description=This is a Test Image";
                postData += "&tags=testimage,postmanApp,bynderDAM, workfrontCRM";


                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                System.Net.WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                savingCompletedAsset = JsonConvert.DeserializeObject<savingCompletedAsset>(responseFromServer);
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {

            }

            return savingCompletedAsset;

        }
    }
}

