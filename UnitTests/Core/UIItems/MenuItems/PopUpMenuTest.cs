using NUnit.Framework;
using White.Core.Testing;
using White.Core.UIItems.ListBoxItems;

namespace White.Core.UIItems.MenuItems
{
    [TestFixture]
    public class PopUpMenuTest : ControlsActionTest
    {
        [Test]
        public void GetPopupMenuItems()
        {
            window.Get<ListBox>("listBox").RightClick();
            PopUpMenu popup = window.Popup;
            Assert.AreEqual(1, popup.Items.Count);
        }

        [Test]
        public void ClickOnPopupMenu()
        {
            window.Get<ListBox>("listBox").RightClick();
            Assert.AreEqual(true, window.HasPopup());
            Menu menu = window.PopupMenu("Show Films");
            menu.Click();
            AssertResultLabelText("All good films");
        }

        [Test]
        public void ClickOnNestedMenu()
        {
            window.Get<ListBox>("listBoxWithVScrollBar").RightClick();
            window.PopupMenu("Root", "Level1", "Level2").Click();
            AssertResultLabelText("Level2Click");
        }
    }
}