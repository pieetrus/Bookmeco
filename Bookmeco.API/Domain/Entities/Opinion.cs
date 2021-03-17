using System;

namespace Domain.Entities
{
    /// <summary>
    /// Comment is simple text pinned to reservation.
    /// There is also possible to answer opinion by adding superOpinion.
    /// If the opinion is root opinion then superOpinion is null.
    /// </summary>
    public class Opinion
    {
        public int Id { get; set; }
        public Reservation Reservation { get; set; }
        public Opinion SuperOpinion { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int RateValue { get; set; }
    }
}
