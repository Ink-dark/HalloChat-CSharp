@echo off

REM 设置项目路径
set PROJECT_PATH=%~dp0

REM 进入项目目录
cd /d %PROJECT_PATH%

echo 正在构建项目...
dotnet build -c Release

if %ERRORLEVEL% neq 0 (
    echo 构建失败！
    pause
    exit /b %ERRORLEVEL%
)

echo 构建成功！
echo 正在运行项目...
dotnet run -c Release

if %ERRORLEVEL% neq 0 (
    echo 运行失败！
    pause
    exit /b %ERRORLEVEL%
)

echo 项目已退出。
pause