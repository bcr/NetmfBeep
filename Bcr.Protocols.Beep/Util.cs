using System;
using Microsoft.SPOT;

namespace Bcr.Protocols.Beep
{
    public class Util
    {
        public static FrameType FrameTypeFromString(string theString)
        {
            var thisFrameType = FrameType.None;
            switch (theString)
            {
                case "MSG":
                    thisFrameType = FrameType.Message;
                    break;
                case "RPY":
                    thisFrameType = FrameType.Reply;
                    break;
                case "ANS":
                    thisFrameType = FrameType.Answer;
                    break;
                case "ERR":
                    thisFrameType = FrameType.Error;
                    break;
                case "NUL":
                    thisFrameType = FrameType.Null;
                    break;
            }
            return thisFrameType;
        }

        public static bool IsMoreIndicator(string theString)
        {
            bool returnValue = false;
            switch (theString)
            {
                case ".":
                    returnValue = false;
                    break;
                case "*":
                    returnValue = true;
                    break;
            }
            return returnValue;
        }
    }
}
