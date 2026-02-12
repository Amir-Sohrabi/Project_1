using System;
using System.IO;
using System.Threading;
using _Scripts.DataBase.Scripts.Enums;
using _Scripts.Utils;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace _Scripts.DataBase.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "DataHub", menuName = "Scriptable Objects/DataHub")]
    public class DataHub : ScriptableObject
    {
        public UserProfile userProfile = new();
        public UserAccount userAccount = new();

        #region GamePlay Methods

        public void AddCurrency(int amount, ECurrencyType eCurrencyType, bool isIAP)
        {
            switch (eCurrencyType)
            {
                default:
                case ECurrencyType.None:
                    break;
                case ECurrencyType.Coin:
                    if (isIAP)
                        userAccount.iapCoin += amount;
                    else
                        userProfile.earnedCoin += amount;
                    break;
                case ECurrencyType.Gem:
                    if (isIAP)
                        userAccount.iapGem += amount;
                    else
                        userProfile.earnedGem += amount;
                    break;
            }
        }

        public void DecreaseCurrency(int value, ECurrencyType eCurrencyType)
        {
            switch (eCurrencyType)
            {
                default:
                case ECurrencyType.None:
                    return;
                case ECurrencyType.Coin:
                    userProfile.spentCoin += value;
                    break;
                case ECurrencyType.Gem:
                    userProfile.spentGem += value;
                    break;
            }

            SaveProfile(CancellationToken.None).Forget();
        }
        public void OverrideCurrency(int value, ECurrencyType eCurrencyType)
        {
            switch (eCurrencyType)
            {
                default:
                case ECurrencyType.None:
                    return;
                case ECurrencyType.Coin:
                    userProfile.earnedCoin = value;
                    break;
                case ECurrencyType.Gem:
                    userProfile.earnedGem = value;
                    break;
            }

            SaveProfile(CancellationToken.None).Forget();
        }
        public long GetCurrencyBalance(ECurrencyType type)
        {
            var account = userAccount;
            var profile = userProfile;
            switch (type)
            {
                default:
                case ECurrencyType.None:
                    return 0;
                case ECurrencyType.Coin:
                    return account.iapCoin + profile.earnedCoin - profile.spentCoin;
                case ECurrencyType.Gem:
                    return account.iapGem + profile.earnedGem - profile.spentGem;
            }
        }

        #endregion
        
        #region MainMethods

        private static string AccountSavePath => Application.persistentDataPath + "/UserAccount.json";
        private static string ProfileSavePath => Application.persistentDataPath + "/UserProfile.json";

        [JsonIgnore] private CancellationTokenSource _latestProfileSaveCts; // new
        [JsonIgnore] private readonly object _saveProfileLock = new();      // new

        public async UniTask LoadFiles(CancellationToken cancellationToken)
        {
            var accountPath = AccountSavePath;
            var profilePath = ProfileSavePath;

            if (!File.Exists(accountPath))
            {
                Debug.Log("Account Save file not found!");
                return;
            }

            Debug.Log("Start loading User Account Data process.");

            await UniTask.RunOnThreadPool(() =>
            {
                var jsonWithChecksum = File.ReadAllText(accountPath);
                var json = jsonWithChecksum.EnsureChecksum();
                if (string.IsNullOrEmpty(json))
                {
                    /*Debug.Log("Account Save file Checksum Failed", Color.red);
                    if (PlayServicesManager.Instance != null && !string.IsNullOrEmpty(PlayServicesManager.UserGoogleId))
                    {
                        PlayServicesManager.Instance.LoadString(PlayServicesManager.SaveJsonKey.UserProfile, OnLoaded);

                        async void OnLoaded(string result)
                        {
                            var cloudData = JsonConvert.DeserializeObject<UserProfile>(result);
                            await LoadFileFromCloud(cloudData, CancellationToken.None);
                            AppUI.Instance.ShowToast(Messages.LocalSaveDataWasCorruptedSaveDataLoadedFromServer);
                        }
                    }*/

                    return;
                }

                var accountData = JsonConvert.DeserializeObject<UserAccount>(json);
                userAccount = accountData;

                Debug.Log("User Account Data Loaded from " + accountPath);
            }, cancellationToken: cancellationToken);

            if (!File.Exists(profilePath))
            {
                Debug.Log("Profile Save file not found!");
                return;
            }

            Debug.Log("Start loading User Profile Data process.");
            var jsonWithChecksum = await UniTask.RunOnThreadPool(() => File.ReadAllText(profilePath),
                cancellationToken: cancellationToken);

            var json = jsonWithChecksum.EnsureChecksum();
            if (string.IsNullOrEmpty(json))
            {
                Debug.Log("Profile Save file Checksum Failed!");
                await UniTask.SwitchToMainThread();

                do
                {
                    Debug.Log(Messages.Test);

                    await UniTask.Yield();
                } while (true);
            }

            var accountData = JsonConvert.DeserializeObject<UserProfile>(json);
            userProfile = accountData;

            Debug.Log("User Profile Data Loaded from " + profilePath);
        }

        public async UniTask SaveAllFiles(CancellationToken cancellationToken)
        {
            SaveAccount();
            await SaveProfile(cancellationToken);
        }
        
        private static readonly object FileLock = new object();

        public async UniTask SaveProfile(CancellationToken cancellationToken)
        {
            Debug.Log("Start Saving User Profile Data process.");
            var path = ProfileSavePath;

            // Cancel the previous save
            lock (_saveProfileLock)
            {
                _latestProfileSaveCts?.Cancel();
                _latestProfileSaveCts?.Dispose();
                _latestProfileSaveCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            }
            
            try
            {
                await UniTask.RunOnThreadPool(() =>
                {
                    lock (FileLock)
                    {
                        var json = JsonConvert.SerializeObject(userProfile);
                        var checkSumJson = json.ApplyChecksum();

                        using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                        using (var writer = new StreamWriter(fs))
                            writer.Write(checkSumJson);
                        
                        Debug.Log("Player Profile Data Saved to " + path);
                    }
                }/*, cancellationToken: localCts.Token*/);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("SaveProfile was cancelled before completion!");
            }
            catch (Exception ex)
            {
                Debug.LogError("SaveProfile failed: " + ex);
            }
        }

        private void SaveAccount()
        {
            Debug.Log("Start Saving User Account Data process.");
            var path = AccountSavePath;

            var json = JsonConvert.SerializeObject(userAccount);
            json = json.ApplyChecksum();
            File.WriteAllText(path, json);
            Debug.Log("Player Account Data Saved to " + path);
        }

        /*public async UniTask LoadFileFromCloud(UserProfile playerProfile, CancellationToken cancellationToken)
        {
            var profilePath = ProfileSavePath;

            await UniTask.RunOnThreadPool(() =>
            {
                var json = JsonConvert.SerializeObject(playerProfile);
                json = json.ApplyChecksum();
                File.WriteAllText(profilePath, json);
                Debug.Log("Player Profile Data Saved <FROM CLOUD> to " + profilePath);
            }, cancellationToken: cancellationToken);
        }*/

        #endregion
    }
}