
请将【WinServiceTest】拷贝到D盘或C盘根目录;
安装服务【管理员身份】运行【install.bat】即可;
卸载服务【管理员身份】运行【uninstall.bat】即可;

install.bat（安装）
@echo.请稍等，服务启动......  
@echo off  
@sc create GX_To_EMAIL binPath= "D:\WinServiceTest\WinServiceTest\bin\Debug\WinServiceTest.exe" 
DisplayName=每隔一段时间发送邮件的服务 start= auto
@sc description GX_To_EMAIL 定时发送邮件
@sc start GX_To_EMAIL
@echo off  
@echo.启动完毕！  
@pause

uninstall.bat（卸载）
@echo.服务卸载......  
@echo off  
@sc stop GX_To_EMAIL
@sc delete GX_To_EMAIL
@sc stop GX_To_EMAIL
@echo off  
@echo.卸载完毕！  
@pause