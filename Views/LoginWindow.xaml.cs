using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace HalloChat_CSharp.Views
{
    // 引用主命名空间中的类型
    using HalloChat_CSharp;

    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        private List<ServerInfo> servers;
        private ServerInfo selectedServer;
        private string bgColor = "#ffffff";
        private string language = "zh-CN";

        public LoginWindow()
        {
            InitializeComponent();
            InitializeServers();
        }

        private void InitializeServers()
        {
            servers = new List<ServerInfo>
            {
                new ServerInfo { Id = "local-server", Name = "本地服务器", Address = "localhost", Port = "7932" }
            };
        }

        private void ShowError(string message)
        {
            ErrorText.Text = message;
            ErrorGrid.Visibility = Visibility.Visible;
        }

        private void HideError()
        {
            ErrorGrid.Visibility = Visibility.Collapsed;
        }

        private async Task LoginAsync(string username, string password)
        {
            try
            {
                // 管理员模式登录
                if (AdminModeCheckBox.IsChecked == true)
                {
                    if (password == "hallochat123")
                    {
                        var adminUser = new UserInfo
                        {
                            Id = "admin_" + DateTime.Now.Ticks,
                            Username = username,
                            Email = "admin@hallochat.local",
                            Token = "admin_dev_token_" + DateTime.Now.Ticks,
                            IsAdmin = true
                        };

                        // 保存用户信息
                        SaveUserInfo(adminUser);
                        ShowMainWindow(adminUser);
                        return;
                    }
                    else
                    {
                        ShowError("管理员密码错误");
                        return;
                    }
                }

                ShowError("请选择服务器");
            }
            catch (Exception ex)
            {
                ShowError("登录失败: " + ex.Message);
            }
        }

        private void SaveUserInfo(UserInfo userInfo)
        {
            try
            {
                // 保存用户信息
                Properties.Settings.Default.UserId = userInfo.Id;
                Properties.Settings.Default.Username = userInfo.Username;
                Properties.Settings.Default.Email = userInfo.Email;
                Properties.Settings.Default.Token = userInfo.Token;

                // 保存记住我状态
                if (RememberMeCheckBox.IsChecked == true)
                {
                    Properties.Settings.Default.RememberMe = true;
                    Properties.Settings.Default.Password = PasswordBox.Password;
                }
                else
                {
                    Properties.Settings.Default.RememberMe = false;
                    Properties.Settings.Default.Password = string.Empty;
                }

                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                ShowError("保存用户信息失败: " + ex.Message);
            }
        }

        private void ShowMainWindow(UserInfo userInfo)
        {
            var mainWindow = new MainWindow(userInfo);
            mainWindow.Show();
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            HideError();
            var username = UsernameTextBox.Text.Trim();
            var password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username))
            {
                ShowError("请输入用户名");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                ShowError("请输入密码");
                return;
            }

            _ = LoginAsync(username, password);
        }
    }
}