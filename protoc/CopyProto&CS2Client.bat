@echo off&Setlocal EnableDelayedExpansion

rem 

set commonPath=%cd%/ProtoGen/
set clientPathCS=%cd%/../client/Assets/Code/Network/Protocol/msg/

cd %clientPathCS%
del /s *.cs

cd %commonPath%

for /f %%i in ('dir /a-d /b *.proto') do (	
	copy "%%i" "%clientPathProto%"
	@echo "%%i"
)

for /f %%i in ('dir /a-d /b *.cs') do (	
	copy "%%i" "%clientPathCS%"
	@echo "%%i"
)

EndLocal
pause

rem call something else
