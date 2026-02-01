using System;
using System.Configuration;
using System.Data;
using System.Windows;

namespace HalloChat_CSharp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        this.Startup += App_Startup;
        this.DispatcherUnhandledException += App_DispatcherUnhandledException;
    }

    private void App_Startup(object sender, StartupEventArgs e)
    {
        try
        {
            Console.WriteLine("Starting HalloChat application...");
            // 手动创建窗口以捕获可能的异常
            var loginWindow = new Views.LoginWindow();
            Console.WriteLine("Login window created successfully");
            loginWindow.Show();
            Console.WriteLine("Login window shown successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during startup: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            MessageBox.Show($"启动错误: {ex.Message}\n\n{ex.StackTrace}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        Console.WriteLine($"Unhandled exception: {e.Exception.Message}");
        Console.WriteLine($"Stack trace: {e.Exception.StackTrace}");
        MessageBox.Show($"未处理的异常: {e.Exception.Message}\n\n{e.Exception.StackTrace}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
    }
}

