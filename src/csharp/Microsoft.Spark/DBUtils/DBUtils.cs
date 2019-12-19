// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using Microsoft.Spark.Interop;
using Microsoft.Spark.Interop.Ipc;
using Microsoft.Spark.Services;

namespace Microsoft.Spark
{
    /// <summary>
    /// Azure Databricks dbutils wrapper. 
    /// This is example code, and has not been thouroughly tested or reviewed.
    /// </summary>
    public static class DBUtils
    {
        private static IJvmBridge JvmBridge { get; } = SparkEnvironment.JvmBridge;

        // This reference has not worked during testing.
        private static readonly string
            s_dbutilsClassName = "com.databricks.service.DBUtils",
            s_dbutilsSecretsClassName = $"{s_dbutilsClassName}.secrets",
            s_dbutilsFSClassName = $"{s_dbutilsClassName}.fs";

        /// <summary>
        /// Calls the dbutils help method for file system.
        /// </summary>
        public static string GetFSHelp() =>
            (string)JvmBridge.CallStaticJavaMethod(className: s_dbutilsFSClassName, methodName: "help");

        /// <summary>
        /// Retrieve a secret value, similar to `dbutils.secrets.get(string scope, string key)`
        /// </summary>
        /// <param name="scope">The secret scope being accessed by the user running the Databricks Job.
        /// </param>
        /// <param name="key">The name of the secret being accessed.
        /// </param>
        /// <returns>The secret value as a string. </returns>
        public static string GetSecret(string scope, string key) =>
            (string)JvmBridge.CallStaticJavaMethod(className: s_dbutilsSecretsClassName, methodName: "get", scope, key);

        /// <summary>
        /// Calls the dbutils help method for secrets.
        /// </summary>
        public static void GetSecretsHelp() => JvmBridge.CallStaticJavaMethod(className: s_dbutilsSecretsClassName, methodName: "help");

        /// <summary>
        /// List dbutils secret scopes.
        /// </summary>
        public static void ListScopes() => JvmBridge.CallStaticJavaMethod(className: s_dbutilsSecretsClassName, methodName: "listScopes");
    }
}
