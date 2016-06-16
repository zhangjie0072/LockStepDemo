@echo off
set CurDir=%cd%
set ToolPath=%CurDir%\tool\

@echo/

@echo "generating..."


%ToolPath%DataMaker2.exe lua ChatConstMsg %CurDir%\datasheet\Chat.xlsm %CurDir%\luaRes%

@echo/

pause