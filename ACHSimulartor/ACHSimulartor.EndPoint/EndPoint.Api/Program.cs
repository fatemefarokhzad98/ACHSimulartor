using EndPoint.Api.Extentions;
using Microsoft.Extensions.Logging;

try
{
    WebApplication.CreateBuilder(args)
        .ConfigServices()
        .ConfigPipLines();
}
catch (Exception?)
{
   //nloger
    throw;
}

