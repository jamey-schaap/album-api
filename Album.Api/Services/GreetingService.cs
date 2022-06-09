using System.Net;
using System.Text.RegularExpressions;

namespace Album.Api.Services
{
  public class GreetingService
  {
    public static string Greet(string name)
    {
      string output = "Hello ";
      if (name == null || name == "" || name.Trim() == "")
        output += "World";
      else
        output += name;
      
      return $"{output} from {Dns.GetHostName()} v2";
    }
  }
}