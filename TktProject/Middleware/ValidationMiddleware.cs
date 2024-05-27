using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;
using Newtonsoft.Json.Serialization;

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
        var validationResult=await ValidateRequest(httpContext);
        if(string.IsNullOrEmpty(validationResult))
        {
            httpContext.Response.StatusCode=StatusCodes.Status400BadRequest;
            httpContext.Request.ContentType="application/json";
            await httpContext.Response.WriteAsync(validationResult);
            return;
        }
        await _next(httpContext);
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
            
            foreach(var parameter in parameters)
            {
                parameterPropertyMapping[parameter.Name.ToLower()]=parameter.Name.ToLower();
            }
            foreach(var property in jsonObject.Properties())
            {
                if(parameterPropertyMapping.ContainsKey(property.Name))
                {
                    var parameterName=parameterPropertyMapping[property.Name.ToLower()];
                    var parameterType=parameters.FirstOrDefault(x=>x.Name.ToLower()==parameterName)?.ParameterType;
                    if(parameterType!=null)
                    {
                        var propertyValue=Convert.ChangeType(property.Value,parameterType);
                        objList.Add(propertyValue);
                    }   
                }else
                {
                    Console.WriteLine($"Warning: Not Found Matching Constructor Paramater{property.Name}");
                }
            }
            var constructorArgs=objList.ToArray();
            var validatorObject=Activator.CreateInstance(properties.First().PropertyType,constructorArgs);

            var validateResult=validateMethod.Invoke(validatorInstance,new[] {validatorObject}) as List<ValidationFailure>;
            var errorMessages = validateResult.Select(x=>x.ErrorMessage).ToList();
            if(validateResult !=null&&validateResult.Any())
            {
                var jsonSettings=new JsonSerializerSettings
                {
                    ContractResolver=new CamelCasePropertyNamesContractResolver()
                };
                var json =JsonConvert.SerializeObject(errorMessages,jsonSettings);
                return json;
            }
        }
        return null;
    }

    private async Task<JObject> GetRequestBodyAsync(HttpRequest request)
    {
        JObject? objRequestBody =new();
        HttpRequestRewindExtensions.EnableBuffering(request);
        using(StreamReader reader =new StreamReader(
            request.Body,
            Encoding.UTF8,
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
