//
//  OPParam.h
//  OurpalmSDK
//
//  Created by op-mac1 on 13-7-29.
//
//

#ifndef OurpalmSDK_OPParam_h
#define OurpalmSDK_OPParam_h

#include <iostream>
using namespace std;

enum OPUserType{
    kNoneType=0,
    kRegister,                      //注册
    kNormalLogin,                   //常规登陆
    kPhoneLogin,                    //手机登陆
    kEmailLogin,                    //邮箱登陆
    kQuickLogin,                    //快速登陆
    kModifyPassword,                //修改密码
    kBindEmail,                     //绑定邮箱
    kBindAccount,                   //绑定账号
    kGetVerifyCode,                 //获取验证码
    kBindTelephone,                 //绑定手机
    kUnBindTelephone,               //解绑手机
    kModifyBindingTelephone,        //修改绑定手机
    kFindPassword,                  //找回密码
    
    //第三方
    kSina_Type,                     //新浪微博
    kWechat_Type,                   //微信
    kTencent_Type,                  //腾讯微博
    kQQ_Type,                       //QQ
    kFacebook_Type,                 //Facebook
    kTwitter_Type,                  //Twitter
};

enum OPGameType{
    kGameRegister=0,
    kGameLogin,
};

enum OPScreenOrientation{
    OPOrientationLandscapeRight = 1,
    OPOrientationLandscapeLeft,
    OPOrientationPortrait,
    OPOrientationPortraitUpsideDown,
};

enum OPToolBarPlace{
    OPToolBarAtTopLeft = 1,           /**< 左上 */
    OPToolBarAtTopRight,              /**< 右上 */
    OPToolBarAtMiddleLeft,            /**< 左中 */
    OPToolBarAtMiddleRight,           /**< 右中 */
    OPToolBarAtBottomLeft,            /**< 左下 */
    OPToolBarAtBottomRight,           /**< 右下 */
};

class OPInitParam
{
public:
    OPInitParam(){
        mGameResVersion = "";
        mGameOnline = true;
        mDebugModel = false;
        mAllowCharge = true;
        mAutoOrientation = true;
        mScreenOrientation = OPOrientationLandscapeRight;
        mUseAPIInterface = false;
    }
    
    ~OPInitParam(){}
    
    bool mDebugModel;                           //debug模式，注意游戏提交ipa时一定要设置为false
    bool mGameOnline;                           //游戏类型（false：单机  true：网游 ）
    bool mAutoOrientation;                      //自动旋转
    OPScreenOrientation mScreenOrientation;     //界面初始化方向
    bool mUseAPIInterface;                      //使用官网SDK时，需要设置是否使用官网登录API接口（true：使用API接口，false：使用官网界面）
    std::string mGameResVersion;                //游戏资源版本号（可选）
    bool mAllowCharge;                          //设置是否允许充值，主要针对于封挡测试时，不支持充值功能，例如PP渠道
    
};

class OPGameInfo
{
public:
    OPGameInfo(){
        mGame_RoleName = "";
        mGame_RoleId = "";
        mGame_ServerId = "";
        mGame_ServerName = "";
        mGame_RoleLevel= "";
        mGame_RoleVipLevel = "";
    }
    ~OPGameInfo(){}
    
    std::string mGame_RoleName;             //游戏角色名称(必填)
    std::string mGame_RoleId;               //游戏角色id(必填)
    std::string mGame_ServerId;             //游戏服务id（必填）
    std::string mGame_ServerName;           //游戏服名称（必填）
    std::string mGame_RoleLevel;            //游戏角色等级（可选）
    std::string mGame_RoleVipLevel;         //游戏角色vip等级（可选）
};

class OPPurchaseParam
{
public:
    OPPurchaseParam(){
        mPrice = "";
        mCurrencyType = "";
        mPropName = "";
        mPropId = "";
        mPropDescribe = "";
        mPropNum = "";
        mDeleverUrl = "";
        mExtendParams = "";
        mGameRoleLevel = "";
        mGameRoleVipLevel = "";
    }
    
    ~OPPurchaseParam(){}
    
    std::string mPrice;                     //道具价格(必填)，例如，100代表1元，以分为最小单位
    std::string mCurrencyType;              //货币类型(必填)，例如，1：人民币
    std::string mPropName;                  //商品名称(必填)
    std::string mPropId;                    //商品id(必填)
    std::string mPropDescribe;              //商品描述（可选）
    std::string mPropNum;                   //商品数量(必填)
    std::string mDeleverUrl;                //发货地址（可选）
    std::string mExtendParams;              //自定义参数（可选）
    std::string mGameRoleLevel;            //游戏角色等级（可选）
    std::string mGameRoleVipLevel;         //游戏角色vip等级（可选）
};

#endif