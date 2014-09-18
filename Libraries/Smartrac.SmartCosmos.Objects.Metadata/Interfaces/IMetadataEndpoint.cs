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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.Metadata
{
    public enum MetadataDataType
    {
        [Description("StringType")]
        String,
        [Description("DateType")]
        Date,
        [Description("IntegerType")]
        Integer,
        [Description("LongType")]
        Long,
        [Description("BooleanType")]
        Boolean,
        [Description("FloatType")]
        Float,
        [Description("DoubleType")]
        Double,
        [Description("JSONType")]
        JSON,
        [Description("XMLType")]
        XML,
        [Description("CustomType")]
        Custom
    }
    
    public enum MetadataActionResult
    {
        /// <summary>
        /// action was successful
        /// </summary>
        Successful,

        /// <summary>
        /// problem occurred, check message parameter for detailed information
        /// </summary>
        Failed
    }

    public interface IMetadataEndpoint : IBaseEndpoint
    {

    }
}
