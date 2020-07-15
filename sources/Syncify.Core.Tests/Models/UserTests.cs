using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syncify.Core.Models;

namespace Syncify.Core.Tests.Models
{
    [TestClass]
    public class UserTests
    {
        private static readonly Assembly assembly = Assembly.GetExecutingAssembly();

        [TestMethod]
        public async Task WhenParsingJsonReturnsUser()
        {
            using var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Resources.User.json");

            var user = await JsonSerializer.DeserializeAsync<User>(stream);

            Assert.AreEqual("SE", user?.Country);
            Assert.AreEqual("JM Wizzler", user?.DisplayName);
            Assert.AreEqual("email@example.com", user?.Email);
            Assert.AreEqual(1, user?.ExternalUrls?.Count);
            Assert.AreEqual("spotify", user?.ExternalUrls?.Keys?.Single());
            Assert.AreEqual(0, Uri.Compare(new Uri("https://open.spotify.com/user/wizzler"), user?.ExternalUrls?.Values?.Single(), UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.IsNull(user?.Followers?.Link);
            Assert.AreEqual(3829, user?.Followers?.Total);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://api.spotify.com/v1/users/wizzler"), user.Url, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.IsNull(user?.Images?.Single().Height);
            Assert.IsNull(user?.Images?.Single().Width);
            Assert.AreEqual(0, Uri.Compare(new Uri("https://fbcdn-profile-a.akamaihd.net/hprofile-ak-frc3/t1.0-1/1970403_10152215092574354_1798272330_n.jpg"), user?.Images?.Single().Url, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.Ordinal));
            Assert.AreEqual("premium", user?.Product);
            Assert.AreEqual("user", user?.Type);
            Assert.AreEqual("spotify:user:wizzler", user?.Uri);
        }
    }
}
