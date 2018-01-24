using System;

namespace Sample.Common
{
    public class IdentityServerConfig
    {
        public string Authority { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public string ApiName { get; set; }
    }
}
