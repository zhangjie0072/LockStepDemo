#import "UnityAppController.h"
#import "UnityAppController+ViewHandling.h"
#import "UnityAppController+Rendering.h"
#import "iPhone_Sensors.h"

#import <CoreGraphics/CoreGraphics.h>
#import <QuartzCore/QuartzCore.h>
#import <QuartzCore/CADisplayLink.h>
#import <Availability.h>

#import <OpenGLES/EAGL.h>
#import <OpenGLES/EAGLDrawable.h>
#import <OpenGLES/ES2/gl.h>
#import <OpenGLES/ES2/glext.h>

#include <mach/mach_time.h>

// MSAA_DEFAULT_SAMPLE_COUNT was moved to iPhone_GlesSupport.h
// ENABLE_INTERNAL_PROFILER and related defines were moved to iPhone_Profiler.h
// kFPS define for removed: you can use Application.targetFrameRate (30 fps by default)
// DisplayLink is the only run loop mode now - all others were removed

#include "CrashReporter.h"

#include "UI/OrientationSupport.h"
#include "UI/UnityView.h"
#include "UI/Keyboard.h"
#include "UI/SplashScreen.h"
#include "Unity/InternalProfiler.h"
#include "Unity/DisplayManager.h"
#include "Unity/EAGLContextHelper.h"
#include "Unity/GlesHelper.h"
#include "PluginBase/AppDelegateListener.h"
#include "OPGameSDK.h"
#include "json_cpp.h"

bool	_ios42orNewer			= false;
bool	_ios43orNewer			= false;
bool	_ios50orNewer			= false;
bool	_ios60orNewer			= false;
bool	_ios70orNewer			= false;
bool	_ios80orNewer			= false;
bool	_ios81orNewer			= false;
bool	_ios82orNewer			= false;
bool 	_ios90orNewer			= false;
bool 	_ios91orNewer			= false;

// was unity rendering already inited: we should not touch rendering while this is false
bool	_renderingInited		= false;
// was unity inited: we should not touch unity api while this is false
bool	_unityAppReady			= false;
// see if there's a need to do internal player pause/resume handling
//
// Typically the trampoline code should manage this internally, but
// there are use cases, videoplayer, plugin code, etc where the player
// is paused before the internal handling comes relevant. Avoid
// overriding externally managed player pause/resume handling by
// caching the state
bool	_wasPausedExternal		= false;
// should we skip present on next draw: used in corner cases (like rotation) to fill both draw-buffers with some content
bool	_skipPresent			= false;
// was app "resigned active": some operations do not make sense while app is in background
bool	_didResignActive		= false;

// was startUnity scheduled: used to make startup robust in case of locking device
static bool	_startUnityScheduled	= false;

bool	_supportsMSAA			= false;

int _hpRestoreTime = 360;

@implementation UnityAppController

@synthesize unityView				= _unityView;
@synthesize unityDisplayLink		= _unityDisplayLink;

@synthesize rootView				= _rootView;
@synthesize rootViewController		= _rootController;
@synthesize mainDisplay				= _mainDisplay;
@synthesize renderDelegate			= _renderDelegate;
@synthesize quitHandler				= _quitHandler;

#if !UNITY_TVOS
@synthesize interfaceOrientation	= _curOrientation;
#endif

- (id)init
{
	if( (self = [super init]) )
	{
		// due to clang issues with generating warning for overriding deprecated methods
		// we will simply assert if deprecated methods are present
		// NB: methods table is initied at load (before this call), so it is ok to check for override
		NSAssert(![self respondsToSelector:@selector(createUnityViewImpl)],
			@"createUnityViewImpl is deprecated and will not be called. Override createUnityView"
		);
		NSAssert(![self respondsToSelector:@selector(createViewHierarchyImpl)],
			@"createViewHierarchyImpl is deprecated and will not be called. Override willStartWithViewController"
		);
		NSAssert(![self respondsToSelector:@selector(createViewHierarchy)],
			@"createViewHierarchy is deprecated and will not be implemented. Use createUI"
		);
	}
	return self;
}


- (void)setWindow:(id)object		{}
- (UIWindow*)window					{ return _window; }


- (void)shouldAttachRenderDelegate	{}
- (void)preStartUnity				{}


