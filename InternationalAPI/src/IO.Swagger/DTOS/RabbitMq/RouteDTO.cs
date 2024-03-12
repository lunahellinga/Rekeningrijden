using System;
using System.Collections.Generic;

namespace LTS.DTOs;

/// <summary>
/// 
/// </summary>
public class RouteDTO
{
    public Guid Id { get; set; }

    public decimal PriceTotal { get; set; }

    public List<SegmentDTO> Segments { get; set; } = new List<SegmentDTO>();
}