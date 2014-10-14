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

using System;
using System.Runtime.Serialization;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.ObjectInteractionSession
{
    [DataContract]
    public class StartObjectInteractionSessionRequest : BaseRequest
    {
        /// <summary>
        /// type is required and constrained to 255 characters
        /// </summary>
        [DataMember]
        public string type { get; set; }

        /// <summary>
        /// name is required and constrained to 255 characters
        /// </summary>
        [DataMember]
        public string name { get; set; }

        /// <summary>
        /// description is optional and constrained to 1024 characters and may be omitted; defaults to null
        /// </summary>
        [DataMember]
        public string description { get; set; }

        /// <summary>
        /// activeFlag is optional and may be omitted; defaults to true
        /// </summary>
        [DataMember]
        public bool activeFlag { get; set; }

        /// <summary>
        /// moniker is optional and constrained to 2048 characters may be omitted; defaults to null
        /// </summary>
        [DataMember]
        public string moniker { get; set; }

        public StartObjectInteractionSessionRequest()
            : base()
        {
            this.moniker = null;
            this.description = null;
            this.activeFlag = true;
        }

        public override bool IsValid()
        {
            return base.IsValid() &&
                !String.IsNullOrEmpty(type) &&
                (type.Length <= 255) &&
                !String.IsNullOrEmpty(name) &&
                (name.Length <= 255) &&
                (String.IsNullOrEmpty(description) || description.Length <= 1024) &&
                (String.IsNullOrEmpty(moniker) || moniker.Length <= 2048);
        }
    }
}