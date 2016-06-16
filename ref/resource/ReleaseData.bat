@echo off
set CurDir=%cd%
set ToolPath=%CurDir%\tool\

@echo/

@echo 生成游戏数据配置xml：

%ToolPath%DataMaker.exe FunctionSwitch %CurDir%\datasheet\FunctionSwitch.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe MatchMsg %CurDir%\datasheet\MatchMsg.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe PracticePve %CurDir%\datasheet\PracticePve.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Level %CurDir%\datasheet\Level.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Model %CurDir%\datasheet\Model.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe NPC %CurDir%\datasheet\NPC.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe AttrData %CurDir%\datasheet\AttrData.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe BaseData %CurDir%\datasheet\BaseData.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Career %CurDir%\datasheet\Career.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Goods %CurDir%\datasheet\Goods.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Skill %CurDir%\datasheet\Skill.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe HonorCompetition %CurDir%\datasheet\HonorCompetition.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe AwardPack %CurDir%\datasheet\AwardPack.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Store %CurDir%\datasheet\Store.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe BaseDataBuy %CurDir%\datasheet\BaseDataBuy.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Common %CurDir%\datasheet\Common.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Task %CurDir%\datasheet\Task.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Mail %CurDir%\datasheet\Mail.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Practice %CurDir%\datasheet\Practice.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe ArticleStrength %CurDir%\datasheet\ArticleStrength.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe PhRegain %CurDir%\datasheet\PhRegain.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe ConstString %CurDir%\datasheet\ConstString.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe rebound %CurDir%\datasheet\rebound.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Training %CurDir%\datasheet\Training.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe GameMode %CurDir%\datasheet\GameMode.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Tour %CurDir%\datasheet\Tour.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Guide %CurDir%\datasheet\Guide.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe FunctionCondition %CurDir%\datasheet\FunctionCondition.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe MatchAchievement %CurDir%\datasheet\MatchAchievement.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Fashion %CurDir%\datasheet\Fashion.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe FashionShop %CurDir%\datasheet\FashionShop.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe SpecialAction %CurDir%\datasheet\SpecialAction.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Steal %CurDir%\datasheet\Steal.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe CurveRate %CurDir%\datasheet\CurveRate.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe DunkRate %CurDir%\datasheet\DunkRate.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe MassBallRefresh %CurDir%\datasheet\MassBallRefresh.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe PVPPoint %CurDir%\datasheet\PVPPoint.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe VIP %CurDir%\datasheet\VIP.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe AttrReduce %CurDir%\datasheet\AttrReduce.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe AI %CurDir%\datasheet\AI.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe PresentHp %CurDir%\datasheet\PresentHp.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Lottery %CurDir%\datasheet\Lottery.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Sign %CurDir%\datasheet\Sign.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Qualifying %CurDir%\datasheet\Qualifying.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Rank %CurDir%\datasheet\Rank.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe RobotName %CurDir%\datasheet\RobotName.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe TourNPC %CurDir%\datasheet\TourNPC.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe RoleGift %CurDir%\datasheet\RoleGift.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe ServerInfo %CurDir%\datasheet\ServerInfo.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe BullFight %CurDir%\datasheet\BullFight.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Push %CurDir%\datasheet\Push.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Announcement %CurDir%\datasheet\Announcement.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe ShootGame %CurDir%\datasheet\ShootGame.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe NewComerSign %CurDir%\datasheet\NewComerSign.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe FightingCapacity %CurDir%\datasheet\FightingCapacity.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Notice %CurDir%\datasheet\Notice.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Map %CurDir%\datasheet\Map.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Activity %CurDir%\datasheet\Activity.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Trial %CurDir%\datasheet\Trial.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe MVP %CurDir%\datasheet\MVP.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Talent %CurDir%\datasheet\Talent.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Ladder %CurDir%\datasheet\Ladder.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe Badge %CurDir%\datasheet\Badge.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe MatchSound %CurDir%\datasheet\MatchSound.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe MatchMsg %CurDir%\datasheet\MatchMsg.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe TourNew %CurDir%\datasheet\TourNew.xlsm %CurDir%\res%

%ToolPath%DataMaker.exe QualifyingNewer %CurDir%\datasheet\QualifyingNewer.xlsm %CurDir%\res%


%ToolPath%DataMaker.exe PropWarning %CurDir%\datasheet\PropWarning.xlsm %CurDir%\res%

copy %CurDir%\datasheet\GoodsAttr.bytes %CurDir%\res\GoodsAttr.bytes
copy %CurDir%\datasheet\GoodsAttr.dat %CurDir%\res\GoodsAttr.dat   
copy %CurDir%\datasheet\maskWord.txt %CurDir%\res\maskWord.txt  
del %CurDir%\res\GoodsAttr.xml

@echo/

pause
