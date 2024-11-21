public class ReservationDTO
{
    public int Id { get; set; }
    public int CampsiteId { get; set; }
    public int UserProfileId { get; set; }
    public DateTime CheckinDate { get; set; }
    public DateTime CheckoutDate { get; set; }

    // Calculated property for the total number of nights
    public int TotalNights => (CheckoutDate - CheckinDate).Days;

    private static readonly decimal _reservationBaseFee = 10M;

    // Calculated property for the total cost
    public decimal TotalCost
    {
        get
        {
            return Campsite.CampsiteType.FeePerNight * TotalNights + _reservationBaseFee;
        }
    }

    public UserProfileDTO UserProfile { get; set; }
    public CampsiteDTO Campsite { get; set; }
}
