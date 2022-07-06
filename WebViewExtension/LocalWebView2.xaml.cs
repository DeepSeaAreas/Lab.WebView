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
                "https://www.baidu.com",
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
            Debug.WriteLine("test");
            if (d is not LocalWebView2 control) return;
            control.webView.Source = new Uri(control.Folder);
        }

        public LocalWebView2()
        {
            InitializeComponent();
        }
    }
}
