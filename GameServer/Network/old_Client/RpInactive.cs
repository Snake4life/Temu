﻿namespace Tera.Network.old_Client
{
    public class RpInactive : ARecvPacket
    {
        public override void Read()
        {
            ReadDword(); //1
        }

        public override void Process()
        {
            
        }
    }
}