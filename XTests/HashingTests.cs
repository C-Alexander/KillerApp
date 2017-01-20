using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Shadow_Arena.Services;
using Xunit;
using Xunit.Abstractions;

namespace XTests
{
    public class HashingTests
    {
        private readonly ITestOutputHelper _output;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public HashingTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory(DisplayName = "If two identical passwords are hashed, the results are the same. With separate passwords, they are not.")]
        [InlineData("tester", "tester")]
        [InlineData("tester", "test123")]
        [InlineData("Corgi42!!!", "Corgi42!!!")]
        [InlineData("corgi42!", "Corgi42!")]
        public void TestHashingEquality(string password, string comparisonPassword)
        {
            IHashing hashing = new Hashing();
            Assert.True(hashing.GetHashedPassword(password) == hashing.GetHashedPassword(password));
            Assert.True(hashing.GetHashedPassword(comparisonPassword) == hashing.GetHashedPassword(comparisonPassword));
            if (password == comparisonPassword)
            {
                Assert.True(hashing.GetHashedPassword(password) == hashing.GetHashedPassword(comparisonPassword));
            }
            else
            {
                Assert.False(hashing.GetHashedPassword(password) == hashing.GetHashedPassword(comparisonPassword));
            }
        }
    }
}
