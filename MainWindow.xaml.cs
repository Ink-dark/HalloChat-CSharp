﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HalloChat_CSharp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserInfo currentUser;

        public MainWindow(UserInfo userInfo)
        {
            InitializeComponent();
            currentUser = userInfo;
            UpdateUI();
        }

        private void UpdateUI()
        {
            // 更新用户信息
            UsernameText.Text = currentUser.Username;
            EmailText.Text = currentUser.Email;
            UserIdText.Text = "ID: " + currentUser.Id;

            // 更新系统状态
            ConnectionStatusText.Text = "已连接";
            LastOnlineText.Text = "最后在线: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            // 更新聊天窗口标题
            ChatTitleText.Text = "选择联系人开始聊天";
            ChatStatusText.Text = "";
        }

        private void ServerItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 处理服务器选择
            MessageBox.Show("服务器选择功能开发中", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ContactItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 处理联系人选择
            MessageBox.Show("联系人选择功能开发中", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            // 处理发送消息
            MessageBox.Show("消息发送功能开发中", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddContactButton_Click(object sender, RoutedEventArgs e)
        {
            // 处理添加联系人
            MessageBox.Show("添加联系人功能开发中", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CreateGroupButton_Click(object sender, RoutedEventArgs e)
        {
            // 处理创建群聊
            MessageBox.Show("创建群聊功能开发中", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EncryptChatButton_Click(object sender, RoutedEventArgs e)
        {
            // 处理加密聊天
            MessageBox.Show("加密聊天功能开发中", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MoreOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            // 处理更多选项
            MessageBox.Show("更多选项功能开发中", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // 处理打开设置
            SettingsOverlay.Visibility = Visibility.Visible;
        }

        private void CloseSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // 处理关闭设置
            SettingsOverlay.Visibility = Visibility.Collapsed;
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // 处理保存设置
            MessageBox.Show("设置保存功能开发中", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            SettingsOverlay.Visibility = Visibility.Collapsed;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // 处理登出
            var loginWindow = new Views.LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            // 处理个人资料
            MessageBox.Show("个人资料功能开发中", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void PrivacyButton_Click(object sender, RoutedEventArgs e)
        {
            // 处理隐私设置
            MessageBox.Show("隐私设置功能开发中", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            // 处理通知设置
            MessageBox.Show("通知设置功能开发中", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            // 处理关于
            MessageBox.Show("HalloChat v0.2.0-alpha\n安全、高效的即时通讯应用", "关于", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    public class UserInfo
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class ServerInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Port { get; set; }
        public string Status { get; set; }
    }

    public class ContactInfo
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Status { get; set; }
        public string LastMessage { get; set; }
        public string LastMessageTime { get; set; }
    }

    public class MessageInfo
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public string Timestamp { get; set; }
        public bool IsSelf { get; set; }
    }

    public class GroupInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<ContactInfo> Members { get; set; }
    }
}