- (void)startUnity:(UIApplication*)application
{
	NSAssert(_unityAppReady == NO, @"[UnityAppController startUnity:] called after Unity has been initialized");

	UnityInitApplicationGraphics();

	// we make sure that first level gets correct display list and orientation
	[[DisplayManager Instance] updateDisplayListInUnity];

	UnityLoadApplication();
	Profiler_InitProfiler();

	[self showGameUI];
	[self createDisplayLink];
    UnitySetPlayerFocus(1);
//    ourpalmpay::OPGameSDK::GetInstance().ApplicationDidFinishLaunchingWithOptions(application, launchOptions);
    
    [self initSDk];
}

//- (void)transitionToViewController:(UIViewController*)vc
//{
//    _rootController.view = nil;
//    vc.view = _rootView;
//    _rootController = vc;
//    _window.rootViewController = vc;
//    
//    [_rootView layoutSubviews];
//}
//- (void)checkOrientationRequest
//{
//    ScreenOrientation requestedOrient = (ScreenOrientation)UnityRequestedScreenOrientation();
////    if(requestedOrient == autorotation)
////    {
////        if(!_isAutorotating)
////        {
////            [self transitionToViewController:[self createRootViewController]];
////            [UIViewController attemptRotationToDeviceOrientation];
////        }
////        _isAutorotating = true;
////    }
////    else
//    {
//        if(requestedOrient != _unityView.contentOrientation)
//            [self orientUnity:requestedOrient];
//        _isAutorotating = false;
//    }
//}
- (void) initSDk
{
    OPInitParam opInfo;
    opInfo.mDebugModel = true;                                  //debug模式，注意游戏提交ipa时一定要设置为false
    opInfo.mAllowCharge = false;                  //设置是否允许充值，主要针对于封挡测试时，不支持充值功能，例如PP渠道
    opInfo.mAutoOrientation = true;          //是否自动旋转
    opInfo.mGameResVersion = "1.0";                             //游戏资源版本号(可选)
    opInfo.mGameOnline = true;                                  //游戏类型（false：单机  true：网游 ）
    opInfo.mScreenOrientation = OPOrientationLandscapeRight;     //界面初始化方向
    opInfo.mUseAPIInterface = false;                            //使用官网SDK时，需要设置是否使用官网登录API接口（true：使用API接口，false：使用官网界面）
    
    
    
    //设置初始化回调
    ourpalmpay::OPGameSDK::GetInstance().InitCallBack(initCallBack);
    //设置登录回调
    ourpalmpay::OPGameSDK::GetInstance().RegisterLoginCallBack(loginCallBack);
    //设置注销回调
    ourpalmpay::OPGameSDK::GetInstance().RegisterLogoutCallBack(logoutCallBack);
    //设置购买回调
    // ourpalmpay::OPGameSDK::GetInstance().SetListener(this);
    //初始化接口
//    ourpalmpay::OPGameSDK::GetInstance().Init(_rootController, opInfo);
    void* ctl = (__bridge void*)_rootController;
    
    NSLog(@"controller is %s",ctl==NULL?"null":"not null");
    
    ourpalmpay::OPGameSDK::GetInstance().Init(ctl,opInfo);
    
    // OPSDK::GetInstance().init(self.viewController,opInfo);
}

void initCallBack(bool result, const char* jsonStr)
{
    cout<<"**********initCallBack**********"<<endl;
    if (result) {
        // 初始化成功
        //OPSDK::GetInstance().ShowMsg("初始化成功");
        cout<<"初始化成功"<<endl;
        ourpalmpay::OPGameSDK::GetInstance().RegisterLogin();
        Json::Value loginJson;
        loginJson["id"] = 1;
        Json::FastWriter fast_writer;
        const char* log = fast_writer.write(loginJson).c_str();
        ourpalmpay::OPGameSDK::GetInstance().SendLog("loginId", "loginKey", log);
    } else {
        //OPSDK::GetInstance().ShowMsg("初始化失败");
        // 初始化失败
        cout<<"初始化失败"<<endl;
    }
}


