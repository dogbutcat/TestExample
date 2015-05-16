using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CalculateMoneyToCash.Resources;

namespace CalculateMoneyToCash
{
    public partial class MainPage : PhoneApplicationPage
    {
        private float percent;
        private float totalCash;
        private float cashInput;
        private const float least = 5.0f;
        private float serverFee;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();
            percent = 0.01f;
            totalCash = 0;
            cashInput = 0;
            serverFee = 0;
            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        private void btnCalcu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtRatio.Text.Trim())||string.IsNullOrEmpty(this.txtTotalCash.Text.Trim()))
                {
                    MessageBox.Show("Please Input Your Cash!", "Error", MessageBoxButton.OK);
                    if (string.IsNullOrEmpty(this.txtRatio.Text.Trim()))
                    {
                        this.txtRatio.Focus();
                    }
                    this.txtTotalCash.Focus();
                    return;
                }

                txtCashInput.Text = "";
                totalCash = float.Parse(txtTotalCash.Text.Trim());
                //percent = float.Parse(txtRatio.Text.Trim()) / 100;
                cashInput = totalCash / (1 + percent);
                txtCashInput.Text = cashInput.ToString("F2");
                Reset();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void Reset()
        {
            txtTotalCash.Text = "";
            txtRatio.Text = "1.0";
        }

        private void txtRatio_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        // 用于生成本地化 ApplicationBar 的示例代码
        //private void BuildLocalizedApplicationBar()
        //{
        //    // 将页面的 ApplicationBar 设置为 ApplicationBar 的新实例。
        //    ApplicationBar = new ApplicationBar();

        //    // 创建新按钮并将文本值设置为 AppResources 中的本地化字符串。
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // 使用 AppResources 中的本地化字符串创建新菜单项。
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}