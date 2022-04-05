using PhoneGuide.Shared.Enums;

namespace PhoneGuide.Shared.Dtos
{
    public class ReportDto:IDto
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string RequestedDate { get; set; }
        public string CreatedDate { get; set; }
        public ReportState ReportState { get; set; }
    }
}
