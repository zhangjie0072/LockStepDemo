@echo off&Setlocal EnableDelayedExpansion

rem 

set exPath[0]=''

set commonPath=%cd%/res/
set clientPath=%cd%/../client/Assets/Resources/Config/Func/

cd %commonPath%

for /f %%i in ('dir /a-d /b *.xml') do (	
	copy "%%i" "%clientPath%"
)

for /f %%i in ('dir /a-d /b *.txt') do (	
	copy "%%i" "%clientPath%"
)

for /f %%i in ('dir /a-d /b *.bytes') do (	
	copy "%%i" "%clientPath%"
)


EndLocal
pause

rem call something else