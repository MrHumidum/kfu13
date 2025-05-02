public class Tariff
{
    public int Id { get; set; }
    public string TariffName { get; set; }
    public decimal MonthlyFee { get; set; }
    public int MinutesMobile { get; set; }
    public int MinutesLandline { get; set; }
    public int InternetDaytime { get; set; }
    public int InternetNighttime { get; set; }
    public string ExtraServices { get; set; }
}
