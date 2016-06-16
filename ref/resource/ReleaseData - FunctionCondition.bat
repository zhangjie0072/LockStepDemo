@echo off
set CurDir=%cd%
set ToolPath=%CurDir%\tool\

@echo/

@echo 生成游戏数据配置xml：



%ToolPath%DataMaker.exe FunctionCondition %CurDir%\datasheet\FunctionCondition.xlsm %CurDir%\res%

copy %CurDir%\res\FunctionCondition.xml %CurDir%\..\client\Assets\Resources\Config\Func

@echo/

pause
