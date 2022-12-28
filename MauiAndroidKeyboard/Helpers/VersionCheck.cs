using MauiAndroidKeyboard.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAndroidKeyboard.Helpers
{
    public class VersionCheck
    {
        private static readonly VersionCheck instance = new VersionCheck();
        Version versionServer;
        Version versionClient;

        string url = GlobalSetting.Instance.ApkVerUri;

        private VersionCheck()
        {
            VersionTracking.Track();

            GetVersionClient();
        }

        public static VersionCheck Instance
        {
            get => instance;
        }

        public bool IsNetworkAccess()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<Version> GetVersionServer()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(new Uri(url));

                    if (response.IsSuccessStatusCode)
                    {
                        versionServer = new Version(response.Content.ReadAsStringAsync().Result.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return versionServer;
        }

        public async Task<bool> IsUpdate()
        {
            await GetVersionServer();

            if (versionServer > versionClient)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task UpdateCheck()
        {
            if (versionServer == null)
            {
                await GetVersionServer();
            }

            if (versionServer > versionClient)
            {
                AutoUpdatePage autoUpdateView = new AutoUpdatePage();
                await Application.Current.MainPage.Navigation.PushModalAsync(autoUpdateView);
            }
        }

        /// <summary>
        /// Client 버전 확인
        /// </summary>
        /// <returns></returns>
        public Version GetVersionClient()
        {
            versionClient = new Version(VersionTracking.CurrentVersion);

            return versionClient;
        }

    }
}
