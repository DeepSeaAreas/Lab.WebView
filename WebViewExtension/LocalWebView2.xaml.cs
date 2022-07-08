using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebViewExtension
{
    /// <summary>
    /// Interaction logic for LocalWebView2.xaml
    /// </summary>
    public partial class LocalWebView2 : UserControl
    {

        /// <summary>
        /// 本地目录地址
        /// </summary>
        public string Folder
        {
            get
            {
                return (string)GetValue(FolderProperty);
            }
            set
            {
                SetValue(FolderProperty, value);
            }
        }

        /// <summary>
        /// 声明本地属性Folder
        /// </summary>
        public static readonly DependencyProperty FolderProperty = DependencyProperty.Register(
            "Folder",
            typeof(string),
            typeof(LocalWebView2),
            new PropertyMetadata(
                "TestWebPage",
                new PropertyChangedCallback(OnFolderChanged)
            )
        );

        /// <summary>
        /// 当folder参数发生改变时的触发回调，重新初始化本地站点
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnFolderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not LocalWebView2 control) return;
            //if (control.webView.CoreWebView2 == null) return;
            //control.webView.CoreWebView2.SetVirtualHostNameToFolderMapping("local.webview", control.Folder,
            //    Microsoft.Web.WebView2.Core.CoreWebView2HostResourceAccessKind.Allow);
            //control.webView.Reload();
            control.InitializeAsync();
        }

        public LocalWebView2()
        {
            InitializeComponent();
            InitializeAsync();
        }

        async void InitializeAsync()
        {
            Debug.WriteLine($"Start Localhost initialize at {Folder}...");
            await webView.EnsureCoreWebView2Async(null);
            webView.CoreWebView2.SetVirtualHostNameToFolderMapping("local.webview", Folder, 
                Microsoft.Web.WebView2.Core.CoreWebView2HostResourceAccessKind.Allow);
            webView.Source = new Uri("https://local.webview/index.html");
        }
    }
}
