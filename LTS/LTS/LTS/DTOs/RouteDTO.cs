namespace LTS.DTOs;

public class RouteDTO
{
    public Guid Id { get; set; }

    public decimal PriceTotal { get; set; }

    public List<SegmentDTO> Segments { get; set; } = new List<SegmentDTO>();
}