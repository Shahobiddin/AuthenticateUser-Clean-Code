using System.Collections.Generic;
using System.Text;

namespace Functions.Task2
{
    public class SitePage
    {
        private readonly string siteGroup;
        private readonly string userGroup;
        
        private const string Protocol = "http://";
        private const string Host = "mysite.com";
        private const string EditableQueryParameter = "/?edit=true";

        public SitePage(string siteGroup, string userGroup)
        {
            this.siteGroup = siteGroup;
            this.userGroup = userGroup;
        }

        public string GetEditablePageUrl(IDictionary<string, string> parameters)
        {
            return GetPageUrl(EditableQueryParameter, GetQueryParameters(parameters), GetGroupQueryParameters());
        }

        private string GetPageUrl(params string[] queryParameters)
        {
            var paramters = string.Join("", queryParameters);
            return $"{Protocol}{Host}{paramters}";
        }

        private string GetQueryParameters(IDictionary<string, string> parameters)
        {
            var sb = new StringBuilder();
            foreach (var parameter in parameters)
            {
                sb.Append(CreateQueryParameter(parameter.Key, parameter.Value));
            }

            return sb.ToString();
        }

        private string GetGroupQueryParameters()
        {
            return string.Join("",
                CreateQueryParameter("siteGrp", siteGroup),
                CreateQueryParameter("userGrp", userGroup));
        }

        private string CreateQueryParameter(string key, string value)
        {
            return $"&{key}={value}";
        }
    }
}