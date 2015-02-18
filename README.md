Azure API Management Disaster Recovery tool is written based on:
http://azure.microsoft.com/en-us/documentation/articles/api-management-howto-disaster-recovery-backup-restore/

In order to successfully run the tool you need to:

1. Have the credentials of co-administrator of the subscription

2. Register an app in AAD
https://msdn.microsoft.com/en-us/library/dn790557.aspx

3. Set values in the config files in APIM.DR and APIM.Upload projects

The solution consists of:

1. APIM.DR - disaster recovery tool to backup and restore APIM.

2. APIM.Upload - a utility to upload a backup to a different subscription storage.
It can be handy if you have Prod\Dev subscription as a part of your ALM.

3. APIM.Storage, Config - a support projects for APIM.DR

IMPORTANT:
 - The tier of the service being restored into must match the tier of the backed up service being restored.
 - Restore of a backup is guaranteed only for 7 days since the moment of its creation.
 - Usage data used for creating analytics reports is not included in the backup

 
