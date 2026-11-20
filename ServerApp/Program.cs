using ServerApp.Transport;

try
{
    Server server = new Server();
    server.openConnection();
}
catch (Exception ex)
{
    Console.WriteLine("Something went wrong. " + ex);
}