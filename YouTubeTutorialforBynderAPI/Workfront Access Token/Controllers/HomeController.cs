using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Workfront_Access_Token.Models;
using Workfront_Access_Token.Common;
using Microsoft.AspNetCore.Http;
using Workfront_Access_Token.BynderUploadAsset;

namespace Workfront_Access_Token.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        //[HttpPost]
        //public string GetAuthentication()
        //{
        //    string message = "The Access Token for the request is:";
        //    BynderTokenCC bynderTokenCC = new BynderTokenCC();
        //    bynderTokenCC = BynderApiHelper.GetTokenByClientCredentials();
        //    string access_token = bynderTokenCC.access_token;
        //    if (!string.IsNullOrEmpty(access_token))
        //    {
        //        string ClosestAmazonS3UploadEndpoint = BynderApiHelper.AWSUploadEndpoint();
        //        BynderInitializeUpload bynderInitializeUpload = BynderApiHelper.InitialiseUpload(access_token);
        //        if (!string.IsNullOrEmpty(ClosestAmazonS3UploadEndpoint))
        //        {
        //            string Content_Type = bynderInitializeUpload.multipart_params.Content_Type;
        //            string Policy = bynderInitializeUpload.multipart_params.Policy;
        //            string X_Amz_Signature = bynderInitializeUpload.multipart_params.X_Amz_Signature;
        //            string acl = bynderInitializeUpload.multipart_params.acl;
        //            string key = bynderInitializeUpload.multipart_params.key;
        //            string success_action_status = bynderInitializeUpload.multipart_params.success_action_status;
        //            string X_Amz_algorithm = bynderInitializeUpload.multipart_params.x_amz_algorithm;
        //            string X_Amz_credential = bynderInitializeUpload.multipart_params.x_amz_credential;
        //            string X_Amz_date = bynderInitializeUpload.multipart_params.x_amz_date;
        //            string name = "testimage.jpg";
        //            int chunk = 1;
        //            int chunks = 1;
        //            string Filename = bynderInitializeUpload.s3_filename;
        //            string file = @"C:\Users\dheer\source\repos\YouTubeTutorialforBynderAPI\YouTubeTutorialforBynderAPI\UploadAsset\testimage.jpg";
        //            string uploadid = bynderInitializeUpload.s3file.uploadid;
        //            string targetid = bynderInitializeUpload.s3file.targetid;
        //            string UploadedChunkKey = BynderApiHelper.UploadingChunk1(Content_Type, Policy, X_Amz_Signature, acl, key, success_action_status, X_Amz_algorithm, X_Amz_credential, X_Amz_date, name, chunk, chunks, Filename, file);
        //            if (!string.IsNullOrEmpty(uploadid) && !string.IsNullOrEmpty(targetid) && !string.IsNullOrEmpty(UploadedChunkKey))
        //            {
        //                string status = BynderApiHelper.RegisterUploadedChunk(access_token, uploadid, targetid, UploadedChunkKey);
        //                if (!string.IsNullOrEmpty(status) && status.Equals("ok"))
        //                {
        //                    FinaliseUploadedFile finaliseUploadedFile = new FinaliseUploadedFile();
        //                    finaliseUploadedFile = BynderApiHelper.FinaliseCompletelyUploadedFile(access_token, uploadid, targetid, Filename, name);

        //                    string importId = finaliseUploadedFile.importId;
        //                    if (!string.IsNullOrEmpty(importId))
        //                    {
        //                        pollProcessingStateOfFinalisedFiles pollProcessingStateOfFinalisedFiles = new pollProcessingStateOfFinalisedFiles();
        //                        pollProcessingStateOfFinalisedFiles = BynderApiHelper.PollProcessingState(access_token, importId);
        //                    }
        //                    if (!string.IsNullOrEmpty(importId))
        //                    {
        //                        savingCompletedAsset savingCompletedAsset = new savingCompletedAsset();
        //                        savingCompletedAsset = BynderApiHelper.SavingACompletedAsset(access_token, importId, name);
        //                    }

        //                }

        //            }
        //        }
        //    }
        //    else
        //    {
        //        message = "Access Token does not Exist";
        //    }

        //    return message + " " + access_token;
        //}



        public string CustomFormDocuments()
        {
            string downloadPath = @"D:\Tech Compiler\Files\";
            string message = "Something went wrong or Document linked with the Custom Form does not exist";
            CustomFormsAndDocumentsOutput customFormsAndDocumentsOutput = new CustomFormsAndDocumentsOutput();
            string apiKey = WorkfrontApiHelper.apiKey;
            customFormsAndDocumentsOutput = WorkfrontApiHelper.DocumentsWithCustomFormsLinked(apiKey);
            if (customFormsAndDocumentsOutput.data != null)
            {
                Output[] data = customFormsAndDocumentsOutput.data;
                foreach (var dataarr in data)
                {
                    string documentID = dataarr.ID;
                    WorkfrontToken workfrontToken = new WorkfrontToken();
                    workfrontToken = WorkfrontApiHelper.WorkfrontAuthentication();
                    string success = workfrontToken.data.success;
                    string Cookie = workfrontToken.AuthenticationCookie.Cookie;
                    if (!string.IsNullOrEmpty(success) && success.Equals("true"))
                    {
                        string workfrontDocumentname = WorkfrontApiHelper.DownloadDocument(Cookie, documentID, downloadPath);
                        if (!string.IsNullOrEmpty(workfrontDocumentname))
                            message = "downloaded successfully.";




                        //string message = "The Access Token for the request is:";
                        BynderTokenCC bynderTokenCC = new BynderTokenCC();
                        bynderTokenCC = BynderApiHelper.GetTokenByClientCredentials();
                        string access_token = bynderTokenCC.access_token;
                        if (!string.IsNullOrEmpty(access_token))
                        {
                            string ClosestAmazonS3UploadEndpoint = BynderApiHelper.AWSUploadEndpoint();
                            BynderInitializeUpload bynderInitializeUpload = BynderApiHelper.InitialiseUpload(access_token);
                            if (!string.IsNullOrEmpty(ClosestAmazonS3UploadEndpoint))
                            {
                                string Content_Type = bynderInitializeUpload.multipart_params.Content_Type;
                                string Policy = bynderInitializeUpload.multipart_params.Policy;
                                string X_Amz_Signature = bynderInitializeUpload.multipart_params.X_Amz_Signature;
                                string acl = bynderInitializeUpload.multipart_params.acl;
                                string key = bynderInitializeUpload.multipart_params.key;
                                string success_action_status = bynderInitializeUpload.multipart_params.success_action_status;
                                string X_Amz_algorithm = bynderInitializeUpload.multipart_params.x_amz_algorithm;
                                string X_Amz_credential = bynderInitializeUpload.multipart_params.x_amz_credential;
                                string X_Amz_date = bynderInitializeUpload.multipart_params.x_amz_date;
                                string name = "testimage.jpg";
                                int chunk = 1;
                                int chunks = 1;
                                string Filename = bynderInitializeUpload.s3_filename;
                                string file = @"C:\Users\dheer\source\repos\YouTubeTutorialforBynderAPI\YouTubeTutorialforBynderAPI\UploadAsset\testimage.jpg";
                                string uploadid = bynderInitializeUpload.s3file.uploadid;
                                string targetid = bynderInitializeUpload.s3file.targetid;
                                string UploadedChunkKey = BynderApiHelper.UploadingChunk1(Content_Type, Policy, X_Amz_Signature, acl, key, success_action_status, X_Amz_algorithm, X_Amz_credential, X_Amz_date, name, chunk, chunks, Filename, file);
                                if (!string.IsNullOrEmpty(uploadid) && !string.IsNullOrEmpty(targetid) && !string.IsNullOrEmpty(UploadedChunkKey))
                                {
                                    string status = BynderApiHelper.RegisterUploadedChunk(access_token, uploadid, targetid, UploadedChunkKey);
                                    if (!string.IsNullOrEmpty(status) && status.Equals("ok"))
                                    {
                                        FinaliseUploadedFile finaliseUploadedFile = new FinaliseUploadedFile();
                                        finaliseUploadedFile = BynderApiHelper.FinaliseCompletelyUploadedFile(access_token, uploadid, targetid, Filename, name);

                                        string importId = finaliseUploadedFile.importId;
                                        if (!string.IsNullOrEmpty(importId))
                                        {
                                            pollProcessingStateOfFinalisedFiles pollProcessingStateOfFinalisedFiles = new pollProcessingStateOfFinalisedFiles();
                                            pollProcessingStateOfFinalisedFiles = BynderApiHelper.PollProcessingState(access_token, importId);
                                        }
                                        if (!string.IsNullOrEmpty(importId))
                                        {
                                            savingCompletedAsset savingCompletedAsset = new savingCompletedAsset();
                                            savingCompletedAsset = BynderApiHelper.SavingACompletedAsset(access_token, importId, name);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return message;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
