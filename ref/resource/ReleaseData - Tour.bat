@echo off
set CurDir=%cd%
set ToolPath=%CurDir%\tool\

@echo/

@echo 生成游戏数据配置xml：



%ToolPath%DataMaker.exe Tour %CurDir%\datasheet\Tour.xlsm %CurDir%\res%

copy %CurDir%\res\Tour.xml %CurDir%\..\client\Assets\Resources\Config\Func
copy %CurDir%\res\TourResetCost.xml %CurDir%\..\client\Assets\Resources\Config\Func
copy %CurDir%\res\TourResetLimit.xml %CurDir%\..\client\Assets\Resources\Config\Func

@echo/

pause
