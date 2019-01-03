@echo.请稍等，服务启动......  
@echo off  
@sc create Capture_Update_Service binPath= "D:\AppletOfWeChat\WeChatApplet\ReadNovels.Solution\ReadNovels.WinService\bin\Debug\ReadNovels.WinService.exe" 
DisplayName=每隔一段时间抓取更新章节 start= auto
@sc description Capture_Update_Service 定时抓取更新章节
@sc start Capture_Update_Service
@echo off  
@echo.启动完毕！  
@pause