//登陆回调函数
void loginCallBack(bool result,const char* jsonStr)
{
    cout<<"*********loginCallBack********"<<endl;
    
    if (result) {
        Json::Reader    json_readerEnable;
        Json::Value     json_objectEnable;
        const char* enable = ourpalmpay::OPGameSDK::GetInstance().GetEnableInterface();
        if (!json_readerEnable.parse(enable, json_objectEnable)){
            return;
        }
        
        int  userCenterI = json_objectEnable["EnterPlatform"].asInt();
        int  switchAccountI = json_objectEnable["SwitchAccount"].asInt();
        int  logoutI = json_objectEnable["Logout"].asInt();
        
        NSLog(@"userCenter=%d",userCenterI);
        NSLog(@"switchAccount=%d",switchAccountI);
        NSLog(@"logout=%d",logoutI);
        string userCenter;
        string switchAccount;
        string logout;
        
//        stringstream userCenterS;
//        stringstream switchAccountS;
//        stringstream logoutS;
//        
//        userCenterS << userCenterI;
//        switchAccountS << switchAccountI;
//        logoutS << logoutI;
//        
//        userCenterS >> userCenter;
//        switchAccountS >> switchAccount;
//        logoutS >> logout;
        char buff[500];
        sprintf(buff, "%d",userCenterI);
        userCenter = buff;
        sprintf(buff, "%d",switchAccountI);
        switchAccount = buff;
        sprintf(buff, "%d",logoutI);
        logout = buff;
        
        Json::Reader    json_reader;
        Json::Value     json_object;
        
        if (!json_reader.parse(jsonStr, json_object)){
            return;
        }
        
        string opUserId = json_object["id"].asCString();//掌趣分配的用户唯一ID
        if (json_object["userName"].asString().length() == 0) {
            //快速登录返回的用户名为“”
            // OPSDK::GetInstance().ShowMsg("快登成功");
            NSLog(@"Login ok");
            
        }else{
            //OPSDK::GetInstance().ShowMsg("登录成功");
        }
        
        cout<<jsonStr<<endl;
        string palmId = json_object["returnJson"]["palmId"].asString();
        NSLog(@"palmId=%s",palmId.c_str());
        
        string nickName = json_object["returnJson"]["nickName"].asString();
        NSLog(@"nickName=%s",nickName.c_str());
        
        
        
        string tokenId = json_object["tokenId"].asString();
        NSLog(@"tokenId=%s",tokenId.c_str());
        
        
        string userName = json_object["userName"].asString();
        NSLog(@"userName=%s",userName.c_str());
        
        string sendStr =  opUserId +"&" + tokenId +"&"+  nickName+"&"+ userName + "&" + ourpalmpay::OPGameSDK::GetInstance().GetServiceId() + "&" + userCenter + "&" + switchAccount + "&" + logout;
        NSLog(@"Before UnitySendMessage");
        UnitySendMessage("UILogo(Clone)", "HandleResp", sendStr.c_str());
        UnitySendMessage("UILogin(Clone)", "HandleResp", sendStr.c_str());
        
        NSLog(@"After UnitySendMessage");
        //用户登录成功，进入创建角色界面
        // [viewController closeUserLoginView];
        //[viewController showGameLoginView];
        
        
    }else{
        // OPSDK::GetInstance().ShowMsg("登录失败");
        //登陆失败
        cout<<"失败！"<<endl;
    }
}


