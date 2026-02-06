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
            LoadSettings();
            InitializeUI();
        }

        private void InitializeServers()
        {
            servers = new List<ServerInfo>
            {
                new ServerInfo { Id = "local-server", Name = "本地服务器", Address = "localhost", Port = "7932" }
            };
        }

        private void InitializeUI()
        {
            // 绑定服务器列表到ComboBox
            ServerComboBox.ItemsSource = servers;
            ServerComboBox.DisplayMemberPath = "Name";
            ServerComboBox.SelectedValuePath = "Id";
            
            // 如果之前有选择的服务器，设置默认选择
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Server))
            {
                var savedServer = servers.FirstOrDefault(s => s.Id == Properties.Settings.Default.Server);
                if (savedServer != null)
                {
                    ServerComboBox.SelectedItem = savedServer;
                }
            }
            else if (servers.Count > 0)
            {
                ServerComboBox.SelectedIndex = 0;
            }
        }

        private void LoadSettings()
        {
            // 加载记住的用户名和密码
            if (Properties.Settings.Default.RememberMe)
            {
                UsernameTextBox.Text = Properties.Settings.Default.Username;
                PasswordBox.Password = DecryptPassword(Properties.Settings.Default.Password);
                RememberMeCheckBox.IsChecked = true;
            }
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

        // 简单的密码加密（Base64）
        private string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;
            
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }

        // 简单的密码解密（Base64）
        private string DecryptPassword(string encryptedPassword)
        {
            if (string.IsNullOrEmpty(encryptedPassword))
                return string.Empty;
            
            try
            {
                byte[] bytes = Convert.FromBase64String(encryptedPassword);
                return System.Text.Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        private async Task LoginAsync(string username, string password)
        {
            try
            {
                // 显示加载状态
                ShowLoading(true);
                
                // 管理员模式登录
                if (AdminModeCheckBox.IsChecked == true)
                {
                    if (password == "hallochat123")
                    {
                        // 模拟网络延迟
                        await Task.Delay(1000);
                        
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

                // 非管理员模式，需要连接到服务器
                selectedServer = (ServerInfo)ServerComboBox.SelectedItem;
                
                // 模拟网络延迟
                await Task.Delay(1000);
                
                // 这里可以添加实际的服务器登录逻辑
                ShowError($"已选择服务器: {selectedServer.Name}，登录功能开发中");
            }
            catch (Exception ex)
            {
                ShowError("登录失败: " + ex.Message);
            }
            finally
            {
                // 隐藏加载状态
                ShowLoading(false);
            }
        }
        
        // 显示或隐藏加载状态
        private void ShowLoading(bool isLoading)
        {
            LoginButton.IsEnabled = !isLoading;
            LoadingStackPanel.Visibility = isLoading ? Visibility.Visible : Visibility.Collapsed;
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

                // 保存选择的服务器
                if (ServerComboBox.SelectedItem != null)
                {
                    var selectedServer = (ServerInfo)ServerComboBox.SelectedItem;
                    Properties.Settings.Default.Server = selectedServer.Id;
                }

                // 保存记住我状态
                if (RememberMeCheckBox.IsChecked == true)
                {
                    Properties.Settings.Default.RememberMe = true;
                    Properties.Settings.Default.Password = EncryptPassword(PasswordBox.Password);
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
            PerformLogin();
        }
        
        // 处理键盘回车键登录
        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PerformLogin();
            }
        }
        
        // 执行登录逻辑
        private void PerformLogin()
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

            if (ServerComboBox.SelectedItem == null)
            {
                ShowError("请选择服务器");
                return;
            }

            _ = LoginAsync(username, password);
        }
    }
}