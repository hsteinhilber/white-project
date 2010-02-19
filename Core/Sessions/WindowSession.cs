using System;
using System.Windows;
using System.Windows.Automation;
using White.Core.Factory;
using White.Core.Logging;
using White.Core.ScreenMap;
using White.Core.UIA;
using White.Core.UIItems;
using White.Core.UIItems.Actions;
using White.Core.UIItems.Container;
using White.Core.UIItems.Finders;

namespace White.Core.Sessions
{
    public class WindowSession : IDisposable
    {
        private readonly ApplicationSession applicationSession;
        private readonly WindowItemsMap windowItemsMap;

        public WindowSession(ApplicationSession applicationSession, InitializeOption initializeOption)
        {
            this.applicationSession = applicationSession;
            windowItemsMap = WindowItemsMap.Create(initializeOption, RectX.UnlikelyWindowPosition);
            if (windowItemsMap.LoadedFromFile) initializeOption.NonCached();
        }

        protected WindowSession() {}

        public virtual WindowSession ModalWindowSession(InitializeOption modalInitializeOption)
        {
            return applicationSession.WindowSession(modalInitializeOption);
        }

        public virtual IUIItem Get(ContainerItemFactory containerItemFactory, SearchCriteria searchCriteria, ActionListener actionListener)
        {
            WhiteLogger.Instance.DebugFormat("Finding item based on criteria: {0}", searchCriteria);
            Point location = windowItemsMap.GetItemLocation(searchCriteria);
            if (location.Equals(RectX.UnlikelyWindowPosition))
            {
                WhiteLogger.Instance.Debug("Could not find based on position, finding using search");
                return Create(containerItemFactory, searchCriteria, actionListener);
            }

            AutomationElement automationElement = AutomationElementX.GetAutomationElementFromPoint(location);
            if (searchCriteria.AppliesTo(automationElement))
            {
                IUIItem item = new DictionaryMappedItemFactory().Create(automationElement, actionListener, searchCriteria.CustomItemType);
                return UIItemProxyFactory.Create(item, actionListener);
            }

            WhiteLogger.Instance.DebugFormat("UIItem {0} changed its position, finding using search", searchCriteria);
            return Create(containerItemFactory, searchCriteria, actionListener);
        }

        private IUIItem Create(ContainerItemFactory containerItemFactory, SearchCriteria searchCriteria, ActionListener actionListener)
        {
            IUIItem item = containerItemFactory.Get(searchCriteria, actionListener);
            if (item == null) return null;
            windowItemsMap.Add(item.Location, searchCriteria);
            return item;
        }

        public virtual void Dispose()
        {
            windowItemsMap.Save();
        }

        public virtual void Register(UIItems.WindowItems.Window window)
        {
            window.Focus();
            windowItemsMap.CurrentWindowPosition = window.Location;
        }
    }
}