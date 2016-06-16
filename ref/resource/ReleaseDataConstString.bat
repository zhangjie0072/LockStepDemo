@echo off
set CurDir=%cd%
set ToolPath=%CurDir%\tool\

@echo/

@echo 生成游戏数据配置xml：


%ToolPath%DataMakerNew.exe lua ConstString %CurDir%\datasheet\ConstString.xlsm %CurDir%\res%


@echo/

pause
