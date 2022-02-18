using Newtonsoft.Json;

namespace Workfront_Access_Token.BynderUploadAsset
{ 
    public class BynderTokenCC
{
    public string access_token { get; set; }

    public string token_type { get; set; }

    public string expires_in { get; set; }

    public string scope { get; set; }
}

public class BynderInitializeUpload
{
    public s3file s3file { get; set; }
    public string s3_filename { get; set; }

    public string target_key { get; set; }

    public multipart_params multipart_params { get; set; }
}

public class s3file
{
    public string uploadid { get; set; }

    public string targetid { get; set; }
}

public class multipart_params
{
    public string acl { get; set; }
    public string success_action_status { get; set; }

    [JsonProperty(PropertyName = "Content-Type")]
    public string Content_Type { get; set; }

    public string key { get; set; }

    public string Policy { get; set; }

    [JsonProperty(PropertyName = "X-Amz-Signature")]
    public string X_Amz_Signature { get; set; }

    [JsonProperty(PropertyName = "x-amz-credential")]
    public string x_amz_credential { get; set; }

    [JsonProperty(PropertyName = "x-amz-algorithm")]
    public string x_amz_algorithm { get; set; }

    [JsonProperty(PropertyName = "x-amz-date")]
    public string x_amz_date { get; set; }
}

public class UploadChunk
{
    public string Location { get; set; }
    public string Bucket { get; set; }
    public string Key { get; set; }
    public string ETag { get; set; }

}

public class UploadedChunkRegistered
{
    public string status { get; set; }
}

public class FinalizingUploadedFile
{
    public string locationType { get; set; }
    public string originalFilename { get; set; }
    public string filename { get; set; }
    public string output { get; set; }
    public string batchId { get; set; }
    public string success { get; set; }
    public string importId { get; set; }
    public File File { get; set; }
    public string sendRequest { get; set; }

}

public class File
{
    public string bucket { get; set; }
    public string path { get; set; }
    public string type { get; set; }
}

public class modelaws
{
    [JsonProperty(PropertyName = "Content-Type")]
    public string Content_Type { get; set; }
    public string Policy { get; set; }
    [JsonProperty(PropertyName = "X-Amz-Signature")]
    public string X_Amz_Signature { get; set; }
    public string acl { get; set; }
    public string key { get; set; }
    [JsonProperty(PropertyName = "success_action_status")]
    public string success_action_status { get; set; }

    [JsonProperty(PropertyName = "X-Amz-algorithm")]
    public string X_Amz_algorithm { get; set; }

    [JsonProperty(PropertyName = "X-Amz-credential")]
    public string X_Amz_credential { get; set; }

    [JsonProperty(PropertyName = "X-Amz-date")]
    public string X_Amz_date { get; set; }
    public string name { get; set; }
    public int chunk { get; set; }
    public int chunks { get; set; }
    public string Filename { get; set; }
    public string file { get; set; }



}

public class FinaliseUploadedFile
{
    public metadata metadata { get; set; }
    public data data { get; set; }
    public string locationType { get; set; }
    public string originalFilename { get; set; }
    public string filename { get; set; }
    public string output { get; set; }
    public string batchId { get; set; }
    public string success { get; set; }
    public string importId { get; set; }
    public file file { get; set; }
    public string type { get; set; }
    public string sendRequest { get; set; }
    public string extension { get; set; }
    public string saved { get; set; }
}

public class metadata
{

}

public class data
{
    public string thumbpath { get; set; }
    public string placeholder { get; set; }
    public string orientation { get; set; }
}

public class file
{
    public string bucket { get; set; }
    public string path { get; set; }
    public string type { get; set; }
}

public class pollProcessingStateOfFinalisedFiles
{
    public string[] itemsFailed { get; set; }
    public string[] itemsRejected { get; set; }
    public string[] itemsDone { get; set; }
}

public class savingCompletedAsset
{
    public string accessRequestId { get; set; }
    public string mediaid { get; set; }
    public string batchId { get; set; }
    public string success { get; set; }

    public mediaitems[] mediaitems { get; set; }
}

public class mediaitems
{
    public string original { get; set; }
    public string destination { get; set; }
}
}