#if defined (__cplusplus)
extern "C"
{
#endif
    
    void switchAccount()
    {
        NSLog(@"switchAccount---------");
        ourpalmpay::OPGameSDK::GetInstance().SwitchAccount();
    }
    
    void userFeedback()
    {
        ourpalmpay::OPGameSDK::GetInstance().UserFeedback();
    }
    
    void setLoginInfo(bool isLogin,const char* roleId,const char* roleName, const char* serverId,const char* serverName,const char* playerLevel,const char* vipLevel )
    {
        NSLog(@"setLoginInfo roleId=%s",roleId);
        NSLog(@"setLoginInfo roleName=%s",roleName);
        NSLog(@"setLoginInfo serverId=%s",serverId);
        NSLog(@"setLoginInfo serverName=%s",serverName);
        
        OPGameInfo gameInfo;
        gameInfo.mGame_RoleId = roleId;
        gameInfo.mGame_RoleName = roleName;
        gameInfo.mGame_ServerId = serverId;
        gameInfo.mGame_ServerName = serverName;
        gameInfo.mGame_RoleLevel = playerLevel;
        gameInfo.mGame_RoleVipLevel = vipLevel;
        
        if( isLogin)
        {
            ourpalmpay::OPGameSDK::GetInstance().SetGameLoginInfo(gameInfo, kGameLogin);
        }
        else
        {
            ourpalmpay::OPGameSDK::GetInstance().SetGameLoginInfo(gameInfo, kGameRegister);
        }
    }
    
    
    void sendGoodsLog(int roleLv, int vipLv, const char* updateType, const char* itemId, const char* itemName, int itemCount, const char* custom)
    {
        NSLog(@"roleLv=%d",roleLv);
        NSLog(@"vipLv=%d",vipLv);
        NSLog(@"updateType=%s",updateType);
        NSLog(@"itemId=%s",itemId);
        NSLog(@"itemName=%s",itemName);
        NSLog(@"itemCount=%d",itemCount);
        NSLog(@"custom=%s",custom);
        
        
        Json::Value logJson;
        logJson["roleLevel"] = roleLv;
        logJson["roleVipLevel"] = vipLv;
        logJson["updateType"] = updateType;
        logJson["itemId"] = itemId;
        logJson["itemName"] = itemName;
        logJson["itemCount"] = itemCount;
        logJson["custom"] = custom;
        
        Json::FastWriter fast_writer;
        const char* log = fast_writer.write(logJson).c_str();
        ourpalmpay::OPGameSDK::GetInstance().SendLog("9", "role-item-update", log);
    }
    
    
    void sendPlayerLog(int roleLv, int vipLv, const char* propKey, const char* propValue, const char* rangeAbility )
    {
        NSLog(@"roleLv=%d",roleLv);
        NSLog(@"vipLv=%d",vipLv);
        NSLog(@"propKey=%s",propKey);
        NSLog(@"propValue=%s",propValue);
        NSLog(@"rangeAbility=%s",rangeAbility);
        
        Json::Value logJson;
        logJson["roleLevel"] = roleLv;
        logJson["roleVipLevel"] = vipLv;
        logJson["propKey"] = propKey;
        logJson["propValue"] = propValue;
        logJson["rangeability"] = rangeAbility;
        Json::FastWriter fast_writer;
        const char* log = fast_writer.write(logJson).c_str();
        ourpalmpay::OPGameSDK::GetInstance().SendLog("10", "role-prop-update", log);
    }
    
    void openPlayerPlat()
    {
        ourpalmpay::OPGameSDK::GetInstance().EnterPlatform();
    }
    
    
    NSDate* GetWeekDate(int weekDay, int hour, int minute)
    {
        NSDate *date = [NSDate date];
        
        NSTimeZone *zone = [NSTimeZone timeZoneWithName:@"Asia/Shanghai"];
        NSInteger interval = [zone secondsFromGMTForDate: date];
        NSDate *localeDate = [date  dateByAddingTimeInterval: interval];
        
        NSCalendar * calendar = [NSCalendar currentCalendar];
        calendar.timeZone = [NSTimeZone timeZoneWithName:@"Asia/Shanghai"];
        
        NSDateComponents * nowComponent = [calendar components:
                                           NSCalendarUnitEra |
                                           NSCalendarUnitYear |
                                           NSCalendarUnitMonth |
                                           NSCalendarUnitDay |
                                           NSCalendarUnitHour|
                                           NSCalendarUnitMinute |
                                           NSCalendarUnitSecond |
                                           NSCalendarUnitWeekOfMonth |
                                           NSCalendarUnitWeekday |
                                           NSCalendarUnitWeekdayOrdinal |
                                           NSCalendarUnitQuarter
                                                      fromDate:localeDate];
        int oriWeekDay = nowComponent.weekday;
        int nowHour = (int)nowComponent.hour - 8;
        if( nowHour < 0 )
        {
            nowHour += 24;
        }
        [nowComponent setHour:nowHour];
        [nowComponent setWeekday:oriWeekDay];
        
        NSDateComponents *targetComponent = [calendar components:NSCalendarUnitEra |
                                             NSCalendarUnitYear |
                                             NSCalendarUnitMonth |
                                             NSCalendarUnitDay |
                                             NSCalendarUnitHour|
                                             NSCalendarUnitMinute |
                                             NSCalendarUnitSecond |
                                             NSCalendarUnitWeekOfMonth |
                                             NSCalendarUnitWeekday |
                                             NSCalendarUnitWeekdayOrdinal |
                                             NSCalendarUnitQuarter fromDate:localeDate];
        [targetComponent setHour:hour];
        [targetComponent setMinute:minute];
        [targetComponent setSecond:0];
        [targetComponent setWeekday:oriWeekDay];
        int temp = weekDay - (int)nowComponent.weekday;
        
        int days = -1;
        if( temp > 0 )
        {
            days = temp;
        }
        else if( temp < 0 )
        {
            days = temp + 7;
        }
        else
        {
            // same weekday.
            if( hour > nowComponent.hour)
            {
                days = temp;
            }
            else if( hour < nowComponent.hour)
            {
                days = temp + 7;
            }
            else
            {
                // same hour.
                if( minute > nowComponent.minute)
                {
                    days = temp;
                }
                else if ( minute <= nowComponent.minute )
                {
                    days = temp + 7;
                }
            }
        }
        
        assert( days >= 0);
        
        NSDate *newFireDate = [[calendar dateFromComponents:targetComponent] dateByAddingTimeInterval:3600 * 24 * days];
        return newFireDate;
    }
    
    void setHpRestoreTime(int restoreTime)
    {
        _hpRestoreTime = restoreTime;
        NSLog(@"_hpRestoreTime=%d",_hpRestoreTime);
    }
    
    void recvPush(int identifier, int type, const char* date, const char* time, int online, const char* content)
    {
        UILocalNotification *localNotity = [[UILocalNotification alloc] init];
        localNotity.soundName = UILocalNotificationDefaultSoundName;
        localNotity.alertBody = [NSString stringWithUTF8String:content];
        localNotity.hasAction = NO;
        localNotity.timeZone = [NSTimeZone defaultTimeZone];
        
        if( type == 1 )
        {
            NSDateFormatter *formatter = [[NSDateFormatter alloc] init];
            [formatter setDateFormat:@"HH:mm"];
            NSString *nsTime = [[NSString alloc]initWithUTF8String:time];
            localNotity.fireDate  = [formatter dateFromString:nsTime];
            localNotity.repeatInterval = kCFCalendarUnitDay;
        }
        else if( type == 2)
        {
            NSDateFormatter *formatter = [[NSDateFormatter alloc] init];
            [formatter setDateFormat:@"HH:mm"];
            NSString *nsStrTime= [[NSString alloc]initWithUTF8String:time];
            NSArray *timeSplit = [nsStrTime componentsSeparatedByString:@":"];
            NSString *nsStrHour = [timeSplit objectAtIndex:0];
            NSString *nsStrMinute = [timeSplit objectAtIndex:1];
            
            int day = atoi(date);
            day++;
            day = day == 8 ? 1: day;
            int hour = [nsStrHour intValue];
            int minute = [nsStrMinute intValue];
            
            localNotity.fireDate  = GetWeekDate(day, hour, minute);
            localNotity.repeatInterval = NSCalendarUnitWeekday;
        }
        else if( type == 3)
        {
            NSDateFormatter *formatter = [[NSDateFormatter alloc] init];
            [formatter setDateFormat:@"MM/dd/yyyy-HH:mm"];
            NSString *nsTime = [[NSString alloc]initWithUTF8String:time];
            NSString *nsDate = [[NSString alloc]initWithUTF8String:date];
            NSString *nsResultData = [[NSString alloc] initWithFormat:@"%@-%@", nsDate, nsTime];
            localNotity.fireDate  = [formatter dateFromString:nsResultData];
            localNotity.repeatInterval = 0;
        }
        else if( type == 4)
        {
            for (UILocalNotification *noti in [[UIApplication sharedApplication] scheduledLocalNotifications]) {
                NSString *notiID = [noti.userInfo objectForKey:@"identifier"];
                NSString *receiveNotiID = @"hp";
                if ([notiID isEqualToString:receiveNotiID]) {
                    [[UIApplication sharedApplication] cancelLocalNotification:noti];
                }
            }
            
            if( date == NULL || strcmp(date,"")== 0 )
            {
                return;
            }
            int hp = atoi(date);
            int fullHP = atoi(time);
            if( hp >= fullHP )
            {
                return;
            }
            
            int hpOff = (fullHP - hp) * _hpRestoreTime;
            localNotity.fireDate  = [NSDate dateWithTimeIntervalSinceNow:hpOff];
            localNotity.repeatInterval = 0;
            NSMutableDictionary * aUserInfo = [[NSMutableDictionary alloc] init];
            [aUserInfo setObject:@"hp" forKey:@"identifier"];
            localNotity.userInfo = aUserInfo;
        }
        
        [[UIApplication sharedApplication] scheduleLocalNotification:localNotity];
    }
    
    
#if defined (__cplusplus)
}
#endif




