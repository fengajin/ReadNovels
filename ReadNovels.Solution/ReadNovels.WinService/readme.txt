
�뽫��WinServiceTest��������D�̻�C�̸�Ŀ¼;
��װ���񡾹���Ա��ݡ����С�install.bat������;
ж�ط��񡾹���Ա��ݡ����С�uninstall.bat������;

install.bat����װ��
@echo.���Եȣ���������......  
@echo off  
@sc create GX_To_EMAIL binPath= "D:\WinServiceTest\WinServiceTest\bin\Debug\WinServiceTest.exe" 
DisplayName=ÿ��һ��ʱ�䷢���ʼ��ķ��� start= auto
@sc description GX_To_EMAIL ��ʱ�����ʼ�
@sc start GX_To_EMAIL
@echo off  
@echo.������ϣ�  
@pause

uninstall.bat��ж�أ�
@echo.����ж��......  
@echo off  
@sc stop GX_To_EMAIL
@sc delete GX_To_EMAIL
@sc stop GX_To_EMAIL
@echo off  
@echo.ж����ϣ�  
@pause