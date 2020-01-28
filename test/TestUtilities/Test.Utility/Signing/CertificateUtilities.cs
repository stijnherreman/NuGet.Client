// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using NuGet.Common;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using X509Certificate = Org.BouncyCastle.X509.X509Certificate;

namespace Test.Utility.Signing
{
    public static class CertificateUtilities
    {
        internal static AsymmetricCipherKeyPair CreateKeyPair(int strength = 2048)
        {
            var generator = new RsaKeyPairGenerator();

            generator.Init(new KeyGenerationParameters(new SecureRandom(), strength));

            return generator.GenerateKeyPair();
        }

        internal static string GenerateFingerprint(X509Certificate certificate)
        {
            using (var hashAlgorithm = CryptoHashUtility.GetSha1HashProvider())
            {
                var hash = hashAlgorithm.ComputeHash(certificate.GetEncoded());

                return BitConverter.ToString(hash).Replace("-", "");
            }
        }

        internal static string GenerateRandomId()
        {
            return Guid.NewGuid().ToString();
        }

        public static X509Certificate2 GetCertificateWithPrivateKey(X509Certificate bcCertificate, AsymmetricCipherKeyPair keyPair)
        {
#if IS_DESKTOP
            RSA privateKey = DotNetUtilities.ToRSA(keyPair.Private as RsaPrivateCrtKeyParameters);

            var certificate = new X509Certificate2(bcCertificate.GetEncoded());

            certificate.PrivateKey = privateKey;
#else
            RSAParameters rsaParameters = DotNetUtilities.ToRSAParameters(keyPair.Private as RsaPrivateCrtKeyParameters);

            var rsaCsp = new RSACryptoServiceProvider();

            rsaCsp.ImportParameters(rsaParameters);

            var privateKey = rsaCsp;

            var certificateTmp = new X509Certificate2(bcCertificate.GetEncoded());

            X509Certificate2 certificate = RSACertificateExtensions.CopyWithPrivateKey(certificateTmp, privateKey);
#endif
            return certificate;
        }
    }
}