void logoutCallBack(bool result, const char* jsonStr)
{
    if (result) {
        if (jsonStr) {
            Json::Reader    json_reader;
            Json::Value     json_object;
            if (!json_reader.parse(jsonStr, json_object)){
                return;
            }
            const char *type = json_object["Type"].asCString();
            if (type && strcmp(type, "Logout") == 0) {
                cout<<"logoutCallBack：注销！"<<endl;
                //                OPSDK::GetInstance().ShowMsg("注销成功");
            }else{
                //                cout<<"logoutCallBack：切换账号！"<<endl;
            }
            
            //注销成功，进入登录页面
            //            [viewController closeGameView];
            //            [viewController showUserLoginView];
            
        }else{
            cout<<"logoutCallBack：注销！"<<endl;
            //            OPSDK::GetInstance().ShowMsg("注销失败");
        }
    }
}

extern "C" void UnityRequestQuit()
{
	_didResignActive = true;
	if (GetAppController().quitHandler)
		GetAppController().quitHandler();
	else
		exit(0);
}

#if !UNITY_TVOS
- (NSUInteger)application:(UIApplication*)application supportedInterfaceOrientationsForWindow:(UIWindow*)window
{
	// UIInterfaceOrientationMaskAll
	// it is the safest way of doing it:
	// - GameCenter and some other services might have portrait-only variant
	//     and will throw exception if portrait is not supported here
	// - When you change allowed orientations if you end up forbidding current one
	//     exception will be thrown
	// Anyway this is intersected with values provided from UIViewController, so we are good
    if (ourpalmpay::OPGameSDK::GetInstance().ApplicationSupportedInterfaceOrientationsForWindow()) {
        return ourpalmpay::OPGameSDK::GetInstance().ApplicationSupportedInterfaceOrientationsForWindow((__bridge void*)application, (__bridge void*)window);
    }
    return UIInterfaceOrientationMaskAll;
    
//	return   (1 << UIInterfaceOrientationPortrait) | (1 << UIInterfaceOrientationPortraitUpsideDown)
//		   | (1 << UIInterfaceOrientationLandscapeRight) | (1 << UIInterfaceOrientationLandscapeLeft);
}
#endif

