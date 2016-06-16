@echo off
set CurDir=%cd%
set ToolPath=%CurDir%\tool\

@echo/

@echo 生成游戏数据配置xml：



%ToolPath%DataMaker.exe Rank %CurDir%\datasheet\Rank.xlsm %CurDir%\res%

copy %CurDir%\res\Rank.xml %CurDir%\..\client\Assets\Resources\Config\Func

@echo/

pause
