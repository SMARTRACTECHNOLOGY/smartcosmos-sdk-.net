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

#endregion License

using System.Runtime.Serialization;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.UserManagement
{
    [DataContract]
    public class UserDataResponse : BaseResponse
    {
        private Urn userUrn_;

        [DataMember]
        public string urn
        {
            get
            {
                return (userUrn_ != null) ? userUrn_.UUID : "";
            }
            set
            {
                userUrn_ = new Urn(value);
            }
        }

        [DataMember]
        public RoleType roleType { get; set; }

        [DataMember]
        public long lastModifiedTimestamp { get; set; }

        [DataMember]
        public string emailAddress { get; set; }

        [DataMember]
        public string givenName { get; set; }

        [DataMember]
        public string surname { get; set; }

        public Urn userUrn
        {
            get
            {
                return userUrn_;
            }
        }
    }
}