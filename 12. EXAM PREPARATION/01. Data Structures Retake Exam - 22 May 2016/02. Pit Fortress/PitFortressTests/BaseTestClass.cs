namespace PitFortressTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PitFortress;

    [TestClass]
    public class BaseTestClass
    {
        public PitFortressCollection PitFortressCollection { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            this.PitFortressCollection = new PitFortressCollection();
        }
    }
}