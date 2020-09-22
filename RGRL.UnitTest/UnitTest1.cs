using System.Threading.Tasks;
using NUnit.Framework;

namespace RGRL.UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            var r = new Repository<Bridge>("");
            await r.Create(new Bridge("Jackson", 420));
        }
    }

    internal class Bridge
    {
        public string Name { get; set; }
        public int Length { get; set; }

        public Bridge(string name, int length)
        {
            Name = name;
            Length = length;
        }
    }
}