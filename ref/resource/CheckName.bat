@echo off
echo 开始检查。。
echo.&call echo 请确保名字库文本和屏蔽字文本的编码格式为ANSI
echo.&call echo 正在生成名字文本，此过程可能需要几分钟。。

for /f tokens^=1^ delims^=^" %%i in ('findstr /v "{ }" NameBase1.txt') do (
	for /f tokens^=1^ delims^=^" %%j in ('findstr /v "{ }" NameBase3.txt') do (
		for /f tokens^=1^ delims^=^" %%k in ('findstr /v "{ }" NameBase2.txt') do (
			echo %%i%%j%%k>>tempNameLib.txt
		)
	)
)

echo.&call echo 正在检查屏蔽字，此过程可能需要几分钟。。
for /f tokens^=1^ delims^=^" %%i in (maskWord.txt) do (
	findstr /n "%%i" tempNameLib.txt>>invalidName.txt
)

del tempNameLib.txt

echo.&call echo 检查结束，请确认invalidName.txt文本中的信息！

pause