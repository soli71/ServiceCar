namespace OilChangeApp.Controllers
{
    public class CustomerServiceHistoryViewModel
    {
        public long Kilometers { get; set; }
        public long NextServiceKilometers { get; set; }
        public string ServicesName { get; set; }
        public string OilName { get; set; }
        public DateTime ServiceDate { get; set; }
    }
}