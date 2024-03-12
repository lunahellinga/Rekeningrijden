using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using IO.Swagger.DTOS;
using IO.Swagger.Models;
using LTS.DTOs;
using MassTransit;
using Newtonsoft.Json;

namespace IO.Swagger.Services;

/// <summary>
/// 
/// </summary>
public class RoutingService : IRoutingService
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;
    private static readonly HttpClient client = new HttpClient();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="endpoint"></param>
    public RoutingService(IPublishEndpoint endpoint, IMapper mapper) 
	{
		_publishEndpoint = endpoint;		
        _mapper = mapper;
	}
	
	/// <summary>
	/// test
	/// </summary>
	/// <param name="cc"></param>
	/// <param name="route"></param>
	/// <returns></returns>
	public async Task Routing(string cc, RawRoute route)
	{
		IRawRoute dto = null;
		switch (cc.ToLower())
		{
			case "nl":
                dto = new NLRawDTO();
                dto.Points = route.Points;
                dto.Vehicle = route.Vehicle;
                await _publishEndpoint.Publish<NLRawDTO>(dto);
                Console.WriteLine("Send to NL QUEUE");
                break;
			case "lu":
                dto = new LURawDTO();
                dto.Points = route.Points;
                dto.Vehicle = route.Vehicle;
                await _publishEndpoint.Publish<LURawDTO>(dto);
                Console.WriteLine("Send to LU QUEUE");
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cc"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
	public async Task Processed(string cc, IRoute dto)
	{
        var Url = "";

        using StringContent jsonContent = new(
        System.Text.Json.JsonSerializer.Serialize(dto),
        Encoding.UTF8,
        "application/json");

        var log = JsonConvert.SerializeObject(dto, Formatting.Indented);

        Console.WriteLine("Return object:");
        Console.WriteLine(log);

        switch (cc)
        {
            case ("LU"):
                Url = "http://34.159.70.126/api/return-processed?cc=BE";
                break;
            case ("NL"):
                Url = "http://34.140.232.108/api/invoice/return-processed?cc=BE";
                break;
        }

        Console.WriteLine("Target URL: " + Url);

        try
        {
            var response = await client.PostAsync(Url, jsonContent);
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response from call: " + responseString);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }      
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="route"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task ReturnProcessed(Route route)
    {
        Console.WriteLine("before publish to queue: " + route);
       await _publishEndpoint.Publish<RouteDTO>(ConvertToDTO(route));
    }

    private RouteDTO ConvertToDTO(Route route)
    {
        var dto = new RouteDTO();
        var segments = new List<SegmentDTO>();

        dto.Id = route.Id.Value;
        dto.PriceTotal = route.PriceTotal.Value;

        foreach (var seg in route.Segments)
        {
            var segment = new SegmentDTO();

            segment.Time = seg.Time;
            segment.Price = seg.Price.Value;
            segment.Start = _mapper.Map<NodeDTO>(seg.Start);
            segment.Way = new WayDTO();
            segment.End = _mapper.Map<NodeDTO>(seg.End);
            segments.Add(segment);
        }

        dto.Segments = segments;
        return dto;
    }



}
