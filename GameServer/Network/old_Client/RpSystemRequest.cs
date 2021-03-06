using System;
using System.IO;
using Tera.Communication;
using Tera.Data.Enums;
using Tera.Data.Structures.Player;
using Tera.Data.Structures.World.Requests;
using Utils.Logger;

namespace Tera.Network.old_Client
{
    public class RpSystemRequest : ARecvPacket
    {
        protected Request Request;

        public override void Read()
        {
            
            short nameShift = (short)(ReadWord() - 4);
            short argumentShift = (short)(ReadWord() - 4);

            ReadWord(); //unk shift

            RequestType type = (RequestType)ReadWord();

            switch(type)
            {
                case RequestType.GuildCreate:
                    {
                        Reader.BaseStream.Seek(argumentShift, SeekOrigin.Begin);
                        String guildName = ReadString();
                        Reader.BaseStream.Seek(0, SeekOrigin.End);
                        
                        Request = new GuildCreate(guildName);
                    }
                    break;
                case RequestType.GuildInvite:
                    {
                        Reader.BaseStream.Seek(nameShift, SeekOrigin.Begin);
                        String name = ReadString();
                        Reader.BaseStream.Seek(0, SeekOrigin.End);

                        Player target = Global.PlayerService.GetPlayerByName(name);
                        Request = new GuildInvite(target);
                    }
                    break;
                case RequestType.PartyInvite:
                    {
                        Reader.BaseStream.Seek(nameShift, SeekOrigin.Begin);
                        String name = ReadString();
                        Reader.BaseStream.Seek(0, SeekOrigin.End);

                        Player target = Global.PlayerService.GetPlayerByName(name);
                        Request = new PartyInvite(target);
                    }
                    break;
                case RequestType.Extraction:
                    {
                        Reader.BaseStream.Seek(argumentShift, SeekOrigin.Begin);
                        int extractionType = ReadDword();
                        Reader.BaseStream.Seek(0, SeekOrigin.End);
                        Request = new Extract(extractionType);
                    }
                    break;
                case RequestType.DuelInvite:
                    {
                        Reader.BaseStream.Seek(nameShift, SeekOrigin.Begin);
                        String name = ReadString();
                        Reader.BaseStream.Seek(0, SeekOrigin.End);
                        Player target = Global.PlayerService.GetPlayerByName(name);
                        Request = new DuelInvite(target);
                    }
                    break;
                case RequestType.TradeStart:
                    {
                        Reader.BaseStream.Seek(nameShift, SeekOrigin.Begin);
                        String name = ReadString();
                        Reader.BaseStream.Seek(0, SeekOrigin.End);
                        Player target = Global.PlayerService.GetPlayerByName(name);
                        Request = new TradeStart(target);
                    }
                    break;
                default:
                    Logger.WriteLine(LogState.Debug,"RpSystemRequest: Unknown system request {0}", type);
                    break;
            }
        }

        public override void Process()
        {
            if(Request == null)
                return;

            Request.Owner = Connection.Player;

            if (Request.IsValid())
                Global.ActionEngine.AddRequest(Request);
        }
    }
}