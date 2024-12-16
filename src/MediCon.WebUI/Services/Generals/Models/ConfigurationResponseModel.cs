namespace MediCon.WebUI.Services.Generals.Models
{
    public sealed class ConfigurationResponseModel
    {
        public long Id { get; set; }
        public decimal VATPercentage { get; set; }
        //public decimal USSDRG { get; set; }
        //public decimal USSDNRG { get; set; }
        //public decimal SMSCharge { get; set; }
        public decimal SDPercentage { get; set; }
        public decimal SCPercentage { get; set; }
        public string Letternote { get; set; } = string.Empty;
        public string ROHName { get; set; } = string.Empty;
        public string ROMName { get; set; } = string.Empty;
    }
}
