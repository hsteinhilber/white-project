using System.Collections.Generic;
using System.Text;
using White.Core;
using White.Core.UIItems.TableItems;
using White.Core.UIItems.WindowItems;
using NUnit.Framework;

namespace Repository.EntityMapping
{
    [TestFixture]
    public class EntitiesTest
    {
        [Test]
        public void To_String()
        {
            Entities<Entity> list = new Entities<Entity>();
            NestedEntity nestedEntity = new NestedEntity();
            TestEntity testEntity = new TestEntity(nestedEntity);
            nestedEntity.Yo = "7";
            testEntity.Zo = "8";
            list.Add(testEntity);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("ZO, YO, ");
            builder.AppendLine("8, 7, ");
            Assert.AreEqual(builder.ToString(), list.ToString());
        }

        [Test]
        public void Construction()
        {
            var data = new List<string[]> {new string[] {"1", "2"}, new string[] {"3", "4"}};
            var entities = new Entities<TestEntity>(new string[] {"ZO", "YO"}, data);
            Assert.AreEqual("1", entities[0].Zo);
            Assert.AreEqual("2", entities[0].NestedEntity.Yo);
            Assert.AreEqual("3", entities[1].Zo);
            Assert.AreEqual("4", entities[1].NestedEntity.Yo);
        }
    }

    [TestFixture]
    public class EntitiesFromUIItemsTest
    {
        private Application application;
        private Window window;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            WinFormTestConfiguration configuration = new WinFormTestConfiguration(string.Empty);
            application = configuration.Launch();
            window = application.GetWindow("Form1");
        }

        [Test]
        public void FromTable()
        {
            Table table = window.Get<Table>("people");
            Entities<Cricketer> entities = new Entities<Cricketer>(table);
            Assert.AreEqual(entities.Count, table.Rows.Count);
            Assert.AreEqual("Imran", entities[0].Name);
        }

        [TearDown]
        public void TearDown()
        {
            if (application != null) application.Kill();
        }
    }

    public class Cricketer : Entity
    {
        private string name;
        private string country;
        private bool alive;

        public Cricketer(TableRow tableRow, IList<string> header) : base(tableRow, header){}

        public string Name
        {
            get { return name; }
        }
        public string Country
        {
            get { return country; }
        }
        public bool Alive
        {
            get { return alive; }
        }
    }

    public class GoogleWebsite
    {
        private string key;
        private string value;
    }
}