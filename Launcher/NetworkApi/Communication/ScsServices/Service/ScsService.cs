﻿// Type: Hik.Communication.ScsServices.Service.ScsService
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;

namespace Tera.NetworkApi.Communication.ScsServices.Service
{
    /// <summary>
    /// Base class for all services that is serviced by IScsServiceApplication.
    ///             A class must be derived from ScsService to serve as a SCS service.
    /// 
    /// </summary>
    public abstract class ScsService
    {
        /// <summary>
        /// The current client for a thread that called service method.
        /// 
        /// </summary>
        [ThreadStatic]
        private static IScsServiceClient _currentClient;

        /// <summary>
        /// Gets the current client which called this service method.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// This property is thread-safe, if returns correct client when
        ///             called in a service method if the method is called by SCS system,
        ///             else throws exception.
        /// 
        /// </remarks>
        protected internal IScsServiceClient CurrentClient
        {
            get
            {
                if (ScsService._currentClient == null)
                    throw new Exception("Client channel can not be obtained. CurrentClient property must be called by the thread which runs the service method.");
                else
                    return ScsService._currentClient;
            }
            internal set
            {
                ScsService._currentClient = value;
            }
        }
    }
}
