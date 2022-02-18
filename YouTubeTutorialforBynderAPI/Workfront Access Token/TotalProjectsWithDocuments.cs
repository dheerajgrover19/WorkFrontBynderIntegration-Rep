namespace Workfront_Access_Token
{
    public class TotalProjectsWithDocuments
    {
        public Datum[] data { get; set; }
    }

    public class Datum
    {
        public string ID { get; set; }
        public string name { get; set; }
        public string objCode { get; set; }
        public Document[] documents { get; set; }
    }

    public class Document
    {
        public string ID { get; set; }
        public string name { get; set; }
        public string objCode { get; set; }
    }


    public class DocumentFolderLocation
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public string ID { get; set; }
        public string name { get; set; }
        public string objCode { get; set; }
        public Folder[] folders { get; set; }
    }

    public class Folder
    {
        public string ID { get; set; }
        public string name { get; set; }
        public string objCode { get; set; }
    }

}
