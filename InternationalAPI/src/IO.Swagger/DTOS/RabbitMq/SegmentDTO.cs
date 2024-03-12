using System;

namespace LTS.DTOs;

public class SegmentDTO
{
    public string Time { get; set; }

    public decimal Price { get; set; }

    public NodeDTO Start { get; set; } = new NodeDTO();

    public WayDTO Way { get; set; } = new WayDTO();

    public NodeDTO End { get; set; } = new NodeDTO();

}