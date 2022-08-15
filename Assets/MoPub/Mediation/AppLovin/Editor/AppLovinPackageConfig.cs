using System.Collections.Generic;

public class AppLovinPackageConfig : PackageConfig
{
    public override string Name
    {
        get { return "AppLovin"; }
    }

    public override string Version
    {
        get { return /*UNITY_PACKAGE_VERSION*/"1.8.10"; }
    }

    public override Dictionary<Platform, string> NetworkSdkVersions
    {
        get {
            return new Dictionary<Platform, string> {
                { Platform.ANDROID, /*ANDROID_SDK_VERSION*/"10.3.4" },
                { Platform.IOS, /*IOS_SDK_VERSION*/"10.3.6" }
            };
        }
    }

    public override Dictionary<Platform, string> AdapterClassNames
    {
        get {
            return new Dictionary<Platform, string> {
                { Platform.ANDROID, "com.mopub.mobileads.AppLovin" },
                { Platform.IOS, "AppLovin" }
            };
        }
    }
}
