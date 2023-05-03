using System;

namespace Second.DTOs;

public class SeasonDto
{
    public int SeasonId { get; set; }
    public int TourId { get; set; }
    public int SeatAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Closed { get; set; }
}