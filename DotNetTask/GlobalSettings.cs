

namespace DotNetTask
{
    public static class GlobalSetting
    {
        private static string GetAppSettingValue(string key)
        {
            IConfigurationRoot config;

            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            return config.GetSection(key).Value;
        }
        internal static string EndpointUri()
        {
            return GetAppSettingValue("EndpointUri");
        }
        internal static bool IsAllowLogs()
        {
            return Convert.ToBoolean(GetAppSettingValue("AllowLogs"));
        }
        internal static string PrimaryKey()
        {
            return GetAppSettingValue("PrimaryKey");
        }
        internal static string GetJWTAudience()
        {
            return GetAppSettingValue("JWT:ValidAudience");
        }
        internal static string GetJWTIssuer()
        {
            return GetAppSettingValue("JWT:ValidIssuer");
        }
        internal static string ServerUrl()
        {
            return GetAppSettingValue("ServerUrl");
        }
        
    }
}