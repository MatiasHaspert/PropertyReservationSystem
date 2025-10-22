namespace Backend.Application.DTOs.Property
{
    public class PropertyCalendarDTO
    {
        public int PropertyId { get; set; }
        public List<CalendarRangeDTO> AvailableRanges { get; set; } = new();
        public List<CalendarRangeDTO> ReservedRanges { get; set; } = new();
    }
}
