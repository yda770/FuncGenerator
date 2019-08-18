using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using Google.Apis.Drive.v2;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Drive.v2.Data;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2.Flows;
using System.Text;

namespace FuncGeneretor.Controllers
{
    public class DriveServiceMe
    {
        public DriveService service { get; set; }

        public void authenticate()
        {
            //Scopes for use with the Google Drive API
            string[] scopes = new string[] { DriveService.Scope.Drive,
                                             DriveService.Scope.DriveFile};

            var clientId = "210183744866-m2i5iunhrmtgfeufc90iitn8fnevs60p.apps.googleusercontent.com";      // From https://console.developers.google.com
            var clientSecret = "s-ymnI1L-C5ceyAOrq0pPnzk";          // From https://console.developers.google.com
                                               // here is where we Request the user to give us access, or use the Refresh Token that was previously stored in %AppData%
            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            },
                                                                        scopes,
                                                                        Environment.UserName,
                                                                        CancellationToken.None,
                                                                        new FileDataStore("Daimto.GoogleDrive.Auth.Store")).Result;


            this.service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "FuncGen",
            });

        }

        public void DeleteFile(string FileName)
        {
            try
            {
                
                FilesResource.DeleteRequest DeleteRequest = this.service.Files.Delete(this.GetFileByName(FileName).Id);
                DeleteRequest.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
           
        }

        public File GetFileByName(string fileName)
        {
                        // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.MaxResults = 100;
            //listRequest.Fields = "nextPageToken, files(id, name, parents)";

            // List files.
            IList<File> files = listRequest.Execute().Items;
            File file = files.FirstOrDefault(x => x.OriginalFilename == fileName && x.ExplicitlyTrashed == false);
            return file;

        }

        public File GetFileByTitle(string fileName)
        {
            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.MaxResults = 100;
            //listRequest.Fields = "nextPageToken, files(id, name, parents)";

            // List files.
            IList<File> files = listRequest.Execute().Items;
            File file = files.FirstOrDefault(x => x.Title == fileName && x.ExplicitlyTrashed == false);
            return file;

        }

        public string ReadFile(string FileName)
        {
            string result = "";
            File myFile = this.GetFileByName(FileName);

            try
            {
                var x = service.HttpClient.GetByteArrayAsync(myFile.DownloadUrl);
                byte[] arrBytes = x.Result;
                result = System.Text.Encoding.UTF8.GetString(arrBytes);
            }
            catch (Exception e)
            {

            }


            return result;
        }

        public void createFile(string content)
        {
            string[] lines;
            List<string> linesList = new List<string>();
  
            linesList.Add(content);
            lines = linesList.ToArray();

            System.IO.File.WriteAllLines(@"C:\Users\yehuda_da\Desktop\פרוייקט גמר\request.txt", lines);

            System.Threading.Thread.Sleep(10);


            File parent = this.GetFileByTitle("FuncGenServer");
            File body = new File();
            body.Title = System.IO.Path.GetFileName(@"C:\Users\yehuda_da\Desktop\פרוייקט גמר\request.txt");
            body.Description = "request.txt";
            body.MimeType = GetMimeType(@"C:\Users\yehuda_da\Desktop\פרוייקט גמר\request.txt");
            body.Parents = new List<ParentReference> { new ParentReference() { Id = parent.Id } };

            // File's content.
            byte[] bytes = System.IO.File.ReadAllBytes(@"C:\Users\yehuda_da\Desktop\פרוייקט גמר\request.txt");
            System.IO.MemoryStream stream = new System.IO.MemoryStream(bytes);
            try
            {
                FilesResource.InsertMediaUpload request = service.Files.Insert(body, stream, "application/vnd.google-apps.file");
                request.Upload();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }
        private static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
    }
}