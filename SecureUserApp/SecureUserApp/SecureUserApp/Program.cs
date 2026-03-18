
using SecureUserApp.Logging;
using SecureUserApp.Services;
UserService service = new UserService();

try
{
    // Register user
    service.Register("prashant", "12345", "prashant@gmail.com");
    Console.WriteLine("User Registered Successfully!");
    FileLogger.LogInfo("User registered: prashant");

    // Login user
    bool login = service.Login("prashant", "12345");

    if (login)
    {
        Console.WriteLine("Login Successful");
        FileLogger.LogInfo("Login success: prashant");
    }
    else
    {
        Console.WriteLine("Login Failed");
        FileLogger.LogInfo("Login failed: prashant");
    }
}
catch (Exception ex)
{
    Console.WriteLine("Something went wrong!");
    FileLogger.LogError(ex, "Application error");
}

Console.ReadKey();