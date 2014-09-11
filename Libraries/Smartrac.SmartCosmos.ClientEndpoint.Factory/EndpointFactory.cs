﻿#region License
// SMART COSMOS .Net SDK
// (C) Copyright 2014 SMARTRAC TECHNOLOGY GmbH, (http://www.smartrac-group.com)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.Objects.File;
using Smartrac.SmartCosmos.Profiles.DataImport;
using Smartrac.SmartCosmos.Profiles.PlatformAvailability;
using Smartrac.SmartCosmos.Profiles.TagMetadata;
using Smartrac.SmartCosmos.Profiles.TagVerification;

namespace Smartrac.SmartCosmos.ClientEndpoint.Factory
{
    public class EndpointFactory : IEndpointFactory
    {
        public string ProfilesServerURL { get; set; }
        public string ObjectsServerURL { get; set; }

        public bool KeepAlive { get; set; }
        public bool AllowInvalidServerCertificates { get; set; }
        public string AcceptLanguage { get; set; }
        public IMessageLogger Logger { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }

        public EndpointFactory(IMessageLogger logger)
        {
            this.Logger = logger;
            this.KeepAlive = true;
            this.AcceptLanguage = "en";
            this.ProfilesServerURL = "https://www.smart-cosmos.com/service/rest";
            this.ObjectsServerURL = ""; // TODO
            this.AllowInvalidServerCertificates = false;
        }

        #region PROFILES
        public IPlatformAvailabilityEndpoint CreatePlatformAvailabilityEndpoint()
        {
            return new PlatformAvailabilityEndpointBuilder()
                .setLogger(Logger)
                .setKeepAlive(KeepAlive)
                .setServerURL(ProfilesServerURL)
                .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
                .build();
        }

        public IDataImportEndpoint CreateDataImportEndpoint()
        {
            return new DataImportEndpointBuilder()
                .setLogger(Logger)
                .setKeepAlive(KeepAlive)
                .setServerURL(ProfilesServerURL)
                .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
                .setUserAccount(UserName, UserPassword)
                .build();
        }

        public ITagVerificationEndpoint CreateTagVerificationEndpoint()
        {
            return new TagVerificationEndpointBuilder()
                .setLogger(Logger)
                .setKeepAlive(KeepAlive)
                .setServerURL(ProfilesServerURL)
                .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
                .setUserAccount(UserName, UserPassword)
                .build();
        }

        public ITagMetadataEndpoint CreateTagMetadataEndpoint()
        {
            return new TagMetadataEndpointBuilder()
                .setLogger(Logger)
                .setKeepAlive(KeepAlive)
                .setServerURL(ProfilesServerURL)
                .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
                .setUserAccount(UserName, UserPassword)
                .build();
        }
        #endregion

        #region OBJECTS
        public IFileEndpoint CreateFileEndpoint()
        {
            return new FileEndpointBuilder()
            .setLogger(Logger)
            .setKeepAlive(KeepAlive)
            .setServerURL(ProfilesServerURL)
            .setAllowInvalidServerCertificates(AllowInvalidServerCertificates)
            .setUserAccount(UserName, UserPassword)
            .build();
        }
        #endregion
    }
}
