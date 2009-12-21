using System;
using System.Collections.Generic;
using System.Windows.Automation;

namespace White.Core.UIItems.Finders
{
    public abstract class SearchCondition
    {
        public virtual List<AutomationElement> Filter(List<AutomationElement> automationElements)
        {
            return automationElements.FindAll(Satisfies);
        }

        public abstract bool Satisfies(AutomationElement element);
        public abstract Condition AutomationCondition { get; }
        internal abstract string ToString(string operation);
        protected internal abstract object SearchValue { get; }

        public abstract bool AppliesTo(AutomationElement element);
    }
}
