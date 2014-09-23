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
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.ObjectManagement
{
    /// <summary>
    /// Client for ObjectManagement Endpoints
    /// </summary>
    class ObjectManagementEndpoint : BaseObjectsEndpoint, IObjectManagementEndpoint
    {
        /// <summary>
        /// Create a new Object associated with the specified email address
        /// </summary>
        /// <param name="requestData">Object data</param>
        /// <param name="responseData">result</param>
        /// <returns>ObjectManagementActionResult</returns>
        public ObjectActionResult Create(ObjectManagementNewRequest requestData, out ObjectManagementResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return ObjectActionResult.Failed;
                }

                var request = CreateWebRequest("/objects", WebRequestOption.Authorization);
                object responseDataObj = null;
                ExecuteWebRequestJSON(request, typeof(ObjectManagementRequest), requestData, typeof(ObjectManagementResponse), out responseDataObj, WebRequestMethods.Http.Put);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as ObjectManagementResponse;
                    if (responseData != null)
                    {
                        switch (responseData.HTTPStatusCode)
                        {
                            case HttpStatusCode.Created:
                            case HttpStatusCode.OK:
                                responseData.objectUrn = new Urn(responseData.message);
                                return ObjectActionResult.Successful;
                            default: return ObjectActionResult.Failed;
                        }
                    }
                }

                return ObjectActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjectActionResult.Failed;
            }
        }

        /// <summary>
        /// Update an existing Object
        /// </summary>
        /// <param name="requestData">Object data</param>
        /// <param name="responseData">result, empty if successful</param>
        /// <returns>ObjectManagementActionResult</returns>
        public ObjectActionResult Update(ObjectManagementRequest requestData, out ObjectManagementResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return ObjectActionResult.Failed;
                }

                var request = CreateWebRequest("/objects", WebRequestOption.Authorization);
                object responseDataObj = null;
                ExecuteWebRequestJSON(request, typeof(ObjectManagementRequest), requestData, typeof(ObjectManagementResponse), out responseDataObj, WebRequestMethods.Http.Post);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as ObjectManagementResponse;
                    if (responseData != null)
                    {
                        switch (responseData.HTTPStatusCode)
                        {
                            case HttpStatusCode.NoContent: return ObjectActionResult.Successful;
                            case HttpStatusCode.BadRequest: return ObjectActionResult.Failed;
                            default: return ObjectActionResult.Failed;
                        }
                    }
                }

                return ObjectActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjectActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific object by its system-assigned URN key
        /// </summary>
        /// <param name="urn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Object data</param>
        /// <returns>ObjectManagementActionResult</returns>
        public ObjectActionResult Lookup(Urn urn, out ObjectDataResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == urn) || (!urn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return ObjectActionResult.Failed;
                }

                var request = CreateWebRequest("/objects/" + urn.UUID + "?view=" + viewType.GetDescription(), WebRequestOption.Authorization);
                object responseDataObj;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(ObjectDataResponse), out responseDataObj);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as ObjectDataResponse;
                    if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                        return ObjectActionResult.Successful;
                }
                return ObjectActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjectActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific object by their arbitrary developer assigned object URN
        /// </summary>
        /// <param name="objectUrn">Case-sensitive object URN to locate</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="exact">Defaults to true; when false, a starts-with search is performed</param>
        /// <param name="responseData">Object data</param>
        /// <returns>ObjectManagementActionResult</returns>
        public ObjectActionResult LookupByObjectUrn(Urn objectUrn, out ObjectDataResponse responseData, ViewType viewType = ViewType.Standard, bool exact = true)
        {
            responseData = null;
            try
            {
                if ((null == objectUrn) || (!objectUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return ObjectActionResult.Failed;
                }

                var request = CreateWebRequest("/objects/" + objectUrn.UUID + "?view=" + viewType.GetDescription() + "&exact=" + exact.ToString(), WebRequestOption.Authorization);
                object responseDataObj;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(ObjectDataResponse), out responseDataObj);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as ObjectDataResponse;
                    if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                        return ObjectActionResult.Successful;
                }
                return ObjectActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjectActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup an array of matching objects
        /// </summary>
        /// <param name="requestData">Object query request (e.g. filters like name, objectURN, ...)</param>
        /// <param name="responseData">List of objects</param>
        /// <returns>ObjectManagementActionResult</returns>
        public ObjectActionResult QueryObjects(QueryObjectsRequest requestData, out QueryObjectsResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return ObjectActionResult.Failed;
                }

                Uri url = new Uri("/objects").
                    AddQuery("objectUrnLike", requestData.objectUrnLike).
                    AddQuery("type", requestData.type).
                    AddQuery("nameLike", requestData.nameLike).
                    AddQuery("monikerLike", requestData.monikerLike).
                    AddQuery("modifiedAfter", requestData.modifiedAfter).
                    AddQuery("view", requestData.viewType.GetDescription());

                var request = CreateWebRequest(url.AbsoluteUri, WebRequestOption.Authorization);
                object responseDataObj = null;
                var HTTPStatusCode = ExecuteWebRequestJSON(request, typeof(QueryObjectsResponse), out responseDataObj);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as QueryObjectsResponse;
                    if (responseData != null)
                    {
                        foreach (var elm in responseData)
                        {
                            elm.HTTPStatusCode = HTTPStatusCode;
                        }

                        switch (HTTPStatusCode)
                        {
                            case HttpStatusCode.NoContent: return ObjectActionResult.Successful;
                            case HttpStatusCode.BadRequest: return ObjectActionResult.Failed;
                            default: return ObjectActionResult.Failed;
                        }
                    }
                }

                return ObjectActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjectActionResult.Failed;
            }
        }
    }
}
