using Renci.SshNet;

namespace FTP.hohoho;

public class Client
{
    static void DropFile()
    {
        // Parametrize however you want
        var host = "sftp.example.com";
        var port = 22;
        var username = "usr";
        var localFilePath = @"C:\path\to\your\file.txt";
        var remoteFilePath = "/remote/path/file.txt";
        
        // These two should be kept in a secure manner.
        var cert = "path/to/cert.cer";
        var certPsw = "psw";

        // Upload file
        try
        {

            var connectionInfo = new PrivateKeyConnectionInfo(host, port, username, new PrivateKeyFile(cert, certPsw));
            using var sftp = new SftpClient(connectionInfo);
            sftp.Connect();
            
            Console.WriteLine("Connected to SFTP server.");
            using (var fileStream = new FileStream(localFilePath, FileMode.Open))
            {
                sftp.UploadFile(fileStream, remoteFilePath);
                Console.WriteLine("File uploaded successfully.");
            }

            sftp.Disconnect();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error during SFTP upload: " + ex.Message);
        }
    }
}