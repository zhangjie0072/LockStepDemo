@echo off&Setlocal EnableDelayedExpansion

rem 

set exPath[0]='DBInfo'
set exPath[1]='DBOperate'
set exPath[2]='Log'
set exPath[3]='Rpc'
set exPath[4]='ServerConfig'

set commonPath=%cd%/ProtoGen/
set protoExPath=%cd%/ProtoEx/
set clientPathProto=%cd%/../client/Assets/Proto/
set clientPathCS=%cd%/../client/Assets/Code/Network/Protocol/msg/

cd %clientPathProto%
del /s *.proto

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

cd %protoExPath%

for /f %%i in ('dir /a-d /b *.proto') do (	
	copy "%%i" "%clientPathProto%"
	@echo "%%i"
)

EndLocal
pause

rem call something else