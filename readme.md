# 概述

# 模块
- Client
    - 该模块主要用于调用 CEF 以加载本程序的界面。
- ClientWebView
    - 该模块主要实现用户界面。
    
# 调试
1. 以 Debug 模式运行 ClientWebView 项目。保证 NodeJS 调试 web 服务器端口为 `8080` 。
2. 以 Debug 模式运行 Client 项目。在 `MainWindow.xaml.cs` 中定义了 NodeJS 调试 web 服务器端口为 `8080` 。
3. 使用浏览器打开 `http://localhost:8088/` 访问 CEF 调试工具。

# 发布
- 择日再写