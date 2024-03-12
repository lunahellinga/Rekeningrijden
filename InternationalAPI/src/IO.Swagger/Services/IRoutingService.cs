using IO.Swagger.Models;
using System;
using System.Threading.Tasks;

public interface IRoutingService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cc"></param>
    /// <param name="route"></param>
    /// <returns></returns>
    Task Routing(string cc, RawRoute route);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cc"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task Processed(string cc, IRoute dto);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="route"></param>
    /// <returns></returns>
    Task ReturnProcessed(Route route);




}
