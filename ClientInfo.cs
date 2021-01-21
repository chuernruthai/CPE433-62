using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
  class ClientInfo : IPlugin
  {
    protected static Dictionary<String, int> statDictionary = null;
    public ClientInfo()
    {
      if (statDictionary == null)
      {
        statDictionary = new Dictionary<String, int>();

      }
    }

    public void PreProcessing(HTTPRequest request)
    {
      if (statDictionary.ContainsKey(request.Url))
      {
        statDictionary[request.Url] = (int)statDictionary[request.Url] + 1;
      }
      else
      {
        statDictionary[request.Url] = 1;
      }
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
       String[] lines = request.getPropertyByKey("remoteendpoint").Split( ":");
      
  
      sb.Append("Client IP : " + lines[0] + "<br>");
      sb.Append("Client Port : " + lines[1] + "<br>");
      sb.Append("Browser Information : " + request.getPropertyByKey("User-Agent") + "<br>");
      sb.Append("Accept Language : " + request.getPropertyByKey("Accept-Language") + "<br>");
      sb.Append("Accept Encoding : " + request.getPropertyByKey("Accept-Encoding") + "<br>");
      
      response = new HTTPResponse(200);
      response.body = Encoding.UTF8.GetBytes(sb.ToString());
      return response;
    }

    public HTTPResponse PostProcessing(HTTPResponse response)
    {
      throw new NotImplementedException();
    }
  }
}