#if !UNITY_TVOS
- (void)application:(UIApplication*)application didReceiveLocalNotification:(UILocalNotification*)notification
{
	AppController_SendNotificationWithArg(kUnityDidReceiveLocalNotification, notification);
	UnitySendLocalNotification(notification);
}
#endif

- (void)application:(UIApplication*)application didReceiveRemoteNotification:(NSDictionary*)userInfo
{
	AppController_SendNotificationWithArg(kUnityDidReceiveRemoteNotification, userInfo);
	UnitySendRemoteNotification(userInfo);
}

- (void)application:(UIApplication*)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData*)deviceToken
{
	AppController_SendNotificationWithArg(kUnityDidRegisterForRemoteNotificationsWithDeviceToken, deviceToken);
	UnitySendDeviceToken(deviceToken);
}

#if !UNITY_TVOS
- (void)application:(UIApplication *)application didReceiveRemoteNotification:(NSDictionary *)userInfo fetchCompletionHandler:(void (^)(UIBackgroundFetchResult result))handler
{
	AppController_SendNotificationWithArg(kUnityDidReceiveRemoteNotification, userInfo);
	UnitySendRemoteNotification(userInfo);
	if (handler)
	{
		handler(UIBackgroundFetchResultNoData);
	}
}
#endif

