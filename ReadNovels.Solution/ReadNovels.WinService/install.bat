@echo.���Եȣ���������......  
@echo off  
@sc create Capture_Update_Service binPath= "D:\AppletOfWeChat\WeChatApplet\ReadNovels.Solution\ReadNovels.WinService\bin\Debug\ReadNovels.WinService.exe" 
DisplayName=ÿ��һ��ʱ��ץȡ�����½� start= auto
@sc description Capture_Update_Service ��ʱץȡ�����½�
@sc start Capture_Update_Service
@echo off  
@echo.������ϣ�  
@pause