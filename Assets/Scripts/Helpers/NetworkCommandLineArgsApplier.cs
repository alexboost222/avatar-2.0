using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Helpers
{
    [DisallowMultipleComponent]
    public class NetworkCommandLineArgsApplier : MonoBehaviour
    {
        private const string NetworkModeArgName = "network_mode";
        private const string ServerMode = "server";
        private const string HostMode = "host";
        private const string ClientMode = "client";
        
        private void Start()
        {
            if (Application.isEditor) return;

            Dictionary<string, string> commandLineArgs = GetCommandLineArgs();
            Debug.Log(string.Join(";", commandLineArgs));

            if (!commandLineArgs.TryGetValue($"-{NetworkModeArgName}", out string networkMode)) return;
            
            Debug.Log($"Network mode processing started with value {networkMode}");
            
            switch (networkMode)
            {
                case ServerMode:
                    NetworkManager.Singleton.StartServer();
                    Debug.Log("Server started");
                    break;
                case HostMode:
                    NetworkManager.Singleton.StartHost();
                    Debug.Log("Host started");
                    break;
                case ClientMode:
                    NetworkManager.Singleton.StartClient();
                    Debug.Log("Client started");
                    break;
                default:
                    Debug.LogError($"{networkMode} is not supported", this);
                    break;
            }
        }

        private static Dictionary<string, string> GetCommandLineArgs()
        {
            Dictionary<string, string> argsDictionary = new Dictionary<string, string>();

            string[] args = Environment.GetCommandLineArgs();

            for (int i = 0; i < args.Length; ++i)
            {
                string arg = args[i].ToLower();
                
                if (!arg.StartsWith("-")) continue;
                
                string value = i < args.Length - 1 ? args[i + 1].ToLower() : null;
                value = value?.StartsWith("-") ?? false ? null : value;

                argsDictionary.Add(arg, value);
            }
            
            return argsDictionary;
        }
    }
}