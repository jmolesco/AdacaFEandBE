namespace Adaca.Api.Models
{
    public class MobileSpeedCameraResponse
    {
        public string Date { get; set; }
        public string Timeatsiteinhours { get; set; }
        public string Description_of_site { get; set; }
        public string Camera_Location { get; set; }  
        public string Street { get; set; }
        public int Number_Checked { get; set; }
        public int Highest_Speed { get; set; }
        public int Average_Speed { get; set; }
        public int Posted_Speed { get; set; }
    }
}
