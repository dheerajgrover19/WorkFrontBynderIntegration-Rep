namespace Workfront_Access_Token
{
    public class CustomFormsAndDocumentsOutput
    {
        public Output[] data { get; set; }
    }

    public class Output
    {
        public string ID { get; set; }
        public string name { get; set; }
        public string objCode { get; set; }
        public Category category { get; set; }
        public string DEMSDMetadataCompleteReadyforDAMimport { get; set; }
        public object DEMSDEventTitle { get; set; }
        public string DEMSDMetadataAssetType { get; set; }
        public string DEMSDMetadataAssetSubTypePhotography { get; set; }
        public object DEMSDMetadataAssetSubTypeIllustrationsDiagrams { get; set; }
        public object DEMSDMetadataAssetSubTypeApplicationImages { get; set; }
        public object DEMSDMetadataAssetSubTypeBranding { get; set; }
        public object DEMSDMetadataAssetSubTypeDocuments { get; set; }
        public object DEMSDMetadataAssetSubTypeAdvertisements { get; set; }
        public object DEMSDMetadataAssetSubTypeAudio { get; set; }
        public object DEMSDMetadataAssetSubTypePresentations { get; set; }
        public object DEMSDMetadataAssetSubTypeSalesEnablement { get; set; }
        public object DEMSDMetadataAssetSubTypeTradeshowEventAssets { get; set; }
        public object DEMSDMetadataAssetSubTypeVideos { get; set; }
        public object DEMSDMetadataChannel { get; set; }
        public object DEMSDMetadataBrand { get; set; }
        public string DEMSDMetadataBusinessUnit { get; set; }
        public object DEMSDMetadataSubBusinessUnitSpec { get; set; }
        public object DEMSDMetadataSubBusinessUnitLS { get; set; }
        public string DEMSDMetadataTechnology { get; set; }
        public object DEMSDMetadataProductNameCoreUV { get; set; }
        public object DEMSDMetadataProductNameFTIR { get; set; }
        public object DEMSDMetadataProductNameNIR { get; set; }
        public object DEMSDMetadataProductNameRaman { get; set; }
        public object DEMSDMetadataProductNameNanoDrop { get; set; }
        public object DEMSDMetadataProductNameMCLab { get; set; }
        public object DEMSDMetadataProductNameMCProcess { get; set; }
        public object DEMSDMetadataProductNameNMR { get; set; }
        public object DEMSDMetadataProductNameMCPharma { get; set; }
        public object DEMSDMetadataProductNameOES { get; set; }
        public object DEMSDMetadataProductNameXRD { get; set; }
        public object DEMSDMetadataProductNameXRF { get; set; }
        public object DEMSDMetadataProductNameEDS { get; set; }
        public object DEMSDMetadataProductNameWDS { get; set; }
        public object DEMSDMetadataProductNameEBSD { get; set; }
        public object DEMSDMetadataProductNameElectricalFaultAnalysisEFA { get; set; }
        public object DEMSDMetadataProductNameLargeDualBeamLDB { get; set; }
        public object DEMSDMetadataProductNameSmallDualBeamSDB { get; set; }
        public object DEMSDMetadataProductNameScanningElectronMicroscopeSEM { get; set; }
        public object DEMSDMetadataProductNameTransmissionElectronMicroscopyTEM { get; set; }
        public object DEMSDMetadataProductNameSurfaceAnalysisXPS { get; set; }
        public object DEMSDMetadataProductNamemicroCTXRS { get; set; }
        public object DEMSDMetadataProductNameSoftwareSWA { get; set; }
        public object DEMSDMetadataMarket { get; set; }
        public object DEMSDMetadataApplication { get; set; }
        public object DEMSDMetadataSampleMaterialType { get; set; }
        public object DEMSDMetadataLanguage { get; set; }
        public string DEMSDMetadataYearCreatedUpdated { get; set; }
        public string DEMSDMetadataUsageRights { get; set; }
        public object DEMSDMetadataProductStatus { get; set; }
        public string DEMSDMetadataWFTacticReferenceNumber { get; set; }
        public object DEMSDMetadataPartNumber { get; set; }
    }

    public class Category
    {
        public string ID { get; set; }
        public string name { get; set; }
        public string objCode { get; set; }
        public string catObjCode { get; set; }
        public string customerID { get; set; }
        public string description { get; set; }
        public string enteredByID { get; set; }
        public object extRefID { get; set; }
        public string groupID { get; set; }
        public bool hasCalculatedFields { get; set; }
        public bool isActive { get; set; }
        public string lastUpdateDate { get; set; }
        public string lastUpdatedByID { get; set; }
    }
}
