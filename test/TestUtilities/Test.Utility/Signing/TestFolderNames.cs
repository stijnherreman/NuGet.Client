// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Test.Utility.Signing
{
    /// <summary>
    /// Folder names ensure uniqueness in path when signed packages are stored on disk for verification in later steps
    /// </summary>
    public enum FolderNames
    {
        One,
        Two
    }

    public static class TestFolderNames
    {
        public const string Windows_NetFullFrameworkFolder = "Windows_NetFullFramework";
        public const string Windows_NetCoreFolder = "Windows_NetCore";
        public const string Mac_NetCoreFolder = "Mac_NetCore";
        public const string Linux_NetCoreFolder = "Linux_NetCore";
        public const string PreGenPackagesFolder = "GeneratedPackages";
    }
}