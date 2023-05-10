using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using UnityEngine;

public partial class AdMobManager
{
    private class OpenAdObject : IAdMobAdObject
    {

#if UNITY_ANDROID
        private const string AD_UNIT_ID = "ca-app-pub-4375360713005735/8874334654"; // ½ÇÁ¦ ID
        //private const string AD_UNIT_ID = "ca-app-pub-3940256099942544/3419835294"; // Test ID
#elif UNITY_IOS
    private const string AD_UNIT_ID = "ca-app-pub-3940256099942544/5662855259";
#else
    private const string AD_UNIT_ID = "ca-app-pub-3940256099942544/3419835294";
    // private const string AD_UNIT_ID = "unexpected_platform";
#endif
        private AppOpenAd appOpenAd;
        private DateTime _expireTime;

        private bool IsAdAvailable
        {
            get
            {
                return appOpenAd != null
                   && appOpenAd.CanShowAd()
                   && DateTime.Now < _expireTime;
            }
        }
        public void LoadAd()
        {
            // Clean up the old ad before loading a new one.
            if (appOpenAd != null)
            {
                appOpenAd.Destroy();
                appOpenAd = null;
            }

            Debug.Log("Loading the app open ad.");

            // Create our request used to load the ad.
            var adRequest = new AdRequest.Builder().Build();

            // send the request to load the ad.
            AppOpenAd.Load(AD_UNIT_ID, ScreenOrientation.Portrait, adRequest, (AppOpenAd ad, LoadAdError error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("app open ad failed to load an ad " + "with error : " + error);
                        return;
                    }

                    Debug.Log("App open ad loaded with response : " + ad.GetResponseInfo());

                    // App open ads can be preloaded for up to 4 hours.
                    _expireTime = DateTime.Now + TimeSpan.FromHours(4);

                    this.appOpenAd = ad;
                    RegisterEventHandlers(this.appOpenAd);
                });
        }

        private void RegisterEventHandlers(AppOpenAd ad)
        {
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("App open ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () =>
            {
                Debug.Log("App open ad recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                Debug.Log("App open ad was clicked.");
            };
            // Raised when an ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("App open ad full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("App open ad full screen content closed.");

                // Reload the ad so that we can show another as soon as possible.
                LoadAd();
            };
            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("App open ad failed to open full screen content " +
                               "with error : " + error);

                // Reload the ad so that we can show another as soon as possible.
                LoadAd();
            };
        }

        public bool ShowAdIfAvailable()
        {
            if (!IsAdAvailable)
            {
                return false;
            }
            Debug.Log("AdMobManager.ShowIfAvailable() : Show Opening Ad!");
            AdMobManager.Instance.adCount++;
            appOpenAd.Show();
            return true;
        }
    }
}