using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIM.Config
{
    public class Config
    {
        static public bool PrintToken
        {
            get
            {
                return GetValue<bool>("printToken");
            }
        }

        static public char CsvDelimeter
        {
            get
            {
                return GetValue<char>("csvDelimeter");
            }
        }

        static public string AzureApiUrl
        {
            get
            {
                return GetValue<string>("azureApiUrl");
            }
        }

        static public string AzureSig
        {
            get
            {
                return GetValue<string>("azureSig");
            }
        }

        static public string AccessKey
        {
            get
            {
                return GetValue<string>("accessKey");
            }
        }

        static public string StorageAccount
        {
            get
            {
                return GetValue<string>("storageAccount");
            }
        }

        static public string ContainerName
        {
            get
            {
                return GetValue<string>("containerName");
            }
        }

        static public string BackupName
        {
            get
            {
                return GetValue<string>("backupName");
            }
        }

        static public string SubscriptionId
        {
            get
            {
                return GetValue<string>("subscriptionId");
            }
        }

        static public string ResourceGroupName
        {
            get
            {
                return GetValue<string>("resourceGroupName");
            }
        }

        static public string Version
        {
            get
            {
                return GetValue<string>("version");
            }
        }

        static public string TenantId
        {
            get
            {
                return GetValue<string>("tenantId");
            }
        }

        static public string AppId
        {
            get
            {
                return GetValue<string>("appId");
            }
        }
        static public string ServiceName
        {
            get
            {
                return GetValue<string>("serviceName");
            }
        }
        static public string RedirectUrl
        {
            get
            {
                return GetValue<string>("redirectUrl");
            }
        }
        static public string ServiceUrl
        {
            get
            {
                return GetValue<string>("serviceUrl");
            }
        }

        public static T GetValue<T>(string entry)
        {
            // Make sure the key represents a possible valid entry
            if (string.IsNullOrWhiteSpace(entry))
                return default(T);

            var value = ConfigurationManager.AppSettings[entry];

            // If the key is available but does not contain any value, return the default value of the specfied type
            if (string.IsNullOrWhiteSpace(value))
                return default(T);

            // In case the specified type is an enum, try to parse the entry as an enum value
            if (typeof(T).IsEnum)
                return (T)Enum.Parse(typeof(T), value, true);

            // In case the specified type is a bool and the entry value represents an integer
            int val;
            if (typeof(T) == typeof(bool) && int.TryParse(value, out val))
                // We convert to value to an integer first before changing the entry value to the specified type
                return (T)Convert.ChangeType(val, typeof(T));

            // Change the entry value to the specified type
            return (T)Convert.ChangeType(value, typeof(T));
        }

    }
}
