﻿

using Tera.Data.Structures.World.Requests;

namespace Tera.Network.old_Client
{
    public class RpOwnerCancelRequest : ARecvPacket
    {
        protected int RequestId;

        public override void Read()
        {
            ReadDword(); // Request type
            RequestId = ReadDword();
        }

        public override void Process()
        {
            Request request = Communication.Global.ActionEngine.GetRequest(RequestId);

            if(request != null)
            {
                if (request.Owner != Connection.Player)
                    return;

                Communication.Global.ActionEngine.RemoveRequest(request);
            }

            Communication.Global.PlayerService.InterruptNpcTraid(Connection.Player);
        }
    }
}