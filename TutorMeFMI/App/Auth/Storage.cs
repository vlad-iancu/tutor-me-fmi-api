using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace TutorMeFMI.App.Auth
{
    public class Storage
    {
        private const string bucketName = "tutormefmi.appspot.com";

        private static StorageClient storage = null;

        public static StorageClient FileStorage
        {
            get
            {
                if (storage == null)
                {
                    using var serviceAccountFile = new FileStream(Secrets.ServiceAccountFile, FileMode.Open, FileAccess.Read);
                    var serviceAccount = ServiceAccountCredential.FromServiceAccountData(serviceAccountFile);
                    var credentials = GoogleCredential.FromServiceAccountCredential(serviceAccount);
                    _credential = serviceAccount;
                    storage = StorageClient.Create(credentials);
                }
                return storage;
            }
        }

        private static ServiceAccountCredential _credential = null;
        public static ServiceAccountCredential Credentials
        {
            get
            {
                if (_credential == null)
                {
                    using var serviceAccountFile = new FileStream(Secrets.ServiceAccountFile, FileMode.Open, FileAccess.Read);
                    var serviceAccount = ServiceAccountCredential.FromServiceAccountData(serviceAccountFile);
                    _credential = serviceAccount;
                }
                return _credential;
            }
        }
        /*public void UploadSampleFile()
        {
            using var serviceAccountFile = new FileStream(Secrets.ServiceAccountFile, FileMode.Open, FileAccess.Read);
            using var schemaFile = File.OpenRead("Data/schema.sql");
            var serviceAccount = ServiceAccountCredential.FromServiceAccountData(serviceAccountFile);
            var storage = StorageClient.Create(GoogleCredential.FromServiceAccountCredential(serviceAccount));
            storage.UploadObject("tutormefmi.appspot.com", "schema.sql", null, schemaFile);
        }

        public string GetSampleDownloadUrl()
        {
            var bucketName = "tutormefmi.appspot.com";
            var serviceAccountFile = Secrets.ServiceAccountFile;
            var objectPath = "schema.sql";
            var signer = UrlSigner.FromServiceAccountPath(serviceAccountFile);
            return signer.Sign(bucketName, "schema.sql", TimeSpan.FromHours(1), HttpMethod.Get);
        }*/

        public void UploadFile(string filePath, Stream file)
        {
            FileStorage.UploadObject(bucketName, filePath, null, file);
        }

        public void DeleteFile(string filePath)
        {
            FileStorage.DeleteObject(bucketName, filePath);
        }
        public string GetDownloadUrl(string filePath)
        {
            var signer = UrlSigner.FromServiceAccountCredential(Credentials);
            return signer.Sign(bucketName, filePath, TimeSpan.FromHours(1), HttpMethod.Get);
        }
    }
}