- (void)application:(UIApplication*)application didFailToRegisterForRemoteNotificationsWithError:(NSError*)error
{
	AppController_SendNotificationWithArg(kUnityDidFailToRegisterForRemoteNotificationsWithError, error);
	UnitySendRemoteNotificationError(error);
}
//
- (BOOL)application:(UIApplication*)application openURL:(NSURL*)url sourceApplication:(NSString*)sourceApplication annotation:(id)annotation
{
	NSMutableArray* keys	= [NSMutableArray arrayWithCapacity:3];
	NSMutableArray* values	= [NSMutableArray arrayWithCapacity:3];

	#define ADD_ITEM(item)	do{ if(item) {[keys addObject:@#item]; [values addObject:item];} }while(0)

	ADD_ITEM(url);
	ADD_ITEM(sourceApplication);
	ADD_ITEM(annotation);

	#undef ADD_ITEM

	NSDictionary* notifData = [NSDictionary dictionaryWithObjects:values forKeys:keys];
	AppController_SendNotificationWithArg(kUnityOnOpenURL, notifData);
    
    if(ourpalmpay::OPGameSDK::GetInstance().HandleOpenURL((__bridge void*)application,(__bridge void*)url,(__bridge void*)sourceApplication,(__bridge void*)annotation)) {
        return YES;
    }
    return NO;
}

-(BOOL)application:(UIApplication*)application willFinishLaunchingWithOptions:(NSDictionary*)launchOptions
{
	return YES;
}

- (BOOL)application:(UIApplication*)application didFinishLaunchingWithOptions:(NSDictionary*)launchOptions
{

	::printf("-> applicationDidFinishLaunching()\n");

	// send notfications
#if !UNITY_TVOS
	if(UILocalNotification* notification = [launchOptions objectForKey:UIApplicationLaunchOptionsLocalNotificationKey])
		UnitySendLocalNotification(notification);

	if(NSDictionary* notification = [launchOptions objectForKey:UIApplicationLaunchOptionsRemoteNotificationKey])
		UnitySendRemoteNotification(notification);

	if ([UIDevice currentDevice].generatesDeviceOrientationNotifications == NO)
		[[UIDevice currentDevice] beginGeneratingDeviceOrientationNotifications];
#endif

	UnityInitApplicationNoGraphics([[[NSBundle mainBundle] bundlePath] UTF8String]);

	[self selectRenderingAPI];
	[UnityRenderingView InitializeForAPI:self.renderingAPI];

	_window			= [[UIWindow alloc] initWithFrame:[UIScreen mainScreen].bounds];
	_unityView		= [self createUnityView];

	[DisplayManager Initialize];
	_mainDisplay	= [DisplayManager Instance].mainDisplay;
	[_mainDisplay createWithWindow:_window andView:_unityView];

	[self createUI];
    [self preStartUnity];

	// if you wont use keyboard you may comment it out at save some memory
    [KeyboardDelegate Initialize];
    ourpalmpay::OPGameSDK::GetInstance().ApplicationDidFinishLaunchingWithOptions((__bridge void*)application, (__bridge void*)launchOptions);

	return YES;
}

- (void)applicationDidEnterBackground:(UIApplication*)application
{
	::printf("-> applicationDidEnterBackground()\n");
    
    ourpalmpay::OPGameSDK::GetInstance().ShowPausePage();

}
- (BOOL)application:(UIApplication *)application handleOpenURL:(NSURL *)url
{
    if (ourpalmpay::OPGameSDK::GetInstance().HandleOpenURL((__bridge void*)url)) {
        return YES;
    }
    return NO;
}


- (void)applicationWillEnterForeground:(UIApplication*)application
{
	::printf("-> applicationWillEnterForeground()\n");

	// applicationWillEnterForeground: might sometimes arrive *before* actually initing unity (e.g. locking on startup)
	if(_unityAppReady)
	{
		// if we were showing video before going to background - the view size may be changed while we are in background
		[GetAppController().unityView recreateGLESSurfaceIfNeeded];
	}
}

- (void)applicationDidBecomeActive:(UIApplication*)application
{
	::printf("-> applicationDidBecomeActive()\n");

	if(_snapshotView)
	{
		[_snapshotView removeFromSuperview];
		_snapshotView = nil;
	}

	if(_unityAppReady)
	{
		if(UnityIsPaused() && _wasPausedExternal == false)
		{
			UnityPause(0);
			UnityWillResume();
		}
		UnitySetPlayerFocus(1);
	}
	else if(!_startUnityScheduled)
	{
		_startUnityScheduled = true;
		[self performSelector:@selector(startUnity:) withObject:application afterDelay:0];
	}

	_didResignActive = false;
}

