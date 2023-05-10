using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 221128 written by gz :
/// AdmobManager
/// 1. 표시해야할 광고의 종류가 다양해진다면 partial에 구현한 하위클래스를 멤버로 Manager에서 제공하는 메소드로만으로도 띄울 수 있도록. 
/// 1-2. 표시해야할 광고가 한정적이라면 멤버 클래스는 지우고 Manager에서 처리해도 상관없다.
/// 2. AppOpenAd 타입의 테스트광고여서 Init 타이밍에 LoadAd()를 호출하고있으나, 광고에 따라 수정될 여지가 있음, 타이밍이슈 주의.
/// </summary>
/// 

public partial class AdMobManager : MonoSingleton<AdMobManager>
{
    interface IAdMobAdObject
    {
        void LoadAd();
        bool ShowAdIfAvailable();
    }
    private static AdMobManager instance = null;
    private static OpenAdObject _appOpenAd = null;
    
    public int adCount = 0;
    public int foreCount = 0;

    /*private readonly Dictionary<AppIdentifier, string> APPIDSTRDICT = new Dictionary<AppIdentifier, string>
    {
        {AppIdentifier.owlGames,  "ca-app-pub-9298155476195333~4500308595"}
    };*/
    /* public static AdMobManager Instance
     {
         get
         {
             if (instance == null)
             {
                 instance = new AdMobManager();
             }

             return instance;
         }
     }*/
    private OpenAdObject appOpenAd
    {
        get
        {
            if (_appOpenAd == null)
                _appOpenAd = new OpenAdObject();
            return _appOpenAd;
        }
    }

    internal void InitAdMobSDK()
    {
        Initialize();
        MobileAds.Initialize((initStatus) =>
        {
            UnityEngine.Debug.Log($"AdMobManager : Admob initStatus = {initStatus}");
            // SDK initialization is complete
            appOpenAd.LoadAd();
        });
    }

    internal void LoadAd()
    {
        appOpenAd.LoadAd();
    }

    internal bool ShowAppOpenAd()
    {
        return appOpenAd.ShowAdIfAvailable();
    }

    //230511 APK에서 AppStateEventNotifier.AppStateChanged 가 정상동작하지 않는 것을 확인.
    //아래 주석을 대체한다.
    void OnApplicationPause(bool pause)
    {
        if (!pause)
            OnAppStateChanged(AppState.Foreground);
    }


    //230511 gz : APK에서 AppStateEventNotifier.AppStateChanged 이벤트가 정상동작하지 않는 것을 확인.
    //AppStateEventNotifier.AppStateChanged에 의존하는 이하 함수들을 주석처리한다.
    /*private void Awake()
    {
        // Use the AppStateEventNotifier to listen to application open/close events.
        // This is used to launch the loaded ad when we open the app.
        AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
    }
    private void OnDestroy()
    {
        // Always unlisten to events when complete.
        AppStateEventNotifier.AppStateChanged -= OnAppStateChanged;
    }
    */

    private void OnAppStateChanged(AppState state)
    {
        Debug.Log("App State changed to : " + state);

        // if the app is Foregrounded and the ad is available, show it.
        if (state == AppState.Foreground)
        {
            foreCount++;
            appOpenAd.ShowAdIfAvailable();
        }
    }
}