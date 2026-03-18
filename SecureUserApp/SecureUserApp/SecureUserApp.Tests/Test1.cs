using SecureUserApp.Logging;
using SecureUserApp.Security;
using SecureUserApp.Services;

namespace SecureUserApp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RegisterAndLogin_ShouldReturnTrue()
        {
            // Arrange
            var service = new UserService();

            // Act
            service.Register("testuser", "12345", "test@gmail.com");
            bool loginResult = service.Login("testuser", "12345");

            // Assert
            Assert.IsTrue(loginResult);
        }

        [TestMethod]
        public void Login_WithWrongPassword_ShouldReturnFalse()
        {
            var service = new UserService();

            service.Register("testuser", "12345", "test@gmail.com");
            bool loginResult = service.Login("testuser", "wrongpass");

            Assert.IsFalse(loginResult);
        }

        [TestMethod]
        public void Encryption_EncryptDecrypt_ShouldReturnOriginalText()
        {
            var enc = new EncryptionService();

            string original = "hello@gmail.com";
            string encrypted = enc.Encrypt(original);
            string decrypted = enc.Decrypt(encrypted);

            Assert.AreEqual(original, decrypted);
        }

        [TestMethod]
        public void Logger_ShouldCreateLogFile()
        {
            // Act
            FileLogger.LogInfo("Test log entry");

            // Assert
            Assert.IsTrue(File.Exists("log.txt"));
        }
    }
}