- (void)applicationWillResignActive:(UIApplication*)application
{
	::printf("-> applicationWillResignActive()\n");

	if(_unityAppReady)
	{
		UnitySetPlayerFocus(0);

		_wasPausedExternal = UnityIsPaused();
		if (_wasPausedExternal == false)
		{
			// do pause unity only if we dont need special background processing
			// otherwise batched player loop can be called to run user scripts
			int bgBehavior = UnityGetAppBackgroundBehavior();
			if(bgBehavior == appbgSuspend || bgBehavior == appbgExit)
			{
				// Force player to do one more frame, so scripts get a chance to render custom screen for minimized app in task manager.
				// NB: UnityWillPause will schedule OnApplicationPause message, which will be sent normally inside repaint (unity player loop)
				// NB: We will actually pause after the loop (when calling UnityPause).
				UnityWillPause();
				[self repaint];
				UnityPause(1);

				_snapshotView = [self createSnapshotView];
				if(_snapshotView)
					[_rootView addSubview:_snapshotView];
			}
		}
	}

	_didResignActive = true;
}

- (void)applicationDidReceiveMemoryWarning:(UIApplication*)application
{
	::printf("WARNING -> applicationDidReceiveMemoryWarning()\n");
}

- (void)applicationWillTerminate:(UIApplication*)application
{
	::printf("-> applicationWillTerminate()\n");

	Profiler_UninitProfiler();
	UnityCleanup();

	extern void SensorsCleanup();
	SensorsCleanup();
}

@end


void AppController_SendNotification(NSString* name)
{
	[[NSNotificationCenter defaultCenter] postNotificationName:name object:GetAppController()];
}
void AppController_SendNotificationWithArg(NSString* name, id arg)
{
	[[NSNotificationCenter defaultCenter] postNotificationName:name object:GetAppController() userInfo:arg];
}
void AppController_SendUnityViewControllerNotification(NSString* name)
{
	[[NSNotificationCenter defaultCenter] postNotificationName:name object:UnityGetGLViewController()];
}

extern "C" UIWindow*			UnityGetMainWindow()		{ return GetAppController().mainDisplay.window; }
extern "C" UIViewController*	UnityGetGLViewController()	{ return GetAppController().rootViewController; }
extern "C" UIView*				UnityGetGLView()			{ return GetAppController().unityView; }
extern "C" ScreenOrientation	UnityCurrentOrientation()	{ return GetAppController().unityView.contentOrientation; }



bool LogToNSLogHandler(LogType logType, const char* log, va_list list)
{
	NSLogv([NSString stringWithUTF8String:log], list);
	return true;
}

void UnityInitTrampoline()
{
#if ENABLE_CRASH_REPORT_SUBMISSION
	SubmitCrashReportsAsync();
#endif
	InitCrashHandling();

	NSString* version = [[UIDevice currentDevice] systemVersion];

	// keep native plugin developers happy and keep old bools around
	_ios42orNewer = true;
	_ios43orNewer = true;
	_ios50orNewer = true;
	_ios60orNewer = true;
	_ios70orNewer = [version compare: @"7.0" options: NSNumericSearch] != NSOrderedAscending;
	_ios80orNewer = [version compare: @"8.0" options: NSNumericSearch] != NSOrderedAscending;
	_ios81orNewer = [version compare: @"8.1" options: NSNumericSearch] != NSOrderedAscending;
	_ios82orNewer = [version compare: @"8.2" options: NSNumericSearch] != NSOrderedAscending;
	_ios90orNewer = [version compare: @"9.0" options: NSNumericSearch] != NSOrderedAscending;
	_ios91orNewer = [version compare: @"9.1" options: NSNumericSearch] != NSOrderedAscending;

	// Try writing to console and if it fails switch to NSLog logging
	::fprintf(stdout, "\n");
	if(::ftell(stdout) < 0)
		UnitySetLogEntryHandler(LogToNSLogHandler);
}

