//
//  OPGameSDK.h
//  OurpalmSDK
//
//  Created by op-mac1 on 13-7-29.
//
//

#ifndef __OurpalmSDK__OPGameSDK__
#define __OurpalmSDK__OPGameSDK__

#include "OPParam.h"
#include "OPPurchaseListener.h"
#include <iostream>

using namespace std;
//

namespace ourpalmpay {
    
    class OPGameSDK
    {
    private:
        OPGameSDK();
        ~OPGameSDK();
        
    public:
        inline static OPGameSDK& GetInstance(){
            static OPGameSDK pur;
            return pur;
        }
        
    public:
        
        //返回SDK可用功能(chargtype 支付类型ID)
        /**
         *	@brief	返回SDK可用功能
         *	例如：RegisterLogin 登录方式(0:不使用，1：使用)
         */
        const char*  GetEnableInterface();
        
        //初始化（含自动调用更新接口）
        void Init(void* controller,OPInitParam opInfo);
        void InitCallBack(void (* pf)(bool result,const char* jsonStr));
        void CallBackInit(bool result,const char* jsonStr) { (* pfuncInit)(result,jsonStr); }
        
        //注册登录
        void RegisterLogin(OPUserType sdkType=kNoneType);
        void RegisterLoginCallBack(void (* pf)(bool result,const char* jsonStr));
        void CallBackLogin(bool result,const char* jsonStr) { (* pfuncLogin)(result,jsonStr); }
        
        //登录状态
        bool IsLogin();
        
        //设置游戏角色信息
        void SetGameLoginInfo(OPGameInfo opGameInfo,OPGameType opGameType);
        
        //注销
        void RegisterLogoutCallBack(void (* pf)(bool result,const char* jsonStr));
        void CallBackLogout(bool result,const char* jsonStr) { (* pfuncLogout)(result,jsonStr); }
        void LogOut();
        
        //官网用户设置
        void UserSettingCallBack(void (* pf)(bool result,const char* jsonStr));
        void CallBackUserSetting(bool result,const char* jsonStr) { (* pfuncUserSetting)(result,jsonStr); }
        
        //发送日志
        void SendLog(const char* logID, const char* logKey ,const char* logValJson);
        
        //设置特殊属性map
        void SetSpecKey(const char* specKeyJson);
        
        //计费（异步方式）
        void SetListener(PurchaseListener* listener);
        
        //购买
        void Purchase(OPPurchaseParam params);
        
        //进入平台中心
        void EnterPlatform();
        //用户反馈
        void UserFeedback();
        //分享到第三方平台
        void ShareToThirdPlatform(const char* jsonContent,OPUserType sdkType=kNoneType);
        //设置本地通知
        void SetLocalNotification(int second,const char* desc);
        //取消本地通知
        void CancelLoaclNotification();
        //设置捕获会话过期的通知
        void SessionInvalidCallBack(void (* pf)());
        void CallBackSessionInvalid(){ (* pfuncSessionInvalid)(); }
        
        //启动接口
        bool ApplicationDidFinishLaunchingWithOptions(void *application,void *launchOptions);
        
        //界面适配接口
        bool ApplicationSupportedInterfaceOrientationsForWindow();
        unsigned int ApplicationSupportedInterfaceOrientationsForWindow(void *application,void *window);
        
        void RotationWithInfo(OPScreenOrientation rotation);
        
        //获取好友接口
        void GetFriendsList(void (* pf)(bool result,const char* jsonStr));
        void CallBackFriendsList(bool result,const char* jsonStr) { (* pfuncLogin)(result,jsonStr); }
        
        //礼包码接口
        void ExchangeGameCode(const char *gamecode,const char *deleverUrl,const char *extendParams);
        
        //SDK信息
        const char*  GetChannelInfo();
        
        // 泰奇需要在登录或注册之后
        void SetValidProductIds(void* productIDs);
        
        //进入平台中心各模块*******************
        //进入好友中心
        void EnterFriendCenter();
        //进入好友空间
        void EnterUserSpace(const char* uin);
        //进入游戏大厅
        void EnterAppCenter();
        //进入指定游戏的主页
        void EnterAppCenter(int appId);
        //进入设置界面
        //kNoneType：默认为进入用户设置界面
        void EnterUserSetting(OPUserType sdkType=kNoneType);
        
        //进入邀请好友界面
        void EnterInviteFriendCenter(const char* desc);
        //进入引用论坛界面
        void EnterAppBBS();
        //进入充值界面
        void EnterRechargeCenter();
        //在游戏暂停或者从后台恢复的时候显示暂停页
        void ShowPausePage();
        
        
        //暂时不用的接口
        //检查更新
        void CheckUpdate();
        //切换账号
        void SwitchAccount();
        
        bool HandleOpenURL(void* url);
        void HandleOpenURL(void* url, void* application);
        bool HandleOpenURL(void* application,void* url,void* sourceApplication,void* annotation);
        void ApplicationWillTerminate(void* application);
        void ApplicationDidBecomeActive(void* application);
        void ExtendAccessTokenIfNeeded();
        
        //显示和隐藏工具栏
        void ShowToolBar(OPToolBarPlace toolBarPlace);
        void HideToolBar();
        
        /* 官网登录界面相关接口 */
        /****************************************************/
        //注册
        void Register(const char* userName, const char* pswd);
        //登录
        void Login(const char* userName, const char* pswd);
        //快速登录
        void QuickLogin();
        //绑定邮箱
        void BindEmail(const char* userId,const char* email,int action);
        //获取验证码
        void GetVerifyCode(const char* phoneNum);
        //绑定手机
        void BindTelephone(const char* phoneNum, const char* verifyCode, const char* pswd);
        //手机解绑定
        void UnbindTelephone(const char* password, const char* verifyCode);
        //修改绑定手机号
        void ModifyBindingTelephone(const char* newMobile, const char* code, const char* password);
        //找回密码
        void FindPassword(const char* username);
        /****************************************************/
        
        //推送通知
        void AppliactionDidRegisterForRemoteNotificationsWithDeviceToken(void *application,void *deviceToken);
        //推送通知失败
        void AppliactionDidFailToRegisterForRemoteNotificationsWithError(void *application,void *error);
        //业务id
        string GetServiceId();
        //推广渠道id
        string GetChannelId();
        //推广渠道Name
        string GetChannelName();
        //机型组id
        string GetDeviceGroupId();
        //地区id
        string GetLocaleId();
        
    public:
        OPUserType mCurSdkType;
        //是否旋转
        bool mIsOrientation;
        
    private:
        void (* pfuncInit)(bool result,const char * jsonStr);
        void (* pfuncLogin)(bool result,const char * jsonStr);
        void (* pfuncLogout)(bool result,const char * jsonStr);
        void (* pfuncUserSetting)(bool result,const char * jsonStr);
        void (* pfuncSessionInvalid)();
        void (* pfuncFriendList)(bool result,const char * jsonStr);
        bool mIsInit;                        //是否初始化
        bool mIsRegisterFun;                 //是否设置登陆注册的回调函数
        bool mIsSetGameLoginInfo;            //是否设置游戏的角色信息
    };
}

#endif /* defined(__OurpalmSDK__OPGameSDK__) */
