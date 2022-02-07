using Heritage.Security;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.Test;

public class SecurityTests
{
    [SetUp]
    public void Setup()
    {

    }

    [TestCase(10000)]
    public void Salt_ランダム生成(int count)
    {
        var salts = new string[count];

        for (int i = 0; i < count; i++)
        {
            var salt = Sha512.GetRandomBase64(64);

            salts[i] = salt;
        }

        for (int i = 0; i < count; i++)
        {
            var salt = salts[i];

            Assert.AreEqual(salts.Count(p => p == salt), 1);
        }
    }

    [TestCase("password", 200)]
    [TestCase("cowl_alone_sorghum_salaried", 100)]
    public void Password_繰り返し認証(string password, int count)
    {
        var salt = Sha512.GetRandomBase64(64);
        var pbkdf2 = new Pbkdf2();

        var hash1 = pbkdf2.GenerateBase64(password, salt);

        for (int i = 0; i < count; i++)
        {
            var hash2 = pbkdf2.GenerateBase64(password, salt);

            Assert.AreEqual(hash1, hash2);
        }
    }
}
