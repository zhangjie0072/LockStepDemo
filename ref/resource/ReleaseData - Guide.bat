@echo off
set CurDir=%cd%
set ToolPath=%CurDir%\tool\

@echo/

@echo 生成游戏数据配置xml：



%ToolPath%DataMaker.exe Guide %CurDir%\datasheet\Guide.xlsm %CurDir%\res%

copy %CurDir%\res\GuideModule.xml %CurDir%\..\client\Assets\Resources\Config\Func
copy %CurDir%\res\GuideStep.xml %CurDir%\..\client\Assets\Resources\Config\Func

@echo/

pause
