namespace Adaca.Api.DTO
{
    public class InsertClientDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessNumber { get; set; }
        public int LoanAmount { get; set; }
        public string CitizenshipStatus { get; set; }
        public int TimeTrading { get; set; }
        public string CountryCode { get; set; }
        public string Industry { get; set; }
    }
}
