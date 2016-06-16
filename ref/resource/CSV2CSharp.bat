@echo off
set CurDir=%cd%
set ToolPath=%CurDir%\tool\

python %ToolPath%CSV2CSharp.py

@echo/

pause
