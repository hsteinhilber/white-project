using NUnit.Framework;
using White.Core.Factory;
using White.Core.Testing;

namespace White.Core.UIItems
{
    [TestFixture, WinFormCategory]
    public class WindowWithoutTitleBarTest : CoreTestTemplate
    {
        protected override string CommandLineArguments
        {
            get { return "notitle"; }
        }

        [Test]
        public void FindWindowOnSplashScreen()
        {
            Assert.AreNotEqual(null, application.GetWindow("Form1", InitializeOption.NoCache));
        }

        public override void TextFixtureTearDown()
        {
            application.Kill();
        }
    }
}