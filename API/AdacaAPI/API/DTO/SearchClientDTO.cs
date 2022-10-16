namespace Adaca.Api.DTO
{
    public class SearchClientDTO
    {
        public string SearchKeyword { get; set; } //Email Address.Industry Phone Number BusinessNumber, Industry
        public OrderClientField OrderClientField { get; set; }
        public FilterClientField FilterClientField { get; set; }
    }
    public class FilterClientField
    {
        public int? FieldName { get; set; }  //  Citizen=1 , LoanAmount =2 , TimeTrading=3
        public string FilterKeyword { get; set; }
    }
    public class OrderClientField
    {
        public int? FieldName { get; set; } //  Citizen=1 , LoanAmount =2 , TimeTrading=3
        public int? OrderBy { get; set; }
    }
}
