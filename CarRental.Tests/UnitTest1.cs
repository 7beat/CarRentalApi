namespace CarRental.Tests;

[TestFixture]
public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [TestCase(1)]
    public void Test2(int number)
    {
        Assert.That(number, Is.EqualTo(1));
    }
}