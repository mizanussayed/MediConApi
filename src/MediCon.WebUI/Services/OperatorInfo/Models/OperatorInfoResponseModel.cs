namespace MediCon.WebUI.Services.OperatorInfo.Models
{
    public class OperatorInfoResponseModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BinNumber { get; set; } = string.Empty;
        public string TinNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string POCNAME { get; set; } = string.Empty;
        public string POCDESIGNATION { get; set; } = string.Empty;
    }
}
