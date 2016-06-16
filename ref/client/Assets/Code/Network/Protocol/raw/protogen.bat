@echo off
::判断输入路径是不是文件夹

for %%i in (%cd%\*.proto) do protogen.exe -i:%%i -o:%%~ni.cs

pause 