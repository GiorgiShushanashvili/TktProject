using System.Text;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace TktProject.Middleware;
public class ValidationMiddleware
{
    private static bool _firstTime =true;
    private static Dictionary<string,Type> validatorAssembly=new();
    private readonly RequestDelegate _next;
    public ValidationMiddleware(RequestDelegate next)
    {
        _next=next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        LoadAssembly();

    }

    private async Task<string> ValidateRequest(HttpContext httpContext)
    {
        var path = httpContext.Request.Path.ToString().Trim('/').Split('/');
        var actionName=path[^1];

        if(validatorAssembly.ContainsKey(actionName))
        {
            var validatorType=validatorAssembly[actionName];
            var validatorInstance=Activator.CreateInstance(validatorType);

            var validateMethod=validatorInstance.GetType().GetMethod("Validate");
            JObject jsonObject =await GetRequestBodyAsync(httpContext.Request);

            var properties=validatorInstance.GetType().GetProperties(BindingFlags.NonPublic|BindingFlags.Instance);
            var constructor=properties.First().PropertyType.GetConstructors().First();
            var objList=new List<object>();
            var parameters = constructor.GetParameters();
            var parameterPropertyMapping=new Dictionary<string,string>();
            
            
        }
    }

    private async Task<JObject> GetRequestBodyAsync(HttpRequest request)
    {
        JObject? objRequestBody =new();
        HttpRequestRewindExtensions.EnableBuffering(request);
        using(StreamReader reader =new StreamReader(
            request.Body,
            Encoding.UTF32,
            detectEncodingFromByteOrderMarks:false,
            leaveOpen:true
        ))
        {
            string strRequestBody=await reader.ReadToEndAsync();
            objRequestBody= JsonConvert.DeserializeObject<JObject>(strRequestBody);
            request.Body.Position=0;
        }
        return objRequestBody;
    }


    private void LoadAssembly()
    {
        if(_firstTime)
        {
            validatorAssembly=new Dictionary<string, Type>()
            {
                {" ",typeof(Type)},
            };
            _firstTime=false;
        }
    }
}
