using EquityX_L00160463.View;

namespace EquityX_L00160463
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {

            InitializeComponent();
            Routing.RegisterRoute(nameof(AddFunds), typeof(AddFunds));
            Routing.RegisterRoute(nameof(WithdrawFunds), typeof(WithdrawFunds));
            Routing.RegisterRoute(nameof(AddCard), typeof(AddCard));
        }
    }
}