@echo off&Setlocal EnableDelayedExpansion

rem 

set exPath[0]=''

set commonPath=%cd%/ProtoGen/
set serverPath=%cd%/../server/d_common/fun-message/proto/

cd %serverPath%

del /s *.proto

cd %commonPath%

for /f %%i in ('dir /a-d /b *.proto') do (	
	copy "%%i" "%serverPath%"
)

EndLocal
pause

rem call something else