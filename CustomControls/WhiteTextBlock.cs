using System.Windows.Automation.Peers;
using System.Windows.Controls;
using White.CustomControls.Peers;

namespace White.CustomControls
{
    public class WhiteTextBlock : TextBlock
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new WhiteTextBlockPeer(this);
        }
    